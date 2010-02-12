/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace IcisMobileDesktopServer
{
	/// <summary>
	/// Summary description for frmSelectFactor.
	/// </summary>
	public class frmSelectFactor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox lbFactors;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSelect;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region Native
		public frmSelectFactor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSelectFactor));
			this.lbFactors = new System.Windows.Forms.ListBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lbFactors
			// 
			this.lbFactors.ItemHeight = 16;
			this.lbFactors.Location = new System.Drawing.Point(16, 16);
			this.lbFactors.Name = "lbFactors";
			this.lbFactors.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lbFactors.Size = new System.Drawing.Size(256, 180);
			this.lbFactors.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(160, 216);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(48, 216);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "Select";
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// frmSelectFactor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(292, 259);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lbFactors);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmSelectFactor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmSelectFactor";
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		#region ICIS-Mobile
		private System.Text.StringBuilder sb;
		private Framework.Engine engine;
		public frmSelectFactor(Framework.Engine engine) 
		{
			InitializeComponent();
			
			this.engine = engine;
		}

		/// <summary>
		/// Fill the list with plant values.
		/// </summary>
		/// <param name="arrTemp"></param>
		public void SetList(ArrayList arrTemp) 
		{
			foreach(string s in arrTemp) 
			{
				lbFactors.Items.Add(s);
			}
		}

		/// <summary>
		/// Cancel and return to the main form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}

		/// <summary>
		/// Event that handles item click of the listbox.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			if(lbFactors.SelectedItems.Count != 2) 
			{
				Framework.Helper.MessageHelper.ShowInfo("Please select 2 factors.");
			} 
			else //process
			{
				sb = new System.Text.StringBuilder();
				foreach(string s in lbFactors.SelectedItems) 
				{
					sb.Append(s);
					sb.Append("|");
				}
				sb.Remove(sb.Length - 1, 1);
				engine.SetFilteredFactors(this, sb.ToString());
			}
		}

		/// <summary>
		/// Gets the index of the selected factors.
		/// </summary>
		/// <returns>string</returns>
		public string GetSelectedFactors() 
		{
			return sb.ToString();
		}

		/// <summary>
		/// Removes all the items in the list box.
		/// </summary>
		public void Destroy() 
		{
			lbFactors.Items.Clear();
		}
		#endregion
	}
}
