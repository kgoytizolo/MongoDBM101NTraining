using MongoDB.Driver;
using MongoDB.Bson;
using M101NDotNetHomeworks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace M101NDotNetHomeworks.DataAccess
{
    public class DataAccessGeneric
    {
        private Connections _cnxMongoDB;

        public DataAccessGeneric(byte db)
        {
            _cnxMongoDB = new Connections(db);
        }

        //************************** MongoDB operations ********************************

        //Homework 3.1 - get List of students
        //Use of PullFilter builder to remove elements - arrays
        public async Task<List<Student>> getListOfStudentsTask31() {
            var collection = _cnxMongoDB.db.GetCollection<Student>("students");

            await collection.Find(new BsonDocument())
              .ForEachAsync(async student =>
              {
                  var lowestHomeworkGrade = student.Grades
                        .Where(x => x.Type == Grade.GradeType.homework)
                        .OrderBy(x => x.Score)
                        .First();

                  // Option 1: Remove it server-side
                  await collection.UpdateOneAsync(
                    filter: x => x.Id == student.Id,
                    update: Builders<Student>.Update.PullFilter(
                      field: x => x.Grades,
                      filter: score => score.Score == lowestHomeworkGrade.Score && score.Type == Grade.GradeType.homework));

                  // Options 2: Remove it client-side and replace only the scores
                  //student.Grades.Remove(lowestHomeworkGrade);
                  //await collection.UpdateOneAsync(
                  //  filter: x => x.Id == student.Id,
                  //  update: Builders<Student>.Update.Set(x => x.Grades, student.Grades));

                  // Option 3: Remove it client-side and replace the whole student
                  //student.Grades.Remove(lowestHomeworkGrade);
                  //await collection.ReplaceOneAsync(
                  //  filter: x => x.Id == student.Id,
                  //  replacement: student);

              });

            // We haven't gotten to this part in the class yet, but it's the
            // translation of the aggregation query from the instructions into .NET.
            var result = await collection.Aggregate()
              .Unwind(x => x.Grades)
              .Group(new BsonDocument
              {{ "_id", "$_id" },{ "average", new BsonDocument("$avg", "$scores.score") }
              })
              .Sort(new BsonDocument("average", -1))
              .FirstAsync();

            //Return all elements of a collection with Find(_ => true)
            return await collection.Find(_ => true).ToListAsync<Student>();
        }

    }
}
