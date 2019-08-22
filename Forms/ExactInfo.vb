Public Class ExactInfo

    Private _OrderNo As String = String.Empty
    Private _FullName As String = String.Empty
    Private _Address As String = String.Empty
    Private _Zipcode As String = String.Empty
    Private _City As String = String.Empty
    Private _data As Exact.Services.Client.Data.EntityData

    Public WriteOnly Property OrderNo As String
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Public WriteOnly Property Fullname As String
        Set(value As String)
            _FullName = value
        End Set
    End Property

    Public WriteOnly Property Address As String
        Set(value As String)
            _Address = value
        End Set
    End Property

    Public WriteOnly Property Zipcode As String
        Set(value As String)
            _Zipcode = value
        End Set
    End Property

    Public WriteOnly Property City As String
        Set(value As String)
            _City = value
        End Set
    End Property

    <CLSCompliant(False)>
    Public WriteOnly Property Data As Exact.Services.Client.Data.EntityData
        Set(value As Exact.Services.Client.Data.EntityData)
            _data = value
            RefreshData(_data)
        End Set
    End Property

    Private Sub ExactInfo_Load(sender As Object, e As EventArgs) Handles Me.Load

        lblOrderNo.Text = appControl.GetLocalizedString("ExactInfo_lblOrderNo")
        lblFullname.Text = appControl.GetLocalizedString("ExactInfo_lblFullname")
        lblAddress.Text = appControl.GetLocalizedString("ExactInfo_lblAddress")
        lblZipCity.Text = appControl.GetLocalizedString("ExactInfo_lblZipCity")
        lblHeader.Text = appControl.GetLocalizedString("ExactInfo_lblHeader")
        Me.Icon = My.Resources.scanner_3
        Me.Text = appControl.GetLocalizedString("ExactInfo_Text")
        cmdOk.Text = appControl.GetLocalizedString("ExactInfo_cmdOk")
        cmdCancel.Text = appControl.GetLocalizedString("ExactInfo_cmdCancel")

        lblAddressData.Text = _Address.Trim
        lblFullnameData.Text = _FullName.Trim
        lblOrderNoData.Text = _OrderNo.Trim
        lblZipZityData.Text = _Zipcode.Trim + "  " + _City.Trim
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub RefreshData(EntityData As Exact.Services.Client.Data.EntityData)
        If Not EntityData Is Nothing Then
            For Each Prop In EntityData.Properties
                With Prop
                    Select Case Prop.Name
                        Case "OrderDebtorAddress1"
                            _Address = .Value
                        Case "OrderDebtorCity"
                            _City = .Value
                        Case "OrderDebtorName"
                            _FullName = .Value
                        Case "OrderDebtorPostCode"
                            _Zipcode = .Value
                        Case "SalesOrderNumber"
                            _OrderNo = .Value
                    End Select
                End With
            Next
        End If
    End Sub


End Class