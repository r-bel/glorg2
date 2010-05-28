using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Glorg2.Resource;
namespace GlorgIDE
{
	public partial class ResourceViewer : Form
	{
		ResourceManager manager;
		public ResourceViewer(ResourceManager man)
		{
			manager = man;
			InitializeComponent();

			foreach (var res in man.Resources)
				AddResource(res);
		}

		public void AddResource(Resource res)
		{
			ListViewItem item = new ListViewItem(res.SourceName);
			item.SubItems.Add(res.GetType().Name);
			item.SubItems.Add(res.Links.ToString());
			ResourceList.Items.Add(item);
		}
	}
}
