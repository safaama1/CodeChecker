using CodeCheckerClient.Core;
using CodeCheckerClient.Models;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class AddHomeWorkPageViewModel : ObservableObject
    {
        private string _title;

        public string HomeWorkName { get { return _title; } set { _title = value; } }
        private List<AddRuleToHomeworkModel> _AllRules = new List<AddRuleToHomeworkModel>();
        private bool _Rule3 = false;
        public bool Rule3 { get { return _Rule3; } set { _Rule3 = value; OnPropertyChanged(); } }
        public RelayCommand GoBackCommand { get; set; }

        public RelayCommand AddRuleCommand { get; set; }

        public RelayCommand AddHomeWorkCommand { get; set; }



        private string _fileName;
        private string _Weight;
        private string _pattern;
        public string FileName { get { return _fileName; } set { _fileName = value; OnPropertyChanged(); } }
        public string Weight { get { return _Weight; } set { _Weight = value; OnPropertyChanged(); } }
        public string Pattern { get { return _pattern; } set { _pattern = value; OnPropertyChanged(); } }

        private DateTime _Deadline;

        public DateTime Deadline { get { return _Deadline; } set { _Deadline = value; } }

        private ObservableCollection<String> _Rules;

        public ObservableCollection<String> Rules
        {
            get { return _Rules; }
            set { _Rules = value; }
        }
        private String _SRules;

        public String SRules
        {
            get { return _SRules; }
            set
            {

                _SRules = value;


                if (_SRules.Equals("does pattern exsist"))
                    Rule3 = true;
                else Rule3 = false;


                OnPropertyChanged();
            }
        }

        public AddHomeWorkPageViewModel()
        {
            Deadline = DateTime.Now;
            Rule3 = false;
            string[] temp = { "does file exsist", "does pattern exsist" }; ;
            _Rules = new ObservableCollection<string>(temp);
            _SRules = "does file exsist";
            //,"does pattern exsist"};)
            GoBackCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            });

            AddHomeWorkCommand = new RelayCommand(async o =>
            {
                Trace.WriteLine(_title + " " + _Deadline + " " + _AllRules);
                var homeworkToAdd = new AddHomeworkToCourseModel { Name = _title, CourseId = UserModel.Instance.CurrentlyShownCourse.CourseId };
                var createHomeworkResponse = await REST_API.PostCallAsync($"Homework/create", homeworkToAdd).ConfigureAwait(false);
                var homeworkId = createHomeworkResponse.Content.ReadAsAsync<HomeworkModel>().Result.HomeworkId;
                foreach (AddRuleToHomeworkModel ruleToAdd in _AllRules)
                {

                    var addRuleToHomeworkResponse = await REST_API.PutCallAsync($"Homework/{homeworkId}/add-rule", ruleToAdd).ConfigureAwait(false);
                    if (addRuleToHomeworkResponse.StatusCode != System.Net.HttpStatusCode.Accepted)
                    {
                        _AllRules = new List<AddRuleToHomeworkModel>();
                        SRules = "";
                        FileName = "";
                        Weight = "";
                        Pattern = "";
                        return;
                    }
                }

                if (createHomeworkResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    MainViewModel.Instance().CurrentView = new CoursePageViewModel();
                else _title = "";


            });

            AddRuleCommand = new RelayCommand(o =>
            {

                if (!(string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(Weight) || (Rule3 == true && string.IsNullOrEmpty(Pattern))))
                {




                    string ruleParameter;
                    if (Rule3 == true)
                        ruleParameter = FileName + "," + Weight + "," + Pattern;
                    else
                        ruleParameter = FileName + "," + Weight;

                    var ruleToAdd = new AddRuleToHomeworkModel { HomeworkRuleId = Guid.NewGuid(), Title = _SRules, Description = ruleParameter, Points = Convert.ToDouble(Weight) };
                    _AllRules.Add(ruleToAdd);

                    FileName = "";
                    Weight = "";
                    Pattern = "";



                }
            });
        }
    }
}
