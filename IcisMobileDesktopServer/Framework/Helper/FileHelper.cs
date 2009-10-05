/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.IO;

namespace IcisMobileDesktopServer.Framework.Helper
{
	/// <summary>
	/// Summary description for FileHelper.
	/// </summary>
	public class FileHelper
	{
		public FileHelper()
		{
			
		}

		public static String ReadAsSchema(String file) 
		{
			String ret = "";
			try 
			{
				if(!File.Exists(file)) 
				{
					ret = "";
				} 
				else 
				{
					using (StreamReader sr = new StreamReader(file)) 
					{
                        ret = sr.ReadToEnd();
					}
				}
			} 
			catch(FileNotFoundException e) 
			{
				ret = "";
				LogHelper.Instance().WriteLog(e.Message);
			}
			return ret;
		}

		public static void WriteSchema(String file, String s) 
		{
			try 
			{
				if(File.Exists(file)) 
				{
					File.Delete(file);
				}
				using(StreamWriter sw = new StreamWriter(file)) 
				{
					sw.Write(s);
				}
			} 
			catch(Exception e) 
			{
				LogHelper.Instance().WriteLog(e.Message);
			}
		}

		public static void WriteToFile(String file, String s) 
		{
			try 
			{
				if(File.Exists(file)) 
				{
					File.Delete(file);
				}
				using(StreamWriter sw = new StreamWriter(file)) 
				{
					sw.Write(s);
				}
			} 
			catch(Exception e) 
			{
				LogHelper.Instance().WriteLog(e.Message);
			}
		}

		public static bool IsExists(string path) 
		{
			return File.Exists(path);
		}
	}
}
