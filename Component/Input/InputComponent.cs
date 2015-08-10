using System.Collections.Generic;

using Assets.Scripts.Framework.Component.Input.Event;
using Assets.Scripts.Framework.Component.Input.Listener;

namespace Assets.Scripts.Framework.Component.Input
{
	public class InputComponent : AbstractGameComponent
	{
		public List<InputListener> Listeners { get; set; }

		public InputComponent()
		{
			Listeners = new List<InputListener> { new SwipeInputListener(), new KeyboardInputListener(), new MouseInputListener() };
		}

		public override void Update(float dt)
		{
			if (!GamePaused)
			{
				Listeners.ForEach(
					listener =>
					{
						listener.ProcessInput();

						while (listener.InputEvents.Count > 0)
						{
							InputEvent inputEvent = listener.InputEvents.Dequeue();
							EventDispatcher.Dispatch(inputEvent.Type, inputEvent);
						}
					});
			}
		}
	}
}