namespace Assets.Scripts.Framework.Component.UI
{
	public interface IUIController
	{
		bool IsOpen { get; }

		void Open();

		void Close();

		void Update(float dt);

		event UIControllerEvent OnOpened;

		event UIControllerEvent OnClosed;
	}
}