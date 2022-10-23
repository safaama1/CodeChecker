namespace CodeCheckerClient.MVVM.Model
{
    internal class UserModel
    {
        private string _id;
        private CourseModel _course;
        private HomeworkModel _homework;
        private string _year;
        private bool _IsALecturer;
        private string _hwname;

        public string Hwname { get { return _hwname; } set { _hwname = value; } }

        public bool IsALecturer { get { return _IsALecturer; } set { _IsALecturer = value; } }
        public string Id { get { return _id; } set { _id = value; } }

        public CourseModel CurrentlyShownCourse { get { return _course; } set { _course = value; } }
        public HomeworkModel CurrentlyShownHomeWork { get { return _homework; } set { _homework = value; } }


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
