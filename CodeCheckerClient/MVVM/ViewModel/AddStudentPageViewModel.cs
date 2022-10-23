using CodeCheckerClient.Core;
using CodeCheckerClient.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class AddStudentPageViewModel
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand AddStudentCommand { get; set; }

        private string _UserName;
        private string _UserId;
        public string UserName { get { return _UserName; } set { _UserName = value; } }
        public string UserId { get { return _UserId; } set { _UserId = value; } }

        public AddStudentPageViewModel()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                MainViewModel.Instance().CurrentView = new CoursePageViewModel();
            });
            AddStudentCommand = new RelayCommand(o =>
            {

               // _UserId _UserName;

                // todo not working yet 
                //Tranform it to Json object
                //string jsonData = JsonConvert.SerializeObject(myData);
                //var a=REST_API.GetCall("​/api​/Course​/all");
                //  dynamic stuff = JsonConvert.DeserializeObject(a);
                //JObject jsonObject = JObject.Parse(a.Result.Content.ToString());

                //REST_API.PutCall("​/api​/Course​/{id}​/add-student",)
                //Trace.WriteLine(jsonObject);
            });
        }
    }
}
