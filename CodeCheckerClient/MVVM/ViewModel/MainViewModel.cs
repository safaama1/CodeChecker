using CodeCheckerClient.Core;

namespace CodeCheckerClient.MVVM.ViewModel
{
    class MainViewModel :ObservableObject
    {
        public RelayCommand StarterPageViewCommand { get; set; }

        public RelayCommand LoginPageViewCommand { get; set; }

        public StarterPageViewModel StarterPageVM { get; set; }
        public LoginPageViewModel LoginPageVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }


        public MainViewModel()
        {

            StarterPageVM = new StarterPageViewModel();
            LoginPageVM = new LoginPageViewModel();
            _currentView = StarterPageVM;

            StarterPageViewCommand = new RelayCommand(o =>
            {
                CurrentView = StarterPageVM;
            });

            LoginPageViewCommand = new RelayCommand(o =>
            {
                CurrentView = LoginPageVM;
            });
        }
    }
}
