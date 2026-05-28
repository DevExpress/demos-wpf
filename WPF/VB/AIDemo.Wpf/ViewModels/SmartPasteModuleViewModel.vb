Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Data.Utils
Imports DevExpress.Export
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace AIDemo.ViewModels

    <POCOViewModel>
    Public Class SmartPasteModuleViewModel

        Public ReadOnly Property PasteModes As ObservableCollection(Of PasteMode) = New ObservableCollection(Of PasteMode)() From {PasteMode.Append, PasteMode.Update}

        Public ReadOnly Property Items As ObservableCollection(Of Record) = New ObservableCollection(Of Record)(DataHelper.GetData(10))

        Public ReadOnly Property Companies As ObservableCollection(Of String) = New ObservableCollection(Of String)(DataHelper.companies.ToList())

        Public Property GridCopyText As String = "Transactions: " & Microsoft.VisualBasic.vbLf & "- Hanari Carnes - Invoice amount due: $123.00, due by 10/30/2024. Status: Processed ✅ " & Microsoft.VisualBasic.vbLf & "- Que Delícia - Amount: $231.00 | Due date: 10/29/2024 | Not processed yet " & Microsoft.VisualBasic.vbLf & "- Romero y tomillo - $369.00 required by 10/28/2024, Processed ✅ " & Microsoft.VisualBasic.vbLf & "- Mère Paillard - Payment: $158.00, due: 10/31/2024, unprocessed " & Microsoft.VisualBasic.vbLf & "- Eastern Delights has an outstanding balance of $97.00, which was due on 10/27/2024. Status: Processed ✅ " & Microsoft.VisualBasic.vbLf

        Public Property LayoutCopyText As String = "Payment: Amount - $123.00 " & Microsoft.VisualBasic.vbLf & "Statement Date: 10/15/2024 " & Microsoft.VisualBasic.vbLf & "Account Number: 133244556 " & Microsoft.VisualBasic.vbLf & "Name: John Smith " & Microsoft.VisualBasic.vbLf & "Contact: (123) 456-7890 " & Microsoft.VisualBasic.vbLf & "Email: john@myemail.com " & Microsoft.VisualBasic.vbLf & "Address: " & Microsoft.VisualBasic.vbLf & "- 123 Elm St Apt 4B " & Microsoft.VisualBasic.vbLf & "- New York, NY 10001 " & Microsoft.VisualBasic.vbLf

        Public ReadOnly Property CopyTextCommand As DelegateCommand

        Public Sub New()
            CopyTextCommand = New DelegateCommand(Sub() Call SafeClipboardWpf.Instance.SetText(LayoutCopyText), Function() LayoutCopyText.Length > 0)
        End Sub
    End Class

    Public Class Record
        Inherits BindableBase

        Public Property ID As Integer

        Public Property CompanyName As String

        Public Property Amount As Decimal?

        Public Property DueDate As Date

        Public Property Processed As Boolean

        Public Sub New()
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("ID = {0}, Text = {1}", ID, CompanyName)
        End Function
    End Class

    Public Class DataHelper

        Public Shared companies As String() = New String() {"Hanari Carnes", "Que Delícia", "Romero y tomillo", "Mère Paillarde", "Comércio Mineiro", "Reggiani Caseifici", "Maison Dewey"}

        Public Shared items As String() = New String() {"Office Chairs", "Laptops", "Printer Ink", "Tables", "Paper"}

        Public Shared Function GetData(ByVal count As Integer) As BindingList(Of Record)
            Dim records As BindingList(Of Record) = New BindingList(Of Record)()
            Dim rnd = NonCryptographicRandom.Default
            For i As Integer = 0 To count - 1
                Dim n As Integer = rnd.Next(10)
                Dim record = New Record() With {.ID = i + 100, .CompanyName = companies(i Mod companies.Length), .DueDate = Date.Today.AddDays(n - 5), .Amount = If(i Mod 2 = 0, (i + 1) * 123, i * 231), .Processed = i Mod 2 = 0}
                records.Add(record)
            Next

            Return records
        End Function
    End Class
End Namespace
