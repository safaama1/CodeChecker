using CodeCheckerClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCheckerClient.MVVM.Model;
using System.Diagnostics;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class CoursePageViewModel: ObservableObject
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
        public string SHomeWork { get { return this._Shomework; } set {
                Trace.WriteLine("" + value);
                UserModel.Instance.Hwname=value;
                this._Shomework = value;
                if (UserModel.Instance.IsALecturer==true)
                {

                }
                else
                {
                    MainViewModel.Instance().CurrentView = new HomeWorkPageViewModel();
                }
                 } }


        public CoursePageViewModel()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                    MainViewModel.Instance().CurrentView = new MainPageViewModel();
            });

            CourseName = UserModel.Instance.CurrentlyShownCourse;
            Year = UserModel.Instance.CurrentlyShownYear;
            IsALecturer = UserModel.Instance.IsALecturer;

            HomeWorks = new string[30] {
                 "homework1",
                 "homework2",
                 "homework3",
                 "homework4",
                 "homework5",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1" ,
                "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1" ,
                "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1",
                 "homework1" };

        }
    }
}
