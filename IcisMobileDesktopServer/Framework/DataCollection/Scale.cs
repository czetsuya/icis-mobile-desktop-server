using System;
using System.Collections;

namespace IcisMobileDesktopServer.Framework.DataCollection
{
	/// <summary>
	/// Summary description for Scale.
	/// </summary>
	public class Scale
	{
		#region Members
		/// <summary>
		/// Scale id
		/// </summary>
		private String id;
		/// <summary>
		/// Scale name
		/// </summary>
		private String name;
		/// <summary>
		/// Scale type
		/// </summary>
		private String type;
		/// <summary>
		/// Scale continuous value from
		/// </summary>
		private String value1;
		/// <summary>
		/// Scale continuous value to
		/// </summary>
		private String value2;
		/// <summary>
		/// Discontinuous values
		/// </summary>
		private Hashtable disconval;
		#endregion

		/// <summary>
		/// Initialize the member values.
		/// </summary>
		public Scale()
		{
			id = "";
			name = "";
			type = "D";
			value1 = "";
			value2 = "";
		}

		#region Properties
		public String ID 
		{
			set { id = value; }
			get { return id; }
		}

		public String NAME 
		{
			set { name = value; }
			get { return name; }
		}

		public String TYPE
		{
			set { 
				String s = value.ToUpper();
				if(!s.Equals("C") && !s.Equals("D")) 
				{
					type = "C";
				}
				else 
				{
					type = s;
					disconval = new Hashtable();
				}
			}
			get { return type; }
		}

		public String VALUE1 
		{
			set { value1 = value; }
			get { return value1; }
		}

		public String VALUE2
		{
			set { value2 = value; }
			get { return value2; }
		}
		#endregion

		/// <summary>
		/// Adds a discontinuous value.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
		public void AddDisconValue(object key, object val) 
		{
			disconval.Add(key, val);
		}

		/// <summary>
		/// Checks if this object is discountinuous
		/// </summary>
		/// <returns>true</returns>
		public bool IsDisContinuous() 
		{
			if(disconval == null)
				return false;
			else
				return true;
		}

		/// <summary>
		/// Gets the discontinuous values.
		/// </summary>
		/// <returns>strnig</returns>
		public String GetDisconValues() 
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			IDictionaryEnumerator e = disconval.GetEnumerator();
			while(e.MoveNext()) 
			{
				sb.Append(e.Key);
				sb.Append("|");
				sb.Append(e.Value);
				sb.Append("\r\n");
			}
			return sb.ToString();
		}
	}
}
