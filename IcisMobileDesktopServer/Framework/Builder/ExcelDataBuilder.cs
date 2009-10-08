/**
 * @author edwardpantojalegaspi
 * @since 2009.09.30
 * This class writes the values from mobile device to excel document.
 * */
using System;
using System.IO;
using System.Collections;

using IcisMobileDesktopServer.Framework.DataCollection;

namespace IcisMobileDesktopServer.Framework.Builder
{
	/// <summary>
	/// Summary description for ExcelDataBuilder.
	/// </summary>
	public class ExcelDataBuilder
	{
		private ExcelManager.ExcelReader excelObject;
		private Engine engine;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="engine"></param>
		/// <param name="excelfile"></param>
		public ExcelDataBuilder(Engine engine, string excelfile)
		{
			this.engine = engine;
            excelObject = new ExcelManager.ExcelReader();
			excelObject.InitExcel(excelfile);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="datafile"></param>
		/// <returns></returns>
		public bool Process(string datafile) 
		{
			//check if the same worksheet/study
			if(!ValidateStudy(datafile)) 
			{ //valid
				Framework.Helper.MessageHelper.ShowInfo(engine.errResourceHelper.GetString("m_study_not_match"));
				return false;
			} 
			else 
			{ //begin processing
				excelObject.SelectWorksheet(2);
				WriteToExcel(MatchExcelColumn(ReadFileToEnd(datafile)));
				excelObject.Save();
				return true;
			}
		}

		/// <summary>
		/// Checks if the study name retrieved from mobile is the same
		/// as the name of the study in the selected workbook.
		/// </summary>
		/// <param name="datafile">file from mobile</param>
		/// <returns>bool</returns>
		private bool ValidateStudy(string datafile) 
		{
			excelObject.SelectWorksheet(1);
			string study_name = GetStudyNameFromFile(datafile);
			try 
			{
				if(excelObject.GetCell(engine.resourceHelper.GetIntPair("name_cell")).Trim() != study_name) 
				{
					return false;
				}
			} 
			catch(NullReferenceException e) 
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Gets the study name from file downloaded from mobile.
		/// </summary>
		/// <param name="datafile">file from mobile</param>
		/// <returns>string</returns>
		private string GetStudyNameFromFile(string datafile) 
		{
			string study_name = "";
			using(TextReader reader = new StreamReader(datafile)) 
			{
				try 
				{
					study_name = reader.ReadLine();
					study_name = study_name.Substring(1, study_name.Length - 2);
					string[] pair = study_name.Split('=');
					if(pair[0] == "czetsuya")
						study_name = pair[1];
				} 
				catch(Exception e) 
				{
					Framework.Helper.LogHelper.Instance().WriteLog(e.Message);
					study_name = "";
				}
			}
			return study_name;
		}

		/// <summary>
		/// Sets the value of specified excel cells based on the data
		/// downloaded from the mobile.
		/// </summary>
		/// <param name="arrTemp">Array of data</param>
		private void WriteToExcel(ArrayList arrTemp) 
		{
			foreach(DeviceData data in arrTemp) 
			{
				foreach(string str in data.GetValues()) 
				{
					string[] pair = new string[2];
					int x = str.IndexOf("=");
					pair[0] = str.Substring(0, x);
					pair[1] = str.Substring(x + 1);
					int row = 1;
					try 
					{
						row = Convert.ToInt16(pair[0]);
					} 
					catch(Exception e) 
					{
						row = 1;
					}
					excelObject.SetCell(row, data.COLUMN, pair[1]);
				}
			}
		}

		/// <summary>
		/// This function match the column number of each variate downloaded,
		/// which will be used in filling the excel workbook cells with values.
		/// </summary>
		/// <param name="arrTemp">ArrayList of data</param>
		/// <returns>ArrayList</returns>
		private ArrayList MatchExcelColumn(ArrayList arrTemp) 
		{
			ArrayList newList = new ArrayList();
			foreach(DeviceData data in arrTemp) 
			{
				string line = "";
				int col = 1;
				while((line = excelObject.GetCell(1, col)) != "") 
				{ //while end of column
					if(line.Equals(data.NAME))
					{
						data.COLUMN = col;
						newList.Add(data);
						break;
					}
					col++;
				}
			}

			return newList;
		}

		/// <summary>
		/// Reads the data file and save the value to a temporary array of DeviceData object.
		/// </summary>
		/// <param name="datafile">data file from mobile</param>
		/// <returns>ArrayList</returns>
		private ArrayList ReadFileToEnd(string datafile) 
		{
			ArrayList arrTemp = new ArrayList();
			DeviceData data = new DeviceData();

			using(TextReader reader = new StreamReader(datafile)) 
			{
				while(reader.Peek() != -1) 
				{
					string line = reader.ReadLine();
					if(line[0] == '[' && line[line.Length - 1] == ']') 
					{ //variate, search column number
						data = new DeviceData();
						data.NAME = line.Substring(1, line.Length - 2);
						arrTemp.Add(data);
					} 
					else 
					{
						if(data != null) 
						{
							data.AddValue(line);
						}
					}
				}
			}
			return arrTemp;
		}

		/// <summary>
		/// Dispose this object.
		/// </summary>
		public void Dispose() 
		{
			excelObject.DisposeExcel();
		}
	}
}
