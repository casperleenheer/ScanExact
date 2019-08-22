Imports WIA
Imports System.Drawing.Printing

Public Enum ScanOutput
    Printer
    File
    PDF
    Exact
End Enum

Public Class ScanSettings

    Private _Brightness As Integer
    Private _center As Boolean
    Private _Contrast As Integer
    Private _BitDepth As Integer
    Private _Resolution As Short
    Private _Quality As Integer
    Private _Intent As WiaImageIntent
    Private _Preview As Boolean
    Private _Scaling As Integer
    Private _Copies As Integer
    Private _Path As String
    Private _UseADF As Boolean
    Private _duplex As Boolean
    Private _scanOutput As ScanOutput
    Private _exportToExact As Boolean
    Private _multipage As Boolean
    Private _paperSize As PaperSize
    Private _TypeOfOrder As Integer
    Private _Administrations As String
    Private _SQLserver As String
    Private _ServiceURI As String

    ''' Creates default properties
    Public Sub New()
        _UseADF = False
        _duplex = False
        _Brightness = 0
        _Contrast = 0
        _Quality = 100
        _Preview = False
        _Copies = 1
        _Intent = WIA.WiaImageIntent.ColorIntent
        _Resolution = 100
        _Scaling = 100
        _BitDepth = 0
        _Path = ""
        _exportToExact = True
        _scanOutput = ScanExact.ScanOutput.Printer
        _multipage = False
        _center = True
        _paperSize = New PaperSize("A4", 595.22, 842)
        _TypeOfOrder = 1
        _Administrations = ""
        _SQLserver = ""
        _ServiceURI = ""
    End Sub

    Public Property Brightness() As Integer
        Get
            Return _Brightness
        End Get
        Set(ByVal value As Integer)
            _Brightness = value
        End Set
    End Property

    Public Property TypeOfOrder() As Integer
        Get
            Return _TypeOfOrder
        End Get
        Set(ByVal value As Integer)
            _TypeOfOrder = value
        End Set
    End Property

    Public Property Center() As Boolean
        Get
            Return _center
        End Get
        Set(value As Boolean)
            _center = value
        End Set
    End Property

    Public Property Contrast() As Integer
        Get
            Return _Contrast
        End Get
        Set(ByVal value As Integer)
            _Contrast = value
        End Set
    End Property

    Public Property PaperSize() As PaperSize
        Get
            Return _paperSize
        End Get
        Set(ByVal value As PaperSize)
            _paperSize = value
        End Set
    End Property

    Public Property Resolution() As Integer
        Get
            Return _Resolution
        End Get
        Set(ByVal value As Integer)
            _Resolution = value
        End Set
    End Property

    <CLSCompliant(False)> _
    Public Property Intent() As WiaImageIntent
        Get
            Return _Intent
        End Get
        Set(ByVal value As WiaImageIntent)
            _Intent = value
        End Set
    End Property

    Public Property Quality() As Integer
        Get
            Return _Quality
        End Get
        Set(ByVal value As Integer)
            _Quality = value
        End Set
    End Property

    Public Property Preview() As Boolean
        Get
            Return _Preview
        End Get
        Set(ByVal value As Boolean)
            _Preview = value
        End Set
    End Property

    Public Property Scaling() As Integer
        Get
            Return _Scaling
        End Get
        Set(ByVal value As Integer)
            _Scaling = value
        End Set
    End Property

    Public Property Copies() As Integer
        Get
            Return _Copies
        End Get
        Set(ByVal value As Integer)
            _Copies = value
        End Set
    End Property

    Public Property Path() As String
        Get
            Return _Path
        End Get
        Set(ByVal value As String)
            _Path = value
        End Set
    End Property

    Public Property Administrations() As String
        Get
            Return _Administrations
        End Get
        Set(ByVal value As String)
            _Administrations = value
        End Set
    End Property

    Public Property SQLServer() As String
        Get
            Return _SQLserver
        End Get
        Set(ByVal value As String)
            _SQLserver = value
        End Set
    End Property

    Public Property ServiceURI() As String
        Get
            Return _SQLserver
        End Get
        Set(ByVal value As String)
            _SQLserver = value
        End Set
    End Property

    Public Property Multipage() As Boolean
        Get
            Return _multipage
        End Get
        Set(ByVal value As Boolean)
            _multipage = value
        End Set
    End Property

    Public Property ExportToExact() As Boolean
        Get
            Return _ExportToExact
        End Get
        Set(ByVal value As Boolean)
            _ExportToExact = value
        End Set
    End Property

    Public Property ScanOutput() As ScanOutput
        Get
            Return _scanOutput
        End Get
        Set(ByVal value As ScanOutput)
            _scanOutput = value
        End Set
    End Property

    Public Property UseADF() As Boolean
        Get
            Return _UseADF
        End Get
        Set(ByVal value As Boolean)
            _UseADF = value
        End Set
    End Property

    Public Property Duplex() As Boolean
        Get
            Return _duplex
        End Get
        Set(ByVal value As Boolean)
            _duplex = value
        End Set
    End Property

    Public Property BitDepth() As Integer
        Get
            Return _BitDepth
        End Get
        Set(ByVal value As Integer)
            _BitDepth = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return vbTab + "Bit Depth: " + vbTab + BitDepth.ToString() + vbCrLf + _
            vbTab + "Brightness: " + vbTab + Brightness.ToString() + vbCrLf + _
            vbTab + "Contrast: " + vbTab + Contrast.ToString() + vbCrLf + _
            vbTab + "Resolution: " + vbTab + Resolution.ToString() + vbCrLf + _
            vbTab + "Intent: " + vbTab + Intent.ToString() + vbCrLf + _
            vbTab + "Quality: " + vbTab + Quality.ToString() + vbCrLf + _
            vbTab + "Scaling: " + vbTab + Scaling.ToString() + vbCrLf + _
            vbTab + "Copies: " + vbTab + Copies.ToString() + vbCrLf + _
            vbTab + "Preview: " + vbTab + Preview.ToString() + vbCrLf + _
            vbTab + "UseADF: " + vbTab + UseADF.ToString() + vbCrLf + _
            vbTab + "Duplex: " + vbTab + Duplex.ToString() + vbCrLf + _
            vbTab + "Multipage: " + vbTab + Multipage.ToString() + vbCrLf + _
            vbTab + "Scan Output: " + vbTab + ScanOutput.ToString() + vbCrLf + _
            vbTab + "Path: " + vbTab + Path.ToString() + vbCrLf + _
            vbTab + "Export to Exact: " + vbTab + ExportToExact.ToString() + vbCrLf + _
            vbTab + "Type of order: " + vbTab + TypeOfOrder.ToString() + vbCrLf + _
            vbTab + "Center: " + vbTab + Center.ToString()
    End Function

End Class


