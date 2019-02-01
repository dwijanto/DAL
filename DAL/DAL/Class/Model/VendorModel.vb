'Imports Npgsql
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
    'Public Function Save()
    '    Dim dataadapter = DataAccess.factory.CreateAdapter

    '    Return True
    'End Function

    Public Function save(obj As Object, mye As ContentBaseEventArgs) As Boolean

        Dim myret As Boolean = False        
        Dim mytransaction As IDbTransaction
        Dim factory = DataAccess.factory
        Using conn As Object = factory.CreateConnection
            conn.Open()
            mytransaction = conn.BeginTransaction
            'Dim dataadapter = DirectCast(DataAccess.factory.CreateAdapter, NpgsqlDataAdapter)
            Dim dataadapter = factory.CreateAdapter
            Dim sqlstr As String
            sqlstr = "sp_deletevendortx"
            dataadapter.DeleteCommand = factory.CreateCommand(sqlstr, conn)
            'dataadapter.DeleteCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").SourceVersion = DataRowVersion.Original
            dataadapter.DeleteCommand.Parameters.Add(factory.CreateParameter("ivendorcode", DbType.Int64, 0, "vendorcode"))
            dataadapter.DeleteCommand.Parameters(0).SourceVersion = DataRowVersion.Original
            dataadapter.DeleteCommand.CommandType = CommandType.StoredProcedure

            sqlstr = "sp_insertvendortx"
            dataadapter.InsertCommand = factory.CreateCommand(sqlstr, conn)
            dataadapter.InsertCommand.Parameters.Add(factory.CreateParameter("ivendorcode", DbType.Int64, 0, "vendorcode"))
            dataadapter.InsertCommand.Parameters(0).SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add(factory.CreateParameter("ivendorname", DbType.String, 0, "vendorname"))
            dataadapter.InsertCommand.Parameters(1).SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add(factory.CreateParameter("ishortname", DbType.String, 0, "shortname"))
            dataadapter.InsertCommand.Parameters(2).SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters(2).Direction = ParameterDirection.InputOutput

            dataadapter.InsertCommand.CommandType = CommandType.StoredProcedure

            sqlstr = "sp_updatevendor"
            dataadapter.UpdateCommand = factory.CreateCommand(sqlstr, conn)
            dataadapter.UpdateCommand.Parameters.Add(factory.CreateParameter("ivendorcode", DbType.Int64, 0, "vendorcode"))
            dataadapter.UpdateCommand.Parameters(0).SourceVersion = DataRowVersion.Original
            dataadapter.UpdateCommand.Parameters.Add(factory.CreateParameter("vendorcode", DbType.Int64, 0, "vendorcode"))
            dataadapter.UpdateCommand.Parameters(1).SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add(factory.CreateParameter("ivendorname", DbType.String, 0, "vendorname"))
            dataadapter.UpdateCommand.Parameters(2).SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add(factory.CreateParameter("ishortname", DbType.String, 0, "shortname"))
            dataadapter.UpdateCommand.Parameters(3).SourceVersion = DataRowVersion.Current

            dataadapter.UpdateCommand.CommandType = CommandType.StoredProcedure

            dataadapter.InsertCommand.Transaction = mytransaction
            dataadapter.UpdateCommand.Transaction = mytransaction
            dataadapter.DeleteCommand.Transaction = mytransaction
            'dataadapter.Update(mye.dataset)
            'Since IDataAdapter.Update only receive DataSet, we need to modify as below
            mye.ra = factory.Update(mye.dataset.Tables(0))


            mytransaction.Commit()
            myret = True
        End Using
        Return myret
    End Function

End Class

