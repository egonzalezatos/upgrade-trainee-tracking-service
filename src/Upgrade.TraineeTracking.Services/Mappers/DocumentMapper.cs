using System.Collections.Generic;
using AutoMapper;
using Upgrade.TraineeTracking.Services.Abstractions.Mappers;

namespace Upgrade.TraineeTracking.Services.Mappers
{
    public class DocumentMapper<TDocument> : IDocumentMapper<TDocument> where TDocument : class
    {
        protected readonly IMapper _mapper;

        public DocumentMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual TDto ToDto<TDto>(TDocument doc)
        {
            return _mapper.Map<TDocument, TDto>(doc);
        }

        public List<TDto> ToDto<TDto>(List<TDocument> doc)
        {
            return _mapper.Map<List<TDocument>, List<TDto>>(doc);
        }

        public virtual TDocument ToDoc<TDto>(TDto dto)
        {
            return _mapper.Map<TDto, TDocument>(dto);
        }

        public List<TDocument> ToDoc<TDto>(List<TDto> dto)
        {
            return _mapper.Map<List<TDto>, List<TDocument>>(dto);
        }
    }
}