using System.Drawing;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MoviePicturesEdit : VRObjectPicturesEdit<Movie> {
        public MoviePicturesEdit(MoviePicturesEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public MoviePicturesEditObject MoviePicturesEditObject { get { return (MoviePicturesEditObject)EditObject; } }
        protected override void AddPicture(Image picture) {
            MoviePicturesEditObject.VideoRentObject.AddPicture(picture);
        }
    }
}
