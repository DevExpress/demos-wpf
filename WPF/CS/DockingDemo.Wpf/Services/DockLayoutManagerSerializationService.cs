using System.IO;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core.Serialization;

namespace DockingDemo {
    public interface IDockLayoutManagerSerializationService {
        void Serialize(string path);
        void Serialize(Stream stream);
        void Deserialize(string path);
        void Deserialize(Stream stream);
    }
    public class DockLayoutManagerSerializationService : ServiceBase, IDockLayoutManagerSerializationService {
        void IDockLayoutManagerSerializationService.Deserialize(string path) {
            DXSerializer.Deserialize(AssociatedObject, path, AssociatedObject.GetType().Name, null);
        }
        void IDockLayoutManagerSerializationService.Deserialize(Stream stream) {
            DXSerializer.Deserialize(AssociatedObject, stream, AssociatedObject.GetType().Name, null);
        }
        void IDockLayoutManagerSerializationService.Serialize(string path) {
            DXSerializer.Serialize(AssociatedObject, path, AssociatedObject.GetType().Name, null);
        }
        void IDockLayoutManagerSerializationService.Serialize(Stream stream) {
            DXSerializer.Serialize(AssociatedObject, stream, AssociatedObject.GetType().Name, null);
        }
    }
}
