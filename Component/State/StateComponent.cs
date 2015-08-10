using Assets.Scripts.Framework.Component.State.Event;

namespace Assets.Scripts.Framework.Component.State
{
	public class StateComponent : AbstractGameComponent
	{
		public new bool GamePaused { get; protected set; }

		public override void Update(float dt)
		{
			EventDispatcher.Dispatch(StateEventType.GameUpdate, new GameUpdateEvent { DeltaTime = dt, GamePaused = GamePaused });
		}

		public virtual void PauseGame()
		{
			GamePaused = true;
			EventDispatcher.Dispatch(StateEventType.GamePaused);
		}

		public void UnpauseGame()
		{
			GamePaused = false;
			EventDispatcher.Dispatch(StateEventType.GameUnpaused);
		}
	}
}