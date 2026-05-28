Imports DevExpress.Xpf.DemoBase
Imports AIDemo.Modules.CodeExamples

Namespace AIDemo

    Public Class CodeExamplesModule
        Inherits ShowcaseBrowserModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Return New IShowcaseInfo() {LoadShowcase("Modify default extension", Nothing, "Modules/CodeExamples", {GetType(ModifyDefaultExtension), GetType(WilliamShakespeareStyleExtension)}, showCodeBehind:=True), LoadShowcase("Create custom extension", Nothing, "Modules/CodeExamples", {GetType(CreateCustomExtension), GetType(AuthoredStyleBehavior), GetType(AuthoredStyleExtension), GetType(AuthoredStyleRequest)}, showCodeBehind:=True), LoadShowcase("Configure AIExtensions composition", Nothing, "Modules/CodeExamples", {GetType(ConfigureAiExtensionsComposition), GetType(ErnestHemingwayStyleExtension)}, showCodeBehind:=True), LoadShowcase("Custom Processing", Nothing, "Modules/CodeExamples", {GetType(CustomProcessing)}, showCodeBehind:=True), LoadShowcase("Error Logging and Handling", Nothing, "Modules/CodeExamples", {GetType(ErrorLoggingAndHandling), GetType(AIExceptionHandler)}, showCodeBehind:=True)}
        End Function
    End Class
End Namespace
