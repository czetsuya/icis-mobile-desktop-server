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
		private ArrayList factors;
		private ArrayList variates;
		private ArrayList scales;

		private String name;
		private String title;
		private String start_date;
		private String end_date;

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
	}
}
