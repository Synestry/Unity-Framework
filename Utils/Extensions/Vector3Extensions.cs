using UnityEngine;

namespace Assets.Scripts.Framework.Utils.Extensions
{
	public static class Vector3Extensions
	{
		public static Vector3 LerpAngle(this Vector3 from, Vector3 to, float duration)
		{
			return new Vector3(Mathf.LerpAngle(from.x, to.x, duration), Mathf.LerpAngle(from.y, to.y, duration), Mathf.LerpAngle(from.z, to.z, duration));
		}
	}
}