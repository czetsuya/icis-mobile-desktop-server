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
		private static FactorBuilder instance;

		public static FactorBuilder Instance() 
		{
			if(instance == null) 
			{
				instance = new FactorBuilder();
			}
			return instance;
		}
		/// <summary>
		/// Reads the factor from workbook and save it to a factor data object.
		/// </summary>
		/// <param name="engine">Engine</param>
		internal static void SetFactors(Engine engine) 
		{
			if(Engine.selFactors.Length == 0)
				return;
			
			int[] x_factor = new int[2];
			x_factor[0] = 1;
			x_factor[1] = 1;
			int xrow_index = x_factor[0];
			string s = "";
			while(!engine.GetExcelReader().GetCell(xrow_index, x_factor[1]).Trim().ToUpper().Equals("FACTOR")) 
			{ 
				s = engine.GetExcelReader().GetCell(xrow_index, x_factor[1]);
				xrow_index++;
			}
			x_factor[0] = xrow_index;

			string x = "";
			while((x = engine.GetExcelReader().GetCell(xrow_index, x_factor[1])) != "") 
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
					xrow_index++;
					continue;
				}

				//sets the factor values
				Factor factor = new Factor();
				factor.NAME = engine.GetExcelReader().GetCell(xrow_index, x_factor[1]);
				factor.PROPERTY = engine.GetExcelReader().GetCell(xrow_index, engine.column_index[0]);
				factor.SCALE = engine.GetExcelReader().GetCell(xrow_index, engine.column_index[1]);
				factor.METHOD = engine.GetExcelReader().GetCell(xrow_index, engine.column_index[2]);
				factor.DATATYPE = engine.GetExcelReader().GetCell(xrow_index, engine.column_index[3]);

				//add the factor to study
				engine.study.AddFactor(factor);
				//move to next row
				xrow_index++;
			}
			engine.readFactors = xrow_index - x_factor[0];
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
				
				result = local.GetPair(sql, false);
				if(result != null) 
				{
					factor.SCALEID = result[0];
				}
				else 
				{
					result = central.GetPair(sql, false);
					factor.SCALEID = result[0];
				}

				if(result != null) 
				{
					engine.study.SetFactor(i, factor);
				
					scale = new Scale();
					scale.ID = result[0];
					scale.NAME = factor.SCALE;
					scale.TYPE = result[1];

					if(result[0] != "") 
					{
						//get scale values
						if(scale.TYPE.ToUpper().Equals("C")) //just 1-row
						{ //continuous
							sql = String.Format("SELECT slevel, elevel FROM scalecon WHERE scaleid={0}", result[0]);
					
							result = local.GetPair(sql, false);
							if(result != null) 
							{
								scale.VALUE1 = result[0];
								scale.VALUE2 = result[1];
							}
							else 
							{
								result = central.GetPair(sql, false);
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
				else 
				{ //scale not found
					LogHelper.Instance().WriteLog("Missing Scale Name: " + factor.SCALE);
				}
			}
		}

		/// <summary>
		/// This method reads the values of factors from the observation sheet and save it to a text file.
		/// </summary>
		internal static void SetFactorValues(string path, Engine engine) 
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			engine.GetExcelReader().SelectWorksheet(2); //observation sheet
			string strCurrent = "";
			bool found = false;
			int xctr = 1;
			int row = 2;

			foreach(Factor f in engine.study.GetFactors()) 
			{
				for(int i = 0; i < engine.readFactors; i++) 
				{	
					SplashScreen.SplashScreen.SetStatus("Updating " + f.NAME);
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
				System.IO.File.Delete(path);
				int col_ctr = 1;				
				
				while(true)
				{
					if(engine.GetExcelReader().GetObject(row, 1) == null) 
					{
						break;
					}
					col_ctr = 1;
					sb.Append(row + "|");
					strCurrent = "";
					foreach(Factor f in engine.study.GetFactors()) 
					{						
						if(f.DATATYPE == "C") 
						{
							strCurrent += engine.GetExcelReader().GetCell(row, f.COLUMN) + "->";
						}
						else  
						{
							strCurrent += engine.GetExcelReader().GetLong(row, f.COLUMN) + "->";
						}
						col_ctr++; //column
					}
					//sb = sb.Remove(sb.Length - 1, 1);
					strCurrent = strCurrent.Remove(strCurrent.Length - 2, 2);
					sb.Append(strCurrent);
					sb.Append("\r\n");
					
					SplashScreen.SplashScreen.SetStatus("Caching " + strCurrent + "...");
					
					if(xctr++ == 1000) 
					{
						xctr = 1;
						SplashScreen.SplashScreen.SetStatus("Saving cache data...");
						System.Threading.Thread.Sleep(500);
						Helper.FileHelper.AppendToFile(path, sb.ToString());
						sb = new System.Text.StringBuilder();
						xctr = 1;
					}
					row++; //increment row
				}
			}
			if(xctr <= 1000) 
			{
				SplashScreen.SplashScreen.SetStatus("Saving cache data...");
				System.Threading.Thread.Sleep(500);
				Helper.FileHelper.AppendToFile(path, sb.ToString());
			}
		}
	}
}
