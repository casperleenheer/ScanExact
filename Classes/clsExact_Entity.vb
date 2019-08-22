Imports Exact.Services.Client
Imports Exact.Services.Client.Data.EntityData
Public Class clsExact_Entity

    Public Enum EntityNames
        Resource
        SalesOrderHeader
        SalesOrderLine
        PurchaseOrderHeader
        PurchaseOrderLine
        QuotationOrderHeader
        QuotationOrderLine
        InvoiceHeader
        InvoiceLine
    End Enum

    Private _client As Exact.Services.Client.Entity.EntityClientEG
    Private _EntityName As EntityNames
    Public Property EntityName As EntityNames
        Get
            Return _EntityName
        End Get
        Set(value As EntityNames)
            _EntityName = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByRef Client As Object, Optional EntityName As EntityNames = EntityNames.Resource)
        _client = Client
        _EntityName = EntityName
    End Sub

    Public Function IsAvailable(OrderNumber As String) As Boolean
        Dim _data As New Exact.Services.Client.Data.EntityData
        _data.EntityName = _EntityName.ToString
        _data.Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "SalesOrderNumber", .Value = OrderNumber})
        Try
            _client.Retrieve(_data)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    <CLSCompliant(False)>
    Public Function EntityData(OrderNumberPropertyName As String, OrderNumberPropertyValue As String) As Exact.Services.Client.Data.EntityData
        Dim _data As New Exact.Services.Client.Data.EntityData
        _data.EntityName = _EntityName.ToString
        _data.Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = OrderNumberPropertyName, .Value = OrderNumberPropertyValue})
        Try
            _data = _client.Retrieve(_data)
        Catch ex As Exception
            Return Nothing
        End Try
        Return _data
    End Function

    Private Function PropertyName(EntityName As String) As String
        Dim _propertyName As String = String.Empty
        Select Case EntityName
            Case "SalesOrderHeader"
                _propertyName = "SalesOrderNumber"
            Case "PurchaseOrderHeader"
                _propertyName = "PurchaseOrderNumber"
            Case Else
                MessageBox.Show("Not implemented in clsExact_Entity")
        End Select
        Return _propertyName
    End Function

End Class
