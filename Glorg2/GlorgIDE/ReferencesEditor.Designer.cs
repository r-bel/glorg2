namespace GlorgIDE
{
	partial class ReferencesEditor
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
			this.AssemblyList = new System.Windows.Forms.ListView();
			this.RefName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.AsmVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.AsmFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.RemoveRef = new System.Windows.Forms.Button();
			this.AddRef = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.Okay = new System.Windows.Forms.Button();
			this.OpenFile = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// AssemblyList
			// 
			this.AssemblyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RefName,
            this.AsmVersion,
            this.AsmFileName});
			this.AssemblyList.Location = new System.Drawing.Point(8, 8);
			this.AssemblyList.Name = "AssemblyList";
			this.AssemblyList.Size = new System.Drawing.Size(448, 224);
			this.AssemblyList.TabIndex = 0;
			this.AssemblyList.UseCompatibleStateImageBehavior = false;
			this.AssemblyList.View = System.Windows.Forms.View.Details;
			// 
			// RefName
			// 
			this.RefName.Text = "Name";
			this.RefName.Width = 125;
			// 
			// AsmVersion
			// 
			this.AsmVersion.Text = "Version";
			this.AsmVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.AsmVersion.Width = 122;
			// 
			// AsmFileName
			// 
			this.AsmFileName.Text = "File name";
			this.AsmFileName.Width = 197;
			// 
			// RemoveRef
			// 
			this.RemoveRef.Location = new System.Drawing.Point(360, 240);
			this.RemoveRef.Name = "RemoveRef";
			this.RemoveRef.Size = new System.Drawing.Size(96, 32);
			this.RemoveRef.TabIndex = 1;
			this.RemoveRef.Text = "Remove";
			this.RemoveRef.UseVisualStyleBackColor = true;
			// 
			// AddRef
			// 
			this.AddRef.Location = new System.Drawing.Point(256, 240);
			this.AddRef.Name = "AddRef";
			this.AddRef.Size = new System.Drawing.Size(96, 32);
			this.AddRef.TabIndex = 2;
			this.AddRef.Text = "Add";
			this.AddRef.UseVisualStyleBackColor = true;
			this.AddRef.Click += new System.EventHandler(this.AddRef_Click);
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(8, 280);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(96, 32);
			this.Cancel.TabIndex = 3;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// Okay
			// 
			this.Okay.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Okay.Location = new System.Drawing.Point(360, 280);
			this.Okay.Name = "Okay";
			this.Okay.Size = new System.Drawing.Size(96, 32);
			this.Okay.TabIndex = 4;
			this.Okay.Text = "OK";
			this.Okay.UseVisualStyleBackColor = true;
			// 
			// OpenFile
			// 
			this.OpenFile.Filter = "Dynamic link libraries (*.dll)|*.dll";
			// 
			// ReferencesEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464, 321);
			this.Controls.Add(this.Okay);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.AddRef);
			this.Controls.Add(this.RemoveRef);
			this.Controls.Add(this.AssemblyList);
			this.Name = "ReferencesEditor";
			this.Text = "References";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView AssemblyList;
		private System.Windows.Forms.ColumnHeader RefName;
		private System.Windows.Forms.ColumnHeader AsmVersion;
		private System.Windows.Forms.ColumnHeader AsmFileName;
		private System.Windows.Forms.Button RemoveRef;
		private System.Windows.Forms.Button AddRef;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.Button Okay;
		private System.Windows.Forms.OpenFileDialog OpenFile;
	}
}