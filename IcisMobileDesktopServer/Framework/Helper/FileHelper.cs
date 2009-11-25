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
		/// <summary>
		/// Reads an xml schema.
		/// </summary>
		/// <param name="file">source file</param>
		/// <returns>string</returns>
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

		/// <summary>
		/// Writes schema to file.
		/// </summary>
		/// <param name="file">destination file</param>
		/// <param name="s">string to write</param>
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

		/// <summary>
		/// Writes string to file. This class might be changed.
		/// </summary>
		/// <param name="file">destination file</param>
		/// <param name="s">string to write</param>
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

		/// <summary>
		/// Append to file
		/// </summary>
		/// <param name="file">destination file</param>
		/// <param name="s">string to write</param>
		public static void AppendToFile(String file, String s) 
		{
			try 
			{	
				using(StreamWriter sw = new StreamWriter(file, true))
				{
					sw.Write(s);
				}
			} 
			catch(Exception e) 
			{
				LogHelper.Instance().WriteLog(e.Message);
			}
		}

		/// <summary>
		/// Checks if the specified file exists.
		/// </summary>
		/// <param name="path">file path</param>
		/// <returns>bool</returns>
		public static bool IsExists(string path) 
		{
			return File.Exists(path);
		}
	}
}
