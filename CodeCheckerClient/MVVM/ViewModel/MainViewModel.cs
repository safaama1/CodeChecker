using CodeCheckerClient.Core;

namespace CodeCheckerClient.MVVM.ViewModel
{
    class MainViewModel :ObservableObject
    {
        private static MainViewModel instance = null;

        public RelayCommand StarterPageViewCommand { get; set; }

        public RelayCommand LoginPageViewCommand { get; set; }

        public RelayCommand MainPageViewCommand { get; set; }

        public RelayCommand CoursePageViewCommand { get; set; }

        public RelayCommand HomeWorkPageViewCommand { get; set; }


        public StarterPageViewModel StarterPageVM { get; set; }
        public LoginPageViewModel LoginPageVM { get; set; }
        public MainPageViewModel MainPageVM { get; set; }
        public CoursePageViewModel CoursePageVm { get; set; }

        public HomeWorkPageViewModel HomeWorkPageVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public static MainViewModel Instance()
    {
   
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        
    }

        public MainViewModel()
        {
            if (instance == null)
            {
                StarterPageVM = new StarterPageViewModel();
                LoginPageVM = new LoginPageViewModel();
                MainPageVM = new MainPageViewModel();
                CoursePageVm = new CoursePageViewModel();
                HomeWorkPageVM = new HomeWorkPageViewModel();

                _currentView = StarterPageVM;

                StarterPageViewCommand = new RelayCommand(o =>
                {
                    CurrentView = StarterPageVM;
                });

                LoginPageViewCommand = new RelayCommand(o =>
                {
                    CurrentView = LoginPageVM;
                });

                MainPageViewCommand = new RelayCommand(o =>
                {
                    CurrentView = MainPageVM;
                });

                CoursePageViewCommand = new RelayCommand(o =>
                {
                    CurrentView = CoursePageVm;
                });

                HomeWorkPageViewCommand = new RelayCommand(o => { 
                CurrentView = HomeWorkPageVM;
                });

                instance = this;
            }
            }
       
    }

}
