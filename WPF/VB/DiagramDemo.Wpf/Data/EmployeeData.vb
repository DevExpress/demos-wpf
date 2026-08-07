Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Xml.Serialization
#If NET
Imports Microsoft.EntityFrameworkCore

#Else
using System.Data.SQLite;
#End If
Namespace DevExpress.Diagram.Demos

    <System.Xml.Serialization.XmlRootAttribute("Employees")>
    Public Class EmployeesWithPhotoData
        Inherits System.Collections.Generic.List(Of DevExpress.Diagram.Demos.Employee)

    End Class

    Public Class Employee

        Public Property Id As Integer

        Public Property ParentId As Integer

        Public Property FirstName As String

        Public Property MiddleName As String

        Public Property LastName As String

        Public ReadOnly Property FullName As String
            Get
                Return Me.FirstName & " " & Me.LastName
            End Get
        End Property

        Public Property JobTitle As String

        Public Property GroupName As String

        Public Property Phone As String

        Public Property EmailAddress As String

        Public Property AddressLine1 As String

        Public Property City As String

        Public Property StateProvinceName As String

        Public Property PostalCode As String

        Public Property CountryRegionName As String

        Public Property BirthDate As DateTime

        Public Property HireDate As DateTime

        Public Property Gender As String

        Public Property MaritalStatus As String

        Public Property Title As String

        <System.Xml.Serialization.XmlIgnoreAttribute>
        Public Property CroppedImageData As Byte()

        Public Property ImageData As Byte()

        Public Overrides Function ToString() As String
            Return Me.FirstName & " " & Me.LastName
        End Function
    End Class

    Public Module EmployeesData

        Public ReadOnly FilteredEmployees As DevExpress.Diagram.Demos.EmployeesWithPhotoData

        Private ReadOnly employees As System.Lazy(Of System.Collections.Generic.Dictionary(Of Integer, DevExpress.Diagram.Demos.Employee)) = New System.Lazy(Of System.Collections.Generic.Dictionary(Of Integer, DevExpress.Diagram.Demos.Employee))(AddressOf DevExpress.Diagram.Demos.EmployeesData.LoadEmployees)

        Sub New()
            DevExpress.Diagram.Demos.EmployeesData.FilteredEmployees = New DevExpress.Diagram.Demos.EmployeesWithPhotoData()
            For Each id In DevExpress.Diagram.Demos.EmployeesData.filteredIds
                Dim employee As DevExpress.Diagram.Demos.Employee
                If Not DevExpress.Diagram.Demos.EmployeesData.employees.Value.TryGetValue(id, employee) Then Continue For
                employee.ParentId = DevExpress.Diagram.Demos.EmployeesData.GetParentId(id)
                employee.CroppedImageData = DevExpress.Diagram.Demos.EmployeesData.CropImage(employee.ImageData)
                Call DevExpress.Diagram.Demos.EmployeesData.FilteredEmployees.Add(employee)
            Next
        End Sub

        Public Function GetOrgChartEmployees() As IEnumerable(Of Object)
            Dim allEmployees = New DevExpress.Diagram.Demos.EmployeesWithPhotoData()
            For Each employee In DevExpress.Diagram.Demos.EmployeesData.employees.Value.Values.OrderBy(Function(x) x.Id)
                employee.ParentId = DevExpress.Diagram.Demos.EmployeesData.GetParentId(employee.Id)
                allEmployees.Add(employee)
            Next

            Return allEmployees
        End Function

        Private Function LoadEmployees() As Dictionary(Of Integer, DevExpress.Diagram.Demos.Employee)
            Dim result = New System.Collections.Generic.Dictionary(Of Integer, DevExpress.Diagram.Demos.Employee)()
            Dim path As String = DevExpress.Internal.DataDirectoryHelper.GetFile("devav.sqlite3", DevExpress.Internal.DataDirectoryHelper.DataFolderName)
            Call DevExpress.Diagram.Demos.EmployeesData.ClearReadOnly(path)
#If NET
            Using devAvDb = New DevExpress.DevAV.DevAVDb($"Data Source={path}")
                Dim sources = devAvDb.Employees.Include(Function(e) e.Picture).ToList()
                For Each source In sources
                    If source.Picture Is Nothing OrElse source.Picture.Data Is Nothing Then Continue For
                    result(CInt(source.Id)) = New DevExpress.Diagram.Demos.Employee With {.Id = CInt(source.Id), .FirstName = source.FirstName, .LastName = source.LastName, .JobTitle = source.Title, .GroupName = DevExpress.Diagram.Demos.EmployeesData.GetDepartmentName(source.Department), .Phone = If(String.IsNullOrEmpty(source.MobilePhone), source.HomePhone, source.MobilePhone), .EmailAddress = source.Email, .AddressLine1 = If(source.Address IsNot Nothing, source.Address.Line, Nothing), .BirthDate = If(source.BirthDate, DirectCast(Nothing, System.DateTime)), .HireDate = If(source.HireDate, DirectCast(Nothing, System.DateTime)), .ImageData = source.Picture.Data}
                Next
            End Using

#Else
            using(var connection = new SQLiteConnection(new SQLiteConnectionStringBuilder { DataSource = path }.ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText =
                        "SELECT e.Id, e.FirstName, e.LastName, e.Department, e.Title, " +
                        "e.MobilePhone, e.HomePhone, e.Email, e.Address_Line, e.BirthDate, e.HireDate, p.Data " +
                        "FROM Employees e LEFT JOIN Pictures p ON p.Id = e.PictureId";
                    using(var reader = command.ExecuteReader()) {
                        while(reader.Read()) {
                            if(reader.IsDBNull(11))
                                continue;
                            var employee = new Employee {
                                Id = (int)reader.GetInt64(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                GroupName = GetDepartmentName((DevExpress.DevAV.EmployeeDepartment)reader.GetInt64(3)),
                                JobTitle = reader.GetString(4),
                                Phone = !reader.IsDBNull(5) ? reader.GetString(5) : (!reader.IsDBNull(6) ? reader.GetString(6) : null),
                                EmailAddress = !reader.IsDBNull(7) ? reader.GetString(7) : null,
                                AddressLine1 = !reader.IsDBNull(8) ? reader.GetString(8) : null,
                                BirthDate = !reader.IsDBNull(9) ? reader.GetDateTime(9) : default(DateTime),
                                HireDate = !reader.IsDBNull(10) ? reader.GetDateTime(10) : default(DateTime),
                                ImageData = (byte[])reader[11]
                            };
                            result[employee.Id] = employee;
                        }
                    }
                }
            }
#End If
            Return result
        End Function

        Private Sub ClearReadOnly(ByVal path As String)
            Try
                Dim attributes = System.IO.File.GetAttributes(path)
                If attributes.HasFlag(System.IO.FileAttributes.[ReadOnly]) Then Call System.IO.File.SetAttributes(path, attributes And Not System.IO.FileAttributes.[ReadOnly])
            Catch
            End Try
        End Sub

        Private Function GetDepartmentName(ByVal department As DevExpress.DevAV.EmployeeDepartment) As String
            Select Case department
                Case DevExpress.DevAV.EmployeeDepartment.Sales
                    Return "Sales"
                Case DevExpress.DevAV.EmployeeDepartment.Support
                    Return "Support"
                Case DevExpress.DevAV.EmployeeDepartment.Shipping
                    Return "Shipping"
                Case DevExpress.DevAV.EmployeeDepartment.Engineering
                    Return "Engineering"
                Case DevExpress.DevAV.EmployeeDepartment.HumanResources
                    Return "Human Resources"
                Case DevExpress.DevAV.EmployeeDepartment.Management
                    Return "Management"
                Case DevExpress.DevAV.EmployeeDepartment.IT
                    Return "IT"
                Case Else
                    Return String.Empty
            End Select
        End Function

        Private Function GetParentId(ByVal id As Integer) As Integer
            For Each pair In DevExpress.Diagram.Demos.EmployeesData.idMap
                If pair.Value.Contains(id) Then Return pair.Key
            Next

            Return 0
        End Function

        Private Function CropImage(ByVal imageBytes As Byte()) As Byte()
            Using ms = New System.IO.MemoryStream(imageBytes)
                Using original = System.Drawing.Image.FromStream(ms)
                    Dim skipYOffset As Integer = 50
                    Dim cropHeight As Integer = 220
                    If skipYOffset + cropHeight > original.Height Then cropHeight = original.Height - skipYOffset
                    Dim cropArea = New System.Drawing.Rectangle(0, skipYOffset, original.Width, cropHeight)
                    Using bmp = New System.Drawing.Bitmap(cropArea.Width, cropArea.Height)
                        Using g = System.Drawing.Graphics.FromImage(bmp)
                            g.DrawImage(original, New System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), cropArea, System.Drawing.GraphicsUnit.Pixel)
                        End Using

                        Using output = New System.IO.MemoryStream()
                            bmp.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg)
                            Return output.ToArray()
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Private ReadOnly filteredIds As Integer() = {1, 2, 3, 4, 5, 6, 30, 8, 9, 10, 32}

#Region "id map"
        Private ReadOnly idMap As System.Collections.Generic.Dictionary(Of Integer, Integer()) = New System.Collections.Generic.Dictionary(Of Integer, Integer())() From {{1, {2, 3, 4}}, {2, {5}}, {3, {32}}, {4, {8}}, {5, {11, 26, 27, 7, 30, 9, 10}}, {6, {21, 22, 23, 24, 25, 28}}, {8, {12, 20, 40, 41, 42, 43, 44, 45, 47}}, {9, {17, 18, 19, 31, 39, 46}}, {10, {13, 14, 15, 16, 49, 50}}, {32, {33, 34, 36, 37, 48, 51, 6}}, {28, {29}}, {33, {38}}}
#End Region
    End Module
End Namespace
