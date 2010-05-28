namespace GlorgIDE
{
	partial class ResourceViewer
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
			this.ResourceList = new System.Windows.Forms.ListView();
			this.CloseButton = new System.Windows.Forms.Button();
			this.ResName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ResType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ResLinks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// ResourceList
			// 
			this.ResourceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ResName,
            this.ResType,
            this.ResLinks});
			this.ResourceList.Location = new System.Drawing.Point(8, 8);
			this.ResourceList.Name = "ResourceList";
			this.ResourceList.Size = new System.Drawing.Size(376, 232);
			this.ResourceList.TabIndex = 0;
			this.ResourceList.UseCompatibleStateImageBehavior = false;
			this.ResourceList.View = System.Windows.Forms.View.Details;
			// 
			// CloseButton
			// 
			this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseButton.Location = new System.Drawing.Point(264, 248);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(123, 32);
			this.CloseButton.TabIndex = 1;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			// 
			// ResName
			// 
			this.ResName.Text = "Name";
			this.ResName.Width = 116;
			// 
			// ResType
			// 
			this.ResType.Text = "Type";
			this.ResType.Width = 181;
			// 
			// ResLinks
			// 
			this.ResLinks.Text = "Links";
			this.ResLinks.Width = 73;
			// 
			// ResourceViewer
			// 
			this.AcceptButton = this.CloseButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(393, 293);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.ResourceList);
			this.Name = "ResourceViewer";
			this.Text = "ResourceViewer";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView ResourceList;
		private System.Windows.Forms.ColumnHeader ResName;
		private System.Windows.Forms.ColumnHeader ResType;
		private System.Windows.Forms.ColumnHeader ResLinks;
		private System.Windows.Forms.Button CloseButton;
	}
}