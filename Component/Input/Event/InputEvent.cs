using Assets.Scripts.Framework.Event;

namespace Assets.Scripts.Framework.Component.Input.Event
{
	public class InputEvent : GameEvent
	{
		public InputEventType Type { get; set; }
	}
}