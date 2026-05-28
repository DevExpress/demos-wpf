using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpf.Utils;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class ConstString : MarkupExtension {
        const string LabelMark = "Label";
        static Func<string, string> getLocalizedString;
        public static void SetConstStrings(Func<string, string> getLocalizedString1) {
            getLocalizedString = getLocalizedString1;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            bool label = false;
            string id = ID;
            if(id.EndsWith(LabelMark)) {
                label = true;
                id = id.Remove(id.Length - LabelMark.Length);
            }
            string s;
            try {
                s = getLocalizedString(id);
            }
            catch {
                s = DevExpress.Data.Helpers.MasterDetailHelper.SplitPascalCaseString(id);
            }
            return label ? s + ":" : s;
        }
        public ConstString() { }
        public string ID { get; set; }
    }
}
