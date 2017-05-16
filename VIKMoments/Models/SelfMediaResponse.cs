using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIKMoments.Models
{
    public class SelfMediaResponse
    {
        public ObservableCollection<UserMedia> data { get; set; }
    }
}
