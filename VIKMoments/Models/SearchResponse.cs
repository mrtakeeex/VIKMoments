﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIKMoments.Models
{
    public class SearchResponse
    {
        public ObservableCollection<SearchedUser> data { get; set; }
    }
}
