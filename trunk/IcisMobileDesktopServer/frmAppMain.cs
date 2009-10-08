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
		private System.Windows.Forms.Button btnFromDevice;
		private System.Windows.Forms.OpenFileDialog ofdXlsFile;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Timer timerBtn;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblHeader1;
		private System.ComponentModel.IContainer components;

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAppMain));
			this.lblHeader1 = new System.Windows.Forms.Label();
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
			this.btnFromDevice = new System.Windows.Forms.Button();
			this.ofdXlsFile = new System.Windows.Forms.OpenFileDialog();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.timerBtn = new System.Windows.Forms.Timer(this.components);
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// lblHeader1
			// 
			this.lblHeader1.Location = new System.Drawing.Point(0, 85);
			this.lblHeader1.Name = "lblHeader1";
			this.lblHeader1.Size = new System.Drawing.Size(384, 23);
			this.lblHeader1.TabIndex = 0;
			this.lblHeader1.Text = "Please Select Workbook Template";
			this.lblHeader1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			this.btnOpenFD.Location = new System.Drawing.Point(288, 184);
			this.btnOpenFD.Name = "btnOpenFD";
			this.btnOpenFD.Size = new System.Drawing.Size(72, 24);
			this.btnOpenFD.TabIndex = 1;
			this.btnOpenFD.Text = "Select";
			this.btnOpenFD.Click += new System.EventHandler(this.btnOpenFD_Click);
			// 
			// tbWB
			// 
			this.tbWB.Location = new System.Drawing.Point(16, 184);
			this.tbWB.Name = "tbWB";
			this.tbWB.Size = new System.Drawing.Size(248, 22);
			this.tbWB.TabIndex = 2;
			this.tbWB.Text = "Workbook";
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point(200, 248);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(152, 23);
			this.btnProcess.TabIndex = 3;
			this.btnProcess.Text = "Upload to Device";
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(200, 288);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(152, 23);
			this.btnExit.TabIndex = 4;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// tbDMSLocal
			// 
			this.tbDMSLocal.Location = new System.Drawing.Point(16, 120);
			this.tbDMSLocal.Name = "tbDMSLocal";
			this.tbDMSLocal.Size = new System.Drawing.Size(248, 22);
			this.tbDMSLocal.TabIndex = 6;
			this.tbDMSLocal.Text = "DMS-Local";
			// 
			// tbDMSCentral
			// 
			this.tbDMSCentral.Location = new System.Drawing.Point(16, 152);
			this.tbDMSCentral.Name = "tbDMSCentral";
			this.tbDMSCentral.Size = new System.Drawing.Size(248, 22);
			this.tbDMSCentral.TabIndex = 7;
			this.tbDMSCentral.Text = "DMS-Central";
			// 
			// btnCentralDMS
			// 
			this.btnCentralDMS.Location = new System.Drawing.Point(288, 152);
			this.btnCentralDMS.Name = "btnCentralDMS";
			this.btnCentralDMS.TabIndex = 8;
			this.btnCentralDMS.Text = "Select";
			this.btnCentralDMS.Click += new System.EventHandler(this.btnCentralDMS_Click);
			// 
			// btnLocalDMS
			// 
			this.btnLocalDMS.Location = new System.Drawing.Point(288, 120);
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
			this.btnSelFacs.Location = new System.Drawing.Point(24, 248);
			this.btnSelFacs.Name = "btnSelFacs";
			this.btnSelFacs.Size = new System.Drawing.Size(152, 23);
			this.btnSelFacs.TabIndex = 10;
			this.btnSelFacs.Text = "Select Factor";
			this.btnSelFacs.Click += new System.EventHandler(this.btnSelFacs_Click);
			// 
			// btnFromDevice
			// 
			this.btnFromDevice.Location = new System.Drawing.Point(24, 288);
			this.btnFromDevice.Name = "btnFromDevice";
			this.btnFromDevice.Size = new System.Drawing.Size(152, 23);
			this.btnFromDevice.TabIndex = 11;
			this.btnFromDevice.Text = "Download from Device";
			this.btnFromDevice.Click += new System.EventHandler(this.btnFromDevice_Click);
			// 
			// ofdXlsFile
			// 
			this.ofdXlsFile.DefaultExt = "xls";
			this.ofdXlsFile.Filter = "Microsoft Excel Document (*.xls) | *.xls";
			this.ofdXlsFile.InitialDirectory = "c:\\\\";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(1, 335);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(175, 23);
			this.progressBar1.TabIndex = 12;
			this.progressBar1.Visible = false;
			// 
			// timerBtn
			// 
			this.timerBtn.Interval = 3000;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(380, 80);
			this.pictureBox1.TabIndex = 13;
			this.pictureBox1.TabStop = false;
			// 
			// frmAppMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(376, 355);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.btnFromDevice);
			this.Controls.Add(this.btnSelFacs);
			this.Controls.Add(this.btnLocalDMS);
			this.Controls.Add(this.btnCentralDMS);
			this.Controls.Add(this.tbDMSCentral);
			this.Controls.Add(this.tbDMSLocal);
			this.Controls.Add(this.tbWB);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.btnOpenFD);
			this.Controls.Add(this.lblHeader1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmAppMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
			//Sets the registry values for splash screen
			SplashScreen.RegistryAccess.SetStringRegistryValue("SOFTWARE_KEY", "Software");
			SplashScreen.RegistryAccess.SetStringRegistryValue("COMPANY_NAME", "IRRI");
			SplashScreen.RegistryAccess.SetStringRegistryValue("APPLICATION_NAME", "ICIS Mobile");

			Application.Run(new frmAppMain());
		}
		#endregion
				
		/// <summary>
		/// Initialize the ICIS Desktop application.
		/// </summary>
		private void InitializeICIS() 
		{
			//starts splash screen
			SplashScreen.SplashScreen.ShowSplashScreen(); 
			Application.DoEvents();
			SplashScreen.SplashScreen.SetStatus("Initializing ICIS Mobile.");
			System.Threading.Thread.Sleep(1000);
			SplashScreen.SplashScreen.SetStatus("Initializing ICIS Mobile..");
			System.Threading.Thread.Sleep(1000);
			SplashScreen.SplashScreen.SetStatus("Initializing ICIS Mobile...");
			System.Threading.Thread.Sleep(1000);

			//set default values for dms database
			String default_dir = @"C:\ICIS5";
			ofddmscentral.InitialDirectory = default_dir;
			ofddmslocal.InitialDirectory = default_dir;
			ofdSelWb.InitialDirectory = default_dir;

			ofddmscentral.InitialDirectory = @"C:\ICIS5\Database\IRIS\Central\IRIS-DMS.mdb";
			ofddmslocal.InitialDirectory = @"C:\ICIS5\current\Training.mdb";
			ofdSelWb.InitialDirectory = @"C:\ICIS5\U03WSHB_data.xls";

			engine = new Engine(progressBar1);

			timerBtn.Tick += new EventHandler(timerBtn_Tick);

			//Set the owner of the splash screen instance to this form
			if( SplashScreen.SplashScreen.SplashForm != null )
				SplashScreen.SplashScreen.SplashForm.Owner = this;
			this.Activate();
			SplashScreen.SplashScreen.CloseForm();
		}

		/// <summary>
		/// Select workbook.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenFD_Click(object sender, System.EventArgs e)
		{
			if(ofdSelWb.ShowDialog() == DialogResult.OK) 
			{
				tbWB.Text = ofdSelWb.FileName;
			}
		}

		/// <summary>
		/// Process the workbook, create an xml and text file representation of study
		/// and upload that to mobile device.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnProcess_Click(object sender, System.EventArgs e)
		{
			if(FileHelper.IsExists(tbDMSCentral.Text) && FileHelper.IsExists(tbDMSLocal.Text)) 
			{
				btnProcess.Text = ResourceHelper.GetStaticString("messages", "m_uploading");
				btnProcess.Enabled = false;
				timerBtn.Enabled = true;
				if(!engine.Process()) 
				{ //no selected factor yet
                    MessageHelper.ShowError(ResourceHelper.GetStaticString("messages", "m_factorselect"));
				}
			} 
			else 
			{
				MessageHelper.ShowError(ResourceHelper.GetStaticString("messages", "m_accessdb_invalid"));
			}
		}

		/// <summary>
		/// Terminates the application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// Override onClosing, so we can close open connections and dispose objects.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e)
		{
			engine.Dispose();
			base.OnClosing (e);
		}

		/// <summary>
		/// Select local dms database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLocalDMS_Click(object sender, System.EventArgs e)
		{
			if(ofddmslocal.ShowDialog() == DialogResult.OK) 
			{
				tbDMSLocal.Text = ofddmslocal.FileName;
			}
		}

		/// <summary>
		/// Select central dms database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCentralDMS_Click(object sender, System.EventArgs e)
		{
			if(ofddmscentral.ShowDialog() == DialogResult.OK) 
			{
				tbDMSCentral.Text = ofddmscentral.FileName;
			}
		}

		/// <summary>
		/// Select factors from workbook.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelFacs_Click(object sender, System.EventArgs e)
		{
			if(FileHelper.IsExists(tbWB.Text))
			{
				engine.Initialize(tbWB.Text);
				engine.SetDatabase(ofddmscentral.FileName, ofddmslocal.FileName);
				engine.ShowFilterFactors();
			} 
			else 
			{
				MessageHelper.ShowError(ResourceHelper.GetStaticString("messages", "m_workbook_invalid"));
			}
		}

		/// <summary>
		/// Downloads data from device.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFromDevice_Click(object sender, System.EventArgs e)
		{
			timerBtn.Enabled = true;
			btnFromDevice.Text = "Downloading...";
			btnFromDevice.Enabled = false;
			string data_file = engine.DownloadFromDevice();			
			
			string excel_file = "";
			if(data_file != "") 
			{
				if(ofdXlsFile.ShowDialog() == DialogResult.OK) 
				{ //this should be the old file use for uploading
					excel_file = ofdXlsFile.FileName;
					engine.WriteToExcel(excel_file, data_file);
				}
			}
		}

		/// <summary>
		/// Enable/disable the upload/download buttons.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerBtn_Tick(object sender, EventArgs e)
		{
			btnProcess.Enabled = true;
			btnFromDevice.Enabled = true;
			btnProcess.Text = "Upload to Device";
			btnFromDevice.Text = "Download from Device";
			timerBtn.Enabled = false;
		}
	}
}
