#nullable enable
using MongoDB.Bson.Serialization;
using Sdk.Domain.Models;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.NonRelational.Designs
{
    public abstract class ModelDesign
    {
        public abstract void Configure();
    }
    public abstract class ModelDesign<TDocument> : ModelDesign where TDocument : Entity
    {
        public override void Configure()
        {
            BsonClassMap.RegisterClassMap<TDocument>(builder =>
            {
                builder.AutoMap();
                builder.SetIsRootClass(true);
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