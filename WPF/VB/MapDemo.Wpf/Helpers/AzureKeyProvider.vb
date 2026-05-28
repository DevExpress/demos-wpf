Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace MapDemo

    Public Module AzureKeyProvider

        Const key As String = "CDOhtwNBSsbmBiN3rUEjmBJGHW2tRMbp5XVwu4J55VBZg8PdRe9MJQQJ99BEACYeBjFllM6LAAAgAZMP1cA4"

        Const testKey As String = "TestKey"

        Public ReadOnly Property AzureKey As String
            Get
                Return If(DemoTestingHelper.IsTesting, testKey, key)
            End Get
        End Property
    End Module
End Namespace
