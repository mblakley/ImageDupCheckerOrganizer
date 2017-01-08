namespace ImageDupCheckerOrganizerService
{
	partial class ImageDupCheckerOrganizerService
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.FsWatcher = new System.IO.FileSystemWatcher();
			this.serviceController1 = new System.ServiceProcess.ServiceController();
			((System.ComponentModel.ISupportInitialize)(this.FsWatcher)).BeginInit();
			// 
			// FsWatcher
			// 
			this.FsWatcher.IncludeSubdirectories = true;
			//this.FsWatcher.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.LastWrite)));
			//this.FsWatcher.Changed += new System.IO.FileSystemEventHandler(this.FsWatcher_Changed_1);
			this.FsWatcher.Created += new System.IO.FileSystemEventHandler(this.FsWatcher_Changed_1);
			this.FsWatcher.EnableRaisingEvents = true;
			// 
			// ImageDupCheckerOrganizerService
			// 
			this.ServiceName = "ImageDupCheckerOrganizerService";
			((System.ComponentModel.ISupportInitialize)(this.FsWatcher)).EndInit();

		}

		#endregion

		private System.IO.FileSystemWatcher FsWatcher;
		private System.ServiceProcess.ServiceController serviceController1;
	}
}
