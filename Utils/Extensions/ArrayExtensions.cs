using System;
using System.Collections.Generic;

namespace Assets.Scripts.Framework.Utils.Extensions
{
	public static class ArrayExtensions
	{
		public static T[] Shuffle<T>(this T[] array)
		{
			var r = new Random();
			for (int i = array.Length; i > 0; i--)
			{
				int j = r.Next(i);
				T k = array[j];
				array[j] = array[i - 1];
				array[i - 1] = k;
			}
			return array;
		}

		public static T RandomElement<T>(this List<T> list)
		{
			var random = new Random();
			return list[random.Next(list.Count)];
		}
	}
}