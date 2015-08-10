using System;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public class UIMoveAnimation : UIEasedAnimation
	{
		private readonly Vector3 from;

		private readonly Vector3 to;

		public UIMoveAnimation(GameObject targetObject, float duration, Vector3 from, Vector3 to, Func<float, float> easingFunction)
			: base(targetObject, duration, easingFunction)
		{
			this.from = from;
			this.to = to;
		}

		protected override void PostUpdate(float progress)
		{
			Vector3 position = Interpolate(from, to, progress);
			SetPosition(position);
		}

		private void SetPosition(Vector3 position)
		{
			TargetRectTransform.localPosition = position;
		}
	}
}