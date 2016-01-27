using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SourceTree
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length <= 0)
                args = new[] {Directory.GetCurrentDirectory()};

            var nodes = new List<TreeViewNode>();
            foreach (var arg in args.Where(arg => Directory.Exists(arg)))
            {
                BookmarkFolderNode root = null;
                Search(arg, ref root, 0);
                if (root != null)
                    nodes.Add(root);
            }
            if (nodes.Count <= 0)
                return;
            var xs = new XmlSerializer(typeof (List<TreeViewNode>),
                new[] {typeof (BookmarkFolderNode), typeof (BookmarkNode)});
            using (var fs = File.OpenWrite("bookmarks.xml"))
            {
                xs.Serialize(fs, nodes);
            }
        }

        static void Search(string folder, ref BookmarkFolderNode dir, int level)
        {
            if (Directory.Exists(Path.Combine(folder, ".git")))
            {
                var node = new BookmarkNode
                {
                    Level = level,
                    Name = Path.GetFileName(folder),
                    Path = folder,
                    RepoType = "Git"
                };

                if (dir == null)
                    dir = node;
                else
                {
                    dir.Children.Add(node);
                }
            }
            else if (Directory.Exists(Path.Combine(folder, ".hg")))
            {
                var node = new BookmarkNode
                {
                    Level = level,
                    Name = Path.GetFileName(folder),
                    Path = folder,
                    RepoType = "Mercurial"
                };

                if (dir == null)
                    dir = node;
                else
                {
                    dir.Children.Add(node);
                }
            }
            else
            {
                var node = new BookmarkFolderNode()
                {
                    Level = level,
                    Name = Path.GetFileName(folder)
                };


                foreach (var subdir in Directory.GetDirectories(folder))
                {
                    Search(subdir, ref node, level + 1);
                }

                if (node.Children.Count <= 0)
                    return;

                if (dir == null)
                    dir = node;
                else
                {
                    dir.Children.Add(node);
                }
            }
        }
    }
}