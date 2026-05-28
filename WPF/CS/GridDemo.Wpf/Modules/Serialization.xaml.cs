using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;

namespace GridDemo {
    public partial class Serialization : GridDemoModule {
        MemoryStream currentLayoutStream;
        public Serialization() {
            InitializeComponent();
            Loaded += OnLoaded;
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            AddLayoutSampleItem("Original", SaveLayout());
            AddLayoutSampleItem("Brief view", GetResourceStream("BriefView"));
            AddLayoutSampleItem("Full view", GetResourceStream("FullView"));
            AddLayoutSampleItem("Banded layout", GetResourceStream("BandedLayout"));
            layoutSamplesComboBox.SelectedIndex = 0;
        }
        void AddLayoutSampleItem(string name, Stream stream) {
            layoutSamplesComboBox.Items.Add(new ComboBoxEditItem() { Content = name, Tag = stream });
        }
        Stream GetResourceStream(string name) {
            Assembly assembly = typeof(Serialization).Assembly;
#pragma warning disable DX0025 
            var resourcePath = DemoHelper.GetPath("GridDemo.Data.LayoutSamples.", assembly) + name + ".xml";
#pragma warning restore DX0025
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }
        MemoryStream SaveLayout() {
            MemoryStream stream = new MemoryStream();
            grid.SaveLayoutToStream(stream);
            return stream;
        }
        void RestoreLayout(Stream stream) {
            if(stream == null)
                return;
            stream.Position = 0;
            grid.RestoreLayoutFromStream(stream);
        }
        void saveLayoutButton_Click(object sender, RoutedEventArgs e) {
            currentLayoutStream = SaveLayout();
            restoreLayoutButton.IsEnabled = true;
        }
        void restoreLayoutButton_Click(object sender, RoutedEventArgs e) {
            RestoreLayout(currentLayoutStream);
        }
        void loadSampleLayoutButton_Click(object sender, RoutedEventArgs e) {
            var item = (ComboBoxEditItem)layoutSamplesComboBox.SelectedItem;
            var stream = (Stream)item.Tag;
            RestoreLayout(stream);
        }
    }
}
