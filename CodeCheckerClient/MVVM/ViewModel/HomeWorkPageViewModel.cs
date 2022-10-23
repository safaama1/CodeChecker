using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.Model;

namespace CodeCheckerClient.MVVM.ViewModel

{
    internal class HomeWorkPageViewModel:ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand UploadHomeWork { get; set; }

        private String _grade;
        public String Grade { get { return _grade; } set { _grade = value; OnPropertyChanged();  } }

        private string _DueDate;
        public string DueDate { get { return _DueDate; } set { _DueDate=value; } }

        private DateTime date { get; set; }


        private string[] _Uploaded;
        public string _SUploaded;
        public string SUploaded { get { return _SUploaded; } set { _SUploaded = value;  } }

        public string[] Uploaded { get { return _Uploaded; } set { _Uploaded = value; OnPropertyChanged(); } }


        public HomeWorkPageViewModel()
        {
            //TODO get from dataBase
           date=new DateTime(2020, 05, 09, 09, 15, 00);



            DueDate = date.ToString();


            //get the grade from database if the grade already exsists in the databse it will be displayed otherwise
            // the program will check if the current time is past the due date in that case the grade will be 
            // set to zero and the submit button locked

   
           //TODO get from database 
           // Grade = "";
            Grade = null;


            if ((date - DateTime.Now).TotalSeconds < 0 && Grade==null)
            {
                //todo update grade in database
                Grade = "0";
            }


            GoBackCommand = new RelayCommand(o =>
            {;
                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            });

            UploadHomeWork = new RelayCommand(o =>
            {
                if ((date - DateTime.Now).TotalSeconds < 0)
                {
                    MessageBox.Show("due date has passed", "mistakes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
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


                    }
                }
            });

        }



        public string GradeHomework(List<Tuple<string,string>> homework)
        {

    

            string errors = "";

            float grade = 100;

            if ((date - DateTime.Now).TotalSeconds < 0)
            {
                errors = "due date has passed";
                grade -= 100;
                MessageBox.Show(errors, "mistakes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return grade.ToString();
            }

       

            List<Tuple<int, string>> rules = new List<Tuple<int, string>>
            {
                new Tuple<int, string>(2, "q0_almost done.txt,15"),

                new Tuple<int, string>(3, @"cyber_security_cheat_sheet.txt,26,\b[M]\w+")
            };


            foreach (Tuple<int, string> rule in rules)
            {
                switch (rule.Item1)
                {
                    case 2:
                        string [] temp1 = rule.Item2.Split(',');
                        if (!Uploaded.Contains(temp1[0]))
                        {
                            grade -= Int64.Parse(temp1[1]);
                            errors+="uploaded files do not contatin file"+temp1[0] +" -" + temp1[1] + "\n";
                        }
                        break;
                    case 3:
                        string[] temp2 = rule.Item2.Split(',');
                        Regex rg = new Regex(@temp2[2]);
                        Trace.WriteLine(@temp2[2]);
                        Tuple<string,string> hw_content = homework.Find(item => item.Item1.Equals(temp2[0]));
                        if (hw_content == null)
                        {
                  
                            errors += " file " + temp2[0] + " does not contain pattern " + temp2[2] +" -" + temp2[1] +"\n";
                            grade -= Int64.Parse(temp2[1]);
                            break;
                        }
                        MatchCollection matchedAuthors = rg.Matches(hw_content.Item2);
                        if(matchedAuthors.Count<1)
                        {
                            grade -= Int64.Parse(temp2[1]);

                            errors += "file " + temp2[0] + " does not contain pattern " + temp2[2] +"-" + temp2[1] +"\n";
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
