using CodeCheckerClient.Core;
using CodeCheckerClient.Services;

namespace CodeCheckerClient.MVVM.ViewModel
{
    public class StarterPageViewModel : ObservableObject
    {
        public StarterPageViewModel()
        {
            var coursesDetails = REST_API.GetCallAsync("Course/all");
            //var courses = coursesDetails.Result.Content.ReadAsAsync<IEnumerable<Course>>().Result;
        }
    }
}
