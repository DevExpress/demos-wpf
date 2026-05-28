Imports System.Collections.Generic
Imports System.IO
Imports DevExpress.Utils

Namespace SankeyDemo

    Public Class ProductTransfer

        Public Property TypeFrom As String

        Public Property TypeTo As String

        Public Property From As String

        Public Property [To] As String

        Public Property TotalPrice As Double
    End Class

    Public Class ProductTransfers

        Public Shared Function GetData() As List(Of ProductTransfer)
            Dim path As String = GetRelativePath("ProductTransfers.xml")
            Using reader As Stream = New FileStream(path, FileMode.Open)
                Dim data = SafeXml.Deserialize(Of List(Of ProductTransfer))(reader)
                Return data
            End Using
        End Function
    End Class
End Namespace
