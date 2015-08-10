using System;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public abstract class UIEasedAnimation : UITimedAnimation
	{
		protected Func<float, float> easingFunction;

		protected UIEasedAnimation(GameObject targetObject, float duration, Func<float, float> easingFunction = null) : base(targetObject, duration)
		{
			this.easingFunction = easingFunction ?? Ease.Linear;
		}

		// Interpolation helper functions

		public Vector3 Interpolate(Vector3 from, Vector3 to, float t)
		{
			float rangeX = to.x - from.x;
			float rangeY = to.y - from.y;

			float ease = easingFunction(t);

			float x = from.x + (ease * rangeX);
			float y = from.y + (ease * rangeY);

			return new Vector3(x, y, 0);
		}

		public Color Interpolate(Color from, Color to, float t)
		{
			float rangeR = to.r - from.r;
			float rangeG = to.g - from.g;
			float rangeB = to.b - from.b;
			float rangeA = to.a - from.a;

			float ease = easingFunction(t);

			float r = from.r + (ease * rangeR);
			float g = from.g + (ease * rangeG);
			float b = from.b + (ease * rangeB);
			float a = from.a + (ease * rangeA);

			return new Color(r, g, b, a);
		}

		public float Interpolate(float from, float to, float t)
		{
			float range = to - from;
			float ease = easingFunction(t);
			return from + (ease * range);
		}

		public float InterpolateEased(float from, float to, float ease)
		{
			float range = to - from;
			return from + (ease * range);
		}
	}
}