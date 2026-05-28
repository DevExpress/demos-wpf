using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;

namespace EditorsDemo.Modules.Views {
    public partial class EditImageEditModeWindow {
        public EditImageEditModeWindow(ImageSource imageSource) {
            InitializeComponent();
            ImageEdit.Source = imageSource;
        }

        public ImageSource ImageSource => ImageEdit.Source;

        void OnCustomEditMenuItems(object sender, CustomEditMenuItemsEventArgs e) {
            e.EditMenuItems.Add(new ImageEditToolSeparatorInfo());
            e.EditMenuItems.Add(new ImageEditToolButtonInfo {
                Glyph = ImageEditToolButtonHelper.ImageIdToImageSource("ok"),
                ToolTip = EditorLocalizer.GetString(EditorStringId.OK),
                Command = new DelegateCommand(Confirm)
            });
            e.EditMenuItems.Add(new ImageEditToolButtonInfo {
                Glyph = ImageEditToolButtonHelper.ImageIdToImageSource("cancel"),
                ToolTip = EditorLocalizer.GetString(EditorStringId.Cancel),
                Command = new DelegateCommand(Close)
            });
        }

        void Confirm() {
            DialogResult = true;
            Close();
        }
    }
}
