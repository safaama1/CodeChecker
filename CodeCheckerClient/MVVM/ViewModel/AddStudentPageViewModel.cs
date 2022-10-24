using CodeCheckerClient.Core;
using CodeCheckerClient.Models;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class AddStudentPageViewModel
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand AddStudentCommand { get; set; }

        private string _UserName;
        private string _UserId;
        public string UserName { get { return _UserName; } set { _UserName = value; } }
        public string UserId { get { return _UserId; } set { _UserId = value; } }

        public AddStudentPageViewModel()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            });
            AddStudentCommand = new RelayCommand(async o =>
            {
                var studentToAdd = new AddStudentToCourseModel { Name = UserName, StudentId = UserId };
                var createStudentResponse = await REST_API.PostCallAsync($"Student/create", studentToAdd).ConfigureAwait(false);
                var addStudentToCourseResponse = await REST_API.PutCallAsync($"Course/{UserModel.Instance.CurrentlyShownCourse.CourseId}/add-student", studentToAdd).ConfigureAwait(false);

                if (createStudentResponse.StatusCode == System.Net.HttpStatusCode.OK
                    && addStudentToCourseResponse.StatusCode == System.Net.HttpStatusCode.Accepted)
                    MainViewModel.Instance().CurrentView = new CoursePageViewModel();
                else
                    UserName = UserId = "";

            });
        }
    }
}
