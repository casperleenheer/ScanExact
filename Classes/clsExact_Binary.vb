Imports System.IO
Imports System.Linq
Imports Exact.Services.Client.Data
Imports Exact.Services.Client.Entity

<CLSCompliant(False)>
Public Class clsExact_Binary
    Private binaryEGclient As New Exact.Services.Client.Entity.EntityClientEG(_URL, _Server, _Database)
    Private binaryEGdata As Exact.Services.Client.Data.EntityData

    Private tmpEGclient As New Exact.Services.Client.Entity.EntityClientEG(_URL, _Server, _Database)
    Private tmpEGdata As Exact.Services.Client.Data.EntityData

    Private Const CHUNK_SIZE As Integer = 50 * 1024
    Private CommonAppFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)

    Dim msgID As Object
    Dim sequence As Integer = 0
    Dim _connected As Boolean

    'Uploading images
    'Dim lastPacketdataSize = filesizeInBytes Mod CHUNK_SIZE

    Private _IsSuccess As Boolean
    Public Property PictureIsSuccess As Boolean
        Get
            Return _IsSuccess
        End Get
        Set(value As Boolean)
            _IsSuccess = value
        End Set
    End Property

    Private _IsUploadCompleted As Boolean
    Public Property IsUploadCompleted As Boolean
        Get
            Return _IsUploadCompleted
        End Get
        Set(value As Boolean)
            _IsUploadCompleted = value
        End Set
    End Property

    Private _entiteitData As String
    Private _IDveld As String
    Private _IDwaarde As String
    Private _Administratie As String
    Private _URL As String
    Private _Database As String
    Private _Server As String

    Public Sub New(URL As String, Server As String, Database As String, EntiteitData As String, IDveld As String, IDwaarde As String)

        _entiteitData = EntiteitData
        _IDveld = IDveld
        _IDwaarde = IDwaarde
        _URL = URL
        _Server = Server
        _Database = Database

        'Dim Exact_Connection As New clsExact_Connection(exactServer, Database)
        'If Exact_Connection.Connected = True Then
        '    _connected = True
        'End If
    End Sub

    Private Function GetEGpropertydataValue(EntiteitData As Exact.Services.Client.Data.EntityData, propName As String) As Object
        Dim prop As PropertyData = (From p As PropertyData In EntiteitData.Properties Where p.Name.ToLower = propName.ToLower Select p).First
        If IsNothing(prop) Then
            Return Nothing
        Else
            Return prop.Value
        End If
    End Function

    Public Function Upload(Bestand As String, OrderData As Exact.Services.Client.Data.EntityData, AccountID As String, DocumentType As String, Subject As String) As Guid

        Dim binaryEGdata As EntityData
        Dim binaryEGclient As New Exact.Services.Client.Entity.EntityClientEG(_URL, _Server, _Database)
        Dim fileInfo As FileInfo
        Dim fileLength As Integer = 0
        Dim fileSizeInBytes As Long
        Dim lastPacketDataSize As Long
        Dim totalChunkPackets As Integer
        Dim completePacketData() As Byte = Nothing
        Dim countPacket As Integer = 0
        Dim chunkSequenceNumber As Integer
        Dim chunkPacketToWrite() As Byte
        Dim dataToRead As Long
        Dim msgID As Guid
        _IsUploadCompleted = False

        fileInfo = New FileInfo(Bestand)
        fileSizeInBytes = fileInfo.Length

        Using iStream As New System.IO.FileStream(Bestand, FileMode.Open, FileAccess.Read, FileShare.Read)

            binaryEGdata = New EntityData With {.EntityName = "Binary"}
            binaryEGdata = binaryEGclient.Create(binaryEGdata)
            msgID = New Guid(GetEGpropertydataValue(binaryEGdata, "MessageID").ToString())
            'Get total size for last packet
            lastPacketDataSize = fileSizeInBytes Mod CHUNK_SIZE
            '- How many packet we need to store all the chunk.
            '- Each packet only can stored 3800 bytes of data.
            totalChunkPackets = Int((fileSizeInBytes / CHUNK_SIZE)) + IIf(lastPacketDataSize > 0, 1, 0)
            'Each complete packet consists of 3800 bytes data
            completePacketData = New Byte(CHUNK_SIZE - 1) {}
            dataToRead = iStream.Length
            While dataToRead > 0
                If countPacket = totalChunkPackets - 1 Then
                    '1) Special handle for last packet of data due to the data size could be less than 3800 bytes. 
                    '   We do not need complete 3800 bytes as packet size.
                    '2) Another reason is due to "File corrupted" when re-open the attachment using office application.
                    '   This is because of extra data has been write into attachment as well.
                    Dim lastPacketData() As Byte = Nothing
                    lastPacketData = New Byte(lastPacketDataSize - 1) {}
                    fileLength = iStream.Read(lastPacketData, 0, lastPacketDataSize)
                    chunkPacketToWrite = lastPacketData
                Else
                    'Read chunk data and assign as [complete packet]
                    fileLength = iStream.Read(completePacketData, 0, CHUNK_SIZE)
                    chunkPacketToWrite = completePacketData
                End If

                '############################################################################
                'Upload chunk
                binaryEGdata = New EntityData With {.EntityName = "Binary"}
                With binaryEGdata.Properties
                    .Add(New PropertyData With {.Name = "MessageID", .Value = msgID})
                    .Add(New PropertyData With {.Name = "Sequence", .Value = countPacket})
                    .Add(New PropertyData With {.Name = "Data", .Value = chunkPacketToWrite})
                End With
                binaryEGdata = binaryEGclient.Save(binaryEGdata)
                chunkSequenceNumber = GetEGpropertydataValue(binaryEGdata, "Sequence")

                If Not String.IsNullOrEmpty(chunkSequenceNumber) Then
                    dataToRead = dataToRead - fileLength
                    'Count total packet
                    countPacket += 1
                Else
                    'Count total packet
                    countPacket -= 1
                End If
                '############################################################################

                Application.DoEvents()
            End While
            iStream.Close()
        End Using

        '*** Call Document Attachment Service *** //Echter nu voor Globe met de Exact.Services.Client
        Try
            Dim docAttachmentData As New Exact.Services.Client.Data.EntityData
            docAttachmentData.EntityName = "Document"
            With docAttachmentData
                If Len(Bestand.Length) > 0 Then
                    'docAttachmentData.Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "ID", .Value = msgID})
                End If
                Dim BestandsnaamZonderDitInfo As String = String.Empty
                Dim AantalStringPakketten As Integer = CInt(Bestand.ToString.Split("\").Length)
                Dim BestandsnaamGesplit() As String = Bestand.ToString.Split("\")
                .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "Filename", .Value = BestandsnaamGesplit(AantalStringPakketten - 1)})
                .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "Type", .Value = TryCast(DocumentType, String)})
                .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "MessageIDDocument", .Value = msgID})
                .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "IsBinaryServiceEnabled", .Value = True})
                '.DocumentID = New EntityDocumentAttachment.EntityGuid With {.Value = New Guid(txtDocID.Text), .IsDirty = True}

                '//WELKE ITEMS ZIJN BELANGRIJK OM NU NOG TOE TE VOEGEN AAN DE DOCUMENT ENTITY, ZODAT DE RELATIE ONTSTAAT
                '//IN EXACT TUSSEN DE VERKOOPORDER EN DOCUMENT.
                For Each prop In OrderData.Properties
                    Select Case prop.Name
                        Case "SalesOrderNumber"
                            If Not prop.Value Is Nothing Then
                                If Subject.ToString.Trim = "" Then
                                    Dim ext() As String = BestandsnaamGesplit(AantalStringPakketten - 1).ToString.Split(".")
                                    Dim extension As String = ext(1)
                                    If extension.ToUpper = "PDF" Then
                                        .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "Subject", .Value = prop.Value & " [PDF ingescand " & Date.Now.ToString & "]"})
                                    ElseIf extension.ToUpper = "JPG" Then
                                        .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "Subject", .Value = prop.Value & " [JPEG ingescand " & Date.Now.ToString & "]"})
                                    ElseIf extension.ToUpper = "TIFF" Then
                                        .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "Subject", .Value = prop.Value & " [TIFF ingescand " & Date.Now.ToString & "]"})
                                    Else
                                        .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "Subject", .Value = prop.Value & " [Overige ingescand " & Date.Now.ToString & "]"})
                                    End If
                                    .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "OrderNumber", .Value = prop.Value})
                                Else
                                    .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "Subject", .Value = Subject})
                                    .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "OrderNumber", .Value = prop.Value})
                                End If
                            End If
                        Case "YourReference"
                            If Not prop.Value Is Nothing Then
                                .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "YourRef", .Value = prop.Value})
                            End If
                        Case "Project"
                            If Not prop.Value Is Nothing Then
                                .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "ProjectNumber", .Value = prop.Value})
                            End If
                    End Select
                Next
                '// VERPLICHT VOLGENS VALIDATIEREGELS IS ACCOUNTID;
                '// DEZE WAARDE GEEF IK MEE IN DE AANROEP VAN DEZE FUNCTIE
                .Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "AccountID", .Value = AccountID})
            End With

            docAttachmentData = binaryEGclient.Create(docAttachmentData)
            _IsUploadCompleted = True

        Catch ex As Exception
            MessageBox.Show("Error Occurred :" & ex.Message, "Entity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return msgID

    End Function

    Public Sub Delete(Item As Guid)

        Dim _ID As String = _IDwaarde
        Dim tmpEGclient As New Exact.Services.Client.Entity.EntityClientEG(_URL, _Server, _Database)
        Dim tmpEGdata = New Exact.Services.Client.Data.EntityData
        With tmpEGdata.Properties
            .Add(New PropertyData With {.Name = _IDveld, .Value = _ID})
            .Add(New PropertyData With {.Name = "IsBinaryServiceEnabled", .Value = True})
        End With
        tmpEGdata = tmpEGclient.Retrieve(tmpEGdata)
        msgID = Item 'GetEGpropertydataValue(tmpEGdata, "MessageID")

        binaryEGclient = New Exact.Services.Client.Entity.EntityClientEG(_URL, _Server, _Database)
        binaryEGdata = New EntityData With {.EntityName = "Binary"}
        With binaryEGdata.Properties
            .Add(New PropertyData With {.Name = "MessageID", .Value = msgID})
        End With
        binaryEGclient.Delete(binaryEGdata)

    End Sub

    Public Sub Download(FileName As String)

        Dim _ID As String = _IDwaarde
        tmpEGclient = New Exact.Services.Client.Entity.EntityClientEG(_URL, _Server, _Database)

        tmpEGdata = New EntityData With {.EntityName = _entiteitData}
        With tmpEGdata.Properties
            .Add(New PropertyData With {.Name = _IDveld, .Value = _ID})
            .Add(New PropertyData With {.Name = "IsBinaryServiceEnabled", .Value = True})
        End With
        tmpEGdata = tmpEGclient.Retrieve(tmpEGdata)

        msgID = GetEGpropertydataValue(tmpEGdata, "MessageID")

        Dim fs As New FileStream("d:\test" + "\" + FileName, FileMode.OpenOrCreate, FileAccess.Write)
        Dim writer As BinaryWriter = New BinaryWriter(fs)
        binaryEGclient = New Exact.Services.Client.Entity.EntityClientEG(_URL, _Server, _Database)
        binaryEGdata = New EntityData With {.EntityName = "Binary"}
        With binaryEGdata.Properties
            .Add(New PropertyData With {.Name = "MessageID", .Value = msgID})
            .Add(New PropertyData With {.Name = "Sequence", .Value = sequence})
        End With
        binaryEGdata = binaryEGclient.Retrieve(binaryEGdata)

        Dim Chunk As Byte()
        Chunk = GetEGpropertydataValue(binaryEGdata, "Data")

        While Not IsNothing(Chunk)
            writer.Write(Chunk, 0, Chunk.Length)
            writer.Flush()
            Chunk = Nothing
            sequence += 1
            binaryEGdata = New EntityData With {.EntityName = "Binary"}
            With binaryEGdata.Properties
                With binaryEGdata.Properties
                    .Add(New PropertyData With {.Name = "MessageID", .Value = msgID})
                    .Add(New PropertyData With {.Name = "Sequence", .Value = sequence})
                End With
            End With
            Try
                binaryEGdata = binaryEGclient.Retrieve(binaryEGdata)
            Catch ex As Exception
                Exit While
                fs.Close()
            End Try

            Chunk = GetEGpropertydataValue(binaryEGdata, "Data")
        End While
        fs.Dispose()

    End Sub

    Public Function UploadEGPicture(picLogo As PictureBox) As Guid
        Dim binaryEGdata As EntityData
        Dim binaryEGclient As New Exact.Services.Client.Entity.EntityClientEG(_URL, _Server, _Database)
        Dim fileInfo As FileInfo
        Dim fileLength As Integer = 0
        Dim fileSizeInBytes As Long
        Dim lastPacketDataSize As Long
        Dim totalChunkPackets As Integer
        Dim completePacketData() As Byte = Nothing
        Dim countPacket As Integer = 0
        Dim chunkSequenceNumber As Integer
        Dim chunkPacketToWrite() As Byte
        Dim dataToRead As Long
        Dim msgID As Guid

        fileInfo = New FileInfo(picLogo.ImageLocation)
        fileSizeInBytes = fileInfo.Length

        Using iStream As New System.IO.FileStream(picLogo.ImageLocation, FileMode.Open, FileAccess.Read, FileShare.Read)

            binaryEGdata = New EntityData With {.EntityName = "Binary"}

            binaryEGdata = binaryEGclient.Create(binaryEGdata)
            msgID = New Guid(GetEGpropertydataValue(binaryEGdata, "MessageID").ToString())

            'Get total size for last packet
            lastPacketDataSize = fileSizeInBytes Mod CHUNK_SIZE

            '- How many packet we need to store all the chunk.
            '- Each packet only can stored 3800 bytes of data.
            totalChunkPackets = Int((fileSizeInBytes / CHUNK_SIZE)) + IIf(lastPacketDataSize > 0, 1, 0)
            'Each complete packet consists of 3800 bytes data

            completePacketData = New Byte(CHUNK_SIZE - 1) {}

            dataToRead = iStream.Length

            While dataToRead > 0

                If countPacket = totalChunkPackets - 1 Then
                    '1) Special handle for last packet of data due to the data size could less than 3800 bytes. 
                    '   We do not need complete 3800 bytes as packet size.
                    '2) Another reason is due to "File corrupted" when re-open the attachment using office application.
                    '   This is because of extra data has been write into attachment as well.
                    Dim lastPacketData() As Byte = Nothing
                    lastPacketData = New Byte(lastPacketDataSize - 1) {}
                    fileLength = iStream.Read(lastPacketData, 0, lastPacketDataSize)
                    chunkPacketToWrite = lastPacketData
                Else
                    'Read chunk data and assign as [complete packet]
                    fileLength = iStream.Read(completePacketData, 0, CHUNK_SIZE)
                    chunkPacketToWrite = completePacketData
                End If

                '############################################################################
                'Upload chunk

                binaryEGdata = New EntityData With {.EntityName = "Binary"}
                With binaryEGdata.Properties
                    .Add(New PropertyData With {.Name = "MessageID", .Value = msgID})
                    .Add(New PropertyData With {.Name = "Sequence", .Value = countPacket})
                    .Add(New PropertyData With {.Name = "Data", .Value = chunkPacketToWrite})
                End With


                binaryEGdata = binaryEGclient.Save(binaryEGdata)
                chunkSequenceNumber = GetEGpropertydataValue(binaryEGdata, "Sequence")


                If Not String.IsNullOrEmpty(chunkSequenceNumber) Then
                    dataToRead = dataToRead - fileLength

                    'Count total packet
                    countPacket += 1
                Else
                    'Count total packet
                    countPacket -= 1
                End If
                '############################################################################

                Application.DoEvents()
            End While
            iStream.Close()
        End Using

        Return msgID
    End Function
End Class

