Imports System
Imports System.Linq
Imports System.Data
Imports System.Collections.Generic
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace AIDemo.SemanticSearch

    Public Class Movie

        Public Property Id As Guid

        Public Property Title As String

        Public Property Plot As String

        Public Property Genre As String

        Public Property Photo As Byte()

        Public Property Release As Date

        Public Property Directors As String

        Public Overrides Function ToString() As String
            Return Title
        End Function
    End Class

    Public Module SemanticSearchDataHelper

        Private _Movies As IList(Of AIDemo.SemanticSearch.Movie)

        <Flags>
        Friend Enum MovieGenre
            None = 0
            Action = &H1
            Adventure = &H2
            Animation = &H4
            Biography = &H8
            Comedy = &H10
            Crime = &H20
            Documentary = &H40
            Drama = &H80
            Family = &H100
            Fantasy = &H200
            History = &H400
            Horror = &H800
            Music = &H1000
            Musical = &H2000
            Mystery = &H4000
            Romance = &H8000
            SciFi = &H10000
            Sport = &H20000
            Thriller = &H40000
            War = &H80000
            Western = &H100000
        End Enum

        Public Property Movies As IList(Of Movie)
            Get
                Return _Movies
            End Get

            Private Set(ByVal value As IList(Of Movie))
                _Movies = value
            End Set
        End Property

        Public Sub Load()
            If Movies IsNot Nothing Then Return
            Dim assembly = GetType(SemanticSearchDataHelper).Assembly
            Dim stream = assembly.GetManifestResourceStream(DemoHelper.GetPath($"{assembly.GetName().Name}.Data.", assembly) & "VideoRent.xml")
            Dim dataSet = New DataSet()
            dataSet.ReadXml(stream)
            Dim directorLine = dataSet.Tables("MovieArtistLine").Rows.Cast(Of DataRow)().FirstOrDefault(Function(r) Equals(CStr(r("Name")), "Director"))
            Dim directorLineId As Guid? = Nothing
            If directorLine IsNot Nothing Then directorLineId = CType(directorLine("Oid"), Guid?)
            If directorLineId Is Nothing Then Return
            Dim directors = From movieArtistRow In dataSet.Tables("MovieArtist").Rows.Cast(Of DataRow)() Where CType(movieArtistRow("Line"), Guid) = directorLineId From personRow In dataSet.Tables("Person").Rows.Cast(Of DataRow)() Where CType(personRow("Oid"), Guid) = CType(movieArtistRow("Artist"), Guid) Select New With {.MovieId = CType(movieArtistRow("Movie"), Guid), .Name = String.Join(" ", CStr(personRow("FirstName")), CStr(personRow("LastName")))}
            Movies = dataSet.Tables("Movie").Rows.Cast(Of DataRow)().[Select](Function(row)
                Dim movieId = CType(row("Oid"), Guid)
                Return New Movie With {.Id = movieId, .Title = TryCast(row("Title"), String), .Release = CDate(row("ReleaseDate")), .Genre = CType(row("Genre"), MovieGenre).ToString("G"), .Plot = TryCast(row("Plot"), String), .Photo = TryCast(row("Photo"), Byte()), .Directors = String.Join(", ", directors.Where(Function(d) d.MovieId = movieId).[Select](Function(d) d.Name))}
            End Function).ToList()
        End Sub
    End Module
End Namespace
