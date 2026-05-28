Imports System
Imports System.Linq
Imports System.Windows
Imports AIDemo.SemanticSearch
Imports DevExpress.AIIntegration
Imports DevExpress.Xpf.Core
Imports Microsoft.Extensions.VectorData

Namespace AIDemo

    Public Partial Class SemanticSearchModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            Call Load()
            gridControl.ItemsSource = Movies
            InitializeVectorStore()
        End Sub

        Private Async Sub InitializeVectorStore()
            Dim sm = SplashScreenManager.CreateWaitIndicator()
            Try
                sm.Show(Me)
                Dim collection = AIExtensionsContainerDesktop.Default.GetVectorCollection(Of String, VectorStoreRecord)("Movies")
                If Await collection.CollectionExistsAsync() Then Return
                Dim records = Movies.[Select](Function(m, index) New VectorStoreRecord With {.Key = m.Id.ToString(), .Vector = $"""{m.Title}"" is a {m.Genre.ToLower()} film directed by {m.Directors}. {m.Plot}"})
                Await collection.EnsureCollectionExistsAsync()
                Await collection.UpsertAsync(records)
            Catch ex As Exception
                DXMessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Finally
                sm.Close()
            End Try
        End Sub

        Public Class VectorStoreRecord

            <VectorStoreKey>
            Public Property Key As String

            <VectorStoreVector(1536, DistanceFunction:=DistanceFunction.CosineDistance)>
            Public Property Vector As String
        End Class
    End Class
End Namespace
