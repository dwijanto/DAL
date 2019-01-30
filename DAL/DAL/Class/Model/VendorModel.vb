Public Class VendorModel
    Private DS As DataSet
    Public Shared Function GetVendors()        
        Return DataAccess.Read(Of List(Of Vendor))("select vendorcode::text,vendorname::text,shortname::text from vendor;", CommandType.Text, AddressOf DataAccess.OnReadAnyList(Of Vendor), Nothing)
    End Function

    Public Shared Function GetDataSet()
        Dim DS As New DataSet
        Try
            DS = DataAccess.GetDataSet("select vendorcode::text,vendorname::text,shortname::text from vendor;", CommandType.Text, DS, AddressOf onGetDataSet, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return DS
    End Function

    Private Shared Function onGetDataSet(ByVal DS As DataSet) As DataSet
        DS.Tables(0).TableName = "Vendor"
        Return DS
    End Function

End Class

<Serializable()> _
Public Class Vendor
    Public Property VendorCode As String
    Public Property VendorName As String
    Public Property Shortname As String
    Public Sub New()

    End Sub
    Public Sub New(ByVal _vendorcode As String, ByVal _vendorname As String, ByVal _shortname As String)
        Me.VendorCode = _vendorcode
        Me.VendorName = _vendorname
        Me.Shortname = _shortname

    End Sub
End Class


<Serializable()> _
Public Class Customer

    Private FCustomerID As String
    Public Property CustomerID() As String
        Get
            Return FCustomerID
        End Get
        Set(ByVal value As String)
            FCustomerID = value
        End Set
    End Property

    Private FCompanyName As String
    Public Property CompanyName() As String
        Get
            Return FCompanyName
        End Get
        Set(ByVal value As String)
            FCompanyName = value
        End Set
    End Property

    Private FContactName As String
    Public Property ContactName() As String
        Get
            Return FContactName
        End Get
        Set(ByVal value As String)
            FContactName = value
        End Set
    End Property


    Private FContactTitle As String
    Public Property ContactTitle() As String
        Get
            Return FContactTitle
        End Get
        Set(ByVal value As String)
            FContactTitle = value
        End Set
    End Property

    Private FAddress As String
    Public Property Address() As String
        Get
            Return FAddress
        End Get
        Set(ByVal value As String)
            FAddress = value
        End Set
    End Property

    Private FCity As String
    Public Property City() As String
        Get
            Return FCity
        End Get
        Set(ByVal value As String)
            FCity = value
        End Set
    End Property

    Private FRegion As String

    Public Property Region() As String
        Get
            Return FRegion
        End Get
        Set(ByVal value As String)
            FRegion = value
        End Set
    End Property


    Private FPostalCode As String
    Public Property PostalCode() As String
        Get
            Return FPostalCode
        End Get
        Set(ByVal value As String)
            FPostalCode = value
        End Set
    End Property

    Private FCountry As String
    Public Property Country() As String
        Get
            Return FCountry
        End Get
        Set(ByVal value As String)
            FCountry = value
        End Set
    End Property

    Private FPhone As String
    Public Property Phone() As String
        Get
            Return FPhone
        End Get
        Set(ByVal value As String)
            FPhone = value
        End Set
    End Property

    Private FFax As String
    Public Property Fax() As String
        Get
            Return FFax
        End Get
        Set(ByVal value As String)
            FFax = value
        End Set
    End Property

    Public Sub New()

    End Sub


    Public Sub New(ByVal customerID As String, ByVal companyName _
       As String, _
    ByVal contactName As String, ByVal contactTitle As String, _
       ByVal address As String, _
    ByVal city As String, ByVal region As String, _
       ByVal postalCode As String, _
    ByVal country As String, ByVal phone As String, ByVal fax As String)

        Me.FCustomerID = customerID
        Me.FCompanyName = companyName
        Me.FContactName = contactName
        Me.FContactTitle = contactTitle
        Me.FAddress = address
        Me.FCity = city
        Me.FRegion = region
        Me.FPostalCode = postalCode
        Me.FCountry = country
        Me.FPhone = phone
    End Sub

End Class
