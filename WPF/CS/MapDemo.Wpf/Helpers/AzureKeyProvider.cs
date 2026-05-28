using DevExpress.Xpf.DemoBase.DemoTesting;

namespace MapDemo {
    public static class AzureKeyProvider {
        const string key = "CDOhtwNBSsbmBiN3rUEjmBJGHW2tRMbp5XVwu4J55VBZg8PdRe9MJQQJ99BEACYeBjFllM6LAAAgAZMP1cA4";
        const string testKey = "TestKey";

        public static string AzureKey { get { return DemoTestingHelper.IsTesting ? testKey : key; } }
    }
}
