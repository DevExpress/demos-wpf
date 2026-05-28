using System.Collections.Generic;
using System.Drawing;
using DevExpress.Drawing;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Layout;

namespace RichEditDemo {
    public class HitTestPagePainter : PagePainter {
        RichEditHitTestResult _hitTestResult;
        Dictionary<LayoutType, RichEditPen> _highlightOptions;
        RichEditPen _defaultHighlightingPen;

        public HitTestPagePainter(RichEditHitTestResult hitTestResult,
            Dictionary<LayoutType, RichEditPen> highlightOptions, RichEditPen defaultHighlightingPen) {
            this._hitTestResult = hitTestResult;
            this._highlightOptions = highlightOptions;
            this._defaultHighlightingPen = defaultHighlightingPen;
        }

        public override void DrawPage(LayoutPage page) {
            base.DrawPage(page);
            RichEditPen currentHighLightPen = null;
            LayoutElement layoutElement;
            while(this._hitTestResult != null) {
                layoutElement = this._hitTestResult.LayoutElement;
                if(!this._highlightOptions.TryGetValue(layoutElement.Type, out currentHighLightPen))
                    currentHighLightPen = this._defaultHighlightingPen;
                if(currentHighLightPen != null) {
                    if(layoutElement.Type == LayoutType.FloatingPicture || layoutElement.Type == LayoutType.TextBox) {
                        Point[] pointToDraw = ((LayoutFloatingObject)layoutElement).GetCoordinates();
                        Canvas.DrawLines(currentHighLightPen, pointToDraw);
                        Canvas.DrawLine(currentHighLightPen, pointToDraw[3], pointToDraw[0]);
                    }
                    else {
                        LayoutTextBox parentTextBox = layoutElement.GetParentByType<LayoutTextBox>();
                        if(parentTextBox != null) {
                            DXMatrix matrix = parentTextBox.GetRotationMatrix();
                            Rectangle bounds = layoutElement.Bounds;
                            PointF[] points = new PointF[] {
                                new Point(bounds.X, bounds.Y),
                                new Point(bounds.X + bounds.Width, bounds.Y),
                                new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height),
                                new Point(bounds.X, bounds.Y+bounds.Height),
                                new Point(bounds.X, bounds.Y)
                            };
                            matrix.TransformPoints(points);
                            Canvas.DrawLines(currentHighLightPen, PointsFToPoints(points));
                        }
                        else {
                            Canvas.DrawRectangle(currentHighLightPen, layoutElement.Bounds);
                        }
                    }
                }
                this._hitTestResult = this._hitTestResult.Next;
            }
        }
        Point[] PointsFToPoints(PointF[] points) {
            Point[] result = new Point[points.Length];
            for(int i = 0; i < points.Length; i++)
                result[i] = new Point((int)points[i].X, (int)points[i].Y);
            return result;
        }
    }
}
