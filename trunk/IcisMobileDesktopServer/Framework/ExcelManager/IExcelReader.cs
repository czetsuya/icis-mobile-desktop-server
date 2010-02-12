using System;

namespace IcisMobileDesktopServer.Framework.ExcelManager
{
	/// <summary>
	/// Summary description for IExcelReader.
	/// </summary>
	public interface IExcelReader
	{
		/// <summary>
		/// Initialize the excel object.
		/// </summary>
		/// <param name="docname"></param>
		void InitExcel(String name);
		/// <summary>
		/// Select a worksheet.
		/// </summary>
		/// <param name="i"></param>
		void SelectWorksheet(int i);
		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <returns>string</returns>
		String GetCell(int x, int y);
		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell coordinates</param>
		/// <returns>string</returns>
		String GetCell(int[] x);
		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <returns>int</returns>
		int GetInt(int x, int y);
		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell coordinates</param>
		/// <returns>int</returns>
		int GetInt(int[] x);
		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <returns>long</returns>
		long GetLong(int x, int y);
		/// <summary>
		/// Gets a value from an excel cell.
		/// </summary>
		/// <param name="x">cell coordinates</param>
		/// <returns>long</returns>
		long GetLong(int[] x);
		/// <summary>
		/// Gets a value from cell.
		/// </summary>
		/// <param name="x">row</param>
		/// <param name="y">column</param>
		/// <returns>object</returns>
		object GetObject(int x, int y);
		/// <summary>
		/// Gets an object value from a cell.
		/// </summary>
		/// <param name="x">cell coordinate</param>
		/// <returns>object</returns>
		object GetObject(int[] x);
		/// <summary>
		/// Gets a value in a range of cells.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <returns>string</returns>
		String GetRange(int x, int y);
		/// <summary>
		/// Sets the value of an excel cell.
		/// </summary>
		/// <param name="x">cell row</param>
		/// <param name="y">cell column</param>
		/// <param name="val">value</param>
		void SetCell(int x, int y, object val);
		/// <summary>
		/// Close the current open WorkBook.
		/// </summary>
		void Close();
		
		void Open();
		/// <summary>
		/// Saves the open workbook.
		/// </summary>
		void Save();
		/// <summary>
		/// Quits and dispose the excel application.
		/// </summary>
		void DisposeExcel();
	}
}
