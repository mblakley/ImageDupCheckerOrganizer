using ExifLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageDupCheckerOrganizer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			label3.Text = "Working...";
			var masterDirFolders = Directory.EnumerateDirectories(txtMaster.Text);

			ImportDirectory(txtImport.Text, masterDirFolders.ToList());

			label3.Text = "Done!";
		}

		private void ImportDirectory(string importDir, List<string> masterDirFolders)
		{
			var importFiles = Directory.EnumerateFiles(importDir, "*.jpg");
			// avi, 3gp, png
			importFiles = importFiles.Concat(Directory.EnumerateFiles(importDir, "*.jpeg"));
			importFiles = importFiles.Concat(Directory.EnumerateFiles(importDir, "*.MOV"));
			importFiles = importFiles.Concat(Directory.EnumerateFiles(importDir, "*.3gp"));
			importFiles = importFiles.Concat(Directory.EnumerateFiles(importDir, "*.mts"));
			importFiles = importFiles.Concat(Directory.EnumerateFiles(importDir, "*.avi"));
			importFiles = importFiles.Concat(Directory.EnumerateFiles(importDir, "*.png"));
			foreach (var importImgFileName in importFiles)
			{
				var foundMatch = false;
				var importImgFile = new FileInfo(importImgFileName);

				DateTime datePictureTaken;
				string subSecTime;
				// If we didn't find a match, it must be an image that we don't have in our master archive yet
				// Find out when the picture was taken, and put it into a folder for that month	
				try
				{
					using (var reader = new ExifReader(importImgFileName))
					{
						// Extract the tag data using the ExifTags enumeration
						if (!reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal,
							out datePictureTaken))
						{
							datePictureTaken = File.GetLastWriteTime(importImgFileName);
						}

						if (!reader.GetTagValue<string>(ExifTags.SubsecTimeOriginal,
							out subSecTime))
						{
							// Couldn't find a date, so make something up!
							subSecTime = "00";
						}
					}
				}
				catch (Exception ex)
				{
					datePictureTaken = importImgFile.LastWriteTime;
					subSecTime = "00";
				}


				// Look at the folder with the same year and month as this file for a matching file
				//foreach (var folderPath in masterDirFolders)
				var folderName = datePictureTaken.ToString("yyyy.MM");
				var fullFolderPath = Path.Combine(txtMaster.Text, folderName);
				Directory.CreateDirectory(fullFolderPath);
				var filesInFolder = Directory.EnumerateFiles(fullFolderPath);
				foreach (var masterImgFileName in filesInFolder)
				{
					var masterImgFile = new FileInfo(masterImgFileName);
					foundMatch = FilesAreEqual(masterImgFile, importImgFile);
					if (foundMatch)
					{
						// We can throw out this image
						break;
					}
				}
				// If we found one, we can safely throw the import file away
				// For now, just move the file to a "trash" folder outside of the master directory (in C:\temp)
				if (foundMatch)
				{
					File.Delete(importImgFileName);
					//File.Move(importImgFileName, Path.Combine(@"C:\temp\imgTrash\", (importDir.Replace('\\','-').Replace(':','-') + Path.GetFileName(importImgFileName))));
				}
				else
				{
					var imgFileCreationDateTimeFilename = datePictureTaken.ToString("yyyyMMddHHmmss") + subSecTime + Path.GetExtension(importImgFileName);
					if (!masterDirFolders.Contains(fullFolderPath))
					{
						masterDirFolders.Add(fullFolderPath);
					}
					var archivePath = Path.Combine(txtMaster.Text, folderName, imgFileCreationDateTimeFilename);
					while (File.Exists(archivePath))
					{
						// add a (1) to the file - this should rarely, if ever, happen!  Just keep adding until we get a unique name
						archivePath = Path.Combine(txtMaster.Text, folderName, Path.GetFileNameWithoutExtension(archivePath) + "(1)" + Path.GetExtension(archivePath));
					}
					// Move it, and name it with the date/time of the picture
					File.Move(importImgFileName, archivePath);
				}
			}

			// Go through the sub directories and check all of them
			var importDirs = Directory.EnumerateDirectories(importDir);
			foreach (var containedDir in importDirs)
			{
				if (containedDir != txtMaster.Text)
				{
					ImportDirectory(containedDir, masterDirFolders);
				}
			}
		}

		const int BYTES_TO_READ = sizeof(Int64);

		// Blatently stolen from StackOverflow
		static bool FilesAreEqual(FileInfo first, FileInfo second)
		{
			DateTime dateFirstPictureTaken;
			string subSecTimeFirst;
			bool dateFirstPictureTakenIsAccurate = true;
			bool subSecFirstPictureTakenIsAccurate = true;
			// If we didn't find a match, it must be an image that we don't have in our master archive yet
			// Find out when the picture was taken, and put it into a folder for that month	
			try
			{
				using (var reader = new ExifReader(first.FullName))
				{
					// Extract the tag data using the ExifTags enumeration
					if (!reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal,
						out dateFirstPictureTaken))
					{
						dateFirstPictureTaken = File.GetLastWriteTime(first.FullName);
						dateFirstPictureTakenIsAccurate = false;
					}

					if (!reader.GetTagValue<string>(ExifTags.SubsecTimeOriginal,
						out subSecTimeFirst))
					{
						// Couldn't find a date, so make something up!
						subSecTimeFirst = "00";
						subSecFirstPictureTakenIsAccurate = false;
					}
				}
			}
			catch (Exception ex)
			{
				dateFirstPictureTaken = first.LastWriteTime;
				subSecTimeFirst = "00";
				dateFirstPictureTakenIsAccurate = false;
				subSecFirstPictureTakenIsAccurate = false;
			}

			DateTime dateSecondPictureTaken;
			string subSecTimeSecond;
			bool dateSecondPictureTakenIsAccurate = true;
			bool subSecSecondPictureTakenIsAccurate = true;
			try
			{
				using (var reader = new ExifReader(second.FullName))
				{
					// Extract the tag data using the ExifTags enumeration
					if (!reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal,
						out dateSecondPictureTaken))
					{
						dateSecondPictureTaken = File.GetLastWriteTime(second.FullName);
						dateSecondPictureTakenIsAccurate = false;
					}

					if (!reader.GetTagValue<string>(ExifTags.SubsecTimeOriginal,
						out subSecTimeSecond))
					{
						// Couldn't find a date, so make something up!
						subSecTimeSecond = "00";
						subSecSecondPictureTakenIsAccurate = false;
					}
				}
			}
			catch (Exception ex)
			{
				dateSecondPictureTaken = second.LastWriteTime;
				subSecTimeSecond = "00";
				dateSecondPictureTakenIsAccurate = false;
				subSecSecondPictureTakenIsAccurate = false;
			}

			if (first.Length != second.Length)
			{
				// Check the file info, not just the bytes
				if (dateFirstPictureTakenIsAccurate &&
				    dateSecondPictureTakenIsAccurate && 
					dateFirstPictureTaken == dateSecondPictureTaken)
				{
					// The lengths don't match, but we think it's accurate, and they are really the same photo
					// TODO: Figure out if the actual image bytes are the same
					return true;
				}
				return false;
			}

			int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

			using (FileStream fs1 = first.OpenRead())
			using (FileStream fs2 = second.OpenRead())
			{
				byte[] one = new byte[BYTES_TO_READ];
				byte[] two = new byte[BYTES_TO_READ];

				for (int i = 0; i < iterations; i++)
				{
					fs1.Read(one, 0, BYTES_TO_READ);
					fs2.Read(two, 0, BYTES_TO_READ);

					if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
						return false;
				}
			}

			return true;
		}
	}
}
