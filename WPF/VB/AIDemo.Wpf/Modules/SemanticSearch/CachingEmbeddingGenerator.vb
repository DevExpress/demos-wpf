Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.Json
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Extensions.AI
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace AIDemo.SemanticSearch

    Public Class CachingEmbeddingGenerator
        Implements IEmbeddingGenerator(Of String, Embedding(Of Single))

        Private ReadOnly generator As IEmbeddingGenerator(Of String, Embedding(Of Single))

        Private ReadOnly cache As Dictionary(Of String, Embedding(Of Single))

        Public Sub New(ByVal generator As IEmbeddingGenerator(Of String, Embedding(Of Single)))
            Me.generator = generator
            cache = If(LoadCache(), New Dictionary(Of String, Embedding(Of Single))())
        End Sub

        Public Async Function GenerateAsync(ByVal values As IEnumerable(Of String), ByVal Optional options As EmbeddingGenerationOptions = Nothing, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of GeneratedEmbeddings(Of Embedding(Of Single))) Implements IEmbeddingGenerator(Of String, Embedding(Of Single)).GenerateAsync
            Dim newValues = values.Except(cache.Keys).ToList()
            Dim usageDetails As UsageDetails = Nothing
            If newValues.Count > 0 Then
                Dim newEmbeddings = Await generator.GenerateAsync(newValues, options, cancellationToken).ConfigureAwait(False)
                usageDetails = newEmbeddings.Usage
                For i = 0 To newValues.Count - 1
                    cache(newValues(i)) = newEmbeddings(i)
                Next
            End If

            Dim embeddings = New List(Of Embedding(Of Single))()
            Dim embedding As Embedding(Of Single) = Nothing
            For Each value In values
                embeddings.Add(If(cache.TryGetValue(value, embedding), embedding, Nothing))
            Next

            Return New GeneratedEmbeddings(Of Embedding(Of Single))(embeddings) With {.Usage = usageDetails}
        End Function

        Public Function GetService(ByVal serviceType As Type, ByVal Optional serviceKey As Object = Nothing) As Object Implements IEmbeddingGenerator.GetService
            Return generator.GetService(serviceType, serviceKey)
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            generator.Dispose()
        End Sub

        Private Shared Function LoadCache() As Dictionary(Of String, Embedding(Of Single))
            Dim assembly = GetType(CachingEmbeddingGenerator).Assembly
            Dim stream = assembly.GetManifestResourceStream(DemoHelper.GetPath($"{assembly.GetName().Name}.Data.", assembly) & "MovieVectors.json")
            If stream Is Nothing Then Return Nothing
            Return JsonSerializer.Deserialize(Of Dictionary(Of String, Embedding(Of Single)))(stream)
        End Function
    End Class
End Namespace
