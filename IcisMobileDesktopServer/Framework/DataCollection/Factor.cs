/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;

namespace IcisMobileDesktopServer.Framework.DataCollection
{
	/// <summary>
	/// Factor specific data.
	/// </summary>
	public class Factor : AbstractType
	{
		private int col;

        /// <summary>
        /// Factor column.
        /// </summary>
		public int COLUMN 
		{
			set { col = Convert.ToInt16(value); }
			get { return col; }
		}
	}
}
