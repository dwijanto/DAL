﻿Imports System.Data.SqlClient

Public Class SqlFactory
    Inherits DbFactory

    Private myadapter As SqlDataAdapter

    Public Overrides ReadOnly Property ConnectionString As String
        Get
            Return String.Format("Data Source=WIN-50GP30FGO75;Initial Catalog=Demodb;User ID=sa;Password=demol23 - replace with your sqlconnectionstring here.")
        End Get
    End Property

    Public Overloads Overrides Function CreateAdapter() As IDbDataAdapter
        myadapter = New SqlDataAdapter
        AddHandler myadapter.RowUpdated, AddressOf onRowUpdate
        Return myadapter
    End Function

    Public Overloads Overrides Function CreateAdapter(commandText As String) As IDbDataAdapter
        Return New SqlDataAdapter(New SqlCommand(commandText))
    End Function

    Public Overrides Function CreateCommand(commandText As String, connection As IDbConnection) As IDbCommand
        Return New SqlCommand(commandText, connection)
    End Function

    Public Overrides Function CreateConnection() As IDbConnection
        Return New SqlConnection(ConnectionString)
    End Function

    Public Overloads Overrides Function CreateParameter() As IDbDataParameter
        Return New SqlParameter
    End Function

    Public Overloads Overrides Function CreateParameter(name As String, value As Object) As IDbDataParameter
        Return New SqlParameter(name, value)
    End Function

    Public Overloads Overrides Function CreateParameter(name As String, type As DbType, size As Integer) As IDbDataParameter
        Dim parm As SqlParameter = New SqlParameter
        parm.ParameterName = name
        parm.DbType = type
        parm.Size = size
        Return parm
    End Function

    Public Overloads Overrides Function CreateParameter(name As String, type As DbType, size As Integer, sourcecolumn As String) As IDbDataParameter
        Dim parm As SqlParameter = New SqlParameter
        parm.ParameterName = name
        parm.DbType = type
        parm.Size = size
        parm.SourceColumn = sourcecolumn
        Return parm
    End Function

    Public Overrides Function GetParameterValue(parameter As Object) As Object
        Debug.Assert(parameter <> Nothing)
        If (parameter Is Nothing) Then Return Nothing
        Return CType(parameter, SqlParameter).Value
    End Function

    Public Overrides Function Update(tablename As Object) As Integer
        Return myadapter.Update(DirectCast(tablename, DataTable))
    End Function

    Private Sub onRowUpdate(sender As Object, e As SqlRowUpdatedEventArgs)
        If e.StatementType = StatementType.Insert Or e.StatementType = StatementType.Update Then
            If e.Status <> UpdateStatus.ErrorsOccurred Then
                e.Status = UpdateStatus.SkipCurrentRow
            End If
        End If
    End Sub

End Class
