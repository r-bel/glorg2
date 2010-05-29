using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Glorg2;
using Glorg2.Scene;

namespace GlorgIDE
{
	public partial class MainForm : Form
	{
		Reflection refl;
		GlorgProject current_project;
		IDEApp file;
		Plane default_plane;

		private string filename;
		private string folder;

		System.Diagnostics.Process this_process;
		public MainForm()
		{


			default_plane = new Plane()
			{
				Position = new Vector3(),
				Normal = Vector3.Up,
				Distance = 0
			};

			InitializeComponent();
			this_process = System.Diagnostics.Process.GetCurrentProcess();
			this_process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(this_process_OutputDataReceived);
			refl = new Reflection();
			refl.AddAssembly(System.IO.Path.Combine(Application.StartupPath, "Glorg2.dll"));
			UpdateTypes();


		}

		public void UpdateTypes()
		{
			ClassTree.Nodes.Clear();
			ClassTree.Nodes.Add(CreateTypeNode(refl.BaseType));
		}

		public void UpdateScene()
		{
			ProjectView.Nodes.Clear();
			ProjectView.Nodes.Add(CreateObjectNode(file.Scene.ParentNode));
		}

		public TreeNode CreateTypeNode(NodeItem item)
		{
			TreeNode result = new TreeNode(item.Name);
			
			result.Tag = item.Type;
			foreach (var child in item.Children)
			{
				result.Nodes.Add(CreateTypeNode(child));
			}
			return result;
		}

		public TreeNode CreateObjectNode(Node nd)
		{
			TreeNode result = new TreeNode(nd.Name);
			result.Tag = nd;
			foreach (var child in nd.Children)
			{
				result.Nodes.Add(CreateObjectNode(child));
			}
			return result;
		}

		void this_process_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
		{
			Output.Text += e.Data;
		}
		
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			file = new IDEApp();
			file.StartAsync(RenderOutput);
			UpdateScene();
		}

		public void LoadProject(string filename)
		{
			this.filename = filename;
			var bin = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var sr = System.IO.File.OpenRead(filename))
			{
				current_project = bin.Deserialize(sr) as GlorgProject;
			}
			folder = System.IO.Path.GetDirectoryName(filename);
		}

		public void NewProject(string filename)
		{
			current_project = new GlorgProject();
			this.filename = filename;
			var bin = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var sr = System.IO.File.OpenWrite(filename))
			{
				bin.Serialize(sr, current_project);
			}
			if(!Properties.Settings.Default.RecentFileList.Contains(filename))
				Properties.Settings.Default.RecentFileList.Add(filename);
			folder = System.IO.Path.GetDirectoryName(filename);
		}

		public void SaveMap()
		{
			string fname = System.IO.Path.Combine(folder, (file.Scene.ParentNode as WorldSpawn).Name + ".gmp");
			using (var sr = System.IO.File.OpenWrite(fname))
			{
				file.Scene.ToStream(sr);
			}
		}
		public void LoadMap(string name)
		{
			string fname = System.IO.Path.Combine(folder, name + ".gmp");
			using (var sr = System.IO.File.OpenRead(fname))
			{
				file.Scene = Scene.FromStream(sr, file);
			}
		}

		public void SaveProject()
		{
			var bin = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var sr = System.IO.File.OpenWrite(filename))
			{
				bin.Serialize(sr, current_project);
			}
		}

		public void AddNode(TreeNode node, Node nd)
		{
			if (node != null)
			{
				(node.Tag as Node).Add(nd);
				node.Nodes.Add(CreateObjectNode(nd));
			}
		}
		public void RemoveNode(TreeNode node)
		{
			if (node != null && node.Parent != null)
			{
				(node.Tag as Node).Parent.Remove(node.Tag as Node);
				node.Remove();
			}
		}


		Vector2 pos = new Vector2();
		int mx, my;
		private void RenderOutput_MouseDown(object sender, MouseEventArgs e)
		{
			mx = e.X;
			my = e.Y;
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}
		private void RenderOutput_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				int dx = e.X - mx;
				int dy = e.Y - my;
				var vec = new Vector2();
				if (dx != 0)
					vec.x = dx / 256f;
				if (dy != 0)
					vec.y = dy / 256f;
				float mag = vec.Length;
				if (mag != 0)
				{
					this.pos += vec;
					file.Scene.Camera.Orientation = Quaternion.FromAxisAngle(this.pos.x, new Vector3(0, 1, 0)) * Quaternion.FromAxisAngle(this.pos.y, new Vector3(1, 0, 0)) ;
				}
				mx = e.X;
				my = e.Y;
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
			{
				int dx = e.X - mx;
				int dy = e.Y - my;
				var vec = new Vector2();
				if (dx != 0)
					vec.x = -dx / 32f;
				if (dy != 0)
					vec.y = dy / 32f;
				float mag = vec.Length;
				if (mag != 0)
				{
					var mat = file.Scene.Camera.Orientation.ToMatrix();
					mat *= Matrix.Translate(vec.x, vec.y, 0);
					file.Scene.Camera.Position += mat.Translation;
				}
				mx = e.X; my = e.Y;
			}
			else if (e.Button == (System.Windows.Forms.MouseButtons.Left | System.Windows.Forms.MouseButtons.Right))
			{
				float delta = -(e.Y - my) / 4f;
				var dir = file.Scene.Camera.Orientation.ToNormal();
				file.Scene.Camera.Position += (dir * delta).ToVector4();
				mx = e.X; my = e.Y;
			}
			//Vector3 near, far;
			//var pos = file.Scene.Unproject(new Vector2(e.X, e.Y), 1f);
			//GetMouseRay(new Vector2(e.X, e.Y), out near, out far);
			//file.cursor.Position = pos.ToVector4();
			//PositionDisplay.Text = pos.ToString();
		
		}

		public bool GetMouseRay(Vector2 mpos, out Vector3 near, out Vector3 far)
		{
			Vector2 screen = new Vector2();
			screen.X = ((2f * mpos.X) / RenderOutput.ClientSize.Width) - 1;
			screen.Y = -((2f * mpos.Y) / RenderOutput.ClientSize.Height) - 1;

			// Inverse View/Proj Matrix
			Matrix proj = file.Scene.Camera.GetProjectionMatrix().Invert();
			Matrix view = file.Scene.Camera.GetTransform();
			Matrix final = proj * view;
			// Near/Far Point
			Vector3 near_pos = new Vector3(screen.X, screen.Y, 0);
			Vector3 far_pos = new Vector3(screen.X, screen.Y, .5f);



			near = final * near_pos;
			far = final * far_pos;

			return true;
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
		}

		private void FileReferences_Click(object sender, EventArgs e)
		{
			ReferencesEditor edit = new ReferencesEditor(refl.Assemblies);
			if (edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				refl.Clear();
				foreach (var item in edit.Assemblies)
				{
					refl.AddAssembly(item.Item1);
				}
				current_project.References.Clear();
				current_project.References.AddRange(refl.Assemblies.Select(item => item.Item1));
				UpdateTypes();
			}
		}

		private void ProjectContext_Opening(object sender, CancelEventArgs e)
		{
			if (ClassTree.SelectedNode != null)
			{
				AddProjectNode.Text = "Add " + ClassTree.SelectedNode.Text;
				AddProjectNode.Enabled = true;
			}
			else
			{
				AddProjectNode.Text = "Add";
				AddProjectNode.Enabled = false;
			}
			AddLevelNode.Enabled = ProjectView.SelectedNode != null && ProjectView.SelectedNode.Parent != null;
			AddSubNode.Enabled = ProjectView.SelectedNode != null;
			DeleteProjectNode.Enabled = ProjectView.SelectedNode != null;
			MoveNodeUp.Enabled =  ProjectView.SelectedNode != null;
			MoveNodeDown.Enabled = ProjectView.SelectedNode != null;

		}

		private void DeleteProjectNode_Click(object sender, EventArgs e)
		{
			RemoveNode(ProjectView.SelectedNode);
		}

		private void ProjectView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			
		}

		private void UpdateSelection()
		{
			if (ProjectView.SelectedNode != null)
			{
				ProjectView.SelectedNode.Text = (ProjectView.SelectedNode.Tag as Node).Name;
			}
		}

		private void AddSubNode_Click(object sender, EventArgs e)
		{
			Type type = ClassTree.SelectedNode.Tag as Type;
			if (type != null)
			{
				Node nd = Activator.CreateInstance(type) as Node;
				AddNode(ProjectView.SelectedNode, nd);
			}
		}

		private void AddLevelNode_Click(object sender, EventArgs e)
		{
			Type type = ClassTree.SelectedNode.Tag as Type;
			if (type != null)
			{
				Node nd = Activator.CreateInstance(type) as Node;
				AddNode(ProjectView.SelectedNode.Parent, nd);
			}
		}

		private void ProjectView_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			var g = e.Graphics;
			var n = e.Node.Tag as Node;
			var s = g.MeasureString(n.Name, ProjectView.Font);
			g.DrawString(n.Name, ProjectView.Font, Brushes.Black, e.Bounds.Location);
			g.DrawString(n.GetType().Name, ProjectView.Font, Brushes.Silver, e.Bounds.X + s.Width, e.Bounds.Y);
		}

		private void showLoadedResourcesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ResourceViewer view = new ResourceViewer(file.Scene.Resources);
			view.ShowDialog();
		}

		private void ProjectView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			ObjectProperties.SelectedObject = ProjectView.SelectedNode != null ? ProjectView.SelectedNode.Tag : null;
		}

		private void DebugTimer_Tick(object sender, EventArgs e)
		{
			string v = null;
			while ((v = Glorg2.Debugging.Debug.ReadLine()) != null)
				Output.AppendText(v + Environment.NewLine);
		}

		private void FileExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
