Imports System.Collections.Generic

Namespace TreeListDemo

    Public Partial Class BandedLayout
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.InitRibbon(treeList)
        End Sub
    End Class

    Public Class BandedViewViewModel

        Public ReadOnly Property SpaceObjects As IList(Of SpaceObjects)
            Get
                Return SpaceObjectData.DataSource
            End Get
        End Property
    End Class
End Namespace
