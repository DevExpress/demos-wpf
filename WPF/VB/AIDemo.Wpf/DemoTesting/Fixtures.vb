Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Extensions.AI
Imports DevExpress.AIIntegration
Imports DevExpress.AIIntegration.Wpf
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.Editors

Namespace AIDemo.Tests

    Public Class AICheckAllDemosFixture
        Inherits CheckAllDemosFixture

#If NET ' keep it separate for right VB conversion
        Private skipMemoryLeaksCheckModules As Type() = {GetType(SemanticSearchModule), GetType(AIToolsModule)}

#Else
        Type[] skipMemoryLeaksCheckModules = { typeof(SemanticSearchModule) };
#End If
        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not skipMemoryLeaksCheckModules.Contains(moduleType)
        End Function

#If NET
        Protected Overrides Function CheckMemoryLeaksWhenFinishingTesting() As Boolean
            Return False
        End Function

#End If
        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
            Return moduleType IsNot GetType(RichEditModule)
        End Function
    End Class

    Public Class CheckAIFeaturesFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As AIDemoModulesAccessor

        Public Sub New()
            modulesAccessor = New AIDemoModulesAccessor(Me)
        End Sub

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            CreateTextEditActions()
        End Sub

        Private Sub CreateTextEditActions()
            AddLoadModuleActions(GetType(TextEditModule))
            Dispatch(Async Sub()
                Dim te = modulesAccessor.TextEdit
                te.SelectionStart = 0
                te.SelectionLength = te.Text.Length
                Call DispatcherHelper.UpdateLayoutAndDoEvents()
                Call AIExtensionsContainerDesktop.Default.UnregisterChatClient()
                Call AIExtensionsContainerDesktop.Default.RegisterChatClient(New ChatClient("a"))
                Dim expandBehavior = Interaction.GetBehaviors(te).OfType(Of ExpandBehavior)().[Single]()
                expandBehavior.BehaviorSource.ShowResultDialog = False
                Await expandBehavior.ExpandAsync()
                Assert.AreEqual("a", te.Text)
            End Sub)
        End Sub
    End Class

    Friend Class AIDemoModulesAccessor
        Inherits DemoModulesAccessor(Of DemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub

        Public ReadOnly Property TextEdit As TextEdit
            Get
                Return CType(DemoModule, TextEditModule).mainTextEdit
            End Get
        End Property
    End Class

    Friend Class ChatClient
        Implements IChatClient

        Private ReadOnly chatResponse As ChatResponse

        Public Sub New(ByVal response As String)
            Dim message = New ChatMessage(ChatRole.Assistant, response)
            chatResponse = New ChatResponse(message) With {.FinishReason = ChatFinishReason.Stop}
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Throw New NotImplementedException()
        End Sub

        Public Function GetResponseAsync(ByVal chatMessages As IEnumerable(Of ChatMessage), ByVal Optional options As ChatOptions = Nothing, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of ChatResponse) Implements IChatClient.GetResponseAsync
            Return Task.FromResult(chatResponse)
        End Function

        Public Function GetStreamingResponseAsync(ByVal chatMessages As IEnumerable(Of ChatMessage), ByVal Optional options As ChatOptions = Nothing, ByVal Optional cancellationToken As CancellationToken = Nothing) As IAsyncEnumerable(Of ChatResponseUpdate) Implements IChatClient.GetStreamingResponseAsync
            Throw New NotImplementedException()
        End Function

        Private Function GetService(ByVal serviceType As Type, Optional serviceKey As Object = Nothing) As Object Implements IChatClient.GetService
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
