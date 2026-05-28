Imports System.Windows.Controls

Namespace EditorsDemo

    ''' <summary>
    ''' Interaction logic for ImageEdit.xaml
    ''' </summary>
    Public Partial Class ImageEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            DataContext = New EmployeeViewModel()
            InitializeComponent()
        End Sub
    End Class
End Namespace
