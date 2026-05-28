using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using CommonDemo.Helpers;
using DevExpress.Internal;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Security;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Dialogs;
using DialogsDemo.Helpers;

namespace DialogsDemo {
    public class FileDialogsViewModel {
        const int MaxFileSize = 10 * 1024 * 1024; 
        string DefaultPhotoGalleryPath { get { return DialogsDemoHelper.GetPhotosPath("Photos"); } }
        public string AdditionalPhotosPath { get { return DialogsDemoHelper.GetPhotosPath("AdditionalPhotos"); } }
        string DefaultPhotoExtension { get { return ".jpg"; } }

        [BindableProperty]
        public virtual ObservableCollection<PhotoData> Photos { get; protected set; }
        [BindableProperty]
        public virtual PhotoData CurrentPhoto { get; set; }

        [BindableProperty]
        public virtual string DefaultExt { get; set; }
        [BindableProperty]
        public virtual string GalleryDirectory { get; protected set; }

        public Filters Filters { get { return FiltersHelper.PhotoFilters; } }
        public string[] Extensions { get; set; }

        public FileDialogsViewModel() {
            UpdateExtensions();
            UpdateGallery(DefaultPhotoGalleryPath);
        }

        void UpdateExtensions() {
            Extensions = new string[] { "JPG", "PNG", "BMP", "JPEG" };
            DefaultExt = Extensions.FirstOrDefault();
        }
        void UpdateGallery(string path) {
            GalleryDirectory = Path.GetFullPath(path);
            Photos = new ObservableCollection<PhotoData>(ImageHelper.GetJPGPhotos(GalleryDirectory));
            CurrentPhoto = Photos.FirstOrDefault();
        }

        public void ChangeGalleryDirectory() {
            var folderBrowser = GetFolderBrowserDialog();
            if(folderBrowser.ShowDialog().Value) {
                UpdateGallery(folderBrowser.FileName);
            }
        }
        public void UploadPhotos() {
            var openDialog = GetOpenDialog();
            if(openDialog.ShowDialog().Value) {
                foreach(var file in openDialog.FileNames) {
                    if(!CheckUploadingFile(file)) {
                        ShowMessage("Error", string.Format("The file '{0}' is not valid.", file));
                        continue;
                    }

                    Photos.Add(ImageHelper.GetJPGPhoto(file));
                    string fileInGalleryPath = Path.Combine(GalleryDirectory, Path.GetFileName(file));
                    if(SafePath.IsRooted(GalleryDirectory, fileInGalleryPath))
                        CopyPhoto(file, fileInGalleryPath, uploading: true);
                }
            }
        }

        bool CheckUploadingFile(string file) {
            
            string decoded = Uri.UnescapeDataString(file);
            decoded = decoded.Normalize(NormalizationForm.FormC);

            
            if(decoded.StartsWith("//") || decoded.StartsWith(@"\\"))
                return false;

            
            if(decoded.Length == 0 || !File.Exists(decoded))
                return false;

            
            string ext = Path.GetExtension(decoded);
            if(string.IsNullOrEmpty(ext) || !Extensions.Contains(ext.Substring(1).ToUpperInvariant()))
                return false;

            
            if(new FileInfo(decoded).Length > MaxFileSize)
                return false;

            return true;
        }

        public void DownloadPhoto() {
            var saveDialog = GetSaveDialog();
            if(saveDialog.ShowDialog().Value) {
                var file = saveDialog.FileName;
                CopyPhoto(Path.Combine(GalleryDirectory, CurrentPhoto.PhotoName + DefaultPhotoExtension), file, uploading: false);
            }
        }
        public bool CanDownloadPhoto() {
            return CurrentPhoto != null;
        }

        void CopyPhoto(string sourceFilePath, string destFileName, bool uploading) {
            try {
                string path = uploading ? destFileName : sourceFilePath;
                if(!DevExpress.Security.SafePath.IsRooted(GalleryDirectory, path))
                    throw new UnauthorizedAccessException("Access to the specified path is denied.");
                File.Copy(sourceFilePath, destFileName, true);
            }
            catch(System.Exception e) {
                ShowMessage("Error", e.Message);
            }
        }

        void ShowMessage(string title, string message) {
            ThemedMessageBox.Show(title, message);
        }

        #region dialogs
        DXOpenFileDialog GetOpenDialog() {
            return new DXOpenFileDialog() {
                InitialDirectory = AdditionalPhotosPath,
                Filter = Filters.GetFilterString(),
                Title = "DX Open Dialog",
                Multiselect = true,
            };
        }
        DXSaveFileDialog GetSaveDialog() {
            return new DXSaveFileDialog() {
                InitialDirectory = AdditionalPhotosPath,
                DefaultExt = DefaultExt,
                Title = "DX Save Dialog",
            };
        }
        DXOpenFileDialog GetFolderBrowserDialog() {
            return new DXOpenFileDialog() {
                InitialDirectory = DialogsDemoHelper.GetPhotosPath(string.Empty),
                Title = "DX Open folder Dialog",
                OpenFileDialogMode = OpenFileDialogMode.Folders,
            };
        }
        #endregion
    }
}
