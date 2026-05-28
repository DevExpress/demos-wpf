Imports System.Windows.Controls

#If NET
using DevExpress.AIIntegration.Wpf;
using DevExpress.DevAV.AiTools;
using DevExpress.Mvvm.UI.Interactivity;
using System.Linq;
#End If
Namespace DevExpress.DevAV.Views

    Public Partial Class OrderCollectionView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
#If NET
            AttachAiBehavior();
#End If
        End Sub
#If NET
        void AttachAiBehavior() {
            if(gridControl == null) return;

            var behaviors = Interaction.GetBehaviors(gridControl);

            if(!behaviors.OfType<AIToolsBehavior>().Any()) {
                behaviors.Add(new AIToolsBehavior {
                    ToolType = typeof(GridControlAITools),
                    TargetDescription = "Grid control displaying order data",
                    ExtensionsContainer = App.ToolChatContainerDefault,
                });
            }
        }
#End If
    End Class
End Namespace
