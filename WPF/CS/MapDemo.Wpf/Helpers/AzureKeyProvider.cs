using DevExpress.Xpf.DemoBase.DemoTesting;

namespace MapDemo {
    public static class AzureKeyProvider {
        const string key = "";
        const string testKey = "TestKey";

        public static string AzureKey { get { return DemoTestingHelper.IsTesting ? testKey : key; } }
    }
}
