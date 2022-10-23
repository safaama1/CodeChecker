using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class CoursePageViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }

        public string _coursename;
        public string _year;

        private bool _IsALecturer;
        public bool IsALecturer { get { return _IsALecturer; } set { _IsALecturer = value; } }
        public string CourseName { get { return _coursename; } set { _coursename = value; } }

        public string Year { get { return _year; } set { _year = value; } }


        public string[] _homeworks;
        public string[] HomeWorks { get { return this._homeworks; } set { this._homeworks = value; } }

        public string _Shomework;
        public string SHomeWork
        {
            get { return this._Shomework; }
            set
            {
                Trace.WriteLine("" + value);
                UserModel.Instance.Hwname = value;
                this._Shomework = value;
                if (UserModel.Instance.IsALecturer == true)
                {
                    UserModel.Instance.CurrentlyShownHomeWork = UserModel.Instance.CurrentlyShownCourse.Homework.Where(h => h.Name == value).FirstOrDefault();
                    MainViewModel.Instance().CurrentView = new HomeWorkPageViewModel();

                }
                else
                {
                }
            }
        }


        public CoursePageViewModel()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new MainPageViewModel();
            });
            if (UserModel.Instance.CurrentlyShownCourse != null)
            {
                string folderName = $@"C:\{UserModel.Instance.CurrentlyShownCourse.AcademicYear}\{UserModel.Instance.CurrentlyShownCourse.Name}";
                // If directory does not exist, create it
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                CourseName = UserModel.Instance.CurrentlyShownCourse.Name;
                Year = UserModel.Instance.CurrentlyShownYear;
                IsALecturer = UserModel.Instance.IsALecturer;

                var response = REST_API.GetCallAsync($"Course/{UserModel.Instance.CurrentlyShownCourse.CourseId}");

                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var courseDetails = response.Result.Content.ReadAsAsync<CourseModel>().Result;
                    UserModel.Instance.CurrentlyShownCourse = courseDetails;
                    var couresHomeworks = courseDetails.Homework;
                    HomeWorks = couresHomeworks.Select(h => h.Name).ToArray();
                    foreach (var homework in HomeWorks)
                    {
                        folderName = $@"C:\{UserModel.Instance.CurrentlyShownCourse.AcademicYear}\{UserModel.Instance.CurrentlyShownCourse.Name}\{homework}";
                        // If directory does not exist, create it
                        if (!Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(folderName);
                        }
                    }
                }
            }



        }
    }
}
