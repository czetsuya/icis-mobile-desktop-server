/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using IcisMobileDesktopServer.Framework;
using IcisMobileDesktopServer.Framework.Helper;

namespace IcisMobileDesktopServer
{
	/// <summary>
	/// User Interface
	/// </summary>
	public class frmAppMain : System.Windows.Forms.Form
	{
		Framework.Engine engine;

		#region Application
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog ofdSelWb;
		private System.Windows.Forms.Button btnOpenFD;
		private System.Windows.Forms.TextBox tbWB;
		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.TextBox tbDMSLocal;
		private System.Windows.Forms.TextBox tbDMSCentral;
		private System.Windows.Forms.Button btnCentralDMS;
		private System.Windows.Forms.Button btnLocalDMS;
		private System.Windows.Forms.OpenFileDialog ofddmslocal;
		private System.Windows.Forms.OpenFileDialog ofddmscentral;
		private System.Windows.Forms.Button btnSelFacs;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAppMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeICIS();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.ofdSelWb = new System.Windows.Forms.OpenFileDialog();
			this.btnOpenFD = new System.Windows.Forms.Button();
			this.tbWB = new System.Windows.Forms.TextBox();
			this.btnProcess = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.tbDMSLocal = new System.Windows.Forms.TextBox();
			this.tbDMSCentral = new System.Windows.Forms.TextBox();
			this.btnCentralDMS = new System.Windows.Forms.Button();
			this.btnLocalDMS = new System.Windows.Forms.Button();
			this.ofddmslocal = new System.Windows.Forms.OpenFileDialog();
			this.ofddmscentral = new System.Windows.Forms.OpenFileDialog();
			this.btnSelFacs = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please Select Workbook Template";
			// 
			// ofdSelWb
			// 
			this.ofdSelWb.DefaultExt = "xls";
			this.ofdSelWb.Filter = "Microsoft Excel Document (*.xls) | *.xls";
			this.ofdSelWb.InitialDirectory = "c:\\\\";
			this.ofdSelWb.RestoreDirectory = true;
			// 
			// btnOpenFD
			// 
			this.btnOpenFD.Location = new System.Drawing.Point(280, 104);
			this.btnOpenFD.Name = "btnOpenFD";
			this.btnOpenFD.Size = new System.Drawing.Size(72, 24);
			this.btnOpenFD.TabIndex = 1;
			this.btnOpenFD.Text = "Select";
			this.btnOpenFD.Click += new System.EventHandler(this.btnOpenFD_Click);
			// 
			// tbWB
			// 
			this.tbWB.Location = new System.Drawing.Point(16, 104);
			this.tbWB.Name = "tbWB";
			this.tbWB.Size = new System.Drawing.Size(248, 22);
			this.tbWB.TabIndex = 2;
			this.tbWB.Text = "Workbook";
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point(8, 328);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(128, 23);
			this.btnProcess.TabIndex = 3;
			this.btnProcess.Text = "Upload to Device";
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(296, 328);
			this.btnExit.Name = "btnExit";
			this.btnExit.TabIndex = 4;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// tbDMSLocal
			// 
			this.tbDMSLocal.Location = new System.Drawing.Point(16, 40);
			this.tbDMSLocal.Name = "tbDMSLocal";
			this.tbDMSLocal.Size = new System.Drawing.Size(248, 22);
			this.tbDMSLocal.TabIndex = 6;
			this.tbDMSLocal.Text = "DMS-Local";
			// 
			// tbDMSCentral
			// 
			this.tbDMSCentral.Location = new System.Drawing.Point(16, 72);
			this.tbDMSCentral.Name = "tbDMSCentral";
			this.tbDMSCentral.Size = new System.Drawing.Size(248, 22);
			this.tbDMSCentral.TabIndex = 7;
			this.tbDMSCentral.Text = "DMS-Central";
			// 
			// btnCentralDMS
			// 
			this.btnCentralDMS.Location = new System.Drawing.Point(280, 72);
			this.btnCentralDMS.Name = "btnCentralDMS";
			this.btnCentralDMS.TabIndex = 8;
			this.btnCentralDMS.Text = "Select";
			this.btnCentralDMS.Click += new System.EventHandler(this.btnCentralDMS_Click);
			// 
			// btnLocalDMS
			// 
			this.btnLocalDMS.Location = new System.Drawing.Point(280, 40);
			this.btnLocalDMS.Name = "btnLocalDMS";
			this.btnLocalDMS.TabIndex = 9;
			this.btnLocalDMS.Text = "Select";
			this.btnLocalDMS.Click += new System.EventHandler(this.btnLocalDMS_Click);
			// 
			// ofddmslocal
			// 
			this.ofddmslocal.DefaultExt = "mdb";
			this.ofddmslocal.Filter = "Microsoft Access Database (*.mdb) | *.mdb";
			// 
			// ofddmscentral
			// 
			this.ofddmscentral.DefaultExt = "mdb";
			this.ofddmscentral.Filter = "Microsoft Access Database (*.mdb) | *.mdb";
			// 
			// btnSelFacs
			// 
			this.btnSelFacs.Location = new System.Drawing.Point(8, 296);
			this.btnSelFacs.Name = "btnSelFacs";
			this.btnSelFacs.Size = new System.Drawing.Size(128, 23);
			this.btnSelFacs.TabIndex = 10;
			this.btnSelFacs.Text = "Select Factor";
			this.btnSelFacs.Click += new System.EventHandler(this.btnSelFacs_Click);
			// 
			// frmAppMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(376, 355);
			this.Controls.Add(this.btnSelFacs);
			this.Controls.Add(this.btnLocalDMS);
			this.Controls.Add(this.btnCentralDMS);
			this.Controls.Add(this.tbDMSCentral);
			this.Controls.Add(this.tbDMSLocal);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.tbWB);
			this.Controls.Add(this.btnOpenFD);
			this.Controls.Add(this.label1);
			this.Name = "frmAppMain";
			this.Text = "ICIS-Mobile Desktop Server";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmAppMain());
		}
		#endregion
	
		private void InitializeICIS() 
		{
			String default_dir = @"C:\ICIS5";
			ofddmscentral.InitialDirectory = default_dir;
			ofddmslocal.InitialDirectory = default_dir;
			ofdSelWb.InitialDirectory = default_dir;

			ofddmscentral.InitialDirectory = @"C:\ICIS5\Database\IRIS\Central\IRIS-DMS.mdb";
			ofddmslocal.InitialDirectory = @"C:\ICIS5\current\Training.mdb";
			ofdSelWb.InitialDirectory = @"C:\ICIS5\U03WSHB_data.xls";

			engine = new Engine();
		}

		private void btnOpenFD_Click(object sender, System.EventArgs e)
		{
			if(ofdSelWb.ShowDialog() == DialogResult.OK) 
			{
				tbWB.Text = ofdSelWb.FileName;
			}
		}

		private void btnProcess_Click(object sender, System.EventArgs e)
		{	
			engine.Process();
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			engine.Dispose();
			base.OnClosing (e);
		}

		private void btnLocalDMS_Click(object sender, System.EventArgs e)
		{
			if(ofddmslocal.ShowDialog() == DialogResult.OK) 
			{
				tbDMSLocal.Text = ofddmslocal.FileName;
			}
		}

		private void btnCentralDMS_Click(object sender, System.EventArgs e)
		{
			if(ofddmscentral.ShowDialog() == DialogResult.OK) 
			{
				tbDMSCentral.Text = ofddmscentral.FileName;
			}
		}

		private void btnSelFacs_Click(object sender, System.EventArgs e)
		{
			engine.Initialize(tbWB.Text);
			engine.SetDatabase(ofddmscentral.FileName, ofddmslocal.FileName);
			engine.ShowFilterFactors();
		}

	}
}
