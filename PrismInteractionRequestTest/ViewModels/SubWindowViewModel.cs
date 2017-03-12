using Prism.Mvvm;

namespace PrismInteractionRequestTest.ViewModels
{
	public class SubWindowViewModel : BindableBase
	{
		private string _title = "Sub Window Title";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public SubWindowViewModel()
		{
		}
	}
}

