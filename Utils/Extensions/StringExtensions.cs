using System;

namespace Assets.Scripts.Framework.Utils.Extensions
{
	/// <summary>
	/// Compares two specified version strings and returns an integer that 
	/// indicates their relationship to one another in the sort order.
	/// </summary>
	/// <param name="strA">the first version</param>
	/// <param name="strB">the second version</param>
	/// <returns>less than zero if strA is less than strB, equal to zero if
	/// strA equals strB, and greater than zero if strA is greater than strB</returns>
	public static class StringExtensions
	{
		public static int CompareVersionsString(this string strA, string strB)
		{
			var vA = new Version(strA.Replace(",", "."));
			var vB = new Version(strB.Replace(",", "."));

			return vA.CompareTo(vB);
		}
	}
}
