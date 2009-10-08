/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Collections;

namespace IcisMobileDesktopServer.Framework.DataCollection
{
	/// <summary>
	/// Summary description for DeviceData.
	/// </summary>
	public class DeviceData
	{
		#region Members
		/// <summary>
		/// Variate name
		/// </summary>
		private string variate_name;
		/// <summary>
		/// Excel column number
		/// </summary>
		private int column_number;
		/// <summary>
		/// Array of values
		/// </summary>
		private ArrayList arrValues = new ArrayList();
		#endregion

		#region Properties, obvious names
		public string NAME 
		{
			set { variate_name = value; }
			get { return variate_name; }
		}

		public int COLUMN 
		{
			set { column_number = value; }
			get { return column_number; }
		}

		public void AddValue(object o) 
		{
			arrValues.Add(o);
		}

		public ArrayList GetValues() 
		{
			return arrValues;
		}
		#endregion
	}
}
