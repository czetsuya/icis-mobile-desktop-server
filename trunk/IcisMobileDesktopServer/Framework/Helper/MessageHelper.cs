/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Windows.Forms;

namespace IcisMobileDesktopServer.Framework.Helper
{
	/// <summary>
	/// Summary description for MessageHelper.
	/// </summary>
	public class MessageHelper
	{
		public static void ShowInfo(String s) 
		{
			MessageBox.Show(s, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
		}

		public static void ShowError(String s) 
		{
			MessageBox.Show(s, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
		}
	}
}
