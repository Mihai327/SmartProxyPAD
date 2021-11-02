using Common.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T: MongoDocument
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IMongoDbSettings dbSettings)
        {
            // se creaza legătura cu baza de date
            _db = new MongoClient(dbSettings.ConnectionString).GetDatabase(dbSettings.DatabaseName);
            // extragem denumirea colectiei din cadrul bazei de date
            string tableName = typeof(T).Name.ToLower();
            //se va extrage referinta la colectia data de tip T din tableName
            _collection = _db.GetCollection<T>(tableName);
        }
        public void DeleteRecord(Guid id)
        {
            
        }

        public List<T> GetAllRecords()
        {
            var records = _collection.Find(new BsonDocument()).ToList();

            return records;
        }

        public T GetRecordById(Guid id)
        {
            throw new NotImplementedException();
        }

        public T InsertRecord(T record)
        {
            _collection.InsertOne(record);

            return record;
        }

        public void UpsertRecord(T record)
        {
            throw new NotImplementedException();
        }
    }
}
