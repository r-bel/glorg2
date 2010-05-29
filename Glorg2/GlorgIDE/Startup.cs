using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlorgIDE
{
	public partial class Startup : Form
	{
		private string action;

		public string Action { get { return action; } }

		public Startup()
		{
			InitializeComponent();
			if (Properties.Settings.Default.RecentFileList != null)
			{
				foreach (string file in Properties.Settings.Default.RecentFileList)
				{
					ListViewItem item = new ListViewItem(System.IO.Path.GetFileNameWithoutExtension(file));
					item.ImageIndex = 2;
					item.Tag = file;
					ActionList.Items.Add(item);
				}
			}
			else
				Properties.Settings.Default.RecentFileList = new System.Collections.Specialized.StringCollection();
		}

		private void ActionList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ActionList.SelectedItems.Count > 0)
			{
				action = (ActionList.SelectedItems[0].Tag as string) ?? "";
				Okay.Enabled = true;
			}
			else
				Okay.Enabled = false;
			
		}

		private void Okay_Click(object sender, EventArgs e)
		{
			if (action == "open")
			{
				var dlg = new OpenFileDialog();
				dlg.Filter = "Glorg IDE Project (*.glp)|*.glp";
				if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					action = dlg.FileName;
					DialogResult = System.Windows.Forms.DialogResult.OK;
					Close();
				}
			}
			else
			{
				DialogResult = System.Windows.Forms.DialogResult.OK;
				Close();
			}
		}

		private void ActionList_DoubleClick(object sender, EventArgs e)
		{
			Okay.PerformClick();
		}
	}
}
