using System.Windows.Media;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditDemo {
    public enum CustomDrawHighlightType {
        Fill,
        Outline
    }
    public class CustomDrawHighlightInfo {
        public FixedRange HighlightRange { get; private set; }
        public CustomDrawHighlightType HighlightType { get; private set; }
        public Color HighlightColor { get; private set; }

        public CustomDrawHighlightInfo(FixedRange highlightedRange, CustomDrawHighlightType highlightType, Color highlightColor) {
            HighlightRange = highlightedRange;
            HighlightType = highlightType;
            HighlightColor = highlightColor;
        }
    }
}
