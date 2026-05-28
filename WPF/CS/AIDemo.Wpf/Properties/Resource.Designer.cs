









using System.Reflection;
using DevExpress.Xpf.DemoBase.Helpers;

namespace AIDemo.Properties {
    using System;


    
    
    
    
    
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }

        
        
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    var assembly = typeof(Resource).Assembly;
                    var baseName = DemoHelper.GetPath($"{assembly.GetName().Name}.Properties.", assembly) + "Resource";
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(baseName, assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        
        
        
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        
        
        
        internal static string EndpointWarning {
            get {
                return ResourceManager.GetString("EndpointWarning", resourceCulture);
            }
        }

        
        
        
        
        
        internal static string TextEditContent {
            get {
                return ResourceManager.GetString("TextEditContent", resourceCulture);
            }
        }
    }
}
