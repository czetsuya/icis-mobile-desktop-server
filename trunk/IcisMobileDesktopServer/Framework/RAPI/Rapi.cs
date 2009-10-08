/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.IO;
using System.Windows.Forms;

namespace IcisMobileDesktopServer.Framework.RAPI
{
	/// <summary>
	/// Summary description for Rapi.
	/// </summary>
	public class Rapi
	{
		private Engine engine;

		public Rapi(Engine engine)
		{
			this.engine = engine;
		}

		/// <summary>
		/// Copies a file from Desktop to PDA.
		/// </summary>
		/// <param name="filePath">desktop source file</param>
		/// <param name="desFile">mobile destination file</param>
		/// <returns>true if file is transferred</returns>
		public bool CopyFilePCtoPDA(String filePath, String desFile) 
		{
			bool flag = true;
			FileStream sourceFile = null;

			try 
			{
				RapiApi.RAPIINIT ri = new RapiApi.RAPIINIT();
				ri.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(ri);

				int hr = RapiApi.CeRapiInitEx(ref ri);
				if (ri.hrRapiInit == RapiApi.S_OK) 
				{
					sourceFile = new FileStream(filePath, FileMode.Open);
					int Handle = RapiApi.CeCreateFile(desFile, RapiApi.GENERIC_WRITE, 0, 0, RapiApi.CREATE_ALWAYS, RapiApi.FILE_ATTRIBUTE_NORMAL, 0);

					byte[] bytes = new byte[sourceFile.Length];
					int numBytesToRead = (int)sourceFile.Length;
					int numBytesRead = 0;
					int lpNumberofBytesWritten = 0;

					while (numBytesToRead > 0)
					{
						// Read may return anything from 0 to numBytesToRead.
						int n = sourceFile.Read(bytes, numBytesRead, numBytesToRead);

						// Break when the end of the file is reached.
						if (n == 0)
							break;

						numBytesRead += n;
						numBytesToRead -= n;
					}
					numBytesToRead = bytes.Length;
			
					RapiApi.CeWriteFile(Handle, bytes, numBytesToRead, ref lpNumberofBytesWritten, 0);
					RapiApi.CeCloseHandle(Handle);
				}
				else 
				{
					SplashScreen.SplashScreen.CloseForm();
					Helper.MessageHelper.ShowInfo(engine.errResourceHelper.GetString("conn_timeout"));
					flag = false;
				}
			} 
			catch(Exception e) 
			{
				SplashScreen.SplashScreen.CloseForm();
				RapiApi.CeRapiUninit();
				Helper.MessageHelper.ShowError(e.Message);
				Helper.LogHelper.Instance().WriteLog(e.Message);
				flag = false;
			}
			finally 
			{
				RapiApi.CeRapiUninit();
				if(sourceFile != null)
					sourceFile.Close();
			}
			return flag;
		}
		
		
		public delegate void CopyFileCallBack(int bytesCopied, int totalBytes);
		/// <summary>
		/// CallBack to avoid cross-threads.
		/// </summary>
		/// <param name="bytesCopied">actual bytes copied</param>
		/// <param name="totalBytes">total bytes to be copied</param>
		private void CallBack(int bytesCopied, int totalBytes)
		{
			//if not null update the progress bytes
			if ((engine.progressBar != null) && (bytesCopied > 0 || totalBytes > 0))
			{
				engine.progressBar.Maximum = totalBytes;
				engine.progressBar.Value = bytesCopied;
			}
			engine.progressBar.Refresh();
		}

		/// <summary>
		/// Copies file from PDA to Desktop
		/// </summary>
		/// <param name="deskTopFileName">desktop destination file</param>
		/// <param name="PDAFileName">mobile source file</param>
		public void CopyPDAtoPC(string deskTopFileName, string PDAFileName)
		{
			engine.progressBar.Enabled = true;
			engine.progressBar.Visible = true;
			engine.progressBar.Show();

			CopyPDAtoPC(deskTopFileName, PDAFileName, null);
		}

		/// <summary>
		/// Copies file from PDA to Desktop
		/// </summary>
		/// <param name="deskTopFileName">desktop destination file</param>
		/// <param name="PDAFileName">mobile source file</param>
		/// <param name="cb"></param>
		public void CopyPDAtoPC(string deskTopFileName, string PDAFileName, CopyFileCallBack cb)
		{
			int Handle;
			int bufferSize = 32768;
			byte[] lpBuffer = new byte[bufferSize + 1];
			System.IO.FileStream outFile;

			try
			{
				#region If no PDA connected connection timeout
				RapiApi.RAPIINIT ri = new RapiApi.RAPIINIT();
				ri.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(ri);

				int hr = RapiApi.CeRapiInitEx(ref ri);
				if (ri.hrRapiInit == RapiApi.S_OK)
				{
					//initialized file for reading
					outFile = new System.IO.FileStream(deskTopFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
					int lpNumberofBytesRead = bufferSize;
					Handle = RapiApi.CeCreateFile(PDAFileName, RapiApi.GENERIC_READ, 0, 0, RapiApi.OPEN_EXISTING, RapiApi.FILE_ATTRIBUTE_NORMAL, 0);

					int fileSize = RapiApi.CeGetFileSize(Handle, 0);
					int bytesToRead = bufferSize;
					int bytesRead = 0;

					while (lpNumberofBytesRead > 0)
					{
						RapiApi.CeReadFile(Handle, lpBuffer, bytesToRead, ref lpNumberofBytesRead, 0);
						outFile.Write(lpBuffer, 0, lpNumberofBytesRead);
						bytesRead += lpNumberofBytesRead;
						if ((cb != null))
						{
							cb(bytesRead, fileSize);
						}
						else
						{
							CallBack(bytesRead, fileSize);
						}
					}
					outFile.Close();
					RapiApi.CeCloseHandle(Handle);
				}
				else
				{
					throw new Exception("Timeout - No Device");
				}
				#endregion
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				engine.progressBar.Enabled = false;
				engine.progressBar.Visible = false;
				RapiApi.CeRapiUninit();
			}            
		}

		/// <summary>
		/// Dispose this object.
		/// </summary>
		public void Dispose() 
		{
			RapiApi.CeRapiUninit();
		}
	}
}
