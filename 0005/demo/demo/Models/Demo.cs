using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Models
{
    public class Demo
    {
        public const string DATABASE = "DemoDB";
        public const string COLLECTION = "DemoCollection";

        public ObjectId _id { get; set; }
        public string demo_user_name { get; set; }
        public int demo_user_age { get; set; }
    }
}
