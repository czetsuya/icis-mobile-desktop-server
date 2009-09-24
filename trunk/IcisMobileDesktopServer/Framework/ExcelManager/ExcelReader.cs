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
	public abstract class ExcelReader
	{
		private Microsoft.Office.Interop.Excel.ApplicationClass excelApp;
		private Workbook workBook;
		private Worksheet workSheet;

		public void InitExcel(String docname) 
		{
			excelApp = new ApplicationClass();
			Missing m = Missing.Value;
			workBook = excelApp.Workbooks.Open(docname, m, m, m, m, m, m, m, m, m, m, m, m, m, m);
			SelectWorksheet(1);
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

		public void DisposeExcel() 
		{
			if(excelApp != null) 
			{	
				excelApp.Quit();
			}
		}
	}
}
