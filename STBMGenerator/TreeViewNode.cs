using System.Collections.Generic;
using System.Xml.Serialization;

namespace SourceTree
{
    [XmlType]
    public abstract class TreeViewNode
    {
        public List<TreeViewNode> Children { get; set; }
    }
}