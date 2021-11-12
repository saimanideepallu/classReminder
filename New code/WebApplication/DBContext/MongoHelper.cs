using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using WebApplication.Models;

namespace WebApplication.DBContext
{
    public class MongoHelper
    {   
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoConnection = "mongodb://sa:sa1234@events-shard-00-00.57uv1.mongodb.net:27017,events-shard-00-01.57uv1.mongodb.net:27017,events-shard-00-02.57uv1.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-3h7rcc-shard-0&authSource=admin&retryWrites=true&w=majority";
        public static string MongoDatabase = "Events_DB";

        public static IMongoCollection<UserModel> User { get; set; }
        public static IMongoCollection<EventsModel> Events { get; set; }
        //public static IMongoCollection<UserModel> user { get; set; }

        internal static void ConnectToMongoService()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex.InnerException);
            }
        }
    }
}