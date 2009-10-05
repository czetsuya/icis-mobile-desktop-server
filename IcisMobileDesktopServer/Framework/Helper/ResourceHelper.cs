/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Resources;

namespace IcisMobileDesktopServer.Framework.Helper
{
	/// <summary>
	/// Summary description for ResourceHelper.
	/// </summary>
	public class ResourceHelper
	{
		//public static ResourceHelper instance = null;
		private ResourceManager rm;

		public ResourceHelper(String resource) 
		{	
			rm = new ResourceManager("IcisMobileDesktopServer.Resource." + resource, System.Reflection.Assembly.GetExecutingAssembly());
		}

		public static string GetStaticString(string resource, string name) 
		{
			ResourceManager rm = new ResourceManager("IcisMobileDesktopServer.Resource." + resource, System.Reflection.Assembly.GetExecutingAssembly());
			return rm.GetString(name);
		}

		public String GetString(String name)
		{
			try 
			{	
				return rm.GetString(name);
			} 
			catch(Exception e) 
			{
				LogHelper.Instance().WriteLog(String.Format("Failed getting property: {0} - {1}", name, e.Message));
				return "";
			}
		}

		public int[] GetIntPair(String str)
		{
			str = GetString(str);
			char[] delim = {','};
			int[] x = new int[2];
			String[] s = str.Split(delim);
			
			try 
			{
				x[0] = Convert.ToInt16(s[0]);
				x[1] = Convert.ToInt16(s[1]);
			} 
			catch(Exception e) 
			{
				LogHelper.Instance().WriteLog(String.Format("Failed getting pair property: {0} - {1}", s, e.Message));
			}
			return x;
		}

		public int GetInt(String name)
		{
			try 
			{	
				return Convert.ToInt16(rm.GetString(name));
			} 
			catch(Exception e) 
			{
				LogHelper.Instance().WriteLog(String.Format("Failed getting property: {0} - {1}", name, e.Message));
				return 0;
			}
		}
	}
}
