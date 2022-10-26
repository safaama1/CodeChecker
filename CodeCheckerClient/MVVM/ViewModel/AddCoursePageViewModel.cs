using CodeCheckerClient.Core;
using CodeCheckerClient.Models;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class AddCoursePageViewModel
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand AddCourseCommand { get; set; }

        private string _CourseName;
        private string _AcademicYear;
        public string CourseName { get { return _CourseName; } set { _CourseName = value; } }
        public string AcademicYear { get { return _AcademicYear; } set { _AcademicYear = value; } }

        public AddCoursePageViewModel()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new MainPageViewModel();
            });
            AddCourseCommand = new RelayCommand(async o =>
            {
                bool isInputValid = true;
                try 
                { 
                    Int64.Parse(AcademicYear);
                }
                catch
                {
                    isInputValid = false;
                }

                var CourseToAdd = new AddCourseToLecturerModel { Name = CourseName, AcademicYear = AcademicYear , TeacherID = UserModel.Instance.Id};


                if (int.TryParse(AcademicYear, out _))
                { 
                    var createStudentResponse = await REST_API.PostCallAsync($"Course/create", CourseToAdd).ConfigureAwait(false);

                    if (createStudentResponse.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        MainViewModel.Instance().CurrentView = new MainPageViewModel();
                    }
                }
            });
        }
    }

}
