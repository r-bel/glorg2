using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlorgIDE
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Startup start = new Startup();

			


			if (start.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
				return;

			string new_file = "";
			if (start.Action == "new")
			{
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Title = "Select project save location";
				dlg.Filter = "Glorg IDE Project files (*.glp)|*.glp";
				while (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
				{
					var ret = MessageBox.Show("Do you really want to exit Glorg IDE?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
					if (ret == System.Windows.Forms.DialogResult.Yes)
						return;
				}
				new_file = dlg.FileName;
			}
			MainForm main = new MainForm();
			if (!string.IsNullOrEmpty(new_file))
				main.NewProject(new_file);
			else
				main.LoadProject(start.Action);
			Application.Run(main);
			Properties.Settings.Default.Save();
		}
	}
}
