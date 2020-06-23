using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace demo
{
    public class CollectionModel
    {
        [BsonId]
        public ObjectId topic_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int favor { get; set; }
        public Author author { get; set; }
        [BsonRepresentation(BsonType.String)]
        public TagEnumeration tag { get; set; }
        [BsonSerializer(typeof(MyDateTimeSerializer))]
        public DateTime post_time { get; set; }
        [BsonIgnore]
        public string ignore_string { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public Dictionary<string, int> extra_info { get; set; }
    }
}
