using System;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public class UIScaleAnimation : UIEasedAnimation
	{
		private readonly bool scaleX;

		private readonly bool scaleY;

		private readonly bool scaleZ;

		private Vector3 from;

		private Vector3 to;

		public UIScaleAnimation(GameObject parent, float duration, Vector3 from, Vector3 to, Func<float, float> easingFunction)
			: base(parent, duration, easingFunction)
		{
			this.from = from;
			this.to = to;

			scaleX = Math.Abs(from.x - to.x) > 0;
			scaleY = Math.Abs(from.y - to.y) > 0;
			scaleZ = Math.Abs(from.z - to.z) > 0;
		}

		protected override void PostUpdate(float progress)
		{
			Vector3 localScale = TargetRectTransform.localScale;

			localScale.x = scaleX ? Interpolate(from.x, to.x, progress) : localScale.x;
			localScale.y = scaleY ? Interpolate(from.y, to.y, progress) : localScale.y;
			localScale.z = scaleZ ? Interpolate(from.z, to.z, progress) : localScale.z;

			TargetRectTransform.localScale = localScale;
		}
	}
}