using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Diagnostics;

namespace PrismInteractionRequestTest.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private string _title = "Prism InteractionRequest Test";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public InteractionRequest<MessageBoxNotification> MessageBoxRequest { get; } = new InteractionRequest<MessageBoxNotification>();
		public DelegateCommand MessageBoxCommand { get; }

		public InteractionRequest<SubWindowNotification> SubWindowRequest { get; } = new InteractionRequest<SubWindowNotification>();
		public DelegateCommand SubWindowShowCommand { get; }

		public DelegateCommand SubWindowCloseCommand { get; }

		public MainWindowViewModel()
		{
			MessageBoxCommand = new DelegateCommand(async () =>
			{
				var info = new MessageBoxNotification()
				{
					Title = "Confirm",
					Content = "aaaaaaaaaaaaaaaaaaaaaaaaaa\nbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb\ncccccc",
					Icon = MessageBoxNotification.IconType.Question,
					Button = MessageBoxNotification.ButtonType.OkCancel
				};
	
				var result = await MessageBoxRequest.RaiseAsync(info);
				Debug.WriteLine(result.Confirmed);
			});

			var notify = new SubWindowNotification();

			SubWindowShowCommand = new DelegateCommand(() =>
			{
				SubWindowRequest.Raise(notify);
			});

			SubWindowCloseCommand = new DelegateCommand(() =>
			{
				notify.DoClosing();
			});
		}
	}
}

