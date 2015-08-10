using System;
using System.Collections.Generic;

using Assets.Scripts.Framework.Utils.Extensions;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public class UIAnimationSequence : UIAnimation
	{
		protected List<UIAnimation> Animations { get; private set; }

		protected int AnimationsCompleted { get; set; }

		public UIAnimationSequence(bool destroyOnComplete = true) : base(destroyOnComplete)
		{
			Animations = new List<UIAnimation>();
			AnimationsCompleted = 0;
		}

		public void Reset()
		{
			Animations.Clear();
		}

		public override void Play(Action onComplete = null)
		{
			base.Play(onComplete);
			AnimationsCompleted = 0;
			PlayAnimations();
		}

		public override void Update(float dt)
		{
			base.Update(dt);
			if (IsPlaying)
			{
				Animations[AnimationsCompleted].Update(dt);
			}
		}

		protected virtual void PlayAnimations()
		{
			PlayNextAnimation();
		}

		private void PlayNextAnimation()
		{
			if (Animations.Count > AnimationsCompleted)
			{
				UIAnimation nextAnimation = Animations[AnimationsCompleted];
				nextAnimation.Play(
					() =>
					{
						AnimationsCompleted++;
						PlayNextAnimation();
					});
			}
			else
			{
				NotifyAnimationComplete();
			}
		}

		public UIAnimationSequence Add(params UIAnimation[] animations)
		{
			Animations.AddRange(animations);
			return this;
		}

		// Helper functions

		public UIAnimationSequence Move(RectTransform transform, float duration, Vector3 from, Vector3 to, Func<float, float> easingFunction = null)
		{
			UIMoveAnimation animation = transform.Move(duration, from, to, easingFunction);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence MoveTo(RectTransform transform, float duration, Vector3 to, Func<float, float> easingFunction = null)
		{
			var animation = new UIDelegateAnimation(() => transform.Move(duration, transform.localPosition, to, easingFunction));
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence MoveRelative(RectTransform transform, float duration, Vector3 to, Func<float, float> easingFunction = null)
		{
			var animation = new UIDelegateAnimation(() => transform.Move(duration, transform.localPosition, transform.localPosition + to, easingFunction));
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence Fade(RectTransform transform, float duration, float from, float to, Func<float, float> easingFunction = null)
		{
			UIFadeAnimation animation = transform.Fade(duration, from, to, easingFunction);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence FadeTo(RectTransform transform, float duration, float to, Func<float, float> easingFunction = null)
		{
			var animation = new UIDelegateAnimation(() => transform.Fade(duration, transform.GetAlpha(), to, easingFunction));
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence Rotate(RectTransform transform, float duration, Vector3 from, Vector3 to, Func<float, float> easingFunction = null)
		{
			UIRotateAnimation animation = transform.Rotate(duration, from, to, easingFunction);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence RotateX(RectTransform transform, float duration, float from, float to, Func<float, float> easingFunction = null)
		{
			UIRotateAnimation animation = transform.RotateX(duration, from, to, easingFunction);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence RotateY(RectTransform transform, float duration, float from, float to, Func<float, float> easingFunction = null)
		{
			UIRotateAnimation animation = transform.RotateY(duration, from, to, easingFunction);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence RotateZ(RectTransform transform, float duration, float from, float to, Func<float, float> easingFunction = null)
		{
			UIRotateAnimation animation = transform.RotateZ(duration, from, to, easingFunction);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence Scale(RectTransform transform, float duration, Vector3 from, Vector3 to, Func<float, float> easingFunction = null)
		{
			UIScaleAnimation animation = transform.Scale(duration, from, to, easingFunction);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence Resize(RectTransform transform, float duration, Vector2 from, Vector2 to, Func<float, float> easingFunction = null)
		{
			UIResizeAnimation animation = transform.Resize(duration, from, to, easingFunction);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence Call(Action action)
		{
			var animation = new FunctionCallAnimation(action);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence Activate(RectTransform transform, bool value)
		{
			var animation = new FunctionCallAnimation(() => transform.gameObject.SetActive(value));
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence Wait(float duration)
		{
			var animation = new UIWaitAnimation(duration);
			Animations.Add(animation);
			return this;
		}

		public UIAnimationSequence Parallel(params UIAnimation[] animations)
		{
			if (animations.Length > 0)
			{
				var sequence = new UIParallelAnimationSequence();
				foreach (UIAnimation animation in animations)
				{
					sequence.Add(animation);
				}

				Animations.Add(sequence);
			}
			return this;
		}
	}
}