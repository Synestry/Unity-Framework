using System.Collections.Generic;

using Assets.Scripts.Framework.Component.Input.Event;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.Input.Listener
{
	public class KeyboardInputListener : InputListener
	{
		private Dictionary<KeyCode, InputEventType> KeyPressHandlers { get; set; }

		public KeyboardInputListener()
		{
			KeyPressHandlers = new Dictionary<KeyCode, InputEventType>
			{
				{ KeyCode.A, InputEventType.KeyLeft },
				{ KeyCode.LeftArrow, InputEventType.KeyLeft },
				{ KeyCode.D, InputEventType.KeyRight },
				{ KeyCode.RightArrow, InputEventType.KeyRight },
				{ KeyCode.W, InputEventType.KeyUp },
				{ KeyCode.UpArrow, InputEventType.KeyUp },
				{ KeyCode.S, InputEventType.KeyDown },
				{ KeyCode.DownArrow, InputEventType.KeyDown },
				{ KeyCode.Space, InputEventType.Space },
			};
		}

		public override void ProcessInput()
		{
			foreach (var handler in KeyPressHandlers)
			{
				if (UnityEngine.Input.GetKeyDown(handler.Key))
				{
					InputEvents.Enqueue(new InputEvent { Type = handler.Value });
				}
			}
		}
	}
}