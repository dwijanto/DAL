Imports System.Reflection

Public Class DataAccess
    Public Shared factory As DbFactory = DbFactory.GetInstancePostgreSQLFactory

    Public Shared Function ExecuteNonQuery(ByVal procedureName As String, ByVal cmdType As CommandType, ByVal ParamArray parameters() As IDbDataParameter) As Integer
        Debug.Assert(procedureName <> Nothing)
        Dim myret As Object = Nothing
        Using connection As IDbConnection = factory.CreateConnection()
            Dim DataAdapter As IDbDataAdapter = factory.CreateAdapter
            connection.Open()
            Dim command As IDbCommand = factory.CreateCommand(procedureName, connection)
            command.Connection = connection
            command.CommandType = cmdType          
            If (Not IsNothing(parameters)) Then
                Dim p As IDbDataParameter
                For Each p In parameters
                    command.Parameters.Add(p)
                Next
            End If
            myret = command.ExecuteNonQuery
        End Using
        Return myret
    End Function

    Public Shared Function ExecuteScalar(ByVal procedureName As String, ByVal cmdType As CommandType, ByVal ParamArray parameters() As IDbDataParameter) As Object
        Debug.Assert(procedureName <> Nothing)
        Dim myret As Object = Nothing
        Using connection As IDbConnection = factory.CreateConnection()
            Dim DataAdapter As IDbDataAdapter = factory.CreateAdapter
            connection.Open()
            Dim command As IDbCommand = factory.CreateCommand(procedureName, connection)
            command.Connection = connection
            command.CommandType = cmdType
            If (Not IsNothing(parameters)) Then
                Dim p As IDbDataParameter
                For Each p In parameters
                    command.Parameters.Add(p)
                Next
            End If
            myret = command.ExecuteScalar
        End Using
        Return myret
    End Function

    Public Shared Function GetDataSet(ByVal procedureName As String, ByVal cmdType As CommandType, ByVal DS As DataSet, ByVal handler As GetEventHandler, ByVal ParamArray parameters() As IDbDataParameter) As DataSet
        Dim myret As Boolean = False
        Debug.Assert(handler <> Nothing)

        Using connection As IDbConnection = factory.CreateConnection()
            Dim DataAdapter As IDbDataAdapter = factory.CreateAdapter
            connection.Open()
            Dim command As IDbCommand = factory.CreateCommand(procedureName, connection)
            command.Connection = connection
            command.CommandType = cmdType
            DataAdapter.SelectCommand = command

            If (Not IsNothing(parameters)) Then
                Dim p As IDbDataParameter
                For Each p In parameters
                    command.Parameters.Add(p)
                Next
            End If
            DataAdapter.Fill(DS)
            Return handler(DS)
        End Using
    End Function

    Public Shared Function Read(Of T)(ByVal procedureName As String, ByVal cmdType As CommandType, ByVal handler As ReadEventHandler(Of T), ByVal ParamArray parameters() As IDbDataParameter) As T
        Debug.Assert(handler <> Nothing)
        Using connection As IDbConnection = factory.CreateConnection()
            connection.Open()
            Dim command As IDbCommand = factory.CreateCommand(procedureName, connection)
            command.Connection = connection
            command.CommandType = cmdType

            If (parameters Is Nothing = False) Then
                Dim p As IDbDataParameter
                For Each p In parameters
                    command.Parameters.Add(p)
                Next
            End If

            Dim reader As IDataReader = command.ExecuteReader()
            'Return handler(reader)
            Return handler.Invoke(reader)
        End Using

    End Function

    Public Shared Sub Write(Of T)(ByVal o As T, ByVal procedurename As String, ByVal handler As WriteEventHandler(Of T), ByVal conn As IDbConnection, ByVal params() As IDbDataParameter)
        Using connection As IDbConnection = factory.CreateConnection()
            connection.Open()
            Write(o, procedurename, handler, connection, Nothing, params)
        End Using
    End Sub

    Public Shared Sub Write(Of T)(ByVal o As T, ByVal procedureName As String, ByVal handler As WriteEventHandler(Of T), ByVal connection As IDbConnection, ByVal transaction As IDbTransaction, ByVal params() As IDbDataParameter)

        Dim command As IDbCommand = factory.CreateCommand(procedureName, connection)
        command.CommandType = CommandType.StoredProcedure      
        command.Transaction = transaction

        If (params Is Nothing = False) Then
            Dim p As IDbDataParameter
            For Each p In params
                command.Parameters.Add(p)
            Next

        End If

        If (handler <> Nothing) Then
            handler(o, command)
        End If

    End Sub

    Private Shared Function GetColumnName(ByVal prop As PropertyInfo) As String

        Debug.Assert(prop Is Nothing = False)
        If (prop Is Nothing) Then Return ""

        Dim attributes() As Object = prop.GetCustomAttributes(True)
        Dim attr As Object
        For Each attr In attributes
            'If (TypeOf attr Is MYVB.BusinessObjects.ColumnNameAttribute) Then
            '    Debug.WriteLine("Uses ColumnNameAttribute")
            '    Return CType(attr, MYVB.BusinessObjects.ColumnNameAttribute).ColumnName
            'End If
        Next

        Return prop.Name

    End Function


    ' read the public properties and use these to read field names
    Public Shared Function OnReadAny(Of T As New)(ByVal reader As IDataReader) As T

        Dim genericType As Type = GetType(T)

        Dim properties() As PropertyInfo = genericType.GetProperties(BindingFlags.Instance Or BindingFlags.Public)

        Dim prop As PropertyInfo
        Dim obj As T = New T()

        For Each prop In properties

            Try
                Dim columnName As String = GetColumnName(prop)

                If (reader(columnName).Equals(System.DBNull.Value) = False) Then
                    Dim value As Object = reader(columnName)
                    prop.SetValue(obj, value, Nothing)
                End If

            Catch ex As Exception
                Debug.WriteLine("Couldn't write " + prop.Name)
            End Try

        Next
        Return obj
    End Function

    Public Shared Function OnReadAnyList(Of T As New)(ByVal reader As IDataReader) As List(Of T)
        If (reader Is Nothing) Then Return New List(Of T)()
        Dim list As List(Of T) = New List(Of T)()
        While (reader.Read())
            list.Add(OnReadAny(Of T)(reader))
        End While

        Return list
    End Function

End Class
