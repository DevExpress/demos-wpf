Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.Extensions.AI

#If NET
Namespace AIDemo.Agents

    Public Module SimpleTools

        Public ReadOnly Property Tools As List(Of AITool) = New List(Of AITool) From {AIFunctionFactory.Create(CType(AddressOf GetCurrentUtcTime, Func(Of String)), "GetCurrentUtcTime", "Returns the current UTC time in ISO 8601 format."), AIFunctionFactory.Create(CType(AddressOf SumNumbers, Func(Of Integer, Integer, Integer)), "SumNumbers", "Returns the sum of two integers."), AIFunctionFactory.Create(CType(AddressOf ReverseText, Func(Of String, String)), "ReverseText", "Reverses the supplied text.")}

        Private Function GetCurrentUtcTime() As String
            Return Date.UtcNow.ToString("O")
        End Function

        Private Function SumNumbers(ByVal a As Integer, ByVal b As Integer) As Integer
            Return a + b
        End Function

        Private Function ReverseText(ByVal text As String) As String
            Return String.Concat(If(text, String.Empty).Reverse())
        End Function
    End Module
End Namespace
#End If
