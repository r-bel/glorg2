using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Glorg2.Scene;
using Glorg2.Graphics;
using Glorg2;

namespace Glorg2.Debugging
{
	public class ResourceMonitor : Form
	{
		public static void Monitor(DynamicNode n)
		{
			monitor m = new monitor(n);
			m.Show();
		}

		private class monitor : Form
		{
			System.Reflection.PropertyInfo[] props;

			TabControl tc;
				TabPage tp_props;
			PropertyGrid pg_props;
			
			Timer t;

			public monitor(DynamicNode n)
			{
				//MessageBox.Show(n.Owner.ToString());

				props = n.GetType().GetProperties();

				this.Text = "Resource monitor - " + n.ToString();
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
				this.ClientSize = new System.Drawing.Size(480, 640);
				this.StartPosition = FormStartPosition.Manual;
				//this.Location = new Point(this.Parent

				// TABS
				tc = new TabControl();
				tc.Dock = DockStyle.Fill;
				tp_props = new TabPage();
				tc.Controls.AddRange(new Control[] { tp_props });

				// Datagrid for properties
				pg_props = new PropertyGrid();
				pg_props.SelectedObject = n;
				pg_props.Dock = DockStyle.Fill;
				tp_props.Controls.Add(pg_props);

				t = new Timer();
				t.Interval = 100;
				t.Tick += delegate(object sender, EventArgs e) { pg_props.Refresh(); };
				t.Start();

				this.Controls.Add(tc);
			}
		}
	}
}