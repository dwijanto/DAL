Public Class VendorController
    Private myModel As New VendorModel
    
    Public Function getVendors() As List(Of VendorModel)
        Return myModel.GetVendors
    End Function

    Public Function GetDataset() As DataSet
        Return myModel.GetDataSet
    End Function



End Class
