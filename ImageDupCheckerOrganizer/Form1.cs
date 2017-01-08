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
using ImageDupCheckerOrganizerLib;

namespace ImageDupCheckerOrganizerForm
{
	public partial class Form1 : Form
	{
		private ImageDupCheckerOrganizer idcol = new ImageDupCheckerOrganizer();

		public Form1()
		{
			InitializeComponent();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			label3.Text = "Working...";
			Directory.CreateDirectory(txtMaster.Text);

			idcol.ImportDirectory(txtImport.Text, txtMaster.Text);

			label3.Text = "Done!";
		}

		
	}
}
