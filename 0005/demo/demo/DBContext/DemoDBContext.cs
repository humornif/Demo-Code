using demo.Models;
using Lib.Core.Mongodb.Helper;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.DBContext
{
    public class DemoDBContext : MongoHelper<Demo>
    {
        protected override async Task CreateIndexAsync()
        {
        }
        public DemoDBContext() : base(Demo.DATABASE, Demo.COLLECTION)
        {

        }

        internal async Task<bool> saveData(DemoDto data)
        {
            Demo new_item = new Demo()
            {
                _id = ObjectId.GenerateNewId(),
                demo_user_name = data.demo_user_name,
                demo_user_age = data.demo_user_age,
            };

            bool result = await CreateAsync(new_item);
            return result;
        }

        internal async Task<IEnumerable<DemoDto>> getAllData()
        {
            var fetch_data = await SelectAllAsync();

            List<DemoDto> result_list = new List<DemoDto>();
            fetch_data.ForEach(p =>
            {
                DemoDto new_item = new DemoDto()
                {
                    demo_user_name = p.demo_user_name,
                    demo_user_age = p.demo_user_age,
                };
                result_list.Add(new_item);
            });
            return result_list;
        }
    }
}
