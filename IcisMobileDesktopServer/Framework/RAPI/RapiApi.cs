/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System.Runtime.InteropServices;
using System;

namespace IcisMobileDesktopServer.Framework.RAPI
{
	public class RapiApi
	{
		[StructLayout(LayoutKind.Sequential)]
			public struct RAPIINIT
		{
			public int cbSize;
			public int heRapiInit;
			public int hrRapiInit;
		};

		public static int FILE_ATTRIBUTE_NORMAL = 128;
		public static int INVALID_HANDLE_VALUE = -1;

		public static int S_OK = 0;
		public static int GENERIC_READ = -2147483648;
		public static int GENERIC_WRITE = -1073741824;		
		public static int CREATE_NEW = 1;
		public static int CREATE_ALWAYS = 2;
		public static int OPEN_EXISTING = 3;
		public static int OPEN_ALWAYS = 4;
		public static int TRUNCATE_EXISTING = 5;
		
		public static int ERROR_FILE_EXISTS = 80;
		public static int ERROR_INVALID_PARAMETER = 87;
		public static int ERROR_DISK_FULL = 112;
		public static int ERROR_FILE_NOT_FOUND = 2;

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern int WaitForSingleObject(int handle, int milliseconds);

		[DllImport("Kernel32.dll", EntryPoint = "RtlZeroMemory", SetLastError = false)]
		public static extern void ZeroMemory(IntPtr dest, int size);

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeRapiInit();

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeRapiInitEx(ref RAPIINIT pRapiInit);

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeCreateFile(string lpfilename, int dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplatefile);

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeGetFileSize(int hFile, int lpFileSizeHigh);

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeReadFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, int lpOverlapped);

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeWriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, int lpOverlapped);

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeCloseHandle(int hobject);

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeRapiUninit();

		[DllImport("rapi.dll", CharSet = CharSet.Unicode)]
		public static extern int CeDeleteFile(string DeviceFileName);
	}
}
