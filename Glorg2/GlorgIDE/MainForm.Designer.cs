namespace GlorgIDE
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.Status = new System.Windows.Forms.StatusStrip();
			this.StateDisplay = new System.Windows.Forms.ToolStripStatusLabel();
			this.Progress = new System.Windows.Forms.ToolStripProgressBar();
			this.PositionDisplay = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.ProjectView = new System.Windows.Forms.TreeView();
			this.ProjectContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddProjectNode = new System.Windows.Forms.ToolStripMenuItem();
			this.AddLevelNode = new System.Windows.Forms.ToolStripMenuItem();
			this.AddSubNode = new System.Windows.Forms.ToolStripMenuItem();
			this.DeleteProjectNode = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.MoveNodeUp = new System.Windows.Forms.ToolStripMenuItem();
			this.MoveNodeDown = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer5 = new System.Windows.Forms.SplitContainer();
			this.RenderOutput = new System.Windows.Forms.PictureBox();
			this.Output = new System.Windows.Forms.RichTextBox();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.ObjectProperties = new System.Windows.Forms.PropertyGrid();
			this.ClassTree = new System.Windows.Forms.TreeView();
			this.ClassImgList = new System.Windows.Forms.ImageList(this.components);
			this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.FileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.FileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.FileSplit0 = new System.Windows.Forms.ToolStripSeparator();
			this.FileReferences = new System.Windows.Forms.ToolStripMenuItem();
			this.FileProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.FileSplit1 = new System.Windows.Forms.ToolStripSeparator();
			this.FileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.EditCut = new System.Windows.Forms.ToolStripMenuItem();
			this.EditCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.EditPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.EditSplit0 = new System.Windows.Forms.ToolStripSeparator();
			this.EditDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewMode = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewModeShadedLight = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewModeShaded = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewModeWireframe = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewBoundingBox = new System.Windows.Forms.ToolStripMenuItem();
			this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showLoadedResourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Tooltip = new System.Windows.Forms.ToolTip(this.components);
			this.SaveFile = new System.Windows.Forms.SaveFileDialog();
			this.OpenFile = new System.Windows.Forms.OpenFileDialog();
			this.ResourcesMaps = new System.Windows.Forms.ToolStripMenuItem();
			this.ResourcesMapsNew = new System.Windows.Forms.ToolStripMenuItem();
			this.ResourcesMapsOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.ResourcesMapsAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.Status.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.ProjectContext.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
			this.splitContainer5.Panel1.SuspendLayout();
			this.splitContainer5.Panel2.SuspendLayout();
			this.splitContainer5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RenderOutput)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			this.MainMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.Status);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(888, 433);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(888, 479);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MainMenuStrip);
			// 
			// Status
			// 
			this.Status.Dock = System.Windows.Forms.DockStyle.None;
			this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StateDisplay,
            this.Progress,
            this.PositionDisplay});
			this.Status.Location = new System.Drawing.Point(0, 0);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(888, 22);
			this.Status.TabIndex = 0;
			// 
			// StateDisplay
			// 
			this.StateDisplay.Name = "StateDisplay";
			this.StateDisplay.Size = new System.Drawing.Size(39, 17);
			this.StateDisplay.Text = "Ready";
			// 
			// Progress
			// 
			this.Progress.Name = "Progress";
			this.Progress.Size = new System.Drawing.Size(100, 16);
			// 
			// PositionDisplay
			// 
			this.PositionDisplay.Name = "PositionDisplay";
			this.PositionDisplay.Size = new System.Drawing.Size(13, 17);
			this.PositionDisplay.Text = "0";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(888, 433);
			this.splitContainer1.SplitterDistance = 161;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.ProjectView);
			this.splitContainer3.Size = new System.Drawing.Size(161, 433);
			this.splitContainer3.SplitterDistance = 201;
			this.splitContainer3.TabIndex = 0;
			// 
			// ProjectView
			// 
			this.ProjectView.ContextMenuStrip = this.ProjectContext;
			this.ProjectView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProjectView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.ProjectView.FullRowSelect = true;
			this.ProjectView.HotTracking = true;
			this.ProjectView.Location = new System.Drawing.Point(0, 0);
			this.ProjectView.Name = "ProjectView";
			this.ProjectView.ShowLines = false;
			this.ProjectView.Size = new System.Drawing.Size(161, 228);
			this.ProjectView.TabIndex = 0;
			this.Tooltip.SetToolTip(this.ProjectView, "Project view\r\nDisplayes your scene in an hierarchical mode.");
			this.ProjectView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.ProjectView_DrawNode);
			this.ProjectView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ProjectView_AfterSelect);
			this.ProjectView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ProjectView_NodeMouseClick);
			// 
			// ProjectContext
			// 
			this.ProjectContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddProjectNode,
            this.DeleteProjectNode,
            this.toolStripMenuItem1,
            this.MoveNodeUp,
            this.MoveNodeDown});
			this.ProjectContext.Name = "ProjectContext";
			this.ProjectContext.Size = new System.Drawing.Size(140, 98);
			this.ProjectContext.Opening += new System.ComponentModel.CancelEventHandler(this.ProjectContext_Opening);
			// 
			// AddProjectNode
			// 
			this.AddProjectNode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddLevelNode,
            this.AddSubNode});
			this.AddProjectNode.Name = "AddProjectNode";
			this.AddProjectNode.Size = new System.Drawing.Size(139, 22);
			this.AddProjectNode.Text = "Add (...)";
			// 
			// AddLevelNode
			// 
			this.AddLevelNode.Name = "AddLevelNode";
			this.AddLevelNode.Size = new System.Drawing.Size(130, 22);
			this.AddLevelNode.Text = "Same level";
			this.AddLevelNode.Click += new System.EventHandler(this.AddLevelNode_Click);
			// 
			// AddSubNode
			// 
			this.AddSubNode.Name = "AddSubNode";
			this.AddSubNode.Size = new System.Drawing.Size(130, 22);
			this.AddSubNode.Text = "Sub node";
			this.AddSubNode.Click += new System.EventHandler(this.AddSubNode_Click);
			// 
			// DeleteProjectNode
			// 
			this.DeleteProjectNode.Name = "DeleteProjectNode";
			this.DeleteProjectNode.Size = new System.Drawing.Size(139, 22);
			this.DeleteProjectNode.Text = "Delete Node";
			this.DeleteProjectNode.Click += new System.EventHandler(this.DeleteProjectNode_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 6);
			// 
			// MoveNodeUp
			// 
			this.MoveNodeUp.Name = "MoveNodeUp";
			this.MoveNodeUp.Size = new System.Drawing.Size(139, 22);
			this.MoveNodeUp.Text = "Move up";
			// 
			// MoveNodeDown
			// 
			this.MoveNodeDown.Name = "MoveNodeDown";
			this.MoveNodeDown.Size = new System.Drawing.Size(139, 22);
			this.MoveNodeDown.Text = "Move down";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer5);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
			this.splitContainer2.Size = new System.Drawing.Size(723, 433);
			this.splitContainer2.SplitterDistance = 538;
			this.splitContainer2.TabIndex = 0;
			// 
			// splitContainer5
			// 
			this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer5.Location = new System.Drawing.Point(0, 0);
			this.splitContainer5.Name = "splitContainer5";
			this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer5.Panel1
			// 
			this.splitContainer5.Panel1.Controls.Add(this.RenderOutput);
			// 
			// splitContainer5.Panel2
			// 
			this.splitContainer5.Panel2.Controls.Add(this.Output);
			this.splitContainer5.Size = new System.Drawing.Size(538, 433);
			this.splitContainer5.SplitterDistance = 344;
			this.splitContainer5.TabIndex = 0;
			// 
			// RenderOutput
			// 
			this.RenderOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.RenderOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RenderOutput.Location = new System.Drawing.Point(0, 0);
			this.RenderOutput.Name = "RenderOutput";
			this.RenderOutput.Size = new System.Drawing.Size(538, 344);
			this.RenderOutput.TabIndex = 1;
			this.RenderOutput.TabStop = false;
			this.RenderOutput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RenderOutput_MouseDown);
			this.RenderOutput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RenderOutput_MouseMove);
			// 
			// Output
			// 
			this.Output.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Output.Location = new System.Drawing.Point(0, 0);
			this.Output.Name = "Output";
			this.Output.Size = new System.Drawing.Size(538, 85);
			this.Output.TabIndex = 0;
			this.Output.Text = "";
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.ObjectProperties);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.ClassTree);
			this.splitContainer4.Size = new System.Drawing.Size(181, 433);
			this.splitContainer4.SplitterDistance = 197;
			this.splitContainer4.TabIndex = 0;
			// 
			// ObjectProperties
			// 
			this.ObjectProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ObjectProperties.Location = new System.Drawing.Point(0, 0);
			this.ObjectProperties.Name = "ObjectProperties";
			this.ObjectProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.ObjectProperties.Size = new System.Drawing.Size(181, 197);
			this.ObjectProperties.TabIndex = 0;
			this.ObjectProperties.ToolbarVisible = false;
			this.Tooltip.SetToolTip(this.ObjectProperties, "Property grid\r\nAllows you to set properties for the current selected object.");
			// 
			// ClassTree
			// 
			this.ClassTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ClassTree.FullRowSelect = true;
			this.ClassTree.ImageIndex = 0;
			this.ClassTree.ImageList = this.ClassImgList;
			this.ClassTree.Location = new System.Drawing.Point(0, 0);
			this.ClassTree.Name = "ClassTree";
			this.ClassTree.SelectedImageIndex = 0;
			this.ClassTree.Size = new System.Drawing.Size(181, 232);
			this.ClassTree.TabIndex = 0;
			this.Tooltip.SetToolTip(this.ClassTree, "Class view\r\nThis window shows all classes that are available for your project.");
			// 
			// ClassImgList
			// 
			this.ClassImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ClassImgList.ImageStream")));
			this.ClassImgList.TransparentColor = System.Drawing.Color.Transparent;
			this.ClassImgList.Images.SetKeyName(0, "EntityDataModel_entity_type_16x16.png");
			// 
			// MainMenuStrip
			// 
			this.MainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.ViewMenu,
            this.simulationToolStripMenuItem,
            this.resourcesToolStripMenuItem});
			this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MainMenuStrip.Name = "MainMenuStrip";
			this.MainMenuStrip.Size = new System.Drawing.Size(888, 24);
			this.MainMenuStrip.TabIndex = 0;
			this.MainMenuStrip.Text = "menuStrip1";
			// 
			// FileMenu
			// 
			this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNew,
            this.FileOpen,
            this.FileSave,
            this.FileSaveAs,
            this.FileSplit0,
            this.FileReferences,
            this.FileProperties,
            this.FileSplit1,
            this.FileExit});
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(37, 20);
			this.FileMenu.Text = "&File";
			// 
			// FileNew
			// 
			this.FileNew.Name = "FileNew";
			this.FileNew.Size = new System.Drawing.Size(167, 22);
			this.FileNew.Text = "&New";
			// 
			// FileOpen
			// 
			this.FileOpen.Name = "FileOpen";
			this.FileOpen.Size = new System.Drawing.Size(167, 22);
			this.FileOpen.Text = "&Open";
			// 
			// FileSave
			// 
			this.FileSave.Name = "FileSave";
			this.FileSave.Size = new System.Drawing.Size(167, 22);
			this.FileSave.Text = "&Save";
			// 
			// FileSaveAs
			// 
			this.FileSaveAs.Name = "FileSaveAs";
			this.FileSaveAs.Size = new System.Drawing.Size(167, 22);
			this.FileSaveAs.Text = "Save &as";
			// 
			// FileSplit0
			// 
			this.FileSplit0.Name = "FileSplit0";
			this.FileSplit0.Size = new System.Drawing.Size(164, 6);
			// 
			// FileReferences
			// 
			this.FileReferences.Name = "FileReferences";
			this.FileReferences.Size = new System.Drawing.Size(167, 22);
			this.FileReferences.Text = "References";
			this.FileReferences.Click += new System.EventHandler(this.FileReferences_Click);
			// 
			// FileProperties
			// 
			this.FileProperties.Name = "FileProperties";
			this.FileProperties.Size = new System.Drawing.Size(167, 22);
			this.FileProperties.Text = "Project properties";
			// 
			// FileSplit1
			// 
			this.FileSplit1.Name = "FileSplit1";
			this.FileSplit1.Size = new System.Drawing.Size(164, 6);
			// 
			// FileExit
			// 
			this.FileExit.Name = "FileExit";
			this.FileExit.Size = new System.Drawing.Size(167, 22);
			this.FileExit.Text = "E&xit";
			// 
			// EditMenu
			// 
			this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditCut,
            this.EditCopy,
            this.EditPaste,
            this.EditSplit0,
            this.EditDelete});
			this.EditMenu.Name = "EditMenu";
			this.EditMenu.Size = new System.Drawing.Size(39, 20);
			this.EditMenu.Text = "&Edit";
			// 
			// EditCut
			// 
			this.EditCut.Name = "EditCut";
			this.EditCut.Size = new System.Drawing.Size(107, 22);
			this.EditCut.Text = "Cut";
			// 
			// EditCopy
			// 
			this.EditCopy.Name = "EditCopy";
			this.EditCopy.Size = new System.Drawing.Size(107, 22);
			this.EditCopy.Text = "Copy";
			// 
			// EditPaste
			// 
			this.EditPaste.Name = "EditPaste";
			this.EditPaste.Size = new System.Drawing.Size(107, 22);
			this.EditPaste.Text = "Paste";
			// 
			// EditSplit0
			// 
			this.EditSplit0.Name = "EditSplit0";
			this.EditSplit0.Size = new System.Drawing.Size(104, 6);
			// 
			// EditDelete
			// 
			this.EditDelete.Name = "EditDelete";
			this.EditDelete.Size = new System.Drawing.Size(107, 22);
			this.EditDelete.Text = "Delete";
			// 
			// ViewMenu
			// 
			this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewMode,
            this.ViewBoundingBox});
			this.ViewMenu.Name = "ViewMenu";
			this.ViewMenu.Size = new System.Drawing.Size(44, 20);
			this.ViewMenu.Text = "&View";
			// 
			// ViewMode
			// 
			this.ViewMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewModeShadedLight,
            this.ViewModeShaded,
            this.ViewModeWireframe});
			this.ViewMode.Name = "ViewMode";
			this.ViewMode.Size = new System.Drawing.Size(145, 22);
			this.ViewMode.Text = "Mode";
			// 
			// ViewModeShadedLight
			// 
			this.ViewModeShadedLight.Name = "ViewModeShadedLight";
			this.ViewModeShadedLight.Size = new System.Drawing.Size(171, 22);
			this.ViewModeShadedLight.Text = "Shaded + Lighting";
			this.ViewModeShadedLight.ToolTipText = "Render with full shading and lighting";
			// 
			// ViewModeShaded
			// 
			this.ViewModeShaded.Name = "ViewModeShaded";
			this.ViewModeShaded.Size = new System.Drawing.Size(171, 22);
			this.ViewModeShaded.Text = "Shaded";
			this.ViewModeShaded.ToolTipText = "Render with shading";
			// 
			// ViewModeWireframe
			// 
			this.ViewModeWireframe.Name = "ViewModeWireframe";
			this.ViewModeWireframe.Size = new System.Drawing.Size(171, 22);
			this.ViewModeWireframe.Text = "Wireframe";
			this.ViewModeWireframe.ToolTipText = "Render with wireframe";
			// 
			// ViewBoundingBox
			// 
			this.ViewBoundingBox.Name = "ViewBoundingBox";
			this.ViewBoundingBox.Size = new System.Drawing.Size(145, 22);
			this.ViewBoundingBox.Text = "Boundingbox";
			this.ViewBoundingBox.ToolTipText = "Show bounding box for all objects";
			// 
			// simulationToolStripMenuItem
			// 
			this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.stopToolStripMenuItem});
			this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
			this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.simulationToolStripMenuItem.Text = "&Simulation";
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.runToolStripMenuItem.Text = "Run";
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.stopToolStripMenuItem.Text = "Stop";
			// 
			// resourcesToolStripMenuItem
			// 
			this.resourcesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLoadedResourcesToolStripMenuItem,
            this.ResourcesMaps});
			this.resourcesToolStripMenuItem.Name = "resourcesToolStripMenuItem";
			this.resourcesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
			this.resourcesToolStripMenuItem.Text = "&Resources";
			// 
			// showLoadedResourcesToolStripMenuItem
			// 
			this.showLoadedResourcesToolStripMenuItem.Name = "showLoadedResourcesToolStripMenuItem";
			this.showLoadedResourcesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.showLoadedResourcesToolStripMenuItem.Text = "Show loaded resources";
			this.showLoadedResourcesToolStripMenuItem.Click += new System.EventHandler(this.showLoadedResourcesToolStripMenuItem_Click);
			// 
			// ResourcesMaps
			// 
			this.ResourcesMaps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResourcesMapsNew,
            this.ResourcesMapsAdd,
            this.ResourcesMapsOpen});
			this.ResourcesMaps.Name = "ResourcesMaps";
			this.ResourcesMaps.Size = new System.Drawing.Size(195, 22);
			this.ResourcesMaps.Text = "Maps";
			// 
			// ResourcesMapsNew
			// 
			this.ResourcesMapsNew.Name = "ResourcesMapsNew";
			this.ResourcesMapsNew.Size = new System.Drawing.Size(152, 22);
			this.ResourcesMapsNew.Text = "New map";
			// 
			// ResourcesMapsOpen
			// 
			this.ResourcesMapsOpen.Name = "ResourcesMapsOpen";
			this.ResourcesMapsOpen.Size = new System.Drawing.Size(152, 22);
			this.ResourcesMapsOpen.Text = "Open";
			// 
			// ResourcesMapsAdd
			// 
			this.ResourcesMapsAdd.Name = "ResourcesMapsAdd";
			this.ResourcesMapsAdd.Size = new System.Drawing.Size(152, 22);
			this.ResourcesMapsAdd.Text = "Add existing";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(888, 479);
			this.Controls.Add(this.toolStripContainer1);
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.Text = "Glorg IDE";
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.Status.ResumeLayout(false);
			this.Status.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.ProjectContext.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer5.Panel1.ResumeLayout(false);
			this.splitContainer5.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
			this.splitContainer5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.RenderOutput)).EndInit();
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			this.MainMenuStrip.ResumeLayout(false);
			this.MainMenuStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.TreeView ProjectView;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.PropertyGrid ObjectProperties;
		private System.Windows.Forms.TreeView ClassTree;
		private System.Windows.Forms.ToolTip Tooltip;
		private System.Windows.Forms.MenuStrip MainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileMenu;
		private System.Windows.Forms.ToolStripMenuItem FileNew;
		private System.Windows.Forms.ToolStripMenuItem FileOpen;
		private System.Windows.Forms.ToolStripMenuItem FileSave;
		private System.Windows.Forms.ToolStripMenuItem FileSaveAs;
		private System.Windows.Forms.ToolStripSeparator FileSplit0;
		private System.Windows.Forms.ToolStripMenuItem FileProperties;
		private System.Windows.Forms.ToolStripSeparator FileSplit1;
		private System.Windows.Forms.ToolStripMenuItem FileExit;
		private System.Windows.Forms.ToolStripMenuItem EditMenu;
		private System.Windows.Forms.ToolStripMenuItem EditCut;
		private System.Windows.Forms.ToolStripMenuItem EditCopy;
		private System.Windows.Forms.ToolStripMenuItem EditPaste;
		private System.Windows.Forms.ToolStripSeparator EditSplit0;
		private System.Windows.Forms.ToolStripMenuItem EditDelete;
		private System.Windows.Forms.ToolStripMenuItem ViewMenu;
		private System.Windows.Forms.ToolStripMenuItem ViewMode;
		private System.Windows.Forms.ToolStripMenuItem ViewModeShadedLight;
		private System.Windows.Forms.ToolStripMenuItem ViewModeShaded;
		private System.Windows.Forms.ToolStripMenuItem ViewModeWireframe;
		private System.Windows.Forms.ToolStripMenuItem ViewBoundingBox;
		private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resourcesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showLoadedResourcesToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer5;
		private System.Windows.Forms.PictureBox RenderOutput;
		private System.Windows.Forms.RichTextBox Output;
		private System.Windows.Forms.ToolStripMenuItem FileReferences;
		private System.Windows.Forms.ImageList ClassImgList;
		private System.Windows.Forms.StatusStrip Status;
		private System.Windows.Forms.ToolStripStatusLabel StateDisplay;
		private System.Windows.Forms.ToolStripProgressBar Progress;
		private System.Windows.Forms.ToolStripStatusLabel PositionDisplay;
		private System.Windows.Forms.SaveFileDialog SaveFile;
		private System.Windows.Forms.OpenFileDialog OpenFile;
		private System.Windows.Forms.ContextMenuStrip ProjectContext;
		private System.Windows.Forms.ToolStripMenuItem AddProjectNode;
		private System.Windows.Forms.ToolStripMenuItem AddLevelNode;
		private System.Windows.Forms.ToolStripMenuItem AddSubNode;
		private System.Windows.Forms.ToolStripMenuItem DeleteProjectNode;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem MoveNodeUp;
		private System.Windows.Forms.ToolStripMenuItem MoveNodeDown;
		private System.Windows.Forms.ToolStripMenuItem ResourcesMaps;
		private System.Windows.Forms.ToolStripMenuItem ResourcesMapsNew;
		private System.Windows.Forms.ToolStripMenuItem ResourcesMapsAdd;
		private System.Windows.Forms.ToolStripMenuItem ResourcesMapsOpen;

	}
}

