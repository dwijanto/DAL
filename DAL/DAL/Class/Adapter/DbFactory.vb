﻿Public MustInherit Class DbFactory
    Public Shared myInstance As DbFactory

    Public Shared Function CreatePostgresqlFactory() As DbFactory
        Return New PostgreSQLFactory
    End Function

    Public Shared Function GetInstancePostgreSQLFactory() As DbFactory
        If IsNothing(myInstance) Then
            myInstance = New PostgreSQLFactory
        End If
        Return myInstance
    End Function

    Public Shared Function GetInstanceSQLFactory() As DbFactory
        If IsNothing(myInstance) Then
            myInstance = New SQLFactory
        End If
        Return myInstance
    End Function
    Public Shared Function CreateSQLFactory() As DbFactory
        Return New PostgreSQLFactory
    End Function

    Public MustOverride Function CreateConnection() As IDbConnection
    Public MustOverride Function CreateCommand(ByVal commandText As String, ByVal connection As IDbConnection) As IDbCommand
    Public MustOverride Function CreateAdapter() As IDbDataAdapter
    Public MustOverride Function CreateAdapter(ByVal commandText As String) As IDbDataAdapter
    Public MustOverride Function CreateParameter() As IDbDataParameter
    Public MustOverride Function CreateParameter(ByVal name As String, ByVal value As Object) As IDbDataParameter
    Public MustOverride Function CreateParameter(ByVal name As String, ByVal type As DbType, ByVal size As Integer) As IDbDataParameter
    Public MustOverride Function CreateParameter(ByVal name As String, ByVal type As DbType, ByVal size As Integer, ByVal sourcecolumn As String) As IDbDataParameter
    Public MustOverride Function GetParameterValue(ByVal parameter As Object) As Object
    Public MustOverride Function Update(ByVal tablename As Object) As Integer
    Public MustOverride ReadOnly Property ConnectionString() As String
End Class

'Public Delegate Sub WriteEventHandler(Of T)(ByVal o As T, ByVal command As IDbCommand)
'Public Delegate Function WriteEventHandler(Of T)(ByVal o As T, ByVal command As IDbCommand) As T
'Public Delegate Function WriteEventHandler(Of T)(ByVal command As IDbCommand) As T
Public Delegate Function ExecuteEventHandler(Of T)(ByVal command As IDbCommand) As T
'Public Delegate Function DataAdapterEventHandler(Of T)(ByVal DataAdapter As IDataAdapter) As T
Public Delegate Function ReadEventHandler(Of T)(ByVal reader As IDataReader) As T
Public Delegate Function GetEventHandler(ByVal DS As Dataset) As Dataset
Public Delegate Sub ProgressReportDelegate(ByVal id As Integer, ByVal message As String)
Public Enum ProgressReportEnum
    SetMessage1 = 1
    SetMessage2 = 2
    SetDataGridView = 4
    StartProgressBar = 5
    StopProgressBar = 6
End Enum

Public Class ContentBaseEventArgs
    Inherits EventArgs
    Public Property dataset As DataSet
    Public Property message As String
    Public Property hasChanges As Boolean
    Public Property ra As Integer
    Public Property continueonerror As Boolean

    Public Sub New(ByVal dataset As DataSet, ByRef haschanges As Boolean, ByRef message As String, ByRef recordaffected As Integer, ByVal continueonerror As Boolean)
        Me.dataset = dataset
        Me.message = message
        Me.ra = ra
        Me.continueonerror = continueonerror
    End Sub
End Class



