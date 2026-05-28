Imports DevExpress.DemoData.Models.Vehicles
#If NET
Imports Microsoft.EntityFrameworkCore
#Else
using System.Data.Entity;
#End If
Imports System.Collections.Generic
Imports VehicleModel = DevExpress.DemoData.Models.Vehicles.Model

Namespace DevExpress.DemoData

    Public Class VehiclesData

        Private context As VehiclesContext = New VehiclesContext()

        Private modelsField As IEnumerable(Of VehicleModel)

        Public ReadOnly Property Models As IEnumerable(Of VehicleModel)
            Get
                If modelsField Is Nothing Then
                    Dim list = New List(Of VehicleModel)()
                    modelsField = list
                    context.Models.Load()
                    context.Trademarks.Load()
                    list.AddRange(context.Models.Local)
                End If

                Return modelsField
            End Get
        End Property
    End Class
End Namespace
