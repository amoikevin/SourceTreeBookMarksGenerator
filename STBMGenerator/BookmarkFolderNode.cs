using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SourceTree
{
    [XmlType]
    public class BookmarkFolderNode : TreeViewNode
    {
        [XmlElement]
        public int Level { get; set; }

        [XmlElement]
        public bool IsExpanded { get; set; }

        [XmlElement]
        public bool IsLeaf { get; set; }

        [XmlElement]
        public string Name { get; set; }


        public BookmarkFolderNode()
        {
            Children = new List<TreeViewNode>();
            IsLeaf = false;
        }
    }
}