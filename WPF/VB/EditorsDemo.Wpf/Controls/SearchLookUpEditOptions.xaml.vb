Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class SearchLookUpEditOptions
        Inherits UserControl

        Public Property FocusedEditor As BaseEdit
            Get
                Return CType(GetValue(FocusedEditorProperty), BaseEdit)
            End Get

            Set(ByVal value As BaseEdit)
                SetValue(FocusedEditorProperty, value)
            End Set
        End Property

        Public Shared ReadOnly FocusedEditorProperty As DependencyProperty = DependencyProperty.Register("FocusedEditor", GetType(BaseEdit), GetType(SearchLookUpEditOptions), Nothing)

        Public Sub New()
            InitializeComponent()
            LookUpEditBase.SetupComboBoxEnumItemSource(Of FilterCondition, FilterCondition)(filterConditionComboBox)
            LookUpEditBase.SetupComboBoxEnumItemSource(Of FindMode, FindMode)(findModeComboBox)
            'ComboBoxEdit.SetupComboBoxEnumItemSource<EditorPlacement, EditorPlacement>(findComboBox);
            LookUpEditBase.SetupComboBoxEnumItemSource(Of EditorPlacement, EditorPlacement)(addNewComboBox)
        'ComboBoxEdit.SetupComboBoxEnumItemSource<EditorPlacement, EditorPlacement>(nullValueComboBox);
        End Sub
    End Class
End Namespace
