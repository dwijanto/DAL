<Serializable()> _
Public Class VendorModel
    Private DS As DataSet
    Public Property VendorCode As String
    Public Property VendorName As String
    Public Property Shortname As String
    Private TableName As String = "Vendor"

    Public Sub New()

    End Sub


    Public Sub New(ByVal _vendorcode As String, ByVal _vendorname As String, ByVal _shortname As String)
        Me.VendorCode = _vendorcode
        Me.VendorName = _vendorname
        Me.Shortname = _shortname
    End Sub

    Public Function GetVendors() As List(Of VendorModel)
        Return DataAccess.Read(Of List(Of VendorModel))("select vendorcode::text,vendorname::text,shortname::text from vendor;", CommandType.Text, AddressOf DataAccess.OnReadAnyList(Of VendorModel), Nothing)
    End Function

    Public Function GetDataSet() As DataSet
        Dim DS As New DataSet
        Try
            DS = DataAccess.GetDataSet("select vendorcode::text,vendorname::text,shortname::text from vendor;", CommandType.Text, DS, AddressOf onGetDataSet, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return DS
    End Function

    Public Function GetDataSet(Optional ByVal Criteria As String = "") As DataSet
        Dim DS As New DataSet
        Try
            Dim sqlstr As String = String.Format("select vendorcode::text,vendorname::text,shortname::text from vendor {0};", Criteria)
            DS = DataAccess.GetDataSet(sqlstr, CommandType.Text, DS, AddressOf onGetDataSet, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return DS
    End Function

    'This function for adding more function, such as Primary Key, Set Table Name etc, related to this model
    Private Function onGetDataSet(ByVal DS As DataSet) As DataSet
        DS.Tables(0).TableName = TableName
        Return DS
    End Function

    'This function is not ready yet. 
    Public Function Save()
        Dim dataadapter = DataAccess.factory.CreateAdapter

        Return True
    End Function

   

End Class

