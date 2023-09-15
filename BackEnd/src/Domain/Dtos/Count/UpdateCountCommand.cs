namespace Domain.Dtos.Count
{
    public class UpdateCountCommand : CommandBase<BaseResponse<CountDto>>
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
