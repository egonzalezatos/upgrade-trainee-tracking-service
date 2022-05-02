using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Sdk.Domain.Models;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.NonRelational.Designs
{
    public sealed class Designer
    {
        public static void RunDesigns<TKey>()
        {
            BsonClassMap.RegisterClassMap<Entity<TKey>>(o =>
            {
                o.SetIsRootClass(true);
                o.MapIdMember(doc => doc.Id);
                o.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                o.AutoMap();
            });
            var designs = typeof(Designer).Assembly.GetTypes()
                .Where(types => !types.IsInterface && !types.IsAbstract && types.IsAssignableTo(typeof(ModelDesign)));
            
            foreach (var type in designs)
            {
                var instance = Activator.CreateInstance(type);
                instance!.GetType().GetMethod(nameof(ModelDesign.Configure))!.Invoke(instance, null);
            }
        }
    }
} 