using System.Collections.Generic;

namespace Upgrade.TraineeTracking.Services.Abstractions.Mappers
{
     public interface IDocumentMapper
     {
          
     }
     public interface IDocumentMapper<TDocument> : IDocumentMapper
          where TDocument : class

     {
          TDto ToDto<TDto>(TDocument entity);

          List<TDto> ToDto<TDto>(List<TDocument> entity);

          TDocument ToDoc<TDto>(TDto dto);

          List<TDocument> ToDoc<TDto>(List<TDto> dto);
     }
}