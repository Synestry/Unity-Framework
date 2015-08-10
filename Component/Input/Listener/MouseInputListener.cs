using Assets.Scripts.Framework.Component.Input.Event;

namespace Assets.Scripts.Framework.Component.Input.Listener
{
	public class MouseInputListener : InputListener
	{
		public override void ProcessInput()
		{
			if(UnityEngine.Input.GetMouseButtonUp(0)) InputEvents.Enqueue(new InputEvent { Type = InputEventType.Click });
		}
	}
}
