Imports System.Data.OleDb

Module koneksi
    Public koneksi As OleDbConnection
    Public cmd As OleDbCommand
    Public adaptor As OleDbDataAdapter
    Public baca As OleDbDataReader
    Public data As DataSet
    Public str As String

    Sub conn()
        koneksi = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=idcardtmu.accdb")
        koneksi.Open()
    End Sub
End Module
