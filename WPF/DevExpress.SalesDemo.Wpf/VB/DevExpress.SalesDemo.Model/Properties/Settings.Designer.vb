Namespace DevExpress.SalesDemo.Model.Properties

    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")>
    Friend NotInheritable Partial Class Settings
        Inherits Global.System.Configuration.ApplicationSettingsBase

        Private Shared defaultInstance As DevExpress.SalesDemo.Model.Properties.Settings = CType((Global.System.Configuration.ApplicationSettingsBase.Synchronized(New DevExpress.SalesDemo.Model.Properties.Settings())), DevExpress.SalesDemo.Model.Properties.Settings)

        Public Shared ReadOnly Property [Default] As Settings
            Get
                Return DevExpress.SalesDemo.Model.Properties.Settings.defaultInstance
            End Get
        End Property

        <Global.System.Configuration.ApplicationScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString)>
        <Global.System.Configuration.DefaultSettingValueAttribute("Data Source=|DataDirectory|\SalesDashboardContext.sdf")>
        Public ReadOnly Property SalesDashboardContextConnectionString As String
            Get
                Return(CStr((Me("SalesDashboardContextConnectionString"))))
            End Get
        End Property
    End Class
End Namespace
