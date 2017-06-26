using MongoDB.Driver;
using System.Threading.Tasks;

namespace M101NDotNetHomeworks.DataAccess
{
    public class Connections
    {
        public MongoClient _client;
        public IMongoDatabase db;

        public Connections(byte cnxSetup)
        {
            switch (cnxSetup)
            {
                case 1:
                    MainMongoDBConnection(cnxSetup);
                    break;
                default:
                    MainMongoDBConnection(0);
                    break;
            }
        }

        public async Task MainMongoDBConnection(byte cnxSetup)
        {
            var connectionString = "mongodb://localhost:27017";
            _client = new MongoClient(connectionString);
            switch (cnxSetup) {
                case 0:
                    db = _client.GetDatabase("test");
                    break;
                case 1:
                    db = _client.GetDatabase("school");
                    break;
            }
        }

    }
}
