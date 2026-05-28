using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ControlsDemo.Helpers;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;

namespace ControlsDemo.TabControl.ColorizedTabs {
    public class MainViewModel {
        public static MainViewModel Create() {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        protected MainViewModel() {
            if(this.IsInDesignMode()) return;
            InitDataSource();
        }

        public virtual List<EmployeeWrapper> Employees { get; protected set; }
        void InitDataSource() {
            Employees =
                  NWindContext.Create().Employees.ToList()
                  .Select(x => EmployeeWrapper.Create(x)).ToList();

            int i = 0;
            foreach(var employee in Employees) {
                employee.AccentColor = ColorPalette.GetColor(i, 100);
                employee.AccentColor = ColorHelper.Brightness(employee.AccentColor, 0.3);
                employee.AccentColorOpacity = 180;
                i++;
            }
        }

    }
    public class EmployeeWrapper {
        public static EmployeeWrapper Create(Employee employee) {
            return ViewModelSource.Create(() => new EmployeeWrapper(employee));
        }
        protected EmployeeWrapper(Employee employee) {
            Employee = employee;
        }

        public Employee Employee { get; private set; }

        [BindableProperty(OnPropertyChangedMethodName = "UpdateColors")]
        public virtual Color AccentColor { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "UpdateColors")]
        public virtual byte AccentColorOpacity { get; set; }

        protected void UpdateColors() {
            var color = AccentColor;
            color.A = AccentColorOpacity;
            AccentColor = color;
        }
    }
}
