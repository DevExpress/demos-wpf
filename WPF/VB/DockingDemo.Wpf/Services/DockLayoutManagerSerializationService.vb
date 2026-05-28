Imports System.IO
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core.Serialization

Namespace DockingDemo

    Public Interface IDockLayoutManagerSerializationService

        Sub Serialize(ByVal path As String)

        Sub Serialize(ByVal stream As Stream)

        Sub Deserialize(ByVal path As String)

        Sub Deserialize(ByVal stream As Stream)

    End Interface

    Public Class DockLayoutManagerSerializationService
        Inherits ServiceBase
        Implements IDockLayoutManagerSerializationService

        Private Sub IDockLayoutManagerSerializationService_Deserialize(ByVal path As String) Implements IDockLayoutManagerSerializationService.Deserialize
            Call DXSerializer.Deserialize(AssociatedObject, path, AssociatedObject.GetType().Name, Nothing)
        End Sub

        Private Sub IDockLayoutManagerSerializationService_Deserialize1(ByVal stream As Stream) Implements IDockLayoutManagerSerializationService.Deserialize
            Call DXSerializer.Deserialize(AssociatedObject, stream, AssociatedObject.GetType().Name, Nothing)
        End Sub

        Private Sub IDockLayoutManagerSerializationService_Serialize(ByVal path As String) Implements IDockLayoutManagerSerializationService.Serialize
            Call DXSerializer.Serialize(AssociatedObject, path, AssociatedObject.GetType().Name, Nothing)
        End Sub

        Private Sub IDockLayoutManagerSerializationService_Serialize1(ByVal stream As Stream) Implements IDockLayoutManagerSerializationService.Serialize
            Call DXSerializer.Serialize(AssociatedObject, stream, AssociatedObject.GetType().Name, Nothing)
        End Sub
    End Class
End Namespace
