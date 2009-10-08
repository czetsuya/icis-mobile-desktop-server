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
		#region Members
		/// <summary>
		/// Object name
		/// </summary>
		private String name;
		/// <summary>
		/// Object property
		/// </summary>
		private String property;
		/// <summary>
		/// Object property id
		/// </summary>
		private String propertyid;
		/// <summary>
		/// Object scale
		/// </summary>
		private String scale;
		/// <summary>
		/// Object method
		/// </summary>
		private String method;
		/// <summary>
		/// Object datatype (N or C)
		/// </summary>
		private String datatype;
		/// <summary>
		/// Object scale id
		/// </summary>
		private String scaleid;
		#endregion

		/// <summary>
		/// Initiliaze the default values of the member variables.
		/// </summary>
		public AbstractType()
		{
			name = "";
			property = "";
			scale = "";
			method = "";
			datatype = "";
			scaleid = "";
		}
	
		#region Properties, obvious names
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
		#endregion
	}
}
