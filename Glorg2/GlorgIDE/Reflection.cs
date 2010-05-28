using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Glorg2;
using Glorg2.Scene;

namespace GlorgIDE
{
	public class NodeItem
	{
		internal System.Drawing.Bitmap icon;
		public Type Type { get; set; }
		public string Name { get; set; }
		public System.Drawing.Bitmap Icon { get { return icon; } }
		List<NodeItem> children;
		public List<NodeItem> Children { get { return children; } }
		public NodeItem()
		{
			children = new List<NodeItem>();
		}
		public override string ToString()
		{
			return Name;
		}
	}
	public class Reflection
	{
		NodeItem base_type;
		public NodeItem BaseType { get { return base_type; } }
		List<Tuple<string, Assembly>> assemblies;
		public IEnumerable<Tuple<string, Assembly>> Assemblies { get { return assemblies; } }

		public Reflection()
		{
			base_type = new NodeItem()
			{
				Type = typeof(Node),
				Name = "Node"
			};
			assemblies = new List<Tuple<string, Assembly>>();
		}

		public void Clear()
		{
			assemblies.Clear();
			base_type.Children.Clear();
		}

		public void AddAssembly(string filename)
		{
			var asm = System.Reflection.Assembly.LoadFile(filename);
			var types = asm.GetTypes();
			var nodes = from item in types where item.IsSubclassOf(typeof(Node)) select item;
			AssignChildren(base_type, nodes);
			assemblies.Add(new Tuple<string,Assembly>(filename, asm));
		}
		public void AssignChildren(NodeItem item, IEnumerable<Type> types)
		{
			foreach (var t in types)
			{
				if (t.BaseType == item.Type)
				{
					NodeItem new_node = new NodeItem()
					{
						Name = t.Name,
						Type = t
					};
					item.Children.Add(new_node);
				}
			}
			foreach (var n in item.Children)
			{
				AssignChildren(n, types);
			}
			item.Children.Sort((a, b) => a.Name.CompareTo(b.Name));
		}
	}
}
