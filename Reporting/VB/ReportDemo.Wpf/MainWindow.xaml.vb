Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports DevExpress.Data.Internal
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports System.Runtime.InteropServices

Namespace ReportWpfDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Shared Sub New()
            XtraReportsDemos.ObjectDataSourceTypesRegistrator.RegisterTrustedTypes()
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ReportWpfDemo.MainWindow.OnAssemblyResolve
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.PropertyGrid, PreloadCategories.Grid)
        End Sub

        Private Shared Function OnAssemblyResolve(ByVal sender As Object, ByVal e As ResolveEventArgs) As Assembly
            Dim domain = TryCast(sender, AppDomain)
            Dim assembly = GetType(MainWindow).Assembly
            Dim partialName As String = DevExpress.Utils.AssemblyHelper.GetPartialName(e.Name)
            Dim loadedAssembly = domain.GetAssemblies().SingleOrDefault(Function(a) Equals(a.GetName().Name, partialName))
            If loadedAssembly IsNot Nothing Then Return loadedAssembly
            Dim assemblyName As String = partialName & ".dll"
            Dim filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), assemblyName)
            If File.Exists(filePath) Then
                Return SafeTypeResolver.GetOrLoadAssemblyFile(filePath, False)
            End If

            Dim manifestResourceName As String = assembly.GetManifestResourceNames().FirstOrDefault(Function(x) x.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase))
            If String.IsNullOrEmpty(manifestResourceName) Then Return Nothing
            Using manifestResourceStream As Stream = assembly.GetManifestResourceStream(manifestResourceName)
                If manifestResourceStream Is Nothing Then Return Nothing
                Dim data As Byte() = New Byte(manifestResourceStream.Length - 1) {}
                manifestResourceStream.Read(data, 0, data.Length)
                Return SafeTypeResolver.GetOrLoadAssemblyBytes(data)
            End Using
        End Function

        Private ReadOnly cachedModule As DocumentPreview

        Private ReadOnly viewModels As Dictionary(Of Type, ReportModuleViewModelBase)

        Public Sub New()
            cachedModule = New DocumentPreview()
            viewModels = New Dictionary(Of Type, ReportModuleViewModelBase)()
            AllowAnimation = False
            InitializeComponent()
        End Sub

        Protected Overrides Sub OnDemoModuleCompletelyLoaded(ByVal demo As DemoModule)
            MyBase.OnDemoModuleCompletelyLoaded(demo)
            If CurrentDemoModule IsNot cachedModule Then cachedModule.DataContext = Nothing
        End Sub

        Protected Overrides Function LoadDemoModule(ByVal demoModuleType As Type, ByVal disableRtl As Boolean, ByVal owner As Func(Of DemoModule, DemoModuleOwner), <Out> ByRef exception As Exception) As DemoModule
            If demoModuleType Is Nothing Then
                exception = Nothing
                Return Nothing
            End If

            If Not demoModuleType.IsSubclassOf(GetType(ReportModuleViewModelBase)) Then Return MyBase.LoadDemoModule(demoModuleType, disableRtl, owner, exception)
            Try
                Dim demoViewModel = GetViewModel(demoModuleType)
                If disableRtl Then cachedModule.FlowDirection = System.Windows.FlowDirection.LeftToRight
                cachedModule.UpdateOwner(owner(cachedModule))
                cachedModule.DataContext = demoViewModel
                exception = Nothing
                Return cachedModule
            Catch e As Exception
                exception = e
                Return Nothing
            End Try
        End Function

        Protected Overrides Sub ClearDemoModule(ByVal [module] As DemoModule, ByVal hide As Boolean)
        End Sub

        Private Function GetViewModel(ByVal demoModuleType As Type) As Object
            Dim viewModel As ReportModuleViewModelBase
            If Not viewModels.TryGetValue(demoModuleType, viewModel) Then
                viewModel = CType(ViewModelSourceHelper.Create(demoModuleType), ReportModuleViewModelBase)
                viewModels.Add(demoModuleType, viewModel)
            End If

            Return viewModel
        End Function
    End Class
End Namespace
