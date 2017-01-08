﻿namespace ImageDupCheckerOrganizerService
{
	partial class Installer1
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
			this.FileImporterService = new System.ServiceProcess.ServiceInstaller();
			this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
			// 
			// FileImporterService
			// 
			this.FileImporterService.DelayedAutoStart = true;
			this.FileImporterService.Description = "MAB Media Import Service";
			this.FileImporterService.DisplayName = "FileImporterService";
			this.FileImporterService.ServiceName = "FileImporterService";
			this.FileImporterService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// serviceProcessInstaller1
			// 
			this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstaller1.Password = null;
			this.serviceProcessInstaller1.Username = null;
			// 
			// Installer1
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FileImporterService,
            this.serviceProcessInstaller1});

		}

		#endregion

		private System.ServiceProcess.ServiceInstaller FileImporterService;
		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
	}
}