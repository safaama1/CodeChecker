using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class LoginPageViewModel : ObservableObject
    {
        public RelayCommand LogInCommand { get; set; }
        public RelayCommand SignUpCommand { get; set; }
        private string _userId;
        public string UserId
        {
            get { return this._userId; }
            set
            {
                this._userId = value;
                OnPropertyChanged();
            }
        }
        public LoginPageViewModel()
        {

            LogInCommand = new RelayCommand(o =>
            {
                CheckLoginAndNavigateIfExists(UserId);
            });


            SignUpCommand = new RelayCommand(o =>
            {

                MainViewModel.Instance().CurrentView = new SignUpPageViewModel();
            });
        }


        private bool CheckLoginAndNavigateIfExists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            // check if there is a student/ teacher with the given id 
            var studentDetails = REST_API.GetCallAsync($"Student/{id}");
            var teacherDetails = REST_API.GetCallAsync($"Teacher/{id}");


            if (teacherDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UserModel.Instance.Id = id;
                UserModel.Instance.IsALecturer = true;
                MainViewModel.Instance().CurrentView = new MainPageViewModel();
                return true;
            }
            if (studentDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UserModel.Instance.Id = id;
                UserModel.Instance.IsALecturer = false;
                MainViewModel.Instance().CurrentView = new MainPageViewModel();

            }
            return false;
        }


    }

}
