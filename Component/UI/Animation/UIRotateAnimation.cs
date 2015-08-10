using System;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public class UIRotateAnimation : UIEasedAnimation
	{
		private readonly bool rotateX;

		private readonly bool rotateY;

		private readonly bool rotateZ;

		private Vector3 from;

		private Vector3 to;

		public UIRotateAnimation(GameObject parent, float duration, Vector3 from, Vector3 to, Func<float, float> easingFunction)
			: base(parent, duration, easingFunction)
		{
			this.from = from;
			this.to = to;

			rotateX = Math.Abs(from.x - to.x) > 0;
			rotateY = Math.Abs(from.y - to.y) > 0;
			rotateZ = Math.Abs(from.z - to.z) > 0;
		}

		protected override void PostUpdate(float progress)
		{
			Vector3 localRotation = TargetRectTransform.localRotation.eulerAngles;

			localRotation.x = rotateX ? Interpolate(from.x, to.x, progress) : localRotation.x;
			localRotation.y = rotateY ? Interpolate(from.y, to.y, progress) : localRotation.y;
			localRotation.z = rotateZ ? Interpolate(from.z, to.z, progress) : localRotation.z;

			TargetRectTransform.localRotation = Quaternion.Euler(localRotation);
		}
	}
}