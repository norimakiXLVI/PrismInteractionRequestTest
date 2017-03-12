using Prism.Interactivity.InteractionRequest;

namespace PrismInteractionRequestTest.ViewModels
{
	public class MessageBoxNotification : Notification
	{
		public enum IconType
		{
			None,
			Error,
			Warning,
			Information,
			Exclamation,
			Question
		}

		public enum ButtonType
		{
			Ok,
			OkCancel
		}

		public bool Confirmed { get; set; }

		public IconType Icon { get; set; }

		public ButtonType Button { get; set; }
	}
}

