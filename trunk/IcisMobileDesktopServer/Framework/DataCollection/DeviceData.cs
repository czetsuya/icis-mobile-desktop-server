using System;
using System.Collections;

namespace IcisMobileDesktopServer.Framework.DataCollection
{
	/// <summary>
	/// Summary description for DeviceData.
	/// </summary>
	public class DeviceData
	{
		private string variate_name;
		private int column_number;

		private ArrayList arrValues = new ArrayList();

		public DeviceData()
		{
		}

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

	}
}
