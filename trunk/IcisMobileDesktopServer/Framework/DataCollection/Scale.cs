using System;
using System.Collections;

namespace IcisMobileDesktopServer.Framework.DataCollection
{
	/// <summary>
	/// Summary description for Scale.
	/// </summary>
	public class Scale
	{
		private String id;
		private String name;
		private String type;
		private String value1;
		private String value2;

		private Hashtable disconval;

		public Scale()
		{
			id = "";
			name = "";
			type = "D";
			value1 = "";
			value2 = "";
		}

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

		public void AddDisconValue(object key, object val) 
		{
			disconval.Add(key, val);
		}

		public bool IsDisContinuous() 
		{
			if(disconval == null)
				return false;
			else
				return true;
		}

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
