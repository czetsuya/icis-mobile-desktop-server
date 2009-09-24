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
		public StudyBuilder()
		{

		}

		internal static void SetStudyProperties(Engine engine) 
		{
			engine.study.NAME = engine.GetCell(engine.resourceHelper.GetIntPair("name_cell")).Trim();
			engine.study.TITLE = engine.GetCell(engine.resourceHelper.GetIntPair("title_cell")).Trim();
			engine.study.STARTDATE = engine.GetCell(engine.resourceHelper.GetIntPair("startdate_cell")).Trim();
			engine.study.ENDDATE = engine.GetCell(engine.resourceHelper.GetIntPair("enddate_cell")).Trim();
		}
	}
}
