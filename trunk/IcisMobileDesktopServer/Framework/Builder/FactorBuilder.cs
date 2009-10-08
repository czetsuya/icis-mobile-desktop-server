/**
 * @author edwardpantojalegaspi
 * @since 2009.09.24
 * */
using System;
using System.Data;

using IcisMobileDesktopServer.Framework.DataCollection;
using IcisMobileDesktopServer.Framework.Helper;

namespace IcisMobileDesktopServer.Framework.Builder
{
	/// <summary>
	/// Summary description for FactorBuilder.
	/// </summary>
	public class FactorBuilder
	{
		/// <summary>
		/// Reads the factor from workbook and save it to a factor data object.
		/// </summary>
		/// <param name="engine">Engine</param>
		internal static void SetFactors(Engine engine) 
		{
			if(Engine.selFactors.Length == 0)
				return;
			int[] x_factor = engine.resourceHelper.GetIntPair("factor_cell");
			int row_index = x_factor[0];
			string x = "";
			
			while((x = engine.GetExcelReader().GetCell(row_index, x_factor[1])) != "") 
			{  //read columnwise
				bool hit = false;
				for(int i = 0; i < Engine.selFactors.Length; i++) 
				{ //factors count from worksheet 1, if more than the length then move to next row
					if(Engine.selFactors[i].Equals(x)) 
					{ //factor found
						hit = true;
						break;
					}
				}
				if(!hit) 
				{
					row_index++;
					continue;
				}

				//sets the factor values
				Factor factor = new Factor();
				factor.NAME = engine.GetExcelReader().GetCell(row_index, x_factor[1]);
				factor.PROPERTY = engine.GetExcelReader().GetCell(row_index, engine.column_index[0]);
				factor.SCALE = engine.GetExcelReader().GetCell(row_index, engine.column_index[1]);
				factor.METHOD = engine.GetExcelReader().GetCell(row_index, engine.column_index[2]);
				factor.DATATYPE = engine.GetExcelReader().GetCell(row_index, engine.column_index[3]);

				//add the factor to study
				engine.study.AddFactor(factor);
				//move to next row
				row_index++;
			}
			engine.readFactors = row_index - x_factor[0];
		}

		/// <summary>
		/// Sets the scale value of each factors.
		/// </summary>
		/// <param name="engine">Engine</param>
		internal static void SetFactorScales(Engine engine) 
		{
			DataAccessHelper local = new DataAccessHelper(engine.localDMS);
			DataAccessHelper central = new DataAccessHelper(engine.centralDMS);
			String sql = "";
			String[] result;
			Scale scale = null;

			for(int i = 0; i < engine.study.GetFactors().Count; i++)
			{
				Factor factor = engine.study.GetFactor(i);
				sql = String.Format("SELECT scaleid, sctype FROM scale WHERE scname='{0}'", factor.SCALE);
				
				result = local.GetPair(sql);
				if(result != null) 
				{
					factor.SCALEID = result[0];
				}
				else 
				{
					result = central.GetPair(sql);
					factor.SCALEID = result[0];
				}
				engine.study.SetFactor(i, factor);
				
				scale = new Scale();
				scale.ID = result[0];
				scale.NAME = factor.SCALE;
				scale.TYPE = result[1];

				//get scale values
				if(scale.TYPE.ToUpper().Equals("C")) //just 1-row
				{ //continuous
					sql = String.Format("SELECT slevel, elevel FROM scalecon WHERE scaleid={0}", result[0]);
					
					result = local.GetPair(sql);
					if(result != null) 
					{
						scale.VALUE1 = result[0];
						scale.VALUE2 = result[1];
					}
					else 
					{
						result = central.GetPair(sql);
						if(result != null) 
						{
							scale.VALUE1 = result[0];
							scale.VALUE2 = result[1];
						}
					}
				} 
				else 
				{ //discontinuous
					DataSet ds = null;
					DataTable table = null;

					sql = String.Format("SELECT value, valdesc FROM scaledis WHERE scaleid={0}", result[0]);
					ds = local.Query(sql);
					table = ds.Tables[0];
					
					if(table.Rows.Count > 0) 
					{ //local
						foreach(DataRow row in table.Rows) 
						{
							scale.AddDisconValue(row["value"], row["valdesc"]);
						}
					} 
					else 
					{ //central
						ds = central.Query(sql);
						table = ds.Tables[0];
						if(table.Rows.Count > 0) 
						{
							foreach(DataRow row in table.Rows) 
							{
								scale.AddDisconValue(row["value"], row["valdesc"]);
							}
						}
					}
				}

				engine.study.AddScale(scale);
			}
		}

		/// <summary>
		/// This method reads the values of factors from the observation sheet and save it to a text file.
		/// </summary>
		internal static string SetFactorValues(Engine engine) 
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			engine.GetExcelReader().SelectWorksheet(2); //observation sheet
			bool found = false;


			foreach(Factor f in engine.study.GetFactors()) 
			{
				for(int i = 0; i < engine.readFactors; i++) 
				{	
					if(f.NAME.Equals(engine.GetExcelReader().GetCell(1, i + 1))) //factor not found in obs sheet
					{
						f.COLUMN = i + 1;
						found = true;
						break;
					}
				}
				if(!found) 
				{
					break;
				}
			}

			if(!found) 
			{
				String err = engine.errResourceHelper.GetString("err_obs_sheet");
				LogHelper.Instance().WriteLog(err);
				MessageHelper.ShowError(err);
			} 
			else
			{ //no error found continue, write the factor values to a file
				int col_ctr = 1;
				int row = 2;
				while(true)
				{
					if(engine.GetExcelReader().GetObject(row, 1) == null) 
					{
						break;
					}
					col_ctr = 1;
					sb.Append(row + "|");
					foreach(Factor f in engine.study.GetFactors()) 
					{
						if(f.DATATYPE == "C")
							sb.Append(engine.GetExcelReader().GetCell(row, f.COLUMN) + "/");
						else 
							sb.Append(engine.GetExcelReader().GetLong(row, f.COLUMN) + "/");
						col_ctr++; //column
					}
					sb = sb.Remove(sb.Length - 1, 1);
					sb.Append("\r\n");
					row++; //increment row
				}
			}
			return sb.ToString();
		}
	}
}
