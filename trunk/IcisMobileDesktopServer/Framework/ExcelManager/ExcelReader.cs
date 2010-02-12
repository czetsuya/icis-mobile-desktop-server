/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace IcisMobileDesktopServer.Framework.ExcelManager
{
	/// <summary>
	/// Summary description for ExcelReader.
	/// </summary>
	public class ExcelReader : IExcelReader
	{
		/// <summary>
		/// excel application
		/// </summary>
		private Microsoft.Office.Interop.Excel.ApplicationClass excelApp;
		private Workbooks workBooks;
		private Workbook workBook;
		private Worksheet workSheet;
		private String docname;

		public void InitExcel(String docname) 
		{
			this.docname = docname;
			excelApp = new ApplicationClass();
			Missing m = Missing.Value;
			workBooks = excelApp.Workbooks;
			workBook = workBooks.Open(docname, m, m, m, m, m, m, m, m, m, m, m, m, m, m);
			SelectWorksheet(1); //default sheet
		}

		public void SelectWorksheet(int i) 
		{
			workSheet = (Worksheet)workBook.Sheets[i];
		}

		public String GetCell(int x, int y) 
		{
			try 
			{
				Range r = (Range)workSheet.Cells[x, y];
				if(r.Value2 == null)
					return "";
				else
					return (String)r.Value2;
			} 
			catch(InvalidCastException e) 
			{
				Helper.LogHelper.Instance().WriteLog(e.Message);
			} 
			catch(System.Runtime.InteropServices.COMException e) 
			{ 
				Helper.LogHelper.Instance().WriteLog(e.Message);
			}
			return "";
		}		

		public String GetCell(int[] x) 
		{
			return GetCell(x[0], x[1]);
		}

		public int GetInt(int x, int y) 
		{
			int z = 0;
			Range r = (Range)workSheet.Cells[x, y];
			try 
			{
				z = Convert.ToInt16(r.Value2);
			} 
			catch(Exception e) 
			{
				Helper.LogHelper.Instance().WriteLog(e.Message);
				z = 0;
			}
			return z;
		}

		public int GetInt(int[] x)
		{
			return GetInt(x[0], x[1]);
		}

		public long GetLong(int x, int y)
		{
			long z = 0;
			Range r = (Range)workSheet.Cells[x, y];
			try
			{
				z = Convert.ToInt64(r.Value2);
			}
			catch(Exception e) 
			{
				Helper.LogHelper.Instance().WriteLog(e.Message);
				z = 0;
			}
			return z;
		}

		public long GetLong(int[] x) 
		{
			return GetLong(x[0], x[1]);
		}

		public object GetObject(int x, int y) 
		{
			Range r = (Range)workSheet.Cells[x, y];
			return r.Value2;
		}

		public object GetObject(int[] x) 
		{
			return GetObject(x[0], x[1]);
		}
		
		public String GetRange(int x, int y) 
		{
			Range r = (Range)workSheet.Cells[x, y];
			if(r.Value2 == null)
				return "";
			else
				return (String)r.Value2;
		}
		
		public void SetCell(int x, int y, object val) 
		{
			workSheet.Cells[x, y] = val;
		}
       
		public void Close() 
		{
			workBook.Close(null, null, null);
		}

		public void Open() 
		{
			Missing m = Missing.Value;
			workBook = workBooks.Open(docname, m, m, m, m, m, m, m, m, m, m, m, m, m, m);
			SelectWorksheet(1); //default sheet			
			workBook.RefreshAll();
		}
		
		public void Save() 
		{
			workBook.Save();
		}

		public void DisposeExcel() 
		{
			if(excelApp != null) 
			{
				Close();
				excelApp.Quit();
				System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
				System.Runtime.InteropServices.Marshal.ReleaseComObject(workBooks);
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
		}
	}
}
