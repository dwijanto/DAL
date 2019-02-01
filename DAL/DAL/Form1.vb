Imports System.Threading

Public Delegate Function LoadDataEventHandler()

Public Class Form1

    Private myThread As New System.Threading.Thread(AddressOf DoWork)
    Dim myController As New VendorController

    Dim myhandle As LoadDataEventHandler

    Private Enum SourceTypeEnum
        ListOf = 1
        DataSet = 2
    End Enum

    Private SourceType As SourceTypeEnum

    Private Sub LoadData()
        If Not myThread.IsAlive Then            
            myThread = New System.Threading.Thread(AddressOf DoWork)
            myThread.SetApartmentState(ApartmentState.MTA)
            myThread.Start()
        Else
            MessageBox.Show("Please wait until the current process is finished.")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        myhandle = AddressOf myController.getVendors
        SourceType = SourceTypeEnum.ListOf
        LoadData()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        myhandle = AddressOf myController.GetDataset
        SourceType = SourceTypeEnum.DataSet
        LoadData()
    End Sub

    Sub DoWork()
        Try
            ProgressReport(ProgressReportEnum.StartProgressBar, "Loading Data...")
            Dim myvendor = myhandle()
            'myvendorbs = New BindingSource
            myController.BS = New BindingSource
            Select Case SourceType
                Case SourceTypeEnum.ListOf
                    myController.BS.DataSource = DirectCast(myvendor, List(Of VendorModel))
                Case SourceTypeEnum.DataSet
                    'myvendorbs.DataSource = DirectCast(myvendor, DataSet).Tables("Vendor")
                    myController.BS.DataSource = DirectCast(myvendor, DataSet).Tables("Vendor")
            End Select
            ProgressReport(ProgressReportEnum.SetDataGridView, "Init Data")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            ProgressReport(ProgressReportEnum.StopProgressBar, "Done.")
        End Try
    End Sub

    Public Sub ProgressReport(ByVal id As Integer, ByVal message As String)
        If Me.InvokeRequired Then
            Dim d As New ProgressReportDelegate(AddressOf ProgressReport)
            Me.Invoke(d, New Object() {id, message})
        Else
            Select Case id
                Case ProgressReportEnum.SetMessage1
                    ToolStripStatusLabel1.Text = message
                Case ProgressReportEnum.SetMessage2
                    ToolStripStatusLabel2.Text = message
                Case ProgressReportEnum.SetDataGridView
                    ProgressReport(ProgressReportEnum.SetMessage1, message)
                    DataGridView1.AutoGenerateColumns = False
                    DataGridView1.DataSource = myController.BS
                Case ProgressReportEnum.StartProgressBar
                    ProgressReport(ProgressReportEnum.SetMessage1, message)
                    ToolStripProgressBar1.Style = ProgressBarStyle.Continuous
                Case ProgressReportEnum.StopProgressBar
                    ProgressReport(ProgressReportEnum.SetMessage1, message)
                    ToolStripProgressBar1.Style = ProgressBarStyle.Marquee
            End Select
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Validate()
        myController.save()
    End Sub
End Class
