using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace CodeCheckerClient.MVVM.ViewModel

{
    internal class HomeWorkPageViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand UploadHomeWork { get; set; }

        private String _grade;
        public String Grade { get { return _grade; } set { _grade = value; OnPropertyChanged(); } }

        private string _DueDate;
        public string DueDate { get { return _DueDate; } set { _DueDate = value; } }

        private string[] _Uploaded;
        public string _SUploaded;
        public string SUploaded { get { return _SUploaded; } set { _SUploaded = value; } }

        public string[] Uploaded { get { return _Uploaded; } set { _Uploaded = value; OnPropertyChanged(); } }

        public HomeWorkPageViewModel()
        {
            CreateGradesCSVFile();
            DueDate = "30/10/2022";



            GoBackCommand = new RelayCommand(o =>
            {
                ;
                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            });

            UploadHomeWork = new RelayCommand(o =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] b = fileDialog.FileNames;

                    List<Tuple<string, string>> hwContent = new List<Tuple<string, string>>();

                    foreach (string q in b)
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(q);
                        string a = sr.ReadToEnd();
                        hwContent.Add(new Tuple<string, string>(q, a));
                        MessageBox.Show(a);
                        sr.Close();
                    }



                    List<string> temp = new List<string>();
                    foreach (String file in fileDialog.FileNames)
                    {
                        temp.Add(Path.GetFileName(file));
                    }

                    Uploaded = temp.ToArray();

                    Grade = GradeHomework(hwContent);


                }

            });

        }



        public string GradeHomework(List<Tuple<string, string>> homework)
        {
            string rulename;
            string parameter;



            foreach (Tuple<string, string> homeworkItem in homework)
                Trace.WriteLine(homeworkItem);
            return "100";
        }

        public void CreateGradesCSVFile()
        {
            if (UserModel.Instance.CurrentlyShownHomeWork != null)
            {
                var hwId = UserModel.Instance.CurrentlyShownHomeWork.HomeworkId;
                var homeworkDetails = REST_API.GetCallAsync($"Homework/{hwId}");

                if (homeworkDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var submittedHomeworks = homeworkDetails.Result.Content.ReadAsAsync<HomeworkModel>().Result.SubmittedHomework;
                    using (var writer = new StreamWriter($@"C:\{UserModel.Instance.CurrentlyShownCourse.AcademicYear}\{UserModel.Instance.CurrentlyShownCourse.Name}\{UserModel.Instance.CurrentlyShownHomeWork.Name}\grades.csv"))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(submittedHomeworks);
                    }

                }
            }
        }

    }
}
