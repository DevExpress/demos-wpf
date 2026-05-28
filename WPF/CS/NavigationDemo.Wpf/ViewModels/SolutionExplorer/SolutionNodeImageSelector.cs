using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;

namespace NavigationDemo {
    public class SolutionNodeImageSelector : TreeListNodeImageSelector {
        static SolutionNodeImageSelector() {
            ImageCache = new Dictionary<TypeNode, ImageSource>();
        }
        static readonly Dictionary<TypeNode, ImageSource> ImageCache;
        public override ImageSource Select(DevExpress.Xpf.Grid.TreeList.TreeListRowData rowData) {
            SolutionNode solutionNode = (rowData.Row as SolutionNode);
            return solutionNode == null ? null : GetImageByTypeNode(solutionNode.TypeNode);
        }
        public static ImageSource GetImageByTypeNode(TypeNode typeNode) {
            ImageSource value;
            if(ImageCache.TryGetValue(typeNode, out value))
                return value;
            var extension = new SvgImageSourceExtension() { Uri = new Uri(GetImagePathByTypeNode(typeNode)), Size = new Size(16, 16) };
            var image = (ImageSource)extension.ProvideValue(null);
            ImageCache.Add(typeNode, image);
            return image;
        }
        public static string GetImagePathByTypeNode(TypeNode typeNode) {
#pragma warning disable DX0025 
            return "pack://application:,,,/NavigationDemo;component/Images/SolutionExplorer/" + typeNode.ToString() + ".svg";
#pragma warning restore DX0025
        }
    }
}
