using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace GlorgIDE
{
	public partial class ReferencesEditor : Form
	{
		List<Tuple<string, Assembly>> filenames;

		public IEnumerable<Tuple<string, Assembly>> Assemblies { get { return filenames; } }

		public ReferencesEditor(IEnumerable<Tuple<string, Assembly>> filenames)
		{
			InitializeComponent();
			this.filenames = filenames.ToList();
			foreach (var item in filenames)
				AddListItem(item.Item2, item.Item1);
			
		}

		

		public void AddListItem(Assembly info, string filename)
		{
			string version = "1.0";
			string name = info.FullName;
			var ver = info.GetCustomAttributesData();
			foreach (var item in ver)
			{
				var t = item.Constructor.ReflectedType;
				if (t == typeof(AssemblyVersionAttribute))
				{
					version = item.ConstructorArguments[0].Value.ToString();
				}
				else if (t == typeof(AssemblyTitleAttribute))
				{
					name = item.ConstructorArguments[0].Value.ToString();
				}
			}
			
			/*if (ver.Length > 0)
			{
				AssemblyVersionAttribute attr = ver[0] as AssemblyVersionAttribute;
				version = attr.Version.ToString();
			}
			var nm = info.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
			if (nm.Length > 0)
			{
				AssemblyTitleAttribute attr = nm[0] as AssemblyTitleAttribute;
				name = attr.Title;
			}*/
			ListViewItem ls = new ListViewItem(name);
			ls.SubItems.Add(version);
			ls.SubItems.Add(filename);
			AssemblyList.Items.Add(ls);
		}

		public void AddAssembly(string filename)
		{
			try
			{
				string fn = System.IO.Path.GetFileName(filename);
				foreach (var item in filenames)
					if (item.Item1.ToLower() == fn.ToLower())
						return;
				var asm = Assembly.ReflectionOnlyLoadFrom(filename);
				AddListItem(asm, fn);
				filenames.Add(new Tuple<string, Assembly>(filename, asm));
			}
			catch (Exception ex)
			{
				MessageBox.Show("Unable to load assembly.\n" + ex.Message);
			}
		}

		private void AddRef_Click(object sender, EventArgs e)
		{
			if (OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				AddAssembly(OpenFile.FileName);
			}
		}
		
	}
}
