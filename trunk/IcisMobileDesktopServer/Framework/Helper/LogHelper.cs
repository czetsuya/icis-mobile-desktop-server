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
		private const String LOGFILE = "//log.txt";
		private String executableDirectoryName;

		public LogHelper()
		{
			FileInfo executableFileInfo = new FileInfo(Application.ExecutablePath);
			executableDirectoryName = executableFileInfo.DirectoryName;
			if(!File.Exists(executableDirectoryName + LOGFILE)) 
			{
				File.Create(executableDirectoryName + LOGFILE);
			}
		}

		public static LogHelper Instance() 
		{
			if(instance == null) 
			{
				instance = new LogHelper();
			}
			return instance;
		}

		public void WriteLog(String log) 
		{
			StreamWriter writer = null;

			try 
			{
				if(!File.Exists(executableDirectoryName + LOGFILE)) 
				{
					using(writer = File.CreateText(executableDirectoryName + LOGFILE))
					{
						writer.WriteLine("ICIS-Mobile Log");
						writer.WriteLine(">" + DateTime.Now.ToString());
						writer.WriteLine(log);
						writer.Flush();
					}
				} 
				else 
				{
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
			}
			finally
			{
				if(writer != null) 
				{
					writer.Close();
				}
			}
		}

		public void Dispose() 
		{
			instance.Dispose();
			instance = null;
		}
	}
}
