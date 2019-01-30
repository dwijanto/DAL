Imports Npgsql
Public Class PostgreSQLFactory
    Inherits DbFactory


    Public Overrides ReadOnly Property ConnectionString As String
        Get
            Return String.Format("host=localhost;port=5432;database=LogisticDb20150120;CommandTimeout=10000;TimeOut=1024;Userid=admin;Password=admin;")
        End Get
    End Property

    Public Overloads Overrides Function CreateAdapter() As IDbDataAdapter
        Return New NpgsqlDataAdapter
    End Function

    Public Overrides Function CreateAdapter(commandText As String) As IDbDataAdapter
        Return New NpgsqlDataAdapter(New NpgsqlCommand(commandText))
    End Function

    Public Overrides Function CreateCommand(commandText As String) As IDbCommand
        Return New NpgsqlCommand(commandText)
    End Function

    Public Overrides Function CreateConnection(connectionString As String) As IDbConnection
        Return New NpgsqlConnection(connectionString)
    End Function

    Public Overloads Overrides Function CreateParameter() As IDbDataParameter
        Return New NpgsqlParameter
    End Function

    Public Overloads Overrides Function CreateParameter(name As String, value As Object) As IDbDataParameter
        Return New NpgsqlParameter(name, value)
    End Function

    Public Overloads Overrides Function CreateParameter(name As String, type As DbType, size As Integer) As IDbDataParameter
        Dim parm As NpgsqlParameter = New NpgsqlParameter
        parm.ParameterName = name
        parm.DbType = type
        parm.Size = size
        Return parm
    End Function

    Public Overrides Function GetParameterValue(parameter As Object) As Object
        Debug.Assert(parameter <> Nothing)
        If (parameter Is Nothing) Then Return Nothing
        Return CType(parameter, NpgsqlParameter).Value
    End Function

End Class

