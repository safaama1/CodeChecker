using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCheckerClient.MVVM.Model
{
    internal class UserModel
    {
        private string _id;
        private string _course;
        private string _year;
        public string Id { get { return _id; } set { _id = value; } }

        public string CurrentlyShownCourse { get { return _course; } set { _course = value; } }

        public string CurrentlyShownYear { get { return _year; } set { _year = value; } }

        private UserModel() { }
        private static UserModel instance = null;
        public static UserModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserModel();
                }
                return instance;
            }
        }
    }


}
