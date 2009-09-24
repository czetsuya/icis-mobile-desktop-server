/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.IO;

namespace IcisMobileDesktopServer.Framework.RAPI
{
	/// <summary>
	/// Summary description for Rapi.
	/// </summary>
	public class Rapi
	{
		private Helper.ResourceHelper resourceHelper;

		public Rapi(Helper.ResourceHelper resourceHelper)
		{
			this.resourceHelper = resourceHelper;	
		}

		public void CopyFilePCtoPDA(String filePath, String desFile) 
		{	
			return;
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
					Helper.MessageHelper.ShowInfo(resourceHelper.GetString("conn_timeout"));
				}
			} 
			catch(Exception e) 
			{
				Helper.MessageHelper.ShowError(e.Message);
				RapiApi.CeRapiUninit();
				Helper.LogHelper.Instance().WriteLog(e.Message);
			}
			finally 
			{
				RapiApi.CeRapiUninit();
				sourceFile.Close();
			}
		}

		public void Dispose() 
		{
			RapiApi.CeRapiUninit();
		}
	}
}
