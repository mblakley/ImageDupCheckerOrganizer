namespace ImageDupCheckerOrganizer
{
	partial class Form1
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
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.txtMaster = new System.Windows.Forms.TextBox();
			this.txtImport = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtMaster
			// 
			this.txtMaster.Location = new System.Drawing.Point(39, 54);
			this.txtMaster.Name = "txtMaster";
			this.txtMaster.Size = new System.Drawing.Size(100, 20);
			this.txtMaster.TabIndex = 0;
			this.txtMaster.Text = "C:\\temp\\archive";
			// 
			// txtImport
			// 
			this.txtImport.Location = new System.Drawing.Point(351, 53);
			this.txtImport.Name = "txtImport";
			this.txtImport.Size = new System.Drawing.Size(100, 20);
			this.txtImport.TabIndex = 1;
			this.txtImport.Text = "C:\\temp\\import";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(351, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Folder to Import";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(39, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Master Archive Folder";
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(110, 153);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(75, 23);
			this.btnImport.TabIndex = 4;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(238, 280);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(0, 13);
			this.label3.TabIndex = 5;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(537, 343);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtImport);
			this.Controls.Add(this.txtMaster);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.TextBox txtMaster;
		private System.Windows.Forms.TextBox txtImport;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Label label3;
	}
}

