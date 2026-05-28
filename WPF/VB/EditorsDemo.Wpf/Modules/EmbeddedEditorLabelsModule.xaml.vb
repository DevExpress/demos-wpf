Imports System.Windows
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class EmbeddedEditorLabelsModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If PropertiesGroupBox Is Nothing Then Return
            PropertiesGroupBox.DataContext = GetDateContext(sender)
            If PropertiesGroupBox.DataContext Is Nothing Then Return
            Dim visibility = If(PropertiesGroupBox.DataContext.GetType() Is GetType(ComboBoxEdit), System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible)
            If NullTextTextBlock IsNot Nothing Then NullTextTextBlock.Visibility = visibility
            If NullTextTextBox IsNot Nothing Then NullTextTextBox.Visibility = visibility
        End Sub

        Private Function GetDateContext(ByVal sender As Object) As TextEditBase
            If ReferenceEquals(sender, FirstNameRadioButton) Then Return FirstNameEdit
            If ReferenceEquals(sender, LastNameRadioButton) Then Return LastNameEdit
            If ReferenceEquals(sender, FullNameRadioButton) Then Return FullNameEdit
            If ReferenceEquals(sender, PrefixRadioButton) Then Return PrefixEdit
            If ReferenceEquals(sender, BirthDateRadioButton) Then Return BirthDateEdit
            If ReferenceEquals(sender, MobilePhoneRadioButton) Then Return MobilePhoneEdit
            If ReferenceEquals(sender, TitleRadioButton) Then Return TitleEdit
            If ReferenceEquals(sender, DepartmentRadioButton) Then Return DepartmentEdit
            If ReferenceEquals(sender, HireDateRadioButton) Then Return HireDateEdit
            Return Nothing
        End Function
    End Class
End Namespace
