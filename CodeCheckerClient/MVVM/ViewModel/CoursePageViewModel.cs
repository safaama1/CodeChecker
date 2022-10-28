using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class CoursePageViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand AddStudentCommand { get; set; }

        public RelayCommand AddHomeWorkCommand { get; set; }

        public string _coursename;
        public string _year;

        private bool _IsALecturer;
        public bool IsALecturer { get { return _IsALecturer; } set { _IsALecturer = value; } }
        public string CourseName { get { return _coursename; } set { _coursename = value; } }

        public string Year { get { return _year; } set { _year = value; } }


        public HomeworkModel[] _homeworks;
        public string[] HomeWorks { get { return this._homeworks.Select(h => h.Name).ToArray(); } }

        public string _Shomework;
        public string SHomeWork
        {
            get { return this._Shomework; }
            set
            {
                Trace.WriteLine("" + value);
                UserModel.Instance.CurrentlyShownHomeWork = _homeworks.FirstOrDefault(h => h.Name == value);
                this._Shomework = value;
                if (UserModel.Instance.IsALecturer == true)
                {
                    MainViewModel.Instance().CurrentView = new LectuererHomeWorkPageViewModel();
                }
                else
                {
                    MainViewModel.Instance().CurrentView = new HomeWorkPageViewModel();

                }
            }
        }


        public CoursePageViewModel()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new MainPageViewModel();
            });
            AddStudentCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new AddStudentPageViewModel();
            });

            AddHomeWorkCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new AddHomeWorkPageViewModel();
            });


            if (UserModel.Instance.CurrentlyShownCourse != null)
            {

                CourseName = UserModel.Instance.CurrentlyShownCourse.Name;
                Year = UserModel.Instance.CurrentlyShownYear;
                IsALecturer = UserModel.Instance.IsALecturer;

                var response = REST_API.GetCallAsync($"Course/{UserModel.Instance.CurrentlyShownCourse.CourseId}");

                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var courseDetails = response.Result.Content.ReadAsAsync<CourseModel>().Result;
                    if (UserModel.Instance.IsALecturer)
                    {
                        string rulesJson = JsonSerializer.Serialize(courseDetails);
                        File.WriteAllText(
                            $@"C:\{UserModel.Instance.CurrentlyShownCourse.AcademicYear}\{UserModel.Instance.CurrentlyShownCourse.Name}\course_info.json", rulesJson);

                    }
                    UserModel.Instance.CurrentlyShownCourse = courseDetails;
                    var couresHomeworks = courseDetails.Homework;
                    _homeworks = couresHomeworks.ToArray();
                    foreach (var homework in _homeworks.Select(h => h.Name).ToArray())
                    {
                        var folderName = $@"C:\{UserModel.Instance.CurrentlyShownCourse.AcademicYear}\{UserModel.Instance.CurrentlyShownCourse.Name}\{homework}";
                        // If directory does not exist, create it
                        if (!Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(folderName);
                        }
                    }

                    CreateCourseGradesCSV(_homeworks);
                }
            }
        }

        private void CreateCourseGradesCSV(HomeworkModel[] homeworks)
        {
            using (var writer = new StreamWriter($@"C:\{UserModel.Instance.CurrentlyShownCourse.AcademicYear}\{UserModel.Instance.CurrentlyShownCourse.Name}\course_grades.csv"))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {

                csvWriter.WriteField("Student ID");
                foreach (var homework in homeworks)
                {
                    csvWriter.WriteField(homework.Name);
                }
                csvWriter.WriteField("Avg Grade");
                csvWriter.NextRecord();

                foreach (var student in UserModel.Instance.CurrentlyShownCourse.Students)
                {
                    var response = REST_API.GetCallAsync($"Student/{student.StudentId}");

                    if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var studentDetails = response.Result.Content.ReadAsAsync<StudentModel>().Result;
                        csvWriter.WriteField(studentDetails.StudentId);
                        double gradeAvg = 0;
                        foreach (var homework in homeworks)
                        {
                            var submittedHw = studentDetails.SubmittedHomework.FirstOrDefault(h => h.HomeworkId == homework.HomeworkId);
                            if (submittedHw != null && submittedHw?.Grade != null)
                            {
                                csvWriter.WriteField(submittedHw.Grade);
                                gradeAvg += submittedHw.Grade;
                            }
                            else
                            {
                                csvWriter.WriteField(0);
                                gradeAvg += 0;
                            }

                        }
                        csvWriter.WriteField(gradeAvg / homeworks.Length);
                        csvWriter.NextRecord();

                    }
                    else throw new HttpRequestException(response.ToString());

                }

            }
        }
    }
}

