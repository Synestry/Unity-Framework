using System;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	/// <summary>
	///     UIAnimation_Fade simple fade animation for the UI
	/// </summary>
	public class UIFadeAnimation : UIEasedAnimation
	{
		private readonly float from;

		private readonly float to;

		public UIFadeAnimation(GameObject parent, float duration, float from, float to, Func<float, float> easingFunction)
			: base(parent, duration, easingFunction)
		{
			this.from = from;
			this.to = to;
		}

		public override void Play(Action onComplete = null)
		{
			SetAlpha(TargetObject.transform, from);

			base.Play(onComplete);
		}

		protected override void PostUpdate(float progress)
		{
			float alpha = Interpolate(from, to, progress);

			SetAlpha(TargetObject.transform, alpha);
		}

		private void SetAlpha(Transform transform, float alpha)
		{
			var canvasRenderer = transform.gameObject.GetComponent<CanvasRenderer>();
			if (canvasRenderer != null)
			{
				canvasRenderer.SetAlpha(alpha);
			}

			for (int i = 0; i < transform.childCount; ++i)
			{
				SetAlpha(transform.GetChild(i), alpha);
			}
		}
	}
}