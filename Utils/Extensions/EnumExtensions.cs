using System;
using System.Linq;

namespace Assets.Scripts.Framework.Utils.Extensions
{
	public static class EnumExtensions
	{
		public static int Min(this Enum enumType)
		{
			return Enum.GetValues(enumType.GetType()).Cast<int>().Min();
		}

		public static int Max(this Enum enumType)
		{
			return Enum.GetValues(enumType.GetType()).Cast<int>().Max();
		}

		public static bool HasFlag(this Enum enumType, Enum value)
		{
			if (enumType == null)
			{
				return false;
			}

			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!Enum.IsDefined(enumType.GetType(), value))
			{
				throw new ArgumentException(
					string.Format("Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.", value.GetType(), enumType.GetType()));
			}

			ulong num = Convert.ToUInt64(value);
			return ((Convert.ToUInt64(enumType) & num) == num);
		}
	}
}