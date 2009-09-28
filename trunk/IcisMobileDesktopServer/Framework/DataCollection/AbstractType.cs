/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * @description Abstract data type for factor and variate.
 * */
using System;

namespace IcisMobileDesktopServer.Framework.DataCollection
{
	/// <summary>
	/// Abstract type data type for storing similar data from factors and variates.
	/// </summary>
	public abstract class AbstractType
	{
		private String name;
		private String property;
		private String propertyid;
		private String scale;
		private String method;
		private String datatype;
		private String scaleid;

		public AbstractType()
		{
			name = "";
			property = "";
			scale = "";
			method = "";
			datatype = "";
			scaleid = "";
		}

		public String NAME 
		{
			set { name = value; }
			get { return name; }
		}

		public String PROPERTY 
		{
			set { property = value; }
			get { return property; }
		}

		public String PROPERTYID
		{
			set { propertyid = value; }
			get { return propertyid; }
		}

		public String SCALE 
		{
			set { scale = value; }
			get { return scale; }
		}

		public String METHOD 
		{
			set { method = value; }
			get { return method; }
		}

		public String DATATYPE 
		{
			set { datatype = value; }
			get { return datatype; }
		}

		public String SCALEID 
		{
			set { scaleid = value; }
			get { return scaleid; }
		}
	}
}
