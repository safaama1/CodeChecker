using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class LectuererHomeWorkPageViewModel: ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand DownloadCommand { get; set; }

        private string _Average;
        public string Average { get { return _Average; } set { _Average = value; OnPropertyChanged(); 
            } }

        public string HwName { get; set; }


        private DataTable tableResult = new DataTable();
        public DataTable TableResult
        {
            get { return tableResult; }
            set { tableResult = value; OnPropertyChanged(); }
        }


        public LectuererHomeWorkPageViewModel()
        {
            HwName = UserModel.Instance.Hwname;
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

                //Exporting to CSV.
                string folderName = System.Text.Encoding.Default.GetString(stream.ToArray());
                try { 
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\DataGridViewExport_"+HwName+".csv", folderName);

                }
                catch
                {
                    MessageBox.Show("Another process is using file"+ "DataGridViewExport_"+HwName+".csv", "error saving file",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            DataColumn c = new DataColumn();
            c.ColumnName = "Name";
            this.TableResult.Columns.Add(c);

            c = new DataColumn();
            c.ColumnName = "Grade";
            this.TableResult.Columns.Add(c);



      

            List<Tuple<string, string>> list = new List<Tuple<string, string>>();

            // insert the data here
            list.Add(new Tuple<string, string>("1st student", "100"));
            list.Add(new Tuple<string, string>("2st student", "89"));
            list.Add(new Tuple<string, string>("est student", "97"));
            list.Add(new Tuple<string, string>("rst student", "88"));
            list.Add(new Tuple<string, string>("5st student", "96"));


            float sumogrades = 0;
           
            foreach(Tuple<string,string> s in list)
            {

                DataRow row1 = this.TableResult.NewRow();
                row1["Name"] = s.Item1;
                row1["Grade"] = s.Item2 ;
                sumogrades += float.Parse(s.Item2);
                this.TableResult.Rows.Add(row1);

            }
            float temp= sumogrades / list.Count;
            Average = temp.ToString();
        }
    }
}
