using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public abstract class MongoDocument
    {
        //marcăm pentru MongoDB că parametrul dat este un field Id, ca să poată să-l utilizeze pentru baza sa de date
        [BsonId]
        public Guid Id { get; set; }
        public DateTime LastChangedAt { get; set; }
    }
}
