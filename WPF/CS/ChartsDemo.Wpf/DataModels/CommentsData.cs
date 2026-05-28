using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DevExpress.Utils;

namespace ChartsDemo {

    [XmlRoot("Comments")]
    public class CommentsData : List<CommentsInfo> {
        public static CommentsData Load() {
            return SafeXml.Deserialize<CommentsData>(DataLoader.LoadFromResources("/Data/CommentsData.xml"));
        }
    }


    public class CommentsInfo {
        public string Category { get; set; }
        public List<CommentInfo> Items { get; set; }
    }


    public class CommentInfo {
        public DateTime Date { get; set; }
        public int CommentCount { get; set; }
    }

}
