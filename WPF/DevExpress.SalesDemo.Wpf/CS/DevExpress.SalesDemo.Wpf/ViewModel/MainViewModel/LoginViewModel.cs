using System.Windows.Media.Imaging;
using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Wpf.Helpers;
using DevExpress.Xpf.DemoBase.Utils.Security;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class LoginViewModel {
        public static LoginViewModel Create() {
            return ViewModelSource.Create(() => new LoginViewModel());
        }
        public static LoginViewModel Clone(LoginViewModel vm) {
            var res = Create();
            res.UserName = vm.UserName;
            return res;
        }
        readonly SensitiveData userNameData = SensitiveData.CreateForCurrentUser();
        readonly SensitiveData passwordData = SensitiveData.CreateForCurrentUser();
        protected LoginViewModel() {
            userNameData.Text = "John Smith";
            Icon = ResourceImageHelper.GetResourceImage("User.png");
        }
        public virtual string UserName {
            get { return userNameData.Text; }
            set { userNameData.Text = value; }
        }
        public string Password {
            get { return passwordData.Text; }
            set { passwordData.Text = value; }
        }
        public virtual BitmapImage Icon {
            get;
            protected set;
        }
    }
}
