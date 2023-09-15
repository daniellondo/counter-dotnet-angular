namespace Services.QueryHandlers
{
    using AutoMapper;
    using Data;
    using Domain.Dtos;
    using Domain.Dtos.Count;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CountQueryHandlers
    {
        public class GetCountQueryHandler : IRequestHandler<GetCountQuery, BaseResponse<CountDto>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;

            public GetCountQueryHandler(IContext databaseContext, IMapper mapper)
            {
                _context = databaseContext;
                _mapper = mapper;
            }

            public async Task<BaseResponse<CountDto>> Handle(GetCountQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var countDB = new CountDto();
                    if (!_context.Counts.Count().Equals(0))
                    {
                        _mapper.Map(await _context.Counts.FirstAsync(cancellationToken), countDB);
                    }
                    return new BaseResponse<CountDto>("", countDB);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<CountDto>("Error getting data", null, ex);
                }

            }
        }
    }
}
