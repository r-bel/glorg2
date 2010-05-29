namespace GlorgIDE
{
	partial class Startup
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Create new project", 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Open existing project", 1);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Startup));
			this.ActionList = new System.Windows.Forms.ListView();
			this.ProjectImages = new System.Windows.Forms.ImageList(this.components);
			this.Okay = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ActionList
			// 
			listViewItem1.Tag = "new";
			listViewItem2.Tag = "open";
			this.ActionList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.ActionList.LargeImageList = this.ProjectImages;
			this.ActionList.Location = new System.Drawing.Point(16, 16);
			this.ActionList.Name = "ActionList";
			this.ActionList.Size = new System.Drawing.Size(400, 216);
			this.ActionList.TabIndex = 2;
			this.ActionList.UseCompatibleStateImageBehavior = false;
			this.ActionList.SelectedIndexChanged += new System.EventHandler(this.ActionList_SelectedIndexChanged);
			this.ActionList.DoubleClick += new System.EventHandler(this.ActionList_DoubleClick);
			// 
			// ProjectImages
			// 
			this.ProjectImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ProjectImages.ImageStream")));
			this.ProjectImages.TransparentColor = System.Drawing.Color.Transparent;
			this.ProjectImages.Images.SetKeyName(0, "042b_AddCategory_48x48_72.png");
			this.ProjectImages.Images.SetKeyName(1, "075b_UpFolder_48x48_72.png");
			this.ProjectImages.Images.SetKeyName(2, "1403_Globe.png");
			// 
			// Okay
			// 
			this.Okay.Location = new System.Drawing.Point(232, 240);
			this.Okay.Name = "Okay";
			this.Okay.Size = new System.Drawing.Size(104, 32);
			this.Okay.TabIndex = 3;
			this.Okay.Text = "OK";
			this.Okay.UseVisualStyleBackColor = true;
			this.Okay.Click += new System.EventHandler(this.Okay_Click);
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(80, 240);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(104, 32);
			this.Cancel.TabIndex = 4;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// Startup
			// 
			this.AcceptButton = this.Okay;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(432, 293);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.Okay);
			this.Controls.Add(this.ActionList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Startup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Glorg IDE Startup";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView ActionList;
		private System.Windows.Forms.Button Okay;
		private System.Windows.Forms.ImageList ProjectImages;
		private System.Windows.Forms.Button Cancel;
	}
}