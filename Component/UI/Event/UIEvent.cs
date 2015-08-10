using Assets.Scripts.Framework.Event;

namespace Assets.Scripts.Framework.Component.UI.Event
{
	public class UIEvent : GameEvent
	{
		public IUIController Controller { get; set; }
	}
}