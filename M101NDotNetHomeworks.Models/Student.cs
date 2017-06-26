using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace M101NDotNetHomeworks.Models
{
    public class Student
    {
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("scores")]
        public List<Grade> Grades { get; set; }
    }
}
