#nullable enable
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.Infrastructure.Designs
{
    public abstract class ModelDesign
    {
        public abstract void Configure();
    }
    public abstract class ModelDesign<TDocument> : ModelDesign where TDocument : Identifiable
    {
        public override void Configure()
        {
            BsonClassMap.RegisterClassMap<TDocument>(builder =>
            {
                builder.AutoMap();
//                builder.MapIdMember(doc => doc.Id);
//                builder.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                Design(builder);
            });
        }

        /// <summary>
        /// Add configurations for model the entity.
        /// </summary>
        /// <param name="builder"></param>
        public abstract void Design(BsonClassMap<TDocument> builder);
    }
}