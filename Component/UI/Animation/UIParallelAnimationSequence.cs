using System;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	public class UIParallelAnimationSequence : UIAnimationSequence
	{
		public UIParallelAnimationSequence(bool destroyOnComplete = true) : base(destroyOnComplete) {}

		public override void Play(Action onComplete = null)
		{
			base.Play(onComplete);

			PlayAnimations();
		}

		protected override void PlayAnimations()
		{
			for (int i = 0; i < Animations.Count; i++)
			{
				UIAnimation animation = Animations[i];

				animation.Play(() => HandleAnimationComplete(animation));
			}
		}

		public override void Update(float dt)
		{
			if (IsPlaying)
			{
				for (int i = 0; i < Animations.Count; i++)
				{
					UIAnimation animation = Animations[i];
					animation.Update(dt);
				}
			}
		}

		private void HandleAnimationComplete(UIAnimation animation)
		{
			AnimationsCompleted++;

			if (AnimationsCompleted == Animations.Count)
			{
				NotifyAnimationComplete();
			}
		}
	}
}