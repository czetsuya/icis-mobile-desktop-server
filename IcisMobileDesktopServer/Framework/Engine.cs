/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections;

using IcisMobileDesktopServer.Framework.DataCollection;
using IcisMobileDesktopServer.Framework.Helper;
using IcisMobileDesktopServer.Framework.Builder;
using IcisMobileDesktopServer.Framework.ExcelManager;

namespace IcisMobileDesktopServer.Framework
{
	/// <summary>
	/// This serves as the main controller of the application. 
	/// It is responsible for communicating with all other objects.
	/// </summary>
	public class Engine
	{
		internal Study study;
		internal ResourceHelper resourceHelper;
		internal ResourceHelper errResourceHelper;
		internal int[] column_index;
		private String executableDirectoryName;
		private RAPI.Rapi rapi;
		internal String localDMS;
		internal String centralDMS;
		internal static String[] selFactors;
		internal int readFactors;
		private ExcelReader excelReader;
		internal System.Windows.Forms.ProgressBar progressBar;

		/// <summary>
		/// Creates an instance of this object.
		/// </summary>
		/// <param name="docname">Path to excel file</param>		
		public Engine(System.Windows.Forms.ProgressBar progressBar)
		{
			this.progressBar = progressBar;
			//initialize the resources: config, error strings, etc
			study = new DataCollection.Study();
			resourceHelper = new ResourceHelper("config");
			errResourceHelper = new ResourceHelper("messages");
			rapi = new RAPI.Rapi(this);

			//gets the default application path
			System.IO.FileInfo executableFileInfo = new System.IO.FileInfo(System.Windows.Forms.Application.ExecutablePath);
			executableDirectoryName = executableFileInfo.DirectoryName;
		}
		
		public void Initialize(String docname)
		{
			excelReader = new ExcelReader();
			excelReader.InitExcel(docname);

			column_index = new int[4];
			column_index[0] = resourceHelper.GetInt("property_column");
			column_index[1] = resourceHelper.GetInt("scale_column");
			column_index[2] = resourceHelper.GetInt("method_column");
			column_index[3] = resourceHelper.GetInt("datatype_column");
		}

	
		#region Filter Factors
		public void ShowFilterFactors() 
		{
			int[] x_factor = resourceHelper.GetIntPair("factor_cell");
			int row_index = x_factor[0];
			ArrayList list = new ArrayList();

			while(GetExcelReader().GetCell(row_index, x_factor[1]) != "") 
			{
				list.Add(GetExcelReader().GetCell(row_index, x_factor[1]));
				row_index++;
			}

			frmSelectFactor frmTemp = new frmSelectFactor(this);
			frmTemp.SetList(list);
			frmTemp.Show();
		}

		public void SetFilteredFactors(frmSelectFactor obj, string s) 
		{
			if(s.IndexOf("|") != -1) 
			{ //2
				selFactors = s.Split('|');
			} 
			else //1
			{
				selFactors = new String[1];
				selFactors[0] = s;
			}
			obj.Hide();
			obj = null;
		}
		#endregion

		public void SetDatabase(String central, String local) 
		{
			localDMS = local;
			centralDMS = central;
		}
		
		/// <summary>
		/// Actual code that starts the process from reading the excel file,
		/// creating a memory object of the study, creating an xml representation of that study,
		/// and finally sending the study xml format to the mobile device.
		/// </summary>
		public bool Process() 
		{
			if(selFactors == null) 
			{
				return false;
			}
			SplashScreen.SplashScreen.ShowSplashScreen();
			System.Windows.Forms.Application.DoEvents();
			
			bool flag = true;
			flag = BuildStudy();

			if(flag) 
			{
				String studyPath = executableDirectoryName + resourceHelper.GetString("schema_study");
				Xml.XmlBuilder xmlBuilder = new Xml.XmlBuilder(studyPath);
				xmlBuilder.SetAbstractTypeSchema(executableDirectoryName + resourceHelper.GetString("abstracttype_study"));
				xmlBuilder.SetFactorSchema(executableDirectoryName + resourceHelper.GetString("factor_schema"));
				xmlBuilder.SetScaleSchema(executableDirectoryName + resourceHelper.GetString("scale_schema"));
			
				//get the base directory and file name and adds _output
				int period = studyPath.LastIndexOf(".");
				int ext_length = studyPath.Substring(period).Length;
				studyPath = studyPath.Substring(0, period) + "_output" + studyPath.Substring(period, ext_length);
			
				//populate the data object study, replace the xml marker
				xmlBuilder.Process(study, studyPath);

				String mobileStudyName = resourceHelper.GetString("study_data_file");
				SplashScreen.SplashScreen.SetStatus("Copying to device...");
				rapi.CopyFilePCtoPDA(studyPath, resourceHelper.GetString("copy_dir") + mobileStudyName);

				excelReader.DisposeExcel();
				SplashScreen.SplashScreen.CloseForm();
			} 
			return true;
		}

		#region Build Study
		/// <summary>
		/// This method calls other methods that specifically create the xml study and load it with values.
		/// It first load the study and abstract type schema from an xml file. Then add study values, factors and varietes.
		/// </summary>
		private bool BuildStudy() 
		{
			bool flag = true;
			SplashScreen.SplashScreen.SetStatus("Reading study");
			System.Threading.Thread.Sleep(500);
			StudyBuilder.SetStudyProperties(this);

			SplashScreen.SplashScreen.SetStatus("Reading factors");
			System.Threading.Thread.Sleep(500);
			FactorBuilder.SetFactors(this);

			SplashScreen.SplashScreen.SetStatus("Reading variates");
			System.Threading.Thread.Sleep(500);
			VariateBuilder.SetVariates(this);
			
			//save the data to a file
			String studyPath = executableDirectoryName + resourceHelper.GetString("resource_path"); 

			String studyFile = resourceHelper.GetString("study_observation_data_file");

			SplashScreen.SplashScreen.SetStatus("Writing study to file...");
			System.Threading.Thread.Sleep(1000);
			FileHelper.WriteToFile(studyPath + studyFile, FactorBuilder.SetFactorValues(this));

			//copy the file to the mobile device
			SplashScreen.SplashScreen.SetStatus("Copying file to mobile device...");
			System.Threading.Thread.Sleep(1000);
			flag = rapi.CopyFilePCtoPDA(studyPath + studyFile, resourceHelper.GetString("copy_dir") + studyFile);
			if(flag) 
			{
				//SetVariateProperties
				SplashScreen.SplashScreen.SetStatus("Reading variate data");
				System.Threading.Thread.Sleep(500);
				VariateBuilder.SetVariatePropertyID(this);

				//SetVariateScales();
				SplashScreen.SplashScreen.SetStatus("Setting variate scales...");
				System.Threading.Thread.Sleep(1000);
				VariateBuilder.SetVariateScales(this);
			}

			return flag;
		}
		#endregion

		/// <summary>
		/// Download the saved data from mobile device to desktop computer.
		/// </summary>
		/// <returns>observation values</returns>
		public string DownloadFromDevice() 
		{
			string from = "";
			string to = "";
			try 
			{
				string fname = resourceHelper.GetString("study_data_file");
				from = resourceHelper.GetString("copy_dir") + fname;
				to = executableDirectoryName +"/"+ fname;
				rapi.CopyPDAtoPC(to, from);
			} 
			catch(Exception e)
			{
				to = "";
				MessageHelper.ShowInfo(errResourceHelper.GetString("m_perform_download"));
				LogHelper.Instance().WriteLog(e.Message);
			}
			return to;
		}

		/// <summary>
		/// Dispose this object and all its dependencies.
		/// </summary>
		public void Dispose() 
		{
			rapi.Dispose();
			excelReader.DisposeExcel();
			LogHelper.Instance().Dispose();
		}

		public ExcelReader GetExcelReader() 
		{
			return excelReader;
		}

		/// <summary>
		/// Writes the data from device to the excel workbook document.
		/// </summary>
		/// <param name="excelfile"></param>
		/// <param name="inputfile"></param>
		public void WriteToExcel(string excelfile, string inputfile) 
		{
			try 
			{
				if(new ExcelDataBuilder(this, excelfile).Process(inputfile))
					MessageHelper.ShowInfo(errResourceHelper.GetString("m_download_ok"));
			} 
			catch(Exception e) 
			{
				LogHelper.Instance().WriteLog(e.Message);
				MessageHelper.ShowInfo(errResourceHelper.GetString("m_download_failed"));
			}
			
		}
	}
}
