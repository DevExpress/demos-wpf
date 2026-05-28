using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase.Utils.Security;

namespace EditorsDemo {
    
    
    
    public partial class PasswordBoxEditModule : EditorsDemoModule {
        public PasswordBoxEditModule() {
            InitializeComponent();
            sample.DataContext = new Info();
        }
        private void TextEdit_Validate(object sender, ValidationEventArgs e) {
            if(string.IsNullOrEmpty(e.Value as string)) {
                e.IsValid = false;
                e.Handled = true;
                e.ErrorContent = "Enter login";
            }
        }

        private void password_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            BindingExpression binding = passwordConfirm.GetBindingExpression(BaseEdit.EditValueProperty);
            if(binding == null)
                return;
            binding.UpdateSource();
        }

        private void passwordConfirm_EditValueChanged(object sender, EditValueChangedEventArgs e) {
        }
    }

    public class Info : BindableBase, IDataErrorInfo {
        public string Login {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        SensitiveData passwordData = SensitiveData.CreateForCurrentUser();
        SensitiveData passwordDataConfirm = SensitiveData.CreateForCurrentUser();
        public string Password {
            get { return passwordData.Text; }
            set {
                if(passwordData.Equals(value)) {
                    return;
                }
                passwordData.Text = value;
                this.RaisePropertyChanged();
            }
        }
        public string PasswordConfirm {
            get { return passwordDataConfirm.Text; }
            set {
                if(passwordDataConfirm.Equals(value)) {
                    return;
                }
                passwordDataConfirm.Text = value;
                this.RaisePropertyChanged();
            }
        }

        #region IDataErrorInfo Members

        public string Error {
            get { return "error"; }
        }

        public string this[string columnName] {
            get {
                bool isValidPassword = !string.IsNullOrEmpty(Password) && Password.Length > 5;
                if(columnName == "Password")
                    return isValidPassword ? string.Empty : "error";
                if(columnName == "PasswordConfirm") {
                    return passwordData.Equals(passwordDataConfirm) ? string.Empty : "Please verify your password";
                }
                return string.Empty;
            }
        }

        #endregion
    }
    public class PasswordConfirmVisibilityConverter : IMultiValueConverter {

        

        
        
        
        
        

        
        
        

        
        #region IMultiValueConverter Members
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return values[0] != null && (bool)values[1] ? Visibility.Visible : Visibility.Hidden;
        }
        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
