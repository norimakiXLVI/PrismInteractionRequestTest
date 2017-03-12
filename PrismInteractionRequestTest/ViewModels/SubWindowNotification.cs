using Prism.Interactivity.InteractionRequest;
using System;

namespace PrismInteractionRequestTest.ViewModels
{
	public class SubWindowNotification : Notification
	{
		public event EventHandler Closing;

		public void DoClosing()
		{
			Closing?.Invoke(this, EventArgs.Empty);
		}
	}
}

