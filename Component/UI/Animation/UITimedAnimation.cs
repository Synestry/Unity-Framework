using System;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public abstract class UITimedAnimation : UIAnimation
	{
		protected float currentTime;

		protected float duration;

		/// <summary>
		///     The target gameobject of the this animation
		/// </summary>
		public GameObject TargetObject { get; private set; }

		/// <summary>
		///     The RectTransform of the game object
		/// </summary>
		public RectTransform TargetRectTransform { get { return (RectTransform)TargetObject.transform; } }

		/// <summary>
		///     gets a value indicating the progress of the animation where 0 is the start and 1 is the end
		/// </summary>
		protected float Progress { get { return Mathf.Clamp(currentTime / duration, 0f, 1f); } }

		protected UITimedAnimation(GameObject targetObject, float duration)
		{
			TargetObject = targetObject;
			this.duration = duration;
			currentTime = 0f;
		}

		public override void Update(float dt)
		{
			if (IsPlaying)
			{
				currentTime += dt;

				PostUpdate(Progress);

				if (IsCompleted())
				{
					NotifyAnimationComplete();
				}
			}
		}

		protected abstract void PostUpdate(float progress);

		public override void Play(Action onComplete = null)
		{
			base.Play(onComplete);
			currentTime = 0.0f;
			if (duration == 0)
			{
				Update(1);
				NotifyAnimationComplete();
			}
			else
			{
				Update(0);
			}
		}

		/// <summary>
		///     Fastforwards the animation to the end and stops it
		/// </summary>
		public override void End()
		{
			Update(1);
			NotifyAnimationComplete();
		}

		public bool IsCompleted()
		{
			return Progress >= 1f;
		}
	}
}