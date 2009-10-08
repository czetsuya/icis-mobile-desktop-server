/**
 * @author edwardpantojalegaspi
 * @since 2009.09.24
 * */
using System;

using IcisMobileDesktopServer.Framework.DataCollection;
using IcisMobileDesktopServer.Framework.Helper;

namespace IcisMobileDesktopServer.Framework.Builder
{
	/// <summary>
	/// Summary description for StudyBuilder.
	/// </summary>
	public class StudyBuilder
	{
		/// <summary>
		/// Sets the values of the study object.
		/// </summary>
		/// <param name="engine">Engine</param>
		internal static void SetStudyProperties(Engine engine) 
		{
			engine.study.NAME = engine.GetExcelReader().GetCell(engine.resourceHelper.GetIntPair("name_cell")).Trim();
			engine.study.TITLE = engine.GetExcelReader().GetCell(engine.resourceHelper.GetIntPair("title_cell")).Trim();
			engine.study.STARTDATE = engine.GetExcelReader().GetCell(engine.resourceHelper.GetIntPair("startdate_cell")).Trim();
			engine.study.ENDDATE = engine.GetExcelReader().GetCell(engine.resourceHelper.GetIntPair("enddate_cell")).Trim();
		}
	}
}
