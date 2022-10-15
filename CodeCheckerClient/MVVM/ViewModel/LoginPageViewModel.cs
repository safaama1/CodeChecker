using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCheckerClient.Core;
using CodeCheckerClient.MVVM.ViewModel;
using CodeCheckerClient.MVVM.Model;
namespace CodeCheckerClient.MVVM.ViewModel
{
    internal class LoginPageViewModel : ObservableObject
    {
        private string _userName;
        public RelayCommand LogInCommand { get; set; }

        public string UserName
        {
            get { return this._userName; }
            set
            {   
                this._userName = value;
                OnPropertyChanged();
            }
        }
        public LoginPageViewModel()
        {

            LogInCommand = new RelayCommand(o =>
            {
            if (CheckLogin(UserName) == true)
            {
                    UserModel.Instance.Id = UserName;
                    
                    MainViewModel.Instance().CurrentView = new MainPageViewModel();
            }

            });
        }
        
        private bool CheckIfUserIdExsists(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;


            if (name.Equals("sss")) return true;
            if (name.Equals("aaa")) return true;
            return false;
        }

        private bool CheckLogin(string name)
        {
            if (string.IsNullOrEmpty(name) || CheckIfUserIdExsists(name) == false)
                return false;

            return true;
        }
    }

}
