using System;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	/// <summary>
	///     Encapsulates an animation, which is only created at the time it is requested to play.
	///     Useful for animations which depend on an object being in a certain state other than the state in which the
	///     animations are originally created
	/// </summary>
	public class UIDelegateAnimation : UIAnimation
	{
		private readonly Func<UIAnimation> animationCreationFunction;

		private UIAnimation animation;

		public UIDelegateAnimation(Func<UIAnimation> animationCreationFunction)
		{
			if (animationCreationFunction == null)
			{
				throw new ArgumentNullException();
			}
			this.animationCreationFunction = animationCreationFunction;
		}

		public override void Play(Action onComplete = null)
		{
			base.Play(onComplete);

			animation = animationCreationFunction();
			animation.Play(NotifyAnimationComplete);
		}

		public override void Update(float dt)
		{
			base.Update(dt);
			if (IsPlaying)
			{
				animation.Update(dt);
			}
		}
	}
}