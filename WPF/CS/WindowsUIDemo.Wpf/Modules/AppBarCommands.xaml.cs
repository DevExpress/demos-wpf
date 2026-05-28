using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.DemoData.Models.Vehicles;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using static System.Resources.ResXFileRef;

namespace WindowsUIDemo {
    public partial class AppBarCommands : WindowsUIDemoModule {
        public AppBarCommands() {
            InitializeComponent();
        }
    }
    [POCOViewModel]
    public class PhotoCollection {
        static ImageSourceConverter converter = new ImageSourceConverter();
        public virtual List<Photo> Photos { get; set; }
        public virtual Photo SelectedItem { get; set; }
        public PhotoCollection() {
            if(this.IsInDesignMode())
                return;
            var context = new VehiclesContext();
            Photos = context.Models.ToList().Select(x => ViewModelSource.Create(() => new Photo { Caption = x.Name, Source = (ImageSource)converter.ConvertFrom(x.Photo), ViewSize = new Size(150, 100), Model = x })).ToList();
        }
        [Command]
        public void RotateClockwise() {
            if(SelectedItem != null) {
                switch(SelectedItem.Rotation) {
                    case Rotation.Rotate0:
                        SelectedItem.Rotation = Rotation.Rotate90;
                        break;
                    case Rotation.Rotate90:
                        SelectedItem.Rotation = Rotation.Rotate180;
                        break;
                    case Rotation.Rotate180:
                        SelectedItem.Rotation = Rotation.Rotate270;
                        break;
                    case Rotation.Rotate270:
                        SelectedItem.Rotation = Rotation.Rotate0;
                        break;
                }
            }
        }
        [Command]
        public void RotateCounterclockwise() {
            if(SelectedItem != null) {
                switch(SelectedItem.Rotation) {
                    case Rotation.Rotate0:
                        SelectedItem.Rotation = Rotation.Rotate270;
                        break;
                    case Rotation.Rotate90:
                        SelectedItem.Rotation = Rotation.Rotate0;
                        break;
                    case Rotation.Rotate180:
                        SelectedItem.Rotation = Rotation.Rotate90;
                        break;
                    case Rotation.Rotate270:
                        SelectedItem.Rotation = Rotation.Rotate180;
                        break;
                }
            }
        }
        [Command]
        public void Rotate180() {
            if(SelectedItem != null) {
                switch(SelectedItem.Rotation) {
                    case Rotation.Rotate0:
                        SelectedItem.Rotation = Rotation.Rotate180;
                        break;
                    case Rotation.Rotate90:
                        SelectedItem.Rotation = Rotation.Rotate270;
                        break;
                    case Rotation.Rotate180:
                        SelectedItem.Rotation = Rotation.Rotate0;
                        break;
                    case Rotation.Rotate270:
                        SelectedItem.Rotation = Rotation.Rotate90;
                        break;
                }
            }
        }
        [Command]
        public void RotateReset() {
            if(SelectedItem != null) {
                SelectedItem.Rotation = Rotation.Rotate0;
            }
        }
        [Command]
        public void ZoomIn() {
            if(SelectedItem != null) SelectedItem.Scale += 0.1;
        }
        [Command]
        public void ZoomOut() {
            if(SelectedItem != null) SelectedItem.Scale = Math.Max(0.1, SelectedItem.Scale - 0.1);
        }
        [Command]
        public void ResetScale() {
            if(SelectedItem != null) SelectedItem.Scale = 1;
        }
        [Command]
        public void Print() {
            if(SelectedItem == null) return;
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true) {
                printDialog.PrintVisual(new Image() { Source = SelectedItem.Source }, SelectedItem.Caption);
            }
        }
        [Command]
        public void Flip() {
            if(SelectedItem != null) SelectedItem.Flip = !SelectedItem.Flip;
        }
    }
    [POCOViewModel]
    public class Photo {
        public ImageSource Source { get; set; }
        public string Caption { get; set; }
        public Size ViewSize { get; set; }
        public virtual Rotation Rotation { get; set; }
        public virtual double Scale { get; set; }
        public virtual bool Flip { get; set; }
        public virtual Model Model { get; set; }
        public Photo() {
            Scale = 1;
            ViewSize = new Size(double.NaN, double.NaN);
        }
    }
}
