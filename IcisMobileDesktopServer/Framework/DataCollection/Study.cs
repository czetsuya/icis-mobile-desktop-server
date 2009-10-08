/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Collections;

namespace IcisMobileDesktopServer.Framework.DataCollection
{
	/// <summary>
	/// Summary description for Study.
	/// </summary>
	public class Study
	{
		/// <summary>
		/// List of factors
		/// </summary>
		private ArrayList factors;
		/// <summary>
		/// List of variates
		/// </summary>
		private ArrayList variates;
		/// <summary>
		/// List of scales
		/// </summary>
		private ArrayList scales;
		/// <summary>
		/// Study name
		/// </summary>
		private String name;
		/// <summary>
		/// Study title
		/// </summary>
		private String title;
		/// <summary>
		/// Study start date
		/// </summary>
		private String start_date;
		/// <summary>
		/// Study end data
		/// </summary>
		private String end_date;

		/// <summary>
		/// Initialize the study.
		/// </summary>
		public Study()
		{
			name = "";
			title = "";
			start_date = "";
			end_date = "";

			factors = new ArrayList();
			variates = new ArrayList();
			scales = new ArrayList();
		}

		#region Properties
		public String NAME 
		{
			set { name = value; }
			get { return name; }
		}

		public String TITLE 
		{
			set { title = value; }
			get { return title; }
		}

		public String STARTDATE 
		{
			set { start_date = value; }
			get { return start_date; }
		}

		public String ENDDATE 
		{
			set { end_date = value; }
			get { return end_date; }
		}
		#endregion

		#region Factor, Variates, Scales Setters and Getters
		public void AddFactor(Factor f) 
		{
			factors.Add(f);
		}

		public void SetFactor(int i, Factor f) 
		{
			factors[i] = f;
		}

		public void AddVariate(Variate v) 
		{
			variates.Add(v);
		}

		public void SetVariate(int i, Variate v) 
		{
			variates[i] = v;
		}

		public ArrayList GetFactors() 
		{
			return factors;
		}

		public ArrayList GetVariates() 
		{
			return variates;
		}

		public Factor GetFactor(int x) 
		{
			return (Factor)factors[x];
		}

		public Variate GetVariate(int x) 
		{
			return (Variate)variates[x];
		}

		public void AddScale(Scale s) 
		{
			scales.Add(s);
		}

		public ArrayList GetScales() 
		{
			return scales;
		}
		#endregion
	}
}
