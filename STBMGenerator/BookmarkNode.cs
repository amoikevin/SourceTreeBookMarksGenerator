using System.Collections;
using System.Xml.Serialization;

namespace SourceTree
{
    [XmlType]
    public class BookmarkNode : BookmarkFolderNode
    {
        [XmlElement]
        public string Path { get; set; }

        [XmlElement]
        public string RepoType { get; set; }

        public BookmarkNode()
        {
            IsLeaf = true;
            Children = null;
        }
    }
}