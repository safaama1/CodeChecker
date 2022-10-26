using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            if (!string.IsNullOrEmpty(UserModel.Instance.Id))
            {
                IsALecturer = UserModel.Instance.IsALecturer;

                AddCourseCommand = new RelayCommand(o =>
                {
                    MainViewModel.Instance().CurrentView = new AddCoursePageViewModel();


                });
                GoBackCommand = new RelayCommand(o =>
                {
                    UserModel.nullifyUser();
                    MainViewModel.Instance().CurrentView = new LoginPageViewModel();

          
                });

                if (UserModel.Instance.IsALecturer)
                {
                    var teacherDetails = REST_API.GetCallAsync($"Teacher/{UserModel.Instance.Id}");
                    var teacherCourses = teacherDetails.Result.Content.ReadAsAsync<TeacherModel>().Result.Courses;
                    AllCourses = teacherCourses.ToArray();
                }
                else
                {
                    var studentDetails = REST_API.GetCallAsync($"Student/{UserModel.Instance.Id}");
                    var studentCourses = studentDetails.Result.Content.ReadAsAsync<StudentModel>().Result.Courses;
                    AllCourses = studentCourses.ToArray();
                }
                HashSet<string> YearSet = new HashSet<string>();

                foreach (CourseModel course in _allCourses)
                {
                    YearSet.Add(course.AcademicYear);
                }
                _years = new ObservableCollection<string>(YearSet);

                if (UserModel.Instance.CurrentlyShownYear == null)
                    Syears = Years.Last<string>();
                else
                    Syears = UserModel.Instance.CurrentlyShownYear;
            }




        }

        public Boolean IsALecturer { get; set; }

        public RelayCommand AddCourseCommand { get; set; }
        public CourseModel[] _allCourses;
        public CourseModel[] AllCourses { get { return this._allCourses; } set { this._allCourses = value; } }


        public string[] _shownCourses;
        public string[] ShownCourses { get { return this._shownCourses; } set { this._shownCourses = value; OnPropertyChanged("ShownCourses"); } }

        private ObservableCollection<String> _years;

        public ObservableCollection<String> Years
        {
            get { return _years; }
            set { _years = value; }
        }
        private String _syears;

        public String Syears
        {
            get { return _syears; }
            set
            {
                _syears = value;
                UserModel.Instance.CurrentlyShownYear = value;
                string folderName = $@"C:\{UserModel.Instance.CurrentlyShownYear}";
                // If directory does not exist, create it
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                ModifyCourseList();
            }
        }
        private string _scourse;
        public String Scourse
        {
            get { return _scourse; }
            set
            {
                _scourse = value;
                UserModel.Instance.CurrentlyShownCourse = AllCourses.Where(c => c.Name == value).FirstOrDefault();

                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            }
        }

        public RelayCommand GoBackCommand { get; set; }
        public void ModifyCourseList()
        {

            List<string> CourseList = new List<string>();

            foreach (CourseModel item in _allCourses)
            {

                if (item.AcademicYear.Equals(Syears))
                {
                    CourseList.Add(item.Name);

                }
            }
            ShownCourses = CourseList.ToArray();

        }
    }
}
