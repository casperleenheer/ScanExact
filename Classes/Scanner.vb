Imports WIA
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Xml
Imports System.Drawing.Printing

Public Class Scanner

    Dim WithEvents manager As New DeviceManager
    Dim _scanner As WIA.Item
    Dim _AvailableResolutions As List(Of Integer)
    Dim _description As String = ""
    Dim _deviceID As String = ""
    Dim _canUseADF As Boolean = False
    Private _canDoDuplex As Boolean = False
    Private Shared LocRM As Resources.ResourceManager

    'Dim LocalizedRootStr As String

    Sub New(ByVal deviceID As String)
        LocRM = New System.Resources.ResourceManager("ScanExact.WinFormStrings", GetType(mainFrm).Assembly)

        If deviceID Is Nothing Then Throw New ArgumentNullException("deviceID", "No deviceID specified")
        If manager.DeviceInfos.Count = 0 Then Throw New ArgumentException("No WIA devices found!")

        Dim _device As Device
        Try
            Trace.WriteLine(String.Format("Trying to establish connection with the Device {0}", deviceID))
            _device = manager.DeviceInfos.Item(deviceID).Connect
            _deviceID = deviceID
            _scanner = _device.Items(1)
            _description = _device.Properties.Item("Description").Value
            Trace.WriteLine(String.Format("Connection established with {0}. DeviceID: {1}", _description, _deviceID))
            _AvailableResolutions = GetAvailableResolutions()

            Try
                Dim caps As WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES = _device.Properties("Document Handling Capabilities").Value
                If caps And WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES.FEED Then
                    Trace.WriteLine(String.Format("This scanner supports ADF"))
                    _canUseADF = True
                End If
                If caps And WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES.DUP Then
                    _canDoDuplex = True
                End If

            Catch ex As Exception
                'The scanner does not support ADF
            End Try
        Catch ex As Exception
            If ex.Message.Contains("80210067") Then
                Throw
            Else
                MsgBox(ex.Message)
                Throw
            End If
        Finally
            _scanner = Nothing
        End Try

    End Sub

    Private Function GetBrightness() As Integer
        Dim prop_name As String = "Brightness"
        Dim temp As WIA.Property = _scanner.Properties(prop_name)

        If temp.SubType = WiaSubType.RangeSubType Then
            Dim min As Integer = temp.SubTypeMin
            Dim max As Integer = temp.SubTypeMax
            'Dim stp As Integer = temp.SubTypeStep
            Dim center As Integer = (max + min) / 2
            Dim delta As Integer = max - center
            Return CInt(Math.Round((temp.Value - center) / delta * 100, 0))
        Else
            Return temp.Value
        End If
    End Function

    Private Sub SetBrightess(ByVal value As Integer)
        Dim prop_name As String = "Brightness"
        Dim temp As WIA.Property = _scanner.Properties(prop_name)

        If temp.SubType = WiaSubType.RangeSubType Then
            Dim min As Integer = temp.SubTypeMin
            Dim max As Integer = temp.SubTypeMax
            'Dim stp As Integer = temp.SubTypeStep
            Dim center As Integer = (max + min) / 2
            Dim delta As Integer = max - center

            If value <= 100 And value >= -100 Then
                Dim tmpVal As Integer = CInt(Math.Round(value / 100 * delta + center, 0))
                Try
                    temp.Value = tmpVal
                    Trace.WriteLine(String.Format("Brightness set to {0} -> {1}", value.ToString(), _scanner.Properties(prop_name).Value.ToString()))
                Catch ex As Exception
                    Trace.WriteLine(String.Format("Brighness value not accepted by the scanner: {0}", tmpVal.ToString()))
                    Throw New ArgumentException(prop_name + " value not accepted by the scanner: " + value.ToString() + " -> " + tmpVal.ToString())
                End Try
            Else
                Throw New ArgumentOutOfRangeException("value", prop_name + " setting must be between -100 and 100," + value.ToString() + " entered.")
            End If
        Else
            Try
                temp.Value = value
            Catch ex As Exception
                MsgBoxWrap("There was an exception while setting the property " + prop_name + " to " + value.ToString() + "." + vbCrLf + _
                       "property type: " + temp.Type.ToString() + vbCrLf + _
                       "property subtype: " + temp.SubType.ToString(), MsgBoxStyle.Critical, "ScanExact")
            End Try
        End If
    End Sub

    Public Function GetContrast() As Integer
        Dim prop_name As String = "Contrast"
        Dim temp As WIA.Property = _scanner.Properties(prop_name)

        If temp.SubType = WiaSubType.RangeSubType Then
            Dim min As Integer = temp.SubTypeMin
            Dim max As Integer = temp.SubTypeMax
            ' Dim stp As Integer = temp.SubTypeStep
            Dim center As Integer = (max + min) / 2
            Dim delta As Integer = max - center
            Return CInt(Math.Round((temp.Value - center) / delta * 100, 0))
        Else
            Return temp.Value
        End If
    End Function

    Public Sub SetContrast(ByVal value As Integer)
        Dim prop_name As String = "Contrast"
        Dim temp As WIA.Property = _scanner.Properties(prop_name)

        If temp.SubType = WiaSubType.RangeSubType Then
            Dim min As Integer = temp.SubTypeMin
            Dim max As Integer = temp.SubTypeMax
            'Dim stp As Integer = temp.SubTypeStep
            Dim center As Integer = (max + min) / 2
            Dim delta As Integer = max - center

            If value <= 100 And value >= -100 Then
                Dim tmpVal As Integer = CInt(Math.Round(value / 100 * delta + center, 0))
                Try
                    temp.Value = tmpVal
                    Trace.WriteLine(String.Format("Contrast set to {0} -> {1}", value.ToString(), _scanner.Properties(prop_name).Value.ToString()))
                Catch ex As Exception
                    Trace.WriteLine(String.Format("Contrast value not accepted by the scanner: {0}", tmpVal.ToString()))
                    Throw New ArgumentException(prop_name + " value not accepted by the scanner: " + value.ToString() + " -> " + tmpVal.ToString())
                End Try
            Else
                Throw New ArgumentOutOfRangeException("value", prop_name + " setting must be between -100 and 100," + value.ToString() + " entered.")
            End If
        Else
            Try
                temp.Value = value
            Catch ex As Exception
                MsgBoxWrap("There was an exception while setting the property " + prop_name + " to " + value.ToString() + "." + vbCrLf + _
                       "property type: " + temp.Type.ToString() + vbCrLf + _
                       "property subtype: " + temp.SubType.ToString(), MsgBoxStyle.Critical, "ScanExact")
            End Try
        End If
    End Sub

    Public ReadOnly Property DeviceId() As String
        Get
            Return _deviceID
        End Get
    End Property

    Private Sub SetBitDepth(ByVal value As Short) 'TODO: Probably useless
        If value <= 32 And value Mod 8 = 0 Then 'La profondità è multipla di 8 e minore o uguale a 32 bit
            Try
                _scanner.Properties("Bits Per Pixel").Value = value
                Trace.WriteLine(String.Format("Bits per Pixel set to {0}", value))
            Catch ex As ArgumentException
                Trace.WriteLine(String.Format("Couldn't set Bits per Pixel set to {0}. ERROR {1}", value, ex.Message))
                'Do nothing, there isn't any problem 
            Catch ex As UnauthorizedAccessException
                'Do nothing, the scanner doesn't allow to change the property
            Catch ex As COMException
                Trace.WriteLine(String.Format("Couldn't set Bits per Pixel set to {0}. ERROR {1}", value, ex.Message))
            End Try
        Else
            Throw New ArgumentException("Bit depth must be multiple of 8 and less or equal to 32")
        End If

    End Sub

    <CLSCompliant(False)> _
    Public Sub SetIntent(ByVal value As WiaImageIntent)
        If value = WiaImageIntent.ColorIntent Then
            Try
                _scanner.Properties("Current Intent").Value = value
                Trace.WriteLine(String.Format("Intent set to {0}", value))
            Catch e As COMException
                If e.ErrorCode = WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST Then
                    Try
                        _scanner.Properties("Channels per pixel").Value = 3 '3 channels (RGB)
                        Trace.WriteLine(String.Format("E: Couldn't set intent. Set channels per pixel instead"))
                    Catch ex As COMException
                        Trace.WriteLine(String.Format("E: Couldn't set intent. Report error"))
                        Throw
                    End Try
                Else : Throw
                End If
            End Try

            If My.Settings.LastScanSettings.BitDepth <> 0 Then
                Try
                    SetBitDepth(My.Settings.LastScanSettings.BitDepth)
                Catch ex As Exception

                End Try
            End If
        ElseIf value = WiaImageIntent.GrayscaleIntent Or value = WiaImageIntent.TextIntent Then
            Try
                _scanner.Properties("Current Intent").Value = value
            Catch e As ArgumentException
                'TODO:Localization or better handling
                MsgBox("Yor scanner doesn't seem to support this scan intent. ScanExact will use the default intent.", vbExclamation, "ScanExact")
            Catch e As COMException
                If e.ErrorCode = WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST Then
                    Try
                        _scanner.Properties("Channels per pixel").Value = 1 '1 channel (Grayscale)
                        Trace.WriteLine(String.Format("E: Couldn't set intent. Set channels per pixel instead"))
                    Catch ex As COMException
                        Trace.WriteLine(String.Format("E: Couldn't set intent. Report error"))
                        'MsgBox("Yor scanner doesn't seem to support this scan intent. ScanExact will use the default intent.", vbExclamation, "ScanExact")
                    End Try
                Else : Throw
                End If
            End Try
        Else
            _scanner.Properties("Current Intent").Value = WiaImageIntent.UnspecifiedIntent
        End If
    End Sub

    Public Sub SetResolution(ByVal value As Integer)
        If value = 0 Then value = _scanner.Properties("Horizontal Resolution").SubTypeDefault 'In case resolution value hasn't been set
        _scanner.Properties("Horizontal Resolution").Value = value
        _scanner.Properties("Vertical Resolution").Value = value
        Trace.WriteLine(String.Format("Resolution set to {0}", value))

    End Sub

    Public Sub SetExtent(resolution As Short, pagesize As PaperSize)
        'Calculate the extent in pixels based on paper size and resolution
        'Papersize width is in hundredths of an inch. 
        Dim width As Integer = 1.0 * (pagesize.Width * resolution / 100)
        Dim height As Integer = 1.0 * (pagesize.Height * resolution / 100)

        With _scanner.Properties("Horizontal Extent")
            Try
                Dim max As Integer = .SubTypeMax
                If width < max Then
                    .Value = width
                    Trace.WriteLine(String.Format("Set Horizontal Extent to: {0}", .Value))
                Else
                    .Value = max
                    Trace.WriteLine(String.Format("Set Horizontal Extent to its maximum value: {0}", .Value))
                End If
            Catch ex As Exception
                Trace.WriteLine(String.Format("Couldn't set Horizontal Extent. Exception: {0}", ex.ToString()))

            End Try
        End With

        With _scanner.Properties("Vertical Extent")
            Try
                Dim max As Integer = .SubTypeMax
                If height < max Then
                    .Value = height
                    Trace.WriteLine(String.Format("Set Vertical Extent to: {0}", .Value))
                Else
                    .Value = max
                    Trace.WriteLine(String.Format("Set Vertical Extent to its maximum value: {0}", .Value))
                End If
            Catch ex As Exception
                Trace.WriteLine(String.Format("Couldn't set Vertical Extent. Exception: {0}", ex.ToString()))

            End Try
        End With
    End Sub

    ReadOnly Property Description() As String
        Get
            Return _description
        End Get
    End Property

    Public Function GetAvailableResolutions() As List(Of Integer)
        Dim _AvailableResolutions As New List(Of Integer)

        Dim res As WIA.Property = _scanner.Properties("Horizontal Resolution")
        If res.SubType = WiaSubType.RangeSubType Then
            Dim stp As Integer = 100
            Dim min As Integer = 100
            Dim max As Integer = 2000
            If res.SubTypeMin <= 75 Then _AvailableResolutions.Add(75) 'Add ultra low resolution
            If res.SubTypeMin > min Then min = res.SubTypeMin
            If res.SubTypeStep > stp Then stp = res.SubTypeStep
            If res.SubTypeMax < max Then max = res.SubTypeMax
            For i As Integer = min To max Step stp
                _AvailableResolutions.Add(i)
            Next

        ElseIf res.SubType = WiaSubType.ListSubType Then
            For i As Integer = 1 To _scanner.Properties("Horizontal Resolution").SubTypeValues.Count
                _AvailableResolutions.Add(CInt(_scanner.Properties("Horizontal Resolution").SubTypeValues(i)))
            Next i
        End If
        Return _AvailableResolutions
    End Function

    ReadOnly Property AvailableResolutions() As List(Of Integer)
        Get
            Return _AvailableResolutions
        End Get
    End Property

    Private Shared Function Compress(ByVal quality As Integer, ByVal tmpImg As ImageFile) As ImageFile
        Dim ip As New ImageProcess()
        ip.Filters.Add(ip.FilterInfos("Convert").FilterID)
        ip.Filters(1).Properties("FormatID").Value = WIA.FormatID.wiaFormatJPEG
        ip.Filters(1).Properties("Quality").Value = quality

        Dim newimg As ImageFile = ip.Apply(tmpImg)
        tmpImg = Nothing
        Return newimg
    End Function

    Public ReadOnly Property CanUseADF() As Boolean
        Get
            Return _canUseADF
        End Get
    End Property

    Public ReadOnly Property CanDoDuplex() As Boolean
        Get
            Return _canDoDuplex
        End Get
    End Property

    'This function calls the appropriate acquisition function depending on the scanner name / manufacturer.
    Function ScanADF(ByVal options As ScanSettings) As List(Of String)
        If _description = "Brother MFC-6800" Or _description.Contains("Brother MFC-5440CN") Then
            Return ScanADFBrother6800(options)
        ElseIf _description.ToLower.Contains("wsd") Or _description.ToLower.Contains("scanning system") Then
            Return ScanWSD(options)
        ElseIf _description.ToLower().Contains("brother") Or _description.Contains("Canon MF4500") Then
            Return ScanADFBrother(options)
        Else
            Return ScanADFNormal(options)
        End If
    End Function

    <CLSCompliant(False)> _
    Public ReadOnly Property Events As WIA.DeviceEvents
        Get
            Dim _device As Device = manager.DeviceInfos.Item(DeviceId).Connect
            Return _device.Events
        End Get
    End Property

    Private Function ScanADFBrother6800(ByVal settings As ScanSettings) As List(Of String)
        Trace.WriteLine(String.Format("Starting acquisition (Brother MFC-6800/5440CN)"))
        Trace.Indent()
        Dim imageList As New List(Of String)()
        Dim dialog As New WIA.CommonDialog
        Dim _device As Device
        Dim hasMorePages As Boolean = True

        Dim img As WIA.ImageFile = Nothing

        Dim AcquiredPages As Integer = 0

        Try 'Make connection to the device
            _device = manager.DeviceInfos.Item(DeviceId).Connect
            _deviceID = DeviceId
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't connect to the device. ERROR {0}", ex.Message))
            Throw
        End Try

        'Set FEEDER options
        Dim handling As WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_DOCUMENT_HANDLING_SELECT.FLATBED

        If settings.UseADF And CanUseADF Then
            handling = WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER
            If settings.Duplex And CanDoDuplex Then
                handling = handling Or WIA_DPS_DOCUMENT_HANDLING_SELECT.DUPLEX
            End If
        End If

        Try
            _device.Properties("Document Handling Select").Value = handling
            Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT set to {0}", handling))
            Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT value: {0}", CType(_device.Properties("Document Handling Select").Value, WIA_DPS_DOCUMENT_HANDLING_SELECT)))
        Catch ex As COMException
            Select Case ex.ErrorCode
                Case WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST
                    Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT not supported"))
                Case Else
                    Trace.WriteLine(String.Format("Couldn't set WIA_DPS_DOCUMENT_HANDLING_SELECT. Error code {0}", CType(ex.ErrorCode, WIA_ERRORS)))
            End Select
        Catch ex As Exception
            Trace.WriteLine(String.Format("Exception thrown while setting WIA_DPS_DOCUMENT_HANDLING_SELECT"))
            Trace.WriteLine(ex.ToString())
        End Try

        'Connects the scanner
        Try
            _scanner = _device.Items(1)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't connect to the scanner. ERROR {0}", ex.Message))
            Throw
        End Try

        'Set all properties
        Trace.WriteLine(String.Format("Setting scan properties"))
        Trace.WriteLine(settings)

        SetBrightess(settings.Brightness)
        SetContrast(settings.Contrast)
        SetIntent(settings.Intent)

        Try
            SetResolution(settings.Resolution)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't set resolution to {0}.", settings.Resolution))
            Trace.WriteLine(String.Format("\tError: {0}", ex.ToString()))
        End Try

        SetExtent(settings.Resolution, settings.PaperSize) 'After setting resolution, maximize the extent

        Try
            SetBitDepth(settings.BitDepth)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't set BitDepth to {0}.", settings.BitDepth))
        End Try

        'Acquisition loop
        While hasMorePages
            Trace.WriteLine(String.Format("Image count {0}. Acquiring next image", AcquiredPages))

            Try 'Some scanner need WIA_DPS_PAGES to be set to 1, otherwise all pages are acquired but only one is returned as ImageFile
                '  Trace.WriteLine(String.Format("WIA_DPS_PAGES Value: {0}", _device.Properties("Pages").Value))
                _device.Properties("Pages").Value = 1
            Catch ex As COMException
                Trace.WriteLine(String.Format("Couldn't read/write WIA_DPS_PAGES. Error {0}", ex.ErrorCode))
            End Try

            Try 'Check DOCUMENT_HANDLING_STATUS for debug purpose
                Trace.WriteLine(String.Format("DOCUMENT_HANDLING_STATUS: {0}", _device.Properties("Document Handling Status").Value))
            Catch ex As Exception
                Trace.WriteLine("Couldn't evaluate DOCUMENT_HANDLING_STATUS")
            End Try

            Try 'This is the acquisition part.
                If settings.Preview Then
                    img = DirectCast(dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, settings.Intent, , , False, False, False), ImageFile)
                Else
                    img = DirectCast(dialog.ShowTransfer(_scanner, , False), ImageFile) 'This could throw ArgumentException.
                End If

                Trace.WriteLine("Image acquired")
                'Process the image to temporary file
                If img IsNot Nothing Then
                    Dim tpath As String = Path.GetTempFileName()
                    Try
                        File.Delete(tpath)
                    Catch ex As Exception

                    End Try
                    Try
                        img.SaveFile(tpath)
                        imageList.Add(tpath)
                        AcquiredPages += 1
                    Catch ex As Exception
                        Throw
                    End Try

                    img = Nothing
                Else 'Acquisition canceled
                    Trace.WriteLine("Acquisition canceled by the user")
                    Exit While
                End If
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_WARMING_UP
                        If MsgBoxWrap("The device is warming up. Press OK to Retry.", MsgBoxStyle.Information + vbOKCancel) = MsgBoxResult.Ok Then

                            Trace.WriteLine("Device warming up, waiting 2 seconds...")
                            Threading.Thread.Sleep(2000)
                            Continue While
                        Else
                            Exit While
                        End If
                    Case WIA_ERRORS.WIA_ERROR_PAPER_EMPTY   'This error is reported when ADF is empty
                        Trace.WriteLine(String.Format("The ADF is empty"))
                        Exit While                          'The acquisition is complete
                    Case WIA_ERRORS.WIA_ERROR_PAPER_JAM
                        Dim result As MsgBoxResult = MsgBoxWrap("The paper in the document feeder is jammed." + _
                                                             "Please check the feeder and click Ok to resume the acquisition, Cancel to abort", vbOKCancel + vbExclamation, "ScanExact")
                        If result = MsgBoxResult.Ok Then Continue While
                        If result = MsgBoxResult.Cancel Then Exit While
                    Case WIA_ERRORS.WIA_ERROR_BUSY
                        Trace.WriteLine("Device busy, waiting 2 seconds...")
                        Threading.Thread.Sleep(2000)
                        Continue While
                    Case Else
                        Trace.WriteLine(String.Format("Acquisition threw the exception {0}", ex.ErrorCode))
                        Throw
                End Select
            Catch ex As Exception
                Trace.WriteLine(String.Format("Acquisition threw the exception {0}", ex.ToString()))
                Throw 'TODO: Error handling
            End Try

            If Not settings.UseADF Then Exit While
            'Determine if there are any more pages waiting
            Trace.WriteLine(String.Format("Checking if there are more pages..."))

            hasMorePages = False 'Assume there are no more pages
            Try
                Dim status As WIA_DPS_DOCUMENT_HANDLING_STATUS = _device.Properties("Document Handling Status").Value
                Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS: {0}", status))
                hasMorePages = ((status And WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) <> 0)
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST
                        Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS not supported"))
                    Case Else
                        Trace.WriteLine(String.Format("Couldn't get WIA_DPS_DOCUMENT_HANDLING_STATUS. Error code {0}", ex.ErrorCode))
                End Select
            Catch ex As Exception
                Trace.WriteLine(String.Format("Exception thrown on WIA_DPS_DOCUMENT_HANDLING_STATUS"))
                Console.Write(ex.ToString())
            End Try
        End While

        If _scanner IsNot Nothing Then _scanner = Nothing
        Console.Write("Acquisition complete, returning {0} images", AcquiredPages)
        Trace.Unindent()
        Return imageList
    End Function

    Private Function ScanWSD(ByVal settings As ScanSettings) As List(Of String)
        Trace.WriteLine(String.Format("Starting acquisition (WSD)"))
        Trace.Indent()
        Dim imageList As New List(Of String)()
        Dim dialog As New WIA.CommonDialog
        Dim _device As Device
        Dim hasMorePages As Boolean = True

        Dim img As WIA.ImageFile = Nothing

        Dim AcquiredPages As Integer = 0

        Try 'Make connection to the device
            _device = manager.DeviceInfos.Item(DeviceId).Connect
            _deviceID = DeviceId
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't connect to the device. ERROR {0}", ex.Message))
            Throw
        End Try

        'Set FEEDER options
        Dim handling As WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER

        If settings.UseADF And CanUseADF Then
            handling = WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER
            If settings.Duplex And CanDoDuplex Then
                handling = handling Or WIA_DPS_DOCUMENT_HANDLING_SELECT.DUPLEX
            End If
        End If

        Try
            _device.Properties("Document Handling Select").Value = handling
            Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT set to {0}", handling))
            Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT value: {0}", _device.Properties("Document Handling Select").Value))
        Catch ex As COMException
            Select Case ex.ErrorCode
                Case WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST
                    Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT not supported"))
                Case Else
                    Trace.WriteLine(String.Format("Couldn't set WIA_DPS_DOCUMENT_HANDLING_SELECT. Error code {0}", CType(ex.ErrorCode, WIA_ERRORS)))
            End Select
        Catch ex As Exception
            Trace.WriteLine(String.Format("Exception thrown on WIA_DPS_DOCUMENT_HANDLING_SELECT"))
            Console.Write(ex.ToString())
        End Try

        'Connects the scanner
        Try
            _scanner = _device.Items(1)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't connect to the scanner. ERROR {0}", ex.Message))
            Throw
        End Try

        'Set all properties
        Trace.WriteLine(String.Format("Setting scan properties"))
        Trace.WriteLine(settings)

        SetBrightess(settings.Brightness)
        SetContrast(settings.Contrast)
        SetIntent(settings.Intent)

        Try
            SetResolution(settings.Resolution)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't set resolution to {0}.", settings.Resolution))
            Trace.WriteLine(String.Format("\tError: {0}", ex.ToString()))
        End Try

        SetExtent(settings.Resolution, settings.PaperSize) 'After setting resolution, maximize the extent

        Try
            If settings.BitDepth <> 0 Then SetBitDepth(settings.BitDepth)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't set BitDepth to {0}.", settings.BitDepth))
        End Try

        'Acquisition loop
        While hasMorePages
            Trace.WriteLine(String.Format("Image count {0}. Acquiring next image", AcquiredPages))

            Try 'Some scanner need WIA_DPS_PAGES to be set to 2, otherwise all pages are acquired but only one is returned as ImageFile
                'Trace.WriteLine(String.Format("WIA_DPS_PAGES Value: {0}", _device.Properties("Pages").Value))
                _device.Properties("Pages").Value = 2
                '_device.Properties("Server").Value = "local"
                'Trace.WriteLine(String.Format("FORMAT Value: {0}", _device.Properties("Format").Value))
            Catch ex As COMException
                Trace.WriteLine(String.Format("Couldn't read/write WIA_DPS_PAGES. Error {0}", ex.ErrorCode))
            End Try

            Try 'Check DOCUMENT_HANDLING_STATUS for debug purpose
                Trace.WriteLine(String.Format("DOCUMENT_HANDLING_STATUS: {0}", _device.Properties("Document Handling Status").Value))
            Catch ex As Exception
                Trace.WriteLine("Couldn't evaluate DOCUMENT_HANDLING_STATUS")
            End Try

            Try 'This is the acquisition part.
                If settings.Preview Then
                    img = DirectCast(dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, settings.Intent, , , False, False, False), ImageFile)
                Else
                    img = DirectCast(dialog.ShowTransfer(_scanner, , False), ImageFile) 'This could throw ArgumentException.
                End If

                Trace.WriteLine("Image acquired")
                'Process the image to temporary file
                If img IsNot Nothing Then
                    Dim tpath As String = Path.GetTempFileName()
                    Try
                        File.Delete(tpath)
                    Catch ex As Exception

                    End Try
                    Try

                        img.SaveFile(tpath)
                        imageList.Add(tpath)
                        AcquiredPages += 1
                    Catch ex As Exception
                        Throw
                    End Try

                    img = Nothing
                Else 'Acquisition canceled
                    Trace.WriteLine("Acquisition canceled by the user")
                    Exit While
                End If
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_PAPER_EMPTY   'This error is reported when ADF is empty
                        Trace.WriteLine(String.Format("The ADF is empty"))
                        Exit While                          'The acquisition is complete
                    Case WIA_ERRORS.WIA_ERROR_PAPER_JAM
                        Dim result As MsgBoxResult = MsgBoxWrap("The paper in the document feeder is jammed." + _
                                                             "Please check the feeder and click Ok to resume the acquisition, Cancel to abort", vbOKCancel + vbExclamation, "ScanExact")
                        If result = MsgBoxResult.Ok Then Continue While
                        If result = MsgBoxResult.Cancel Then Exit While
                    Case WIA_ERRORS.WIA_ERROR_BUSY
                        Trace.WriteLine("Device busy, waiting 2 seconds...")
                        Threading.Thread.Sleep(2000)
                        Continue While
                    Case WIA_ERRORS.WIA_ERROR_WSD_FEEDER_EMPTY
                        Trace.WriteLine(String.Format("Acquisition threw E_FAIL exception {0}", ex.ErrorCode))
                        Exit While
                    Case Else
                        'Trace.WriteLine(String.Format("Acquisition threw the exception {0}", ex.ErrorCode))
                        'Throw
                End Select
            Catch ex As Exception
                Trace.WriteLine(String.Format("Acquisition threw the exception {0}", ex.ToString()))
                Throw 'TODO: Error handling
            End Try

            If Not settings.UseADF Then Exit While
            'Determine if there are any more pages waiting
            Trace.WriteLine(String.Format("Checking if there are more pages..."))

            hasMorePages = False 'Assume there are no more pages
            Try
                Dim status As WIA_DPS_DOCUMENT_HANDLING_STATUS = _device.Properties("Document Handling Status").Value
                Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS: {0}", status.ToString()))
                hasMorePages = False '((status And WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) <> 0)
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST
                        Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS not supported"))
                    Case Else
                        Trace.WriteLine(String.Format("Couldn't get WIA_DPS_DOCUMENT_HANDLING_STATUS. Error code {0}", ex.ErrorCode))
                End Select
            Catch ex As Exception
                Trace.WriteLine(String.Format("Exception thrown on WIA_DPS_DOCUMENT_HANDLING_STATUS"))
                Console.Write(ex.ToString())
            End Try
        End While
        Trace.WriteLine("Exited loop")
        If _scanner IsNot Nothing Then _scanner = Nothing
        Trace.WriteLine(String.Format("Acquisition complete, returning {0} images", AcquiredPages))
        Trace.Unindent()
        Return imageList
    End Function

    Private Function ScanADFBrother(ByVal settings As ScanSettings) As List(Of String)
        Trace.WriteLine(String.Format("Starting acquisition (Brother)"))
        Trace.Indent()
        Dim imageList As New List(Of String)()
        Dim dialog As New WIA.CommonDialog
        Dim _device As Device
        Dim hasMorePages As Boolean = True

        Dim img As WIA.ImageFile = Nothing

        Dim AcquiredPages As Integer = 0

        Try 'Make connection to the device
            _device = manager.DeviceInfos.Item(DeviceId).Connect
            _deviceID = DeviceId
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't connect to the device. ERROR {0}", ex.Message))
            Throw
        End Try

        'Set FEEDER options
        Dim handling As WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_DOCUMENT_HANDLING_SELECT.FLATBED

        If settings.UseADF And CanUseADF Then
            handling = WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER
            If settings.Duplex And CanDoDuplex Then
                handling = handling Or WIA_DPS_DOCUMENT_HANDLING_SELECT.DUPLEX
            End If
        End If

        Try
            _device.Properties("Document Handling Select").Value = handling
            Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT set to {0}", handling))
            Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT value: {0}", _device.Properties("Document Handling Select").Value))
        Catch ex As COMException
            Select Case ex.ErrorCode
                Case WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST
                    Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT not supported"))
                Case Else
                    Trace.WriteLine(String.Format("Couldn't set WIA_DPS_DOCUMENT_HANDLING_SELECT. Error code {0}", CType(ex.ErrorCode, WIA_ERRORS)))
            End Select
        Catch ex As Exception
            Trace.WriteLine(String.Format("Exception thrown on WIA_DPS_DOCUMENT_HANDLING_SELECT"))
            Console.Write(ex.ToString())
        End Try

        'Connects the scanner
        Try
            _scanner = _device.Items(1)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't connect to the scanner. ERROR {0}", ex.Message))
            Throw
        End Try

        'Set all properties
        Trace.WriteLine(String.Format("Setting scan properties"))
        Trace.WriteLine(settings)

        SetBrightess(settings.Brightness)
        SetContrast(settings.Contrast)
        SetIntent(settings.Intent)

        Try
            SetResolution(settings.Resolution)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't set resolution to {0}.", settings.Resolution))
            Trace.WriteLine(String.Format("\tError: {0}", ex.ToString()))
        End Try

        SetExtent(settings.Resolution, settings.PaperSize) 'After setting resolution, maximize the extent

        Try
            If settings.BitDepth <> 0 Then SetBitDepth(settings.BitDepth)
        Catch ex As Exception
            Trace.WriteLine(String.Format("Couldn't set BitDepth to {0}.", settings.BitDepth))
        End Try

        'Acquisition loop
        While hasMorePages
            Trace.WriteLine(String.Format("Image count {0}. Acquiring next image", AcquiredPages))

            Try 'Some scanner need WIA_DPS_PAGES to be set to 1, otherwise all pages are acquired but only one is returned as ImageFile
                Trace.WriteLine(String.Format("WIA_DPS_PAGES Value: {0}", _device.Properties("Pages").Value))
                _device.Properties("Pages").Value = 1
            Catch ex As COMException
                Trace.WriteLine(String.Format("Couldn't read/write WIA_DPS_PAGES. Error {0}", ex.ErrorCode))
            End Try

            Try 'Check DOCUMENT_HANDLING_STATUS for debug purpose
                Trace.WriteLine(String.Format("DOCUMENT_HANDLING_STATUS: {0}", _device.Properties("Document Handling Status").Value))
            Catch ex As Exception
                Trace.WriteLine("Couldn't evaluate DOCUMENT_HANDLING_STATUS")
            End Try

            Try 'This is the acquisition part.
                If settings.Preview Then
                    img = DirectCast(dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, settings.Intent, , , False, False, False), ImageFile)
                Else
                    img = DirectCast(dialog.ShowTransfer(_scanner, , False), ImageFile) 'This could throw ArgumentException.
                End If

                Trace.WriteLine("Image acquired")
                'Process the image to temporary file
                If img IsNot Nothing Then
                    Dim tpath As String = Path.GetTempFileName()
                    Try
                        File.Delete(tpath)
                    Catch ex As Exception

                    End Try
                    Try

                        img.SaveFile(tpath)
                        imageList.Add(tpath)
                        AcquiredPages += 1
                    Catch ex As Exception
                        Throw
                    End Try

                    img = Nothing
                Else 'Acquisition canceled
                    Trace.WriteLine("Acquisition canceled by the user")
                    Exit While
                End If
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_PAPER_EMPTY   'This error is reported when ADF is empty
                        Trace.WriteLine(String.Format("The ADF is empty"))
                        Exit While                          'The acquisition is complete
                    Case WIA_ERRORS.WIA_ERROR_PAPER_JAM
                        Dim result As MsgBoxResult = MsgBoxWrap("The paper in the document feeder is jammed." + _
                                                             "Please check the feeder and click Ok to resume the acquisition, Cancel to abort", vbOKCancel + vbExclamation, "ScanExact")
                        If result = MsgBoxResult.Ok Then Continue While
                        If result = MsgBoxResult.Cancel Then Exit While
                    Case WIA_ERRORS.WIA_ERROR_BUSY
                        Trace.WriteLine("Device busy, waiting 2 seconds...")
                        Threading.Thread.Sleep(2000)
                        Continue While
                    Case Else
                        'Trace.WriteLine(String.Format("Acquisition threw the exception {0}", ex.ErrorCode))
                        'Throw
                End Select
            Catch ex As Exception
                Trace.WriteLine(String.Format("Acquisition threw the exception {0}", ex.ToString()))
                Throw 'TODO: Error handling
            End Try

            If Not settings.UseADF Then Exit While
            'Determine if there are any more pages waiting
            Trace.WriteLine(String.Format("Checking if there are more pages..."))

            hasMorePages = False 'Assume there are no more pages
            Try
                Dim status As WIA_DPS_DOCUMENT_HANDLING_STATUS = _device.Properties("Document Handling Status").Value
                Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS: {0}", status.ToString()))
                hasMorePages = ((status And WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) <> 0)
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST
                        Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS not supported"))
                    Case Else
                        Trace.WriteLine(String.Format("Couldn't get WIA_DPS_DOCUMENT_HANDLING_STATUS. Error code {0}", ex.ErrorCode))
                End Select
            Catch ex As Exception
                Trace.WriteLine(String.Format("Exception thrown on WIA_DPS_DOCUMENT_HANDLING_STATUS"))
                Console.Write(ex.ToString())
            End Try
        End While
        Trace.WriteLine("Exited loop")
        If _scanner IsNot Nothing Then _scanner = Nothing
        Trace.WriteLine(String.Format("Acquisition complete, returning {0} images", AcquiredPages))
        Trace.Unindent()
        Return imageList
    End Function

    Private Function ScanADFNormal(ByVal settings As ScanSettings) As List(Of String)
        Trace.WriteLine(String.Format("Starting acquisition ADF Normal"))
        Trace.Indent()
        Trace.WriteLine(settings)
        Dim imageList As New List(Of String)()
        Dim dialog As New WIA.CommonDialog
        Dim _device As Device
        Dim hasMorePages As Boolean = True

        Dim img As WIA.ImageFile = Nothing

        Dim AcquiredPages As Integer = 0

        While hasMorePages
            Try 'Make connection to the device
                _device = manager.DeviceInfos.Item(DeviceId).Connect
                _deviceID = DeviceId
            Catch ex As Exception
                Trace.WriteLine(String.Format("Couldn't connect to the device. ERROR {0}", ex.Message))
                Throw
            End Try

            'Set FEEDER options
            Dim handling As WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_DOCUMENT_HANDLING_SELECT.FLATBED

            If settings.UseADF And CanUseADF Then
                handling = WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER
                If settings.Duplex And CanDoDuplex Then
                    handling = handling Or WIA_DPS_DOCUMENT_HANDLING_SELECT.DUPLEX

                    Try 'Some scanner need WIA_DPS_PAGES to be set to 2, otherwise only the front page is acquired
                        Trace.WriteLine(String.Format("WIA_DPS_PAGES Value: {0}", _device.Properties("Pages").Value))
                        _device.Properties("Pages").Value = 2
                    Catch ex As COMException
                        Trace.WriteLine(String.Format("Couldn't read/write WIA_DPS_PAGES. Error {0}", ex.ErrorCode))
                    End Try

                End If
            End If

            Try
                _device.Properties("Document Handling Select").Value = handling
                _device.Properties("Compression").Value = 0
                Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT set to {0}", handling))
                Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT value: {0}", _device.Properties("Document Handling Select").Value))
                If _device.Properties.Exists("Sheet Feeder Registration") Then
                    Trace.WriteLine(String.Format("Sheet feeder registration value: {0}", _device.Properties("Sheet Feeder Registration").Value))
                End If
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST
                        Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_SELECT not supported"))
                    Case Else
                        Trace.WriteLine(String.Format("Couldn't set WIA_DPS_DOCUMENT_HANDLING_SELECT. Error code {0}", CType(ex.ErrorCode, WIA_ERRORS)))
                End Select
            Catch ex As Exception
                Trace.WriteLine(String.Format("Exception thrown on WIA_DPS_DOCUMENT_HANDLING_SELECT"))
                Console.Write(ex.ToString())
            End Try
            'Connects to the scanner
            Try
                _scanner = _device.Items(1)
            Catch ex As Exception
                Trace.WriteLine(String.Format("Couldn't connect to the scanner. ERROR {0}", ex.Message))
                Throw
            End Try

            'Set all properties
            Trace.WriteLine(String.Format("Setting scan properties"))

            SetBrightess(settings.Brightness)
            SetContrast(settings.Contrast)

            If _description.Contains("Kyocera") Then
                Select Case settings.Intent
                    Case WiaImageIntent.ColorIntent
                        SetIntent(65536)
                    Case WiaImageIntent.GrayscaleIntent
                        SetIntent(1)
                    Case WiaImageIntent.TextIntent
                        SetIntent(0)
                    Case WiaImageIntent.UnspecifiedIntent
                        SetIntent(131072)
                End Select
            Else
                SetIntent(settings.Intent)
            End If

            Try
                SetResolution(settings.Resolution)
            Catch ex As Exception
                Trace.WriteLine(String.Format("Couldn't set resolution to {0}.", settings.Resolution))
                Trace.WriteLine(String.Format("\tError: {0}", ex.ToString()))
            End Try

            SetExtent(settings.Resolution, settings.PaperSize) 'After setting resolution, maximize the extent

            'Used to set the page size to A4 to fix longer pages with ADF. Most scanners don't support this property so they won't be affected
            If settings.UseADF And settings.PaperSize.PaperName = "A4" Then
                Try
                    Trace.WriteLine("Setting scan extent to A4")
                    _device.Properties("Page Size").Value = WIA_IPS_PAGE_SIZE.WIA_PAGE_A4
                    Trace.WriteLine(String.Format("Horizontal extent {0}, Vertical extent {1}", _scanner.Properties("Horizontal Extent").Value, _scanner.Properties("Vertical Extent").Value))

                Catch ex As Exception
                    Trace.WriteLine("Scanner does not support WIA_IPS_PAGE_SIZE")
                End Try
            End If

            Try
                SetBitDepth(settings.BitDepth)
            Catch ex As Exception
                Trace.WriteLine(String.Format("Couldn't set BitDepth to {0}.", settings.BitDepth))
            End Try

            Try
                Trace.WriteLine(String.Format("Image count {0}. Acquiring next image", AcquiredPages))
                Try
                    Trace.WriteLine(String.Format("DOCUMENT_HANDLING_STATUS: {0}", _device.Properties("Document Handling Status").Value))
                Catch ex As Exception
                    Trace.WriteLine("Couldn't evaluate DOCUMENT_HANDLING_STATUS")
                End Try
                Trace.WriteLine(String.Format("Subitems count: {0}", _scanner.Items.Count))

                If settings.Preview Then
                    img = DirectCast(dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, settings.Intent, , , False, False, False), ImageFile)
                Else
                    img = DirectCast(dialog.ShowTransfer(_scanner, , False), ImageFile)
                End If

                'LOGGING FOR BUG #236
                Trace.WriteLine(String.Format("Image acquired. Details: W {0} H {1} Hres {2} Vres {3}", img.Width, img.Height, img.HorizontalResolution, img.VerticalResolution))

                If img IsNot Nothing Then
                    If settings.Path.EndsWith("jpg") Then 'If this is a ScanToFile to jpg, apply compression
                        img = Compress(settings.Quality, img)
                    End If

                    Dim tpath As String = Path.GetTempFileName()
                    Try
                        File.Delete(tpath)
                    Catch ex As Exception
                    End Try

                    img.SaveFile(tpath)
                    imageList.Add(tpath)
                    img = Nothing

                    AcquiredPages += 1
                Else 'Acquisition canceled
                    Trace.WriteLine("Acquisition canceled by the user")
                    Exit While
                End If
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_PAPER_EMPTY   'This error is reported when ADF is empty
                        Trace.WriteLine(String.Format("The ADF is empty"))
                        Exit While                          'The acquisition is complete
                    Case WIA_ERRORS.WIA_ERROR_PAPER_JAM
                        Trace.WriteLine("Paper jammed.")
                        Dim result As MsgBoxResult = MsgBoxWrap("The paper in the document feeder is jammed." + _
                                                             "Please check the feeder and click Ok to resume the acquisition, Cancel to abort", vbOKCancel + vbExclamation, "ScanExact")
                        If result = MsgBoxResult.Ok Then Continue While
                        If result = MsgBoxResult.Cancel Then Exit While
                    Case WIA_ERRORS.WIA_ERROR_BUSY
                        Trace.WriteLine("Device busy, waiting 2 seconds...")
                        Threading.Thread.Sleep(2000)
                        _scanner = Nothing
                        Continue While
                    Case WIA_ERRORS.WIA_ERROR_FEEDER_EMPTY
                        Trace.WriteLine("Feeder empty")
                        MsgBoxWrap(LocRM.GetString("Msg_FeederEmpty"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                        '_scanner = Nothing
                        Exit While
                    Case Else
                        Trace.WriteLine(String.Format("Acquisition threw the exception {0}", ex.ErrorCode))
                End Select
                Throw
            Catch ex As Exception
                Trace.WriteLine(String.Format("Acquisition threw the exception {0}", ex.ToString()))
                'TODO: Error handling
                Exit While
            End Try
            If Not settings.UseADF Then Exit While
            'determine if there are any more pages waiting
            Trace.WriteLine(String.Format("Checking if there are more pages..."))

            hasMorePages = False 'assume there are no more pages
            Try
                Dim status As WIA_DPS_DOCUMENT_HANDLING_STATUS = _device.Properties("Document Handling Status").Value
                Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS: {0}", status.ToString()))
                hasMorePages = ((status And WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) <> 0)
            Catch ex As COMException
                Select Case ex.ErrorCode
                    Case WIA_ERRORS.WIA_ERROR_PROPERTY_DONT_EXIST
                        Trace.WriteLine(String.Format("WIA_DPS_DOCUMENT_HANDLING_STATUS not supported"))
                    Case Else
                        Trace.WriteLine(String.Format("Couldn't get WIA_DPS_DOCUMENT_HANDLING_STATUS. Error code {0}", ex.ErrorCode))
                End Select
            Catch ex As Exception
                Trace.WriteLine(String.Format("Exception thrown on WIA_DPS_DOCUMENT_HANDLING_STATUS"))
                Console.Write(ex.ToString())
            End Try

            Try
                Trace.WriteLine(String.Format("WIA_DPS_PAGES Value: {0}", _device.Properties("Pages").Value))
                If Convert.ToInt32(_device.Properties("Pages").Value) > 0 Then
                    'More pages are available
                    hasMorePages = True
                End If
            Catch ex As COMException
                Trace.WriteLine(String.Format("Couldn't read WIA_DPS_PAGES. Error {0}", ex.ErrorCode))
            End Try

            _scanner = Nothing
            Trace.WriteLine(String.Format("Closed connection to the scanner"))
        End While
        If _scanner IsNot Nothing Then _scanner = Nothing
        Console.Write("Acquisition complete, returning {0} images", AcquiredPages)
        Trace.Unindent()
        Return imageList
    End Function

    Private Sub TraceProp(ByVal prop As WIA.IProperty)
        Trace.WriteLine(String.Format("Property {0}: {1}  TYPE {2}", prop.PropertyID, prop.Name, prop.Type))

        If prop.IsVector Then
            Trace.WriteLine(vbTab + "IsVector")
        Else
            If prop.SubType <> WIA.WiaSubType.UnspecifiedSubType Then
                Trace.WriteLine(String.Format(vbTab + "Default value: {0}", prop.SubTypeDefault))
            End If
        End If

        Trace.WriteLine(String.Format(vbTab + "ReadOnly: {0}", prop.IsReadOnly))
        Trace.WriteLine(String.Format(vbTab + "Value: {0}", prop.Value))

        Trace.WriteLine(String.Format(vbTab + "SubType: {0}", prop.SubType))
        Select Case prop.SubType
            Case WiaSubType.FlagSubType
                For i = 1 To prop.SubTypeValues.Count
                    Trace.WriteLine(String.Format(vbTab + vbTab + "{0}", prop.SubTypeValues(i)))
                Next
            Case WiaSubType.ListSubType
                For i = 1 To prop.SubTypeValues.Count
                    Trace.WriteLine(String.Format(vbTab + vbTab + "{0}", prop.SubTypeValues(i)))
                Next
            Case WiaSubType.RangeSubType
                Trace.WriteLine(String.Format(vbTab + vbTab + "Min {0}, Max {1}, Step {2}", prop.SubTypeMin, prop.SubTypeMax, prop.SubTypeStep))
            Case Else 'UnspecifiedSubType
        End Select
    End Sub

    Public Sub XMLReport()
        Dim _device As Device
        Dim _scanner As WIA.Item
        _device = manager.DeviceInfos.Item(DeviceId).Connect
        _deviceID = DeviceId
        _scanner = _device.Items(1)
        Dim doc As New XmlTextWriter("properties.xml", Nothing)
        doc.WriteStartDocument()
        doc.WriteStartElement("Device")
        doc.WriteStartElement("DeviceProperties")
        PropertiesToXML.PropertiesToXML(_device.Properties, doc)
        doc.WriteEndElement()

        doc.WriteStartElement("ScannerProperties")
        PropertiesToXML.PropertiesToXML(_scanner.Properties, doc)
        doc.WriteEndElement()
        doc.WriteEndElement()
        doc.WriteEndDocument()
        doc.Close()
        _scanner = Nothing
    End Sub

    Public Sub WritePropertiesLog()
        Trace.Indent()
        Dim _device As Device
        Dim _scanner As WIA.Item
        _device = manager.DeviceInfos.Item(DeviceId).Connect
        _deviceID = DeviceId
        _scanner = _device.Items(1)
        For Each p As WIA.Property In _device.Properties
            Try
                TraceProp(p)
            Catch ex As Exception
                Trace.TraceError("Couldn't read property {0}", p.ToString())
            End Try
        Next

        For Each p As WIA.Property In _scanner.Properties
            Try
                TraceProp(p)
            Catch ex As Exception
                Trace.TraceError("Couldn't read property {0}", p.PropertyID)
            End Try
        Next

        Trace.Unindent()
        _scanner = Nothing
    End Sub

End Class