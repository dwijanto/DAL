Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim myvendor = VendorModel.GetVendors()
        Dim myvendorbs As New BindingSource
        myvendorbs.DataSource = myvendor
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.DataSource = myvendorbs

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim myvendor As DataSet = VendorModel.GetDataSet()
        Dim myvendorbs As New BindingSource
        myvendorbs.DataSource = myvendor.Tables("Vendor")
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.DataSource = myvendorbs
    End Sub
End Class
