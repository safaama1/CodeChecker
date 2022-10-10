using CodeCheckerClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCheckerClient.MVVM.Model;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class CoursePageViewModel: ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }

        public string _coursename;
        public string _year;

        public string CourseName { get { return _coursename; } set { _coursename = value; } }

        public string Year { get { return _year; } set { _year = value; } }
        public CoursePageViewModel()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                    MainViewModel.Instance().CurrentView = new MainPageViewModel();
            });

            CourseName = UserModel.Instance.CurrentlyShownCourse;
            Year = UserModel.Instance.CurrentlyShownYear;
        }
    }
}
