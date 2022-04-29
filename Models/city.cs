using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace createform.Models
{
    public class city
    {
        public int cityid { get; set; }
        public string cityname { get; set; }
        public string statename { get; set; }

        public SelectList cities { get; set; }
    }
    
}