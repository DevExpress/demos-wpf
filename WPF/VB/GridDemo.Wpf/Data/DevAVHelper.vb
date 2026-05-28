Imports System.Collections.Generic
#If NET
Imports Microsoft.EntityFrameworkCore
Imports System.IO
Imports DevExpress.Internal
#Else
using System.Data.Entity;
#End If
Imports System.Linq

Namespace GridDemo

    Public Module DevAVHelper

        Private employeesField As List(Of DevExpress.DevAV.Employee) = Nothing

        Public ReadOnly Property Employees As List(Of DevExpress.DevAV.Employee)
            Get
                If employeesField Is Nothing Then
#If NET
                    Call SetFilePath()
                    Dim devAvDb = New DevExpress.DevAV.DevAVDb(String.Format("Data Source={0}", filePath))
                    devAvDb.Pictures.Load()
#Else
                    var devAvDb = new DevExpress.DevAV.DevAVDbContext();
#End If
                    devAvDb.Employees.Load()
                    employeesField = devAvDb.Employees.Local.ToList()
                End If

                Return employeesField
            End Get
        End Property

#If NET
        Private filePath As String

        Private Sub SetFilePath()
            If Equals(filePath, Nothing) Then filePath = DataDirectoryHelper.GetFile("devav.sqlite3", DataDirectoryHelper.DataFolderName)
            Try
                Dim attributes = File.GetAttributes(filePath)
                If attributes.HasFlag(FileAttributes.ReadOnly) Then
                    File.SetAttributes(filePath, attributes And Not FileAttributes.ReadOnly)
                End If
            Catch
            End Try
        End Sub

#End If
        Friend Function GetEmployee(ByVal email As String) As DevExpress.DevAV.Employee
            Return Employees.SingleOrDefault(Function(x) x.Email.Equals(email))
        End Function
    End Module
End Namespace
