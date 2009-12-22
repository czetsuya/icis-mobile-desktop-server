/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.IO;
using System.Windows.Forms;

namespace IcisMobileDesktopServer.Framework.Helper
{
	/// <summary>
	/// Summary description for LogHelper.
	/// </summary>
	public class LogHelper
	{
		private static LogHelper instance = null;
		private const String LOGFILE = "//log.txt"; //default log file
		private String executableDirectoryName;

		/// <summary>
		/// Creates the log file in the base directory of the application.
		/// </summary>
		public LogHelper()
		{
			FileInfo executableFileInfo = new FileInfo(Application.ExecutablePath);
			executableDirectoryName = executableFileInfo.DirectoryName;
			FileStream fs = null;
			try 
			{
				if(!File.Exists(executableDirectoryName + LOGFILE)) 
				{
					fs = File.Create(executableDirectoryName + LOGFILE);
				}
			} 
			catch(Exception e) { } 
			finally 
			{
				if(fs != null) 
				{
					fs.Close();
				}
			}
		}

		/// <summary>
		/// Creates an instance of the logger class.
		/// </summary>
		/// <returns>LogHelper</returns>
		public static LogHelper Instance() 
		{
			if(instance == null) 
			{
				instance = new LogHelper();
			}
			return instance;
		}

		/// <summary>
		/// Writes log.
		/// </summary>
		/// <param name="log">string</param>
		public void WriteLog(String log) 
		{
			StreamWriter writer = null;

			try 
			{
				if(!File.Exists(executableDirectoryName + LOGFILE)) 
				{ //file does not exists so create
					using(writer = File.CreateText(executableDirectoryName + LOGFILE))
					{
						writer.WriteLine("ICIS-Mobile Log");
						writer.WriteLine(">" + DateTime.Now.ToString());
						writer.WriteLine(log);
						writer.Flush();
					}
				} 
				else 
				{ //file exists - append
					using(writer = File.AppendText(executableDirectoryName + LOGFILE))
					{
						writer.WriteLine(">" + DateTime.Now.ToString());
						writer.WriteLine(log);
						writer.Flush();
					}
				}
			} 
			catch(Exception e) 
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				if(writer != null) 
				{
					writer.Close();
				}
			}
		}

		/// <summary>
		/// Dispose this object.
		/// </summary>
		public void Dispose() 
		{
			instance.Dispose();
			instance = null;
		}
	}
}
