namespace Services.CommandHandlers
{
    using AutoMapper;
    using Data;
    using Domain.Dtos;
    using Domain.Dtos.Count;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;

    public class CountCommandHandlers
    {
        public class AddCountCommandHandler : IRequestHandler<AddCountCommand, BaseResponse<CountDto>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public AddCountCommandHandler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BaseResponse<CountDto>> Handle(AddCountCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _mapper.Map<CountEntity>(command);
                    await _context.Counts.AddAsync(entity, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<CountDto>("Added successfully!", _mapper.Map<CountDto>(entity));
                }
                catch (Exception ex)
                {
                    return new BaseResponse<CountDto>(ex.Message + " " + ex.StackTrace, new CountDto(), null);
                }
            }
        }

        public class UpdateCountCommandHandler : IRequestHandler<UpdateCountCommand, BaseResponse<CountDto>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public UpdateCountCommandHandler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<BaseResponse<CountDto>> Handle(UpdateCountCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var count = await _context.Counts.FirstAsync(Count => Count.Id == command.Id, cancellationToken);
                    _mapper.Map(command, count);
                    _context.Counts.Update(count);
                    await _context.SaveChangesAsync(cancellationToken);
                    var response = _mapper.Map<CountDto>(count);
                    return new BaseResponse<CountDto>("Updated successfully!", response);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<CountDto>(ex.Message + " " + ex.StackTrace, new CountDto(), null);
                }
            }
        }

    }
}
