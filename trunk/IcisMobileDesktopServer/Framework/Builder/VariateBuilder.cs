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
		public VariateBuilder()
		{

		}

		internal static void SetVariates(Engine engine) 
		{
			int[] x_variate = engine.resourceHelper.GetIntPair("variate_cell");
			int row_index = x_variate[0];
			
			while(engine.GetCell(row_index, x_variate[1]) != "") 
			{
				Variate variate = new Variate();
				variate.NAME = engine.GetCell(row_index, x_variate[1]);
				variate.PROPERTY = engine.GetCell(row_index, engine.column_index[0]);
				variate.SCALE = engine.GetCell(row_index, engine.column_index[1]);
				variate.METHOD = engine.GetCell(row_index, engine.column_index[2]);
				variate.DATATYPE = engine.GetCell(row_index, engine.column_index[3]);

				engine.study.AddVariate(variate);
				row_index++;
			}
		}

		internal static void SetVariatePropertyID(Engine engine) 
		{
			DataAccessHelper local = new DataAccessHelper(engine.localDMS);
			DataAccessHelper central = new DataAccessHelper(engine.centralDMS);
			String sql = "";
			String result;

			for(int i = 0; i < engine.study.GetVariates().Count; i++) 
			{	
				Variate variate = engine.study.GetVariate(i);
				sql = String.Format("SELECT traitid FROM trait WHERE TRNAME='{0}'", variate.PROPERTY);
				result = local.GetScalar(sql);

				if(result != "") 
				{
					variate.PROPERTYID = result;
				}
				else 
				{
					result = central.GetScalar(sql);
					variate.PROPERTYID = result;
				}
				engine.study.SetVariate(i, variate);
			}

		}

		/// <summary>
		/// Set the scales
		/// </summary>
		internal static void SetVariateScales(Engine engine) 
		{
			DataAccessHelper local = new DataAccessHelper(engine.localDMS);
			DataAccessHelper central = new DataAccessHelper(engine.centralDMS);
			String sql = "";
			String[] result;
			Scale scale = null;

			for(int i = 0; i < engine.study.GetVariates().Count; i++)
			{
				Variate variate = engine.study.GetVariate(i);
				sql = String.Format("SELECT scaleid, sctype FROM scale WHERE scname='{0}' AND traitid={1}", variate.SCALE, variate.PROPERTYID);
				
				result = local.GetPair(sql);
				if(result != null) 
				{
					variate.SCALEID = result[0];
				}
				else 
				{
					result = central.GetPair(sql);
					variate.SCALEID = result[0];
				}
				engine.study.SetVariate(i, variate);
				
				scale = new Scale();
				scale.ID = result[0];
				scale.NAME = variate.SCALE;
				scale.TYPE = result[1];

				//get scale values
				if(scale.TYPE.ToUpper().Equals("C")) //just 1-row
				{ //continuous
					sql = String.Format("SELECT slevel, elevel FROM scalecon WHERE scaleid={0}", result[0]);
					
					result = local.GetPair(sql); //seek local
					if(result != null) 
					{
						scale.VALUE1 = result[0];
						scale.VALUE2 = result[1];
					}
					else //seek central
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
	}
}
