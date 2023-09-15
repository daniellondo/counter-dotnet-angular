namespace Services.MapperConfiguration
{
    using AutoMapper;
    using Domain.Dtos.Count;
    using Domain.Entities;

    public class CountProfile : Profile
    {
        public CountProfile()
        {
            CreateMap<CountEntity, CountDto>(MemberList.Source);
            CreateMap<AddCountCommand, CountEntity>(MemberList.Source)
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateCountCommand, CountEntity>(MemberList.Source)
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
