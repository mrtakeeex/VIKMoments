﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIKMoments.Models
{
    public class Images
    {
        public Low_resolution low_resolution { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Standard_resolution standard_resolution { get; set; }
    }
}
