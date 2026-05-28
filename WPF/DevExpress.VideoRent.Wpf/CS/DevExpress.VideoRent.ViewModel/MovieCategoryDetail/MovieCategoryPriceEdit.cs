using System.Collections.Generic;
using System.Windows.Media;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieCategoryPriceEdit : ModuleObjectEdit {
        public MovieCategoryPriceEdit(MovieCategoryPriceEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            RetrieveFormatIcon();
        }
        public MovieCategoryPriceEditObject MovieCategoryEditObject { get { return (MovieCategoryPriceEditObject)EditObject; } }
        public string FormatText { get { return EnumTitlesKeeper<MovieItemFormat>.GetTitle(MovieCategoryEditObject.CategoryPrice.Format); } }
        public ImageSource FormatIcon { get; set; }
        void RetrieveFormatIcon() {
            List<FormatItem> result = ItemsSourceHelper.FormatsList;
            foreach(FormatItem item in result) {
                if(item.Value == MovieCategoryEditObject.CategoryPrice.Format) {
                    FormatIcon = item.Icon;
                    break;
                }
            }
        }
    }
}
