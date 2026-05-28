Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports DevExpress.Data.Utils
Imports DevExpress.Internal
Imports DevExpress.Xpf.Core.Native
#If NET
Imports Microsoft.EntityFrameworkCore

#End If
Namespace DevExpress.DemoData

    Public Class VehiclesData

        Public Shared Function GetMDBData() As List(Of DevExpress.DemoData.VehiclesData.OrderItem)
#If NET
            Return DevExpress.DemoData.VehiclesData.InitOrdersData(Nothing, 10000, 3 * 365)
#Else
            return InitOrdersData("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataDirectoryHelper.GetFile("Vehicles.mdb", DataDirectoryHelper.DataFolderName), 10000, 3 * 365);
#End If
        End Function

        Public Class Trademark

            Public Property ID As Integer

            Public Property Name As String
        End Class

        Public Class BodyStyle

            Public Property ID As Integer

            Public Property Name As String
        End Class

        Public Class OrderItem

            Friend Model As DevExpress.DemoData.VehiclesData.Model

            Public Sub New(ByVal model As DevExpress.DemoData.VehiclesData.Model, ByVal days As Integer, ByVal rnd As DevExpress.Data.Utils.NonCryptographicRandom, ByVal id As Integer)
                Me.Model = model
                Me.ModelPrice = model.Price
                Me.Trademark = model.Trademark
                Me.Name = model.Name
                Me.Modification = model.Modification
                Me.Category = model.Category
                Me.MPGCity = model.MPGCity
                Me.MPGHighway = model.MPGHighway
                Me.Doors = model.Doors
                Me.BodyStyle = model.BodyStyle.ToString()
                Me.Cylinders = model.Cylinders
                Me.Horsepower = model.Horsepower
                Me.Torque = model.Torque
                Me.TransmissionSpeeds = model.TransmissionSpeeds
                Me.TransmissionType = model.TransmissionType
                Me.InStock = model.InStock
                Me.SalesDate = System.DateTime.Now.AddDays(-rnd.[Next](days))
                Me.SalesDateOnly = Me.SalesDate.ToDateOnly()
                Me.Discount = System.Math.Round(0.05 * rnd.[Next](4), 2)
                Me.OrderID = id
            End Sub

            Public Property OrderID As Integer

            Public Property SalesDate As DateTime

            Public Property SalesDateOnly As DateOnly

            Public Property Discount As Double

            Public Property ModelPrice As Decimal

            Public Property Trademark As String

            Public Property Name As String

            Public Property Modification As String

            Public Property Category As Integer

            Public Property MPGCity As Integer?

            Public Property MPGHighway As Integer?

            Public Property Doors As Integer

            Public Property BodyStyle As String

            Public Property Cylinders As Integer

            Public Property Horsepower As String

            Public Property Torque As String

            Public Property TransmissionSpeeds As Integer

            Public Property TransmissionType As Integer

            Public Property InStock As Boolean
        End Class

        Public Class Model

            Public Property ID As Integer

            Public Property Trademark As String

            Public Property Name As String

            Public Property Modification As String

            Public Property Category As Integer

            Public Property Price As Decimal

            Public Property MPGCity As Integer?

            Public Property MPGHighway As Integer?

            Public Property Doors As Integer

            Public Property BodyStyle As String

            Public Property Cylinders As Integer

            Public Property Horsepower As String

            Public Property Torque As String

            Public Property TransmissionSpeeds As Integer

            Public Property TransmissionType As Integer

            Public Property Description As String

            Public Property DeliveryDate As DateTime

            Public Property InStock As Boolean
        End Class

        Public Shared Function InitOrdersData(ByVal connectionString As String, ByVal itemCount As Integer, ByVal dataInterval As Integer) As List(Of DevExpress.DemoData.VehiclesData.OrderItem)
            Dim rnd As DevExpress.Data.Utils.NonCryptographicRandom = DevExpress.Data.Utils.NonCryptographicRandom.[Default]
#If NET
            Dim listModels As System.Collections.Generic.List(Of DevExpress.DemoData.VehiclesData.Model) = DevExpress.DemoData.VehiclesData.InitDataCore(1)
#Else
            DataSet ds;
            List<Model> listModels = InitMDBDataCore(connectionString, out ds, 1);
#End If
            Dim orders As System.Collections.Generic.List(Of DevExpress.DemoData.VehiclesData.OrderItem) = New System.Collections.Generic.List(Of DevExpress.DemoData.VehiclesData.OrderItem)()
            Dim averagePrice As Decimal = System.Linq.Enumerable.[Select](Of DevExpress.DemoData.VehiclesData.Model, Global.System.[Decimal])(listModels, CType((Function(x) CDec((x.Price))), System.Func(Of DevExpress.DemoData.VehiclesData.Model, System.[Decimal]))).Average()
            Dim averageOrders As Integer = itemCount \ listModels.Count
            For Each model As DevExpress.DemoData.VehiclesData.Model In listModels
                For i As Integer = 0 To averageOrders * averagePrice / model.Price - 1
                    orders.Add(New DevExpress.DemoData.VehiclesData.OrderItem(model, dataInterval, rnd, i + 1))
                Next
            Next

            Return orders
        End Function

#If NET
        Private Shared Function InitDataCore(ByVal dataInterval As Integer) As List(Of DevExpress.DemoData.VehiclesData.Model)
            Dim rnd = DevExpress.Data.Utils.NonCryptographicRandom.[Default]
            Dim listModels = New System.Collections.Generic.List(Of DevExpress.DemoData.VehiclesData.Model)()
            Dim vehiclesContext = New DevExpress.DemoData.Models.Vehicles.VehiclesContext()
            vehiclesContext.Models.Load()
            vehiclesContext.BodyStyles.Load()
            vehiclesContext.Categories.Load()
            vehiclesContext.Trademarks.Load()
            vehiclesContext.TransmissionTypes.Load()
            System.Linq.Enumerable.ToList(Of DevExpress.DemoData.Models.Vehicles.Model)(vehiclesContext.Models.Local).ForEach(Sub(source) listModels.Add(New DevExpress.DemoData.VehiclesData.Model() With {.ID = CInt(source.ID), .Trademark = source.TrademarkName, .Name = source.Name, .Modification = source.Modification, .Category = CInt(source.CategoryID), .Price = source.Price, .MPGCity = source.MPGCity, .MPGHighway = source.MPGHighway, .Doors = source.Doors, .BodyStyle = source.BodyStyleName, .Cylinders = source.Cylinders, .Horsepower = source.Horsepower, .Torque = source.Torque, .TransmissionSpeeds = System.Convert.ToInt32(source.TransmissionSpeeds), .TransmissionType = CInt(source.TransmissionTypeID), .Description = source.Description, .DeliveryDate = System.DateTime.Now.AddDays(rnd.[Next](dataInterval)), .InStock = rnd.[Next](100) < 95}))
            Return listModels
        End Function
#Else
        static List<Model> InitMDBDataCore(string connectionString, out DataSet ds, int dataInterval) {
            string Model = "Model";
            string Trademark = "Trademark";
            string Category = "Category";
            string BodyStyle = "BodyStyle";
            string TransmissionType = "TransmissionType";
            ds = new DataSet();
            FillTable(Model, null, connectionString, ds);
            FillTable(Category, null, connectionString, ds);
            FillTable(Trademark, null, connectionString, ds);
            FillTable(BodyStyle, null, connectionString, ds);
            FillTable(TransmissionType, null, connectionString, ds);

            List<Trademark> listTrademarks = new List<Trademark>();
            foreach(DataRow row in ds.Tables[Trademark].Rows)
                listTrademarks.Add(new Trademark {
                    ID = (int)row["ID"],
                    Name = (string)row["Name"]
                });

            List<BodyStyle> listBodyStyles = new List<BodyStyle>();
            foreach(DataRow row in ds.Tables[BodyStyle].Rows)
                listBodyStyles.Add(new BodyStyle {
                    ID = (int)row["ID"],
                    Name = (string)row["Name"]
                });

            List<Model> listModels = new List<Model>();
            NonCryptographicRandom rnd = NonCryptographicRandom.Default;
            foreach(DataRow row in ds.Tables[Model].Rows)
                listModels.Add(new Model() {
                    ID = (int)row["ID"],
                    Name = (string)row["Name"],
                    Trademark = listTrademarks.First(x => x.ID == (int)row["TrademarkID"]).Name,
                    Modification = (string)row["Modification"],
                    Category = (int)row["CategoryID"],
                    Price = (decimal)row["Price"],
                    MPGCity = DBNull.Value.Equals(row["MPG City"]) ? null : (int?)row["MPG City"],
                    MPGHighway = DBNull.Value.Equals(row["MPG City"]) ? null : (int?)row["MPG Highway"],
                    Doors = (int)row["Doors"],
                    BodyStyle = listBodyStyles.First(x => x.ID == (int)row["BodyStyleID"]).Name,
                    Cylinders = (int)row["Cylinders"],
                    Horsepower = (string)row["Horsepower"],
                    Torque = (string)row["Torque"],
                    TransmissionSpeeds = Convert.ToInt32(row["Transmission Speeds"]),
                    TransmissionType = (int)row["Transmission Type"],
                    Description = string.Format("{0}", row["Description"]),
                    DeliveryDate = DateTime.Now.AddDays(rnd.Next(dataInterval)),
                    InStock = rnd.Next(100) < 95
                });
            return listModels;
        }
        static void FillTable(string table, string query, string connectionString, DataSet ds) {
            if(query == null)
                query = string.Format("SELECT * FROM {0}", table);
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(query, connectionString);
            oleDbDataAdapter.Fill(ds, table);
        }
#End If
    End Class
End Namespace
