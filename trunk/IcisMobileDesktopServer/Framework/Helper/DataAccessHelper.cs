using System;
using System.Data;
using System.Data.OleDb;

namespace IcisMobileDesktopServer.Framework.Helper
{
	/// <summary>
	/// Summary description for DataAccessHelper.
	/// </summary>
	public class DataAccessHelper
	{
		OleDbConnection conn;
		OleDbCommand cmd;

		public DataAccessHelper(String path)
		{
			conn = new OleDbConnection();
			conn.ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", path);
		}

		/// <summary>
		/// Gets the value of the data reader.
		/// </summary>
		/// <param name="reader">data reader</param>
		/// <param name="i">index</param>
		/// <returns>object</returns>
		private object GetValue(OleDbDataReader reader, int i) 
		{
			object obj = "";
			try 
			{
				obj = reader.GetString(i);
			} 
			catch(InvalidCastException e)
			{
				try 
				{
					obj = reader.GetInt32(i);
				} 
				catch(InvalidCastException e1) 
				{
					obj = reader.GetDouble(i);
				}
			}
			return obj;
		}

		/// <summary>
		/// Get a pair of values from a reader
		/// </summary>
		/// <param name="sql">sql statement</param>
		/// <returns>string[]</returns>
		public String[] GetPair(String sql)
		{
			String[] str = null;
			try 
			{	
				conn.Open();
				cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				OleDbDataReader reader = cmd.ExecuteReader();
				
				reader.Read();
				str = new String[2];
				str[0] = Convert.ToString(GetValue(reader, 0));
				str[1] = Convert.ToString(GetValue(reader, 1));
			} 
			catch(OleDbException e) 
			{
				str = null;
				LogHelper.Instance().WriteLog(e.Message);
			}
			finally 
			{
				cmd.Connection.Close();
			}

			return str;
		}

		/// <summary>
		/// Get a pair of values from a reader
		/// </summary>
		/// <param name="sql">sql statement</param>
		/// <returns>string[]</returns>
		public String[] GetPair(String sql, bool writeLog)
		{
			String[] str = null;
			try 
			{	
				conn.Open();
				cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				OleDbDataReader reader = cmd.ExecuteReader();
				
				reader.Read();
				str = new String[2];
				str[0] = Convert.ToString(GetValue(reader, 0));
				str[1] = Convert.ToString(GetValue(reader, 1));
			} 
			catch(OleDbException e) 
			{
				str = null;
				if(writeLog) 
				{
					LogHelper.Instance().WriteLog(e.Message);
				}
			}
			finally 
			{
				cmd.Connection.Close();
			}

			return str;
		}

		/// <summary>
		/// Gets a scalar value.
		/// </summary>
		/// <param name="sql">sql statement</param>
		/// <returns>string</returns>
		public String GetScalar(String sql)
		{
			String str = "";
			try 
			{	
				conn.Open();
				cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				OleDbDataReader reader = cmd.ExecuteReader();

				while(reader.Read()) 
				{	
					str = Convert.ToString(GetValue(reader, 0));
					break;
				}
			} 
			catch(OleDbException e) 
			{
				str = "";
				LogHelper.Instance().WriteLog(e.Message);
			}
			finally 
			{
				cmd.Connection.Close();
			}

			return str;
		}

		/// <summary>
		/// Gets a dataset.
		/// </summary>
		/// <param name="sql">sql statement</param>
		/// <returns>DataSet</returns>
		public DataSet Query(String sql) 
		{
			DataSet ds = null;
			try 
			{	 
				conn.Open();
				OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
				ds = new DataSet();
				adapter.Fill(ds);
			} 
			catch(OleDbException e) 
			{
				LogHelper.Instance().WriteLog(e.Message);
			}
			finally 
			{
				conn.Close();
			}
			return ds;
		}

		/// <summary>
		/// Dispose this object.
		/// </summary>
		public void Dispose() 
		{
			conn.Close();
			conn.Dispose();
		}
	}
}
