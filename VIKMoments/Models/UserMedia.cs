using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIKMoments.Models
{
    public class UserMedia
    {
        public Videos videos { get; set; }
        public Comments comments { get; set; }
        public Caption caption { get; set; }
        public Likes likes { get; set; }
        public string link { get; set; }
        public User user { get; set; }
        public string created_time { get; set; }
        public Images images { get; set; }
        public string type { get; set; }
        //public ObservableCollection<string> users_in_photo { get; set; }
        public string filter { get; set; }
        public ObservableCollection<string> tags { get; set; }
        public string id { get; set; }
        public Location location { get; set; }

    }
}
