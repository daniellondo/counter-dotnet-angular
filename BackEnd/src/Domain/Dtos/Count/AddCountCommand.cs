namespace Domain.Dtos.Count
{
    public class AddCountCommand : CommandBase<BaseResponse<CountDto>>
    {
        public int Count { get; set; }
    }
}
