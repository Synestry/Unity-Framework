using System;

using Assets.Scripts.Framework.Entity;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI
{
	public delegate void UIControllerEvent(IUIController controller);

	public abstract class AbstractUIController<TUIView, TScope> : AbstractEntity, IUIController where TScope : AbstractScope
	{
		protected virtual GameObject Element { get { throw new Exception("Getter for Element must be overriden in child class"); } }

		protected virtual GameObject Parent { get { throw new Exception("Getter for Parent must be overriden in child class"); } }

		protected TScope Scope { get { return GameObject != null ? GameObject.GetComponent<TScope>() : null; } }

		protected TUIView View { get; set; }

		protected AbstractUIController(TUIView view)
		{
			View = view;
		}

		public event UIControllerEvent OnOpened;

		public event UIControllerEvent OnClosed;

		public bool IsOpen { get; private set; }

		public virtual void Open()
		{
			Initialise(Element, Parent);
			IsOpen = true;

			RegisterScope();
			OnOpened(this);
		}

		public virtual void Close()
		{
			IsOpen = false;
			Destroy();
			OnClosed(this);
		}

		protected virtual void RegisterScope() {}
	}
}