using CodeCheckerClient.Core;
using CodeCheckerClient.Models;
using CodeCheckerClient.MVVM.Model;
using CodeCheckerClient.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class SignUpPageViewModel
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand AddLecturerCommand { get; set; }

        private string _UserName;
        private string _UserId;
        public string UserName { get { return _UserName; } set { _UserName = value; } }
        public string UserId { get { return _UserId; } set { _UserId = value; } }

        public SignUpPageViewModel()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                UserModel.nullifyUser();
                MainViewModel.Instance().CurrentView = new LoginPageViewModel();
            });
            AddLecturerCommand = new RelayCommand(async o =>
            {
                var lecturerToAdd = new AddLectuerer { name = UserName, teacherID = UserId };
                var createLecturerResponse = await REST_API.PostCallAsync($"Teacher/create", lecturerToAdd).ConfigureAwait(false);

                if (createLecturerResponse.StatusCode == System.Net.HttpStatusCode.Created)
                    MainViewModel.Instance().CurrentView = new CoursePageViewModel();
                else
                {
                    UserName = UserId = "";

                }

            });
        }
    }
}
