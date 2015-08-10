using System;

using Assets.Scripts.Framework.Utils;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public static class Ease
	{
		public static float Linear(float x)
		{
			return x;
		}

		public static float SineIn(float x)
		{
			return (float)Math.Sin(x * MathUtils.HalfPI - MathUtils.HalfPI) + 1;
		}

		public static float SineOut(float x)
		{
			return (float)Math.Sin(x * MathUtils.HalfPI);
		}

		public static float SineInOut(float x)
		{
			return (float)(Math.Sin(x * Math.PI - MathUtils.HalfPI) + 1) / 2;
		}

		public static Func<float, float> GetPowerIn(int power)
		{
			return x => (float)Math.Pow(x, power);
		}

		public static Func<float, float> GetPowerOut(int power)
		{
			return x =>
			{
				int sign = power % 2 == 0 ? -1 : 1;
				return (float)(sign * (Math.Pow(x - 1, power) + sign));
			};
		}

		public static Func<float, float> GetPowerInOut(int power)
		{
			return x =>
			{
				x *= 2;
				if (x < 1)
				{
					return GetPowerIn(power)(x) / 2;
				}
				int sign = power % 2 == 0 ? -1 : 1;
				return (float)(sign / 2.0 * (Math.Pow(x - 2, power) + sign * 2));
			};
		}
	}
}