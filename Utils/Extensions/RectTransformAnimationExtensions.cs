using System;

using Assets.Scripts.Framework.Component.UI.Animation;

using UnityEngine;

namespace Assets.Scripts.Framework.Utils.Extensions
{
	public static class RectTransformAnimationExtensions
	{
		public static UIMoveAnimation Move(this RectTransform transform, float duration, Vector3 from, Vector3 to, Func<float, float> easingFunction = null)
		{
			return new UIMoveAnimation(transform.gameObject, duration, from, to, easingFunction);
		}

		public static UIFadeAnimation Fade(this RectTransform transform, float duration, float from, float to, Func<float, float> easingFunction = null)
		{
			return new UIFadeAnimation(transform.gameObject, duration, from, to, easingFunction);
		}

		public static UIRotateAnimation RotateX(this RectTransform transform, float duration, float fromX, float toX,
												Func<float, float> easingFunction = null)
		{
			var from = new Vector3(fromX, 0, 0);
			var to = new Vector3(toX, 0, 0);
			return new UIRotateAnimation(transform.gameObject, duration, from, to, easingFunction);
		}

		public static UIRotateAnimation RotateY(this RectTransform transform, float duration, float fromY, float toY,
												Func<float, float> easingFunction = null)
		{
			var from = new Vector3(0, fromY, 0);
			var to = new Vector3(0, toY, 0);
			return new UIRotateAnimation(transform.gameObject, duration, from, to, easingFunction);
		}

		public static UIRotateAnimation RotateZ(this RectTransform transform, float duration, float fromZ, float toZ,
												Func<float, float> easingFunction = null)
		{
			var from = new Vector3(0, 0, fromZ);
			var to = new Vector3(0, 0, toZ);
			return new UIRotateAnimation(transform.gameObject, duration, from, to, easingFunction);
		}

		public static UIRotateAnimation Rotate(this RectTransform transform, float duration, Vector3 from, Vector3 to,
												Func<float, float> easingFunction = null)
		{
			return new UIRotateAnimation(transform.gameObject, duration, from, to, easingFunction);
		}

		public static float GetAlpha(this RectTransform transform)
		{
			return transform.gameObject.GetComponent<CanvasRenderer>().GetAlpha();
		}

		public static UIScaleAnimation Scale(this RectTransform transform, float duration, Vector3 from, Vector3 to,
											Func<float, float> easingFunction = null)
		{
			return new UIScaleAnimation(transform.gameObject, duration, from, to, easingFunction);
		}

		public static UIResizeAnimation Resize(this RectTransform transform, float duration, Vector2 from, Vector2 to,
												Func<float, float> easingFunction = null)
		{
			return new UIResizeAnimation(transform.gameObject, duration, from, to, easingFunction);
		}

		public static UIWaitAnimation Wait(float duration)
		{
			return new UIWaitAnimation(duration);
		}
	}
}