using CodeCheckerClient.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCheckerClient.MVVM.Model;
namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {

            AllCourses = new Tuple<string, string>[70] {
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022"),
                 new Tuple<string, string>("חדוא 1","2019"),
                  new Tuple<string, string>("חדוא 2","2020"),
                   new Tuple<string, string>("מבוא לתכנות מערכות","2020"),
                    new Tuple<string, string>("אלוגרתמים 1","2021"),
                   new Tuple<string, string>("אלגורתמים 2","2021"),
                   new Tuple<string, string>("מבני נתונים","2022"),
             new Tuple<string, string>("אלגברה ליניארית","2022")
            };


      
            HashSet<string> YearSet = new HashSet<string>();

            foreach (Tuple<string,string> item in _allCourses)
            {
                YearSet.Add(item.Item2);
            }
            _years = new ObservableCollection<string>(YearSet);
            Syears = Years.Last<string>();




        }
        public Tuple<string, string>[] _allCourses;
        public Tuple<string, string>[] AllCourses { get { return this._allCourses; } set { this._allCourses = value; } }


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
            set { _syears = value;
                UserModel.Instance.CurrentlyShownYear = value;
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
                UserModel.Instance.CurrentlyShownCourse = value;
                
                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            }
        }
        public void ModifyCourseList()
        {

            List<string> CourseList = new List<string>();

            foreach (Tuple<string, string> item in _allCourses)
            {

                if (item.Item2.Equals(Syears))
                {
                    CourseList.Add(item.Item1);

                }
            }
            ShownCourses = CourseList.ToArray();
       
        }
    }
}
