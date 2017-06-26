using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace M101NDotNetHomeworks.Models
{
    public class Grade
    {
        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public GradeType Type { get; set; }

        [BsonElement("score")]
        public double Score { get; set; }

        public enum GradeType
        {
            // I don't like needing to use lowercase here, but we don't have a built-in solution
            // for changing the case of enum values.
            homework,
            exam,
            quiz
        }

    }
}
