Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports DevExpress.AIIntegration
Imports DevExpress.AIIntegration.Wpf
Imports DevExpress.AIIntegration.Wpf.Internal
Imports DevExpress.Xpf.Editors

Namespace AIDemo.Modules.CodeExamples

    Friend Class AuthoredStyleBehavior
        Inherits DevExpress.AIIntegration.Wpf.Internal.TextModificationBehavior

        Shared Sub New()
            Call RegisterBehaviorSource(Of DevExpress.Xpf.Editors.TextEdit, AIDemo.Modules.CodeExamples.AuthoredStyleBehavior)(Function(te) New DevExpress.AIIntegration.Wpf.Internal.TextEditBehaviorSource(te))
            Call DevExpress.AIIntegration.AIExtensionsContainerDesktop.[Default].Register(Of AIDemo.Modules.CodeExamples.AuthoredStyleRequest, AIDemo.Modules.CodeExamples.AuthoredStyleExtension)()
        End Sub

        Public Sub New()
            Me.AuthoredStyleCommand = CreateCommand(Of String)(New Global.System.Func(Of System.String, Global.System.Threading.Tasks.Task)(AddressOf Me.AuthoredStyleAsync))
        End Sub

        Private Property AuthoredStyleCommand As ICommand

        Private Function AuthoredStyleAsync(ByVal author As String) As Task
            Return ExecuteAsync(Function(input) New AIDemo.Modules.CodeExamples.AuthoredStyleRequest(author, input), Function(container, request, token)
                Dim extension = CType(container.GetExtension(GetType(AIDemo.Modules.CodeExamples.AuthoredStyleRequest)), AIDemo.Modules.CodeExamples.AuthoredStyleExtension)
                Dim result = extension.ExecuteAsync(request, token)
                Return result
            End Function)
        End Function

        Protected Overrides Function BuildItem() As ItemInfo
            Return New DevExpress.AIIntegration.Wpf.ItemInfo With {.Caption = "Authored Style", .Items = {"Mark Twain", "Ernest Hemingway", "Maya Angelou"}.[Select](Function(author) New DevExpress.AIIntegration.Wpf.ItemInfo With {.Caption = author, .Command = Me.AuthoredStyleCommand, .CommandParameter = author}).ToList()}
        End Function
    End Class
End Namespace
