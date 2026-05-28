using System.Collections.Generic;
using System.Windows;
using DevExpress.Utils;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.Parameters;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters.ViewModels;

namespace ReportWpfDemo {
    public class DemoParameterTemplateSelector : ParameterTemplateSelector {
        Dictionary<object, DataTemplate> templates = new Dictionary<object, DataTemplate>();
        public Dictionary<object, DataTemplate> Templates { get { return templates; } }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var parameter = item as ParameterItemViewModel;
            if(parameter == null)
                return null;
            if(Templates.ContainsKey(parameter.Name))
                return Templates[parameter.Name];
            return base.SelectTemplate(item, container);
        }
    }

    public class DemoEditingFieldTemplateSelector : EditingFieldTemplateSelector {
        Dictionary<object, DataTemplate> templates = new Dictionary<object, DataTemplate>();
        public Dictionary<object, DataTemplate> Templates { get { return templates; } }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var textEditingField = item as TextEditingField;

            if(textEditingField != null && Templates.ContainsKey(textEditingField.EditorName))
                return Templates[textEditingField.EditorName];
            return base.SelectTemplate(item, container);
        }
    }
}
