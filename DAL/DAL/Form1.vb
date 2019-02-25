Imports System.Threading

Public Delegate Function LoadDataEventHandler()

Public Class Form1

    Private myThread As New System.Threading.Thread(AddressOf DoWork)
    Dim myController As New VendorController

    Dim myhandle As LoadDataEventHandler
    Private myuser As UserController
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
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        myhandle = AddressOf myController.getVendorsCustom
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
            myController.BS = New BindingSource
            Select Case SourceType
                Case SourceTypeEnum.ListOf
                    myController.BS.DataSource = DirectCast(myvendor, List(Of VendorModel))
                Case SourceTypeEnum.DataSet                    
                    myController.BS.DataSource = DirectCast(myvendor, DataSet).Tables("Vendor")
            End Select
            ProgressReport(ProgressReportEnum.SetDataGridView, "Init Data")
        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
            ProgressReport(ProgressReportEnum.SetMessage2, ex.Message)
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
                    ToolStripProgressBar1.Style = ProgressBarStyle.Marquee
                Case ProgressReportEnum.StopProgressBar
                    ProgressReport(ProgressReportEnum.SetMessage1, message)
                    ToolStripProgressBar1.Style = ProgressBarStyle.Continuous
            End Select
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Validate()
        myController.save()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim sqlstr = "update vendor set shortname = :ishortname where vendorcode = :ivendorcode;"
        Dim params(1) As IDbDataParameter
        params(0) = DataAccess.factory.CreateParameter("ishortname", "myshortname")
        params(1) = DataAccess.factory.CreateParameter("ivendorcode", 8026031)
        DataAccess.ExecuteNonQuery(sqlstr, CommandType.Text, params)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim sqlstr = "select shortname from vendor where vendorcode = :ivendorcode;"
        Dim params(0) As IDbDataParameter        
        params(0) = DataAccess.factory.CreateParameter("ivendorcode", 8026031)
        Dim shortname As String = DataAccess.ExecuteScalar(sqlstr, CommandType.Text, params)

    End Sub

   
    Private Sub Button7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Dim myform = New FormRBAC
        myform.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click     
        If User.can("View APO HK") Then
            MessageBox.Show("Can View")
        Else
            MessageBox.Show("Cannot View")
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mydata As DataSet
        Dim userid = Environment.UserDomainName & "\" & Environment.UserName
        mydata = myuser.findByUserName(userid.ToLower)
        If mydata.Tables(0).Rows.Count > 0 Then
            Dim identity = myuser.findIdentity(mydata.Tables(0).Rows(0).Item("id"))
            User.setIdentity(identity)
            User.login(identity)
            User.IdentityClass = myuser
        Else
            MessageBox.Show("User is not registered.")
        End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        myuser = New UserController
    End Sub
End Class
