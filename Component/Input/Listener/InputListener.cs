using System.Collections.Generic;

using Assets.Scripts.Framework.Component.Input.Event;

namespace Assets.Scripts.Framework.Component.Input.Listener
{
	public abstract class InputListener
	{
		public Queue<InputEvent> InputEvents { get; set; }

		protected InputListener()
		{
			InputEvents = new Queue<InputEvent>();
		}

		public virtual void ProcessInput() {}
	}
}