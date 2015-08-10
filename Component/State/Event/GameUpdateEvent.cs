using Assets.Scripts.Framework.Event;

namespace Assets.Scripts.Framework.Component.State.Event
{
	public class GameUpdateEvent : GameEvent
	{
		public float DeltaTime { get; set; }

		public bool GamePaused { get; set; }
	}
}