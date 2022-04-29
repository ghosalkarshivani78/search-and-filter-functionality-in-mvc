using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace createform.Models
{
    public class userform
    {
        public int id { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string email { get; set; }

        public string address { get; set; }

        public string countryname { get; set; }

        public string stateid { get; set; }

        public string cityid { get; set; }


        public string number { get; set; }

        public SelectList countries { get; set; }
        public SelectList states { get; set; }
        public SelectList cities { get; set; }
    }

    public class ddl {
        public int id { get; set; }
        public string name { get; set; }
    }
}