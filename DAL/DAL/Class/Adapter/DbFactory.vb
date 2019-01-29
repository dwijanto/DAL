Public MustInherit Class DbFactory
    Public Shared myInstance As DbFactory

    Public Shared Function CreatePostgresqlFactory() As DbFactory
        Return New PostgreSQLFactory
    End Function

    Public Shared Function CreateSQLFactory() As DbFactory
        Return New PostgreSQLFactory
    End Function

    Public MustOverride Function CreateConnection(ByVal connectionString As String) As IDbConnection
    Public MustOverride Function CreateCommand(ByVal commandText As String) As IDbCommand
    Public MustOverride Function CreateAdapter(ByVal commandText As String) As IDbDataAdapter
    Public MustOverride Function CreateParameter() As IDbDataParameter
    Public MustOverride Function CreateParameter(ByVal name As String, ByVal value As Object) As IDbDataParameter
    Public MustOverride Function CreateParameter(ByVal name As String, ByVal type As DbType, ByVal size As Integer) As IDbDataParameter
    Public MustOverride Function GetParameterValue(ByVal parameter As Object) As Object
    Public MustOverride ReadOnly Property ConnectionString() As String
End Class
Public Delegate Sub WriteEventHandler(Of T)(ByVal o As T, ByVal command As IDbCommand)
Public Delegate Function ReadEventHandler(Of T)(ByVal o As T) As T




