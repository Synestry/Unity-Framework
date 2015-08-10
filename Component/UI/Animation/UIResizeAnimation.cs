using System;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public class UIResizeAnimation : UIEasedAnimation
	{
		private readonly bool widthX;

		private readonly bool widthY;

		private Vector2 from;

		private Vector2 to;

		public UIResizeAnimation(GameObject parent, float duration, Vector2 from, Vector2 to, Func<float, float> easingFunction)
			: base(parent, duration, easingFunction)
		{
			this.from = from;
			this.to = to;

			widthX = Math.Abs(from.x - to.x) > 0;
			widthY = Math.Abs(from.y - to.y) > 0;
		}

		protected override void PostUpdate(float progress)
		{
			Vector2 sizeDelta = TargetRectTransform.sizeDelta;

			sizeDelta.x = widthX ? Interpolate(from.x, to.x, progress) : sizeDelta.x;
			sizeDelta.y = widthY ? Interpolate(from.y, to.y, progress) : sizeDelta.y;

			TargetRectTransform.sizeDelta = sizeDelta;
		}
	}
}