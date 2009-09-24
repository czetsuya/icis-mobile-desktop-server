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

namespace IcisMobileDesktopServer.Framework
{
	/// <summary>
	/// This serves as the main controller of the application. 
	/// It is responsible for communicating with all other objects.
	/// </summary>
	public class Engine : Framework.ExcelManager.ExcelReader
	{
		internal Study study;
		internal ResourceHelper resourceHelper;
		internal ResourceHelper errResourceHelper;
		internal int[] column_index;
		private String executableDirectoryName;
		private RAPI.Rapi rapi;
		internal String localDMS;
		internal String centralDMS;
		internal String[] selFactors;
		internal int readFactors;

		/// <summary>
		/// Creates an instance of this object.
		/// </summary>
		/// <param name="docname">Path to excel file</param>
		public Engine()
		{
			
		}

		public void Initialize(String docname) 
		{
			InitExcel(docname);
			//initialize the resources: config, error strings, etc
			study = new DataCollection.Study();
			resourceHelper = new ResourceHelper("config");
			errResourceHelper = new ResourceHelper("error");
			rapi = new RAPI.Rapi(errResourceHelper);

			column_index = new int[4];
			column_index[0] = resourceHelper.GetInt("property_column");
			column_index[1] = resourceHelper.GetInt("scale_column");
			column_index[2] = resourceHelper.GetInt("method_column");
			column_index[3] = resourceHelper.GetInt("datatype_column");

			//gets the default application path
			System.IO.FileInfo executableFileInfo = new System.IO.FileInfo(System.Windows.Forms.Application.ExecutablePath);
			executableDirectoryName = executableFileInfo.DirectoryName;
		}

		#region Filter Factors
		public void ShowFilterFactors() 
		{
			int[] x_factor = resourceHelper.GetIntPair("factor_cell");
			int row_index = x_factor[0];
			ArrayList list = new ArrayList();

			while(GetCell(row_index, x_factor[1]) != "") 
			{
				list.Add(GetCell(row_index, x_factor[1]));
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
		public void Process() 
		{
			BuildStudy();

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
			
			rapi.CopyFilePCtoPDA(studyPath, resourceHelper.GetString("copy_dir") + mobileStudyName);

			base.DisposeExcel();
		}

		#region Build Study
		/// <summary>
		/// This method calls other methods that specifically create the xml study and load it with values.
		/// It first load the study and abstract type schema from an xml file. Then add study values, factors and varietes.
		/// </summary>
		private void BuildStudy() 
		{
			StudyBuilder.SetStudyProperties(this);

			FactorBuilder.SetFactors(this);

			VariateBuilder.SetVariates(this);
			
			//save the data to a file
			String studyPath = executableDirectoryName + resourceHelper.GetString("resource_path"); 

			String studyFile = resourceHelper.GetString("study_observation_data_file");

			FileHelper.WriteToFile(studyPath + studyFile, FactorBuilder.SetFactorValues(this));

			//copy the file to the mobile device
			rapi.CopyFilePCtoPDA(studyPath + studyFile, resourceHelper.GetString("copy_dir") + studyFile);

			//SetFactorScales();
			VariateBuilder.SetVariateScales(this);
		}
		#endregion

		public void Dispose() 
		{
			rapi.Dispose();
			DisposeExcel();
			LogHelper.Instance().Dispose();
		}
	}
}
