using System;

namespace Assets.Scripts.Framework.Component.UI.Animation
{
	internal class FunctionCallAnimation : UIAnimation
	{
		private readonly Action action;

		public FunctionCallAnimation(Action action)
		{
			this.action = action;
		}

		public override void Play(Action onComplete = null)
		{
			base.Play(onComplete);
			action();
			NotifyAnimationComplete();
		}
	}
}