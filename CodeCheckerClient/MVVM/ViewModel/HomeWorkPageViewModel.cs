using CodeCheckerClient.Core;
using CodeCheckerClient.Models;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CodeCheckerClient.MVVM.ViewModel

{
    internal class HomeWorkPageViewModel : ObservableObject
    {
        public string HwName { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand UploadHomeWork { get; set; }

        private String _grade;
        public String Grade { get { return _grade; } set { _grade = value; OnPropertyChanged(); } }

        private SubmittedHomeworkModel _submittedHomework;
        public SubmittedHomeworkModel SubmittedHomework { get { return _submittedHomework; } set { _submittedHomework = value; OnPropertyChanged(); } }


        //private string _DueDate;
        //public string DueDate { get { return _DueDate; } set { _DueDate = value; } }

       // private DateTime date { get; set; }


        private string[] _Uploaded;
        public string _SUploaded;
        public string SUploaded { get { return _SUploaded; } set { _SUploaded = value; } }

        public string[] Uploaded { get { return _Uploaded; } set { _Uploaded = value; OnPropertyChanged(); } }


        public HomeWorkPageViewModel()
        {

            //TODO get from dataBase
           // date = new DateTime(2020, 05, 09, 09, 15, 00);



          //  DueDate = date.ToString();


            //get the grade from database if the grade already exsists in the databse it will be displayed otherwise
            // the program will check if the current time is past the due date in that case the grade will be 
            // set to zero and the submit button locked
            if (UserModel.Instance.CurrentlyShownHomeWork != null)
            {
                if (!string.IsNullOrEmpty(UserModel.Instance.CurrentlyShownHomeWork.Name))
                    HwName = UserModel.Instance.CurrentlyShownHomeWork.Name;

                var response = REST_API.GetCallAsync($"Homework/{UserModel.Instance.CurrentlyShownHomeWork.HomeworkId}");
                var selectedHomeworkDetails = response.Result.Content.ReadAsAsync<HomeworkModel>().Result;
                UserModel.Instance.CurrentlyShownHomeWork = selectedHomeworkDetails;
                List<Tuple<string, string>> studentSubmitsDetails = new List<Tuple<string, string>>();

                foreach (var submit in selectedHomeworkDetails.SubmittedHomework)
                {
                    if (submit.StudentId == UserModel.Instance.Id)
                    {
                        SubmittedHomework = submit;
                        Grade = SubmittedHomework.Grade.ToString();
                    }
                }

                if (Grade == null)
                    Grade = "0";
            }




            GoBackCommand = new RelayCommand(o =>
            {
                ;
                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            });

            UploadHomeWork = new RelayCommand(async o =>
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
                        hwContent.Add(new Tuple<string, string>(Path.GetFileName(q), a));
                        sr.Close();
                    }

                    List<string> temp = new List<string>();
                    foreach (String file in fileDialog.FileNames)
                    {
                        temp.Add(Path.GetFileName(file));
                    }

                    Uploaded = temp.ToArray();

                    Grade = GradeHomework(hwContent);

                    //todo update grade in database
                    var submittedHomeworkToAdd = new AddSubmittedHomeworkModel { StudentId = UserModel.Instance.Id, SubmittedDate = DateTime.Now };
                    var createSubmittedHomeworkResponse = await REST_API.PutCallAsync($"Homework/{UserModel.Instance.CurrentlyShownHomeWork.HomeworkId}/add-submitted-homework", submittedHomeworkToAdd).ConfigureAwait(false);
                    if (createSubmittedHomeworkResponse.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        var submitted = createSubmittedHomeworkResponse.Content.ReadAsAsync<SubmittedHomeworkModel>().Result;
                        var updateGrade = new UpdateSubmittedHomeworkGrade { Grade = Convert.ToDouble(Grade) };
                        var response2 = await REST_API.PutCallAsync($"Homework/{UserModel.Instance.CurrentlyShownHomeWork.HomeworkId}/submitted-homework/{submitted.SubmittedHomeworkId}/update-grade"
                            , updateGrade).ConfigureAwait(false);
                        if (response2.StatusCode != System.Net.HttpStatusCode.Accepted) Grade = "";

                    }

                }

            });

        }



        public string GradeHomework(List<Tuple<string, string>> homework)
        {

            string errors = "";

            float grade = 100;

            if (homework.Count() == 0)
            {
                errors = "no files uploaded";
                grade -= 100;
                MessageBox.Show(errors, "mistakes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return grade.ToString();
            }

            foreach (HomeworkRuleModel rule in UserModel.Instance.CurrentlyShownHomeWork.HomeworkRules)
            {
                switch (rule.Title)
                {
                    case "does file exsist":
                        string[] temp1 = rule.Description.Split(',');
                        if (!Uploaded.Contains(temp1[0]))
                        {
                            grade -= Int64.Parse(temp1[1]);
                            errors += "uploaded files do not contatin file: " + temp1[0] + " -" + temp1[1] + "\n";
                        }
                        break;
                    case "does pattern exsist":
                        string[] temp2 = rule.Description.Split(',');
                        Regex rg = new Regex(@temp2[2]);
                        Trace.WriteLine(@temp2[2]);
                        Tuple<string, string> hw_content = homework.Find(item => item.Item1.Equals(temp2[0]));
                        if (hw_content == null)
                        {

                            errors += " file " + temp2[0] + " does not contain pattern " + temp2[2] + " -" + temp2[1] + "\n";
                            grade -= Int64.Parse(temp2[1]);
                            break;
                        }
                        MatchCollection matchedAuthors = rg.Matches(hw_content.Item2);
                        if (matchedAuthors.Count < 1)
                        {
                            grade -= Int64.Parse(temp2[1]);

                            errors += "file " + temp2[0] + " does not contain pattern " + temp2[2] + "-" + temp2[1] + "\n";
                        }
                        break;
                }
            }


            if (errors.Length > 0)
            {
                MessageBox.Show(errors, "mistakes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return grade.ToString();
        }

    }
}
