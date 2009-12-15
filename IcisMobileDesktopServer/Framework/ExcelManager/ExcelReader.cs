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
	public class ExcelReader
	{
		/// <summary>
		/// excel application
		/// </summary>
		private Microsoft.Office.Interop.Excel.ApplicationClass excelApp;
		private Workbooks workBooks;
		private Workbook workBook;
		private Worksheet workSheet;
		private String docname;

		/// <summary>
		/// Initialize the excel object.
		/// </summary>
		/// <param name="docname"></param>
		public void InitExcel(String docname) 
		{
			this.docname = docname;
			excelApp = new ApplicationClass();
			Missing m = Missing.Value;
			workBooks = excelApp.Workbooks;
			workBook = workBooks.Open(docname, m, m, m, m, m, m, m, m, m, m, m, m, m, m);
			SelectWorksheet(1); //default sheet
		}

		/// <summary>
		/// Select a worksheet.
		/// </summary>
		/// <param name="i"></param>
		public void SelectWorksheet(int i) 
		{
			workSheet = (Worksheet)workBook.Sheets[i];
		}

		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <returns>string</returns>
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

		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell coordinates</param>
		/// <returns>string</returns>
		public String GetCell(int[] x) 
		{
			return GetCell(x[0], x[1]);
		}

		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <returns>int</returns>
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

		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell coordinates</param>
		/// <returns>int</returns>
		public int GetInt(int[] x)
		{
			return GetInt(x[0], x[1]);
		}

		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <returns>long</returns>
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

		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell coordinates</param>
		/// <returns>long</returns>
		public long GetLong(int[] x) 
		{
			return GetLong(x[0], x[1]);
		}

		/// <summary>
		/// Gets a value from cell.
		/// </summary>
		/// <param name="x">row</param>
		/// <param name="y">column</param>
		/// <returns>object</returns>
		public object GetObject(int x, int y) 
		{
			Range r = (Range)workSheet.Cells[x, y];
			return r.Value2;
		}

		/// <summary>
		/// Gets an object value from a cell.
		/// </summary>
		/// <param name="x">cell coordinate</param>
		/// <returns>object</returns>
		public object GetObject(int[] x) 
		{
			return GetObject(x[0], x[1]);
		}

		/// <summary>
		/// Gets a value in a range of cells.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <returns>string</returns>
		public String GetRange(int x, int y) 
		{
			Range r = (Range)workSheet.Cells[x, y];
			if(r.Value2 == null)
				return "";
			else
				return (String)r.Value2;
		}

		/// <summary>
		/// Sets the value of an excel cell.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <param name="val">value</param>
		public void SetCell(int x, int y, object val) 
		{
			workSheet.Cells[x, y] = val;
		}

        /// <summary>
        /// Close the current open WorkBook.
        /// </summary>
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

		/// <summary>
		/// Saves the open workbook.
		/// </summary>
		public void Save() 
		{
			workBook.Save();
		}

		/// <summary>
		/// Quits and dispose the excel application.
		/// </summary>
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
