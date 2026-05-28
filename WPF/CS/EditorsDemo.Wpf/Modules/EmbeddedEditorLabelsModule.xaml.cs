using System.Windows;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    public partial class EmbeddedEditorLabelsModule {
        public EmbeddedEditorLabelsModule() {
            InitializeComponent();
        }
        void OnChecked(object sender, RoutedEventArgs e) {
            if(PropertiesGroupBox == null)
                return;
            PropertiesGroupBox.DataContext = GetDateContext(sender);
            if(PropertiesGroupBox.DataContext == null)
                return;
            var visibility = PropertiesGroupBox.DataContext.GetType() == typeof(ComboBoxEdit)
                ? Visibility.Collapsed
                : Visibility.Visible;
            if(NullTextTextBlock != null)
                NullTextTextBlock.Visibility = visibility;
            if(NullTextTextBox != null)
                NullTextTextBox.Visibility = visibility;
        }

        TextEditBase GetDateContext(object sender) {
            if(ReferenceEquals(sender, FirstNameRadioButton))
                return FirstNameEdit;
            if(ReferenceEquals(sender, LastNameRadioButton))
                return LastNameEdit;
            if(ReferenceEquals(sender, FullNameRadioButton))
                return FullNameEdit;
            if(ReferenceEquals(sender, PrefixRadioButton))
                return PrefixEdit;
            if(ReferenceEquals(sender, BirthDateRadioButton))
                return BirthDateEdit;
            if(ReferenceEquals(sender, MobilePhoneRadioButton))
                return MobilePhoneEdit;
            if(ReferenceEquals(sender, TitleRadioButton))
                return TitleEdit;
            if(ReferenceEquals(sender, DepartmentRadioButton))
                return DepartmentEdit;
            if(ReferenceEquals(sender, HireDateRadioButton))
                return HireDateEdit;
            return null;
        }
    }
}
