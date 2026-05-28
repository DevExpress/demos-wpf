Imports System
Imports System.Data.Common
Imports System.Globalization

Namespace DevExpress.Demos.SalesDBGenerator

    Public Class SqlHelper(Of T As System.Data.Common.DbConnection, Command As {System.Data.Common.DbCommand, New})

        Public Function ReadValue(ByVal connection As T, ByVal selectQuery As String, ParamArray pars As System.Data.Common.DbParameter()) As Object
            Using dbCommand = New Command() With {.Connection = connection}
                dbCommand.CommandText = selectQuery
                dbCommand.CommandTimeout = 1000
                If pars IsNot Nothing Then dbCommand.Parameters.AddRange(pars)
                Try
                    Return Me.CheckDbNull(dbCommand.ExecuteScalar())
                Catch
                    Return Nothing
                End Try
            End Using
        End Function

        Private Function CheckDbNull(ByVal value As Object) As Object
            If value Is Nothing OrElse TypeOf value Is System.DBNull Then Return Nothing
            Return value
        End Function

        Public Function GetString(ByVal value As Object) As String
            If value Is Nothing OrElse TypeOf value Is System.DBNull Then Return String.Empty
            Return value.ToString()
        End Function

        Public Function GetDateInv(ByVal value As Object) As DateTime
            If value Is Nothing OrElse TypeOf value Is System.DBNull Then Return System.DateTime.MinValue
            Return System.DateTime.ParseExact(value.ToString(), "d/M/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        End Function

        Public Function GetInt(ByVal value As Object) As Integer
            If value Is Nothing OrElse TypeOf value Is System.DBNull Then Return 0
            Return System.Convert.ToInt32(value)
        End Function

        Public Function GetDate(ByVal value As Object) As DateTime
            If value Is Nothing OrElse TypeOf value Is System.DBNull Then Return System.DateTime.MinValue
            If TypeOf value Is System.DateTime Then Return CDate(value)
            Return Me.GetDateInv(value)
        End Function

        Public Function GetDecimal(ByVal value As Object) As Decimal
            If value Is Nothing OrElse TypeOf value Is System.DBNull Then Return 0
            Return CDec(System.Convert.ChangeType(value, GetType(Decimal)))
        End Function

        Public Function GetGuid(ByVal value As Object) As Guid
            If value Is Nothing OrElse TypeOf value Is System.DBNull Then Return System.Guid.Empty
            Return New System.Guid(value.ToString())
        End Function

        Public Function GetBool(ByVal value As Object) As Boolean
            If value Is Nothing OrElse TypeOf value Is System.DBNull Then Return False
            If TypeOf value Is Boolean Then Return CBool(value)
            If TypeOf value Is Integer Then Return CInt(value) = 1
            Return False
        End Function
    End Class
End Namespace
