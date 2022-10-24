using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class LectuererHomeWorkPageViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand DownloadCommand { get; set; }

        private string _Average;
        public string Average
        {
            get { return _Average; }
            set
            {
                _Average = value; OnPropertyChanged();
            }
        }

        public string HwName { get; set; }


        private DataTable tableResult = new DataTable();
        public DataTable TableResult
        {
            get { return tableResult; }
            set { tableResult = value; OnPropertyChanged(); }
        }


        public LectuererHomeWorkPageViewModel()
        {
            if (UserModel.Instance.CurrentlyShownHomeWork != null)
            {
                var response = REST_API.GetCallAsync($"Homework/{UserModel.Instance.CurrentlyShownHomeWork.HomeworkId}");
                var selectedHomeworkDetails = response.Result.Content.ReadAsAsync<HomeworkModel>().Result;
                List<Tuple<string, string>> studentSubmitsDetails = new List<Tuple<string, string>>();


                if (UserModel.Instance.IsALecturer)
                {
                    string rulesJson = JsonSerializer.Serialize(selectedHomeworkDetails.HomeworkRules);
                    File.WriteAllText(
                        $@"C:\{UserModel.Instance.CurrentlyShownCourse.AcademicYear}\{UserModel.Instance.CurrentlyShownCourse.Name}\{UserModel.Instance.CurrentlyShownHomeWork.Name}\rules.json", rulesJson);
                }

                foreach (var submit in selectedHomeworkDetails.SubmittedHomework)
                {
                    studentSubmitsDetails.Add(new Tuple<string, string>(submit.Student.Name, submit.Grade.ToString()));
                }


                float sumogrades = 0;

                DataColumn c = new DataColumn();
                c.ColumnName = "Name";
                this.TableResult.Columns.Add(c);

                c = new DataColumn();
                c.ColumnName = "Grade";
                this.TableResult.Columns.Add(c);

                foreach (Tuple<string, string> s in studentSubmitsDetails)
                {

                    DataRow row1 = this.TableResult.NewRow();
                    row1["Name"] = s.Item1;
                    row1["Grade"] = s.Item2;
                    sumogrades += float.Parse(s.Item2);
                    this.TableResult.Rows.Add(row1);

                }
                float avg;
                if (studentSubmitsDetails.Count != 0)
                    avg = sumogrades / studentSubmitsDetails.Count;
                else
                    avg = 0;
                Average = avg.ToString();

            }
            if (!string.IsNullOrEmpty(UserModel.Instance.CurrentlyShownHomeWork.Name))
                HwName = UserModel.Instance.CurrentlyShownHomeWork.Name;

            GoBackCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            });

            DownloadCommand = new RelayCommand(o =>
            {
                var stream = new MemoryStream();
                StreamWriter sw = new StreamWriter(stream);

                for (int i = 0; i < tableResult.Columns.Count; i++)
                {
                    sw.Write(tableResult.Columns[i]);
                    if (i < tableResult.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow row in tableResult.Rows)
                {
                    for (int i = 0; i < tableResult.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(row[i]))
                        {
                            string value = row[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(row[i].ToString());
                            }
                        }
                        if (i < tableResult.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();

                //Exporting to CSV if the user is a lecturer
                if (UserModel.Instance.IsALecturer)
                {
                    string tableDetails = System.Text.Encoding.Default.GetString(stream.ToArray());
                    string path = $@"C:\{UserModel.Instance.CurrentlyShownCourse.AcademicYear}\{UserModel.Instance.CurrentlyShownCourse.Name}\{UserModel.Instance.CurrentlyShownHomeWork.Name}\grades.csv";

                    try
                    {
                        File.WriteAllText(path, tableDetails);
                    }
                    catch
                    {
                        MessageBox.Show("Another process is using file" + path, "error saving file",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            });




        }
    }
}
