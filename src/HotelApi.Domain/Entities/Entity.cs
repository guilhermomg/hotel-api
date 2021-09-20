using FluentValidation.Results;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;

namespace HotelApi.Domain.Entities
{
    public abstract class Entity
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId Id { get; set; }
        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }
        [JsonIgnore]
        public string[] ErrorMessages => ValidationResult?.Errors?.Select(a => a.ErrorMessage)?.ToArray();
        public abstract bool IsValid();
    }
}