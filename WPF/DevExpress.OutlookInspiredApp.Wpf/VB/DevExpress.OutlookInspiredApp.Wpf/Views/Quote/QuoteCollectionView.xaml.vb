Imports System.Windows.Controls
Imports DevExpress.Xpf.PivotGrid

#If NET
using DevExpress.AIIntegration.Wpf;
using DevExpress.DevAV.AiTools;
using DevExpress.Mvvm.UI.Interactivity;
using System.Linq;
#End If
Namespace DevExpress.DevAV.Views

    Public Partial Class QuoteCollectionView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
#If NET
            AttachAIBehavior();
#End If
        End Sub

#If NET
        void AttachAIBehavior() {
            if(pivotGrid == null) return;

            var behaviors = Interaction.GetBehaviors(pivotGrid);

            if(!behaviors.OfType<AIToolsBehavior>().Any()) {
                behaviors.Add(new AIToolsBehavior {
                    ToolType = typeof(GridControlAITools),
                    TargetDescription = "Grid control displaying quotes data",
                    ExtensionsContainer = App.ToolChatContainerDefault,
                });
            }
        }
#End If
        Private Sub CustomSummary(ByVal sender As Object, ByVal e As PivotCustomSummaryEventArgs)
            Select Case e.DataField.FieldName
                Case "Percentage"
                    Dim quoteSummary As Decimal = 0
                    Dim opportunitySummary As Decimal = 0
                    For Each row As PivotDrillDownDataRow In e.CreateDrillDownDataSource()
                        quoteSummary += CDec(row("Total"))
                        opportunitySummary += CDec(row("MoneyOpportunity"))
                    Next

                    If quoteSummary <> 0 Then e.CustomValue = 100D * opportunitySummary / quoteSummary
            End Select
        End Sub
    End Class
End Namespace
