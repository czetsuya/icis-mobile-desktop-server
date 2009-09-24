/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;

namespace IcisMobileDesktopServer.Framework.Xml
{
	/// <summary>
	/// Summary description for XmlBuilder.
	/// </summary>
	public class XmlBuilder
	{
		private String studyPath;
		private String abstractType;
		private String scale_schema_file;
		private String factorSchema;

		public XmlBuilder(String studyPath)
		{
			this.studyPath = studyPath;
		}

		public void SetStudySchema(String studyPath) 
		{
			this.studyPath = studyPath;
		}

		public void SetAbstractTypeSchema(String abstractType) 
		{
			this.abstractType = abstractType;
		}

		public void SetFactorSchema(String factorSchema) 
		{
			this.factorSchema = factorSchema;
		}

		public void SetScaleSchema(String scale_schema_file) 
		{
			this.scale_schema_file = scale_schema_file;
		}

		public void Process(DataCollection.Study study, String outputFile) 
		{
			String study_schema = Helper.FileHelper.ReadAsSchema(studyPath);
			String abstracttype_schema = Helper.FileHelper.ReadAsSchema(abstractType);
			String scale_schema = Helper.FileHelper.ReadAsSchema(scale_schema_file);

			//replace the study properties' values
			study_schema = study_schema.Replace("{name}", study.NAME);
			study_schema = study_schema.Replace("{title}", study.TITLE);
			study_schema = study_schema.Replace("{start-date}", study.STARTDATE);
			study_schema = study_schema.Replace("{end-date}", study.ENDDATE);

			//add factors
			String factor_variate_schema = abstracttype_schema.Replace("abstract-type", "factor");
			String abstract_list = "";
			
//			foreach(DataCollection.Factor obj in study.GetFactors()) 
//			{
//				String temp = factor_variate_schema;
//				temp = temp.Replace("{name}", obj.NAME);
//				temp = temp.Replace("{property}", obj.PROPERTY);
//				temp = temp.Replace("{scale}", obj.SCALE);
//				temp = temp.Replace("{method}", obj.METHOD);
//				temp = temp.Replace("{data-type}", obj.DATATYPE);
//				temp = temp.Replace("{scaleid}", obj.SCALEID);
//				abstract_list += temp;
//			}
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach(DataCollection.Factor obj in study.GetFactors()) 
			{
				sb.Append(obj.NAME);
				sb.Append("/");
			}
			sb = sb.Remove(sb.Length - 1, 1);
			String stemp = Helper.FileHelper.ReadAsSchema(factorSchema);
			stemp = stemp.Replace("{name}", sb.ToString());
			abstract_list += stemp;
			
			study_schema = study_schema.Replace("{factors}", abstract_list);

			//add variates
			factor_variate_schema = abstracttype_schema.Replace("abstract-type", "variate");
			abstract_list = "";

			foreach(DataCollection.Variate obj in study.GetVariates()) 
			{
				String temp = factor_variate_schema;
				temp = temp.Replace("{name}", obj.NAME);
				temp = temp.Replace("{property}", obj.PROPERTY);
				temp = temp.Replace("{scale}", obj.SCALE);
				temp = temp.Replace("{method}", obj.METHOD);
				temp = temp.Replace("{data-type}", obj.DATATYPE);
				temp = temp.Replace("{scaleid}", obj.SCALEID);
				abstract_list += temp;
			}

			study_schema = study_schema.Replace("{variates}", abstract_list);
			abstract_list = "";
			
			//add scales
			abstract_list = "";
			foreach(DataCollection.Scale obj in study.GetScales()) 
			{
				String temp = scale_schema;
				temp = temp.Replace("{id}", obj.ID);
				temp = temp.Replace("{name}", obj.NAME);
				temp = temp.Replace("{type}", obj.TYPE);
				temp = temp.Replace("{value1}", obj.VALUE1);
				temp = temp.Replace("{value2}", obj.VALUE2);
				if(obj.IsDisContinuous())
					temp = temp.Replace("{value3}", obj.GetDisconValues());
				abstract_list += temp;
			}
			study_schema = study_schema.Replace("{scales}", abstract_list);
			//end scales

			Helper.FileHelper.WriteSchema(outputFile, study_schema);
		}
	}
}