using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PN.Models
{
    public class HomeViewModel
    {
        public List<Forum> Forums { get; set; }
        public Post LastPostRead { get; set; }
    }
}