using System;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	/// <summary>
	///     UIAnimation
	/// </summary>
	public abstract class UIAnimation
	{
		/// <summary>
		///     The onComplete callback
		/// </summary>
		private Action onComplete;

		/// <summary>
		///     if set to true the animation is destroyed when it is completed by the controller that created it.
		/// </summary>
		public bool DestroyOnComplete { get; set; }

		/// <summary>
		///     A boolean value which is true if the animation is currently playing
		/// </summary>
		public bool IsPlaying { get; protected set; }

		/// <summary>
		///     A boolean value which is true if the animation is currently looping
		/// </summary>
		public bool IsLooping { get; protected set; }

		protected UIAnimation(bool destroyOnComplete = true)
		{
			DestroyOnComplete = destroyOnComplete;
		}

		public virtual void Update(float dt) {}

		protected void NotifyAnimationComplete()
		{
			IsPlaying = false;

			if (onComplete != null)
			{
				onComplete.Invoke();
			}
		}

		public virtual void Play(Action onComplete = null)
		{
			this.onComplete = onComplete;
			IsPlaying = true;
		}

		public virtual void Loop()
		{
			IsLooping = true;

			Action onComplete = delegate
			{
				if (IsLooping)
				{
					Loop();
				}
			};

			Play(onComplete);
		}

		/// <summary>
		///     Aborts the current animation at it's current place
		/// </summary>
		public virtual void Abort()
		{
			IsPlaying = false;
			IsLooping = false;
		}

		/// <summary>
		///     Fastforwards the animation to the end and stops it
		/// </summary>
		public virtual void End()
		{
			IsPlaying = false;
			IsLooping = false;
		}
	}
}