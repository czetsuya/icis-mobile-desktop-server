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
	/// Summary description for VariateBuilder.
	/// </summary>
	public class VariateBuilder
	{
		/// <summary>
		/// Sets the values of variates.
		/// </summary>
		/// <param name="engine">Engine</param>
		internal static void SetVariates(Engine engine) 
		{
			int ctr = 1;
			int col_index = 1;
			while(!engine.GetExcelReader().GetCell(ctr++, col_index).Trim().ToUpper().Equals("VARIATE")) { }

			int row_index = ctr;			
			
			while(engine.GetExcelReader().GetCell(row_index, col_index) != "") 
			{
				Variate variate = new Variate();
				variate.NAME = engine.GetExcelReader().GetCell(row_index, col_index);
				variate.PROPERTY = engine.GetExcelReader().GetCell(row_index, engine.column_index[0]);
				variate.SCALE = engine.GetExcelReader().GetCell(row_index, engine.column_index[1]);
				variate.METHOD = engine.GetExcelReader().GetCell(row_index, engine.column_index[2]);
				variate.DATATYPE = engine.GetExcelReader().GetCell(row_index, engine.column_index[3]);

				engine.study.AddVariate(variate);
				row_index++;
			}
		}

		/// <summary>
		/// Sets the variate property id.
		/// Property ID is used to filter the scale and method.
		/// </summary>
		/// <param name="engine">Engine</param>
		internal static void SetVariatePropertyID(Engine engine) 
		{
			DataAccessHelper local = new DataAccessHelper(engine.localDMS);
			DataAccessHelper central = new DataAccessHelper(engine.centralDMS);
			String sql = "";
			String result;
			bool flag = true;

			for(int i = 0; i < engine.study.GetVariates().Count; i++) 
			{
				flag = true;
				Variate variate = engine.study.GetVariate(i);
				if(variate.PROPERTY != "") 
				{
					SplashScreen.SplashScreen.SetStatus("Searching for property: " + variate.PROPERTY);
					sql = String.Format("SELECT traitid FROM trait WHERE TRNAME='{0}'", variate.PROPERTY);
					result = local.GetScalar(sql);					

					if(result != "") 
					{ //query local
						variate.PROPERTYID = result;
					}
					else 
					{ //else query central
						result = central.GetScalar(sql);
						if(result != "") 
						{
							variate.PROPERTYID = result;
						} 
						else 
						{ //not found
							flag = false;
							LogHelper.Instance().WriteLog("Missing Property: " + variate.PROPERTY);
						}
					}
					if(flag) 
					{
						engine.study.SetVariate(i, variate);
					}
				}
			}

		}

		/// <summary>
		/// Set the scales.
		/// </summary>
		internal static void SetVariateScales(Engine engine) 
		{
			DataAccessHelper local = new DataAccessHelper(engine.localDMS);
			DataAccessHelper central = new DataAccessHelper(engine.centralDMS);
			String sql = "";
			String[] result;
			Scale scale = null;
			bool flag = true;

			for(int i = 0; i < engine.study.GetVariates().Count; i++)
			{
				Variate variate = engine.study.GetVariate(i);

				if(variate.PROPERTYID == "")
					continue;

				SplashScreen.SplashScreen.SetStatus("Updating " + variate.SCALE);
				sql = String.Format("SELECT scaleid, sctype FROM scale WHERE scname='{0}' AND traitid={1} ORDER BY scaleid ASC", variate.SCALE, variate.PROPERTYID);
				
				flag = true;
				result = local.GetPair(sql, false);
				if(result != null) 
				{ //query local
					variate.SCALEID = result[0];
				}
				else 
				{ //query local
					result = central.GetPair(sql, false);
					if(result != null)
						variate.SCALEID = result[0];
					else 
					{
						flag = false;
						LogHelper.Instance().WriteLog("Missing scale: " + variate.SCALE);
					}
				}

				if(flag) 
				{
					engine.study.SetVariate(i, variate);
				
					scale = new Scale();
					scale.ID = result[0];
					scale.NAME = variate.SCALE;
					scale.TYPE = result[1];

					//get scale values
					if(scale.TYPE.ToUpper().Equals("C")) //just 1-row
					{ //continuous
						sql = String.Format("SELECT slevel, elevel FROM scalecon WHERE scaleid={0}", result[0]);
					
						result = local.GetPair(sql, false); //seek local
						if(result != null) 
						{
							scale.VALUE1 = result[0];
							scale.VALUE2 = result[1];
						}
						else //seek central
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
						if(result != null) 
						{
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
					}
					engine.study.AddScale(scale);
				}
			}
		}
	}
}
