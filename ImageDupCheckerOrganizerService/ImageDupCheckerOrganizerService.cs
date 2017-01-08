using ImageDupCheckerOrganizerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageDupCheckerOrganizerService
{
	public partial class ImageDupCheckerOrganizerService : ServiceBase
	{
		private readonly ImageDupCheckerOrganizer idcol = new ImageDupCheckerOrganizer();
		private string masterArchive = String.Empty;

		public ImageDupCheckerOrganizerService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			// Configure and Start file watcher
			FsWatcher.Path = ConfigurationManager.AppSettings["WatchPath"];
			masterArchive = ConfigurationManager.AppSettings["ArchivePath"];
		}

		protected override void OnStop()
		{
		}

		///// <summary>
		///// Event occurs when the contents of a File or Directory are changed
		///// </summary>
		//private void FsWatcher_Changed(object sender,
		//				System.IO.FileSystemEventArgs e)
		//{
		//	// Figure out if the thing that changed was a directory or a file
		//	if (Directory.Exists(e.FullPath))
		//	{
		//		idcol.ImportDirectory(e.FullPath, masterArchive);
		//	}
		//	else if (File.Exists(e.FullPath))
		//	{
		//		//Check if the file is a duplicate using the logic in ImageDupCheckerOrganizer and import it if necessary
		//		idcol.ImportFile(e.FullPath, masterArchive);
		//	}
		//}
		///// <summary>
		///// Event occurs when the a File or Directory is created
		///// </summary>
		//private void FsWatcher_Created(object sender,
		//				System.IO.FileSystemEventArgs e)
		//{
		//	// Figure out if the thing that changed was a directory or a file
		//	if (Directory.Exists(e.FullPath))
		//	{
		//		idcol.ImportDirectory(e.FullPath, masterArchive);
		//	}
		//	else if (File.Exists(e.FullPath))
		//	{
		//		//Check if the file is a duplicate using the logic in ImageDupCheckerOrganizer and import it if necessary
		//		idcol.ImportFile(e.FullPath, masterArchive);
		//	}
		//}
		
		private void FsWatcher_Changed_1(object sender, System.IO.FileSystemEventArgs e)
		{
			try
			{
				// Figure out if the thing that changed was a directory or a file
				if (Directory.Exists(e.FullPath))
				{
					EventLog.WriteEntry("Importing Directory: " + e.FullPath, EventLogEntryType.Information);
					// TODO: I think this is causing any subsequent events to get lost while the thread is sleeping
					// This is needed to make sure that all files are there before importing the directory - kludgy
					Thread.Sleep(30000);
					idcol.ImportDirectory(e.FullPath, masterArchive);
					EventLog.WriteEntry("Done Importing Directory: " + e.FullPath, EventLogEntryType.Information);
				}
				else if (File.Exists(e.FullPath))
				{
					var dirPath = Path.GetDirectoryName(e.FullPath);
					// Figure out if it's a "loose" file that's not in a sub-folder
					if (dirPath != null && dirPath.Equals(FsWatcher.Path.Replace(@"\\", @"\").TrimEnd('\\')))
					{
						// It's a file in the root, so import it
						EventLog.WriteEntry("Importing File: " + e.FullPath, EventLogEntryType.Information);
						//Check if the file is a duplicate using the logic in ImageDupCheckerOrganizer and import it if necessary
						idcol.ImportFile(e.FullPath, masterArchive);
						EventLog.WriteEntry("Done Importing File: " + e.FullPath, EventLogEntryType.Information);
					}
					else
					{
						// It's in a directory, so let the directory import handle it
						EventLog.WriteEntry("Skipping File: " + e.FullPath, EventLogEntryType.Information);
					}
				}
				else
				{
					EventLog.WriteEntry("Media Importer - Unable to import " + e.FullPath, EventLogEntryType.Warning);
				}
			}
			catch (Exception ex)
			{
				EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
			}
		}
	}
}
