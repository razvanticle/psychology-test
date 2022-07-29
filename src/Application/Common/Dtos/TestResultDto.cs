namespace Application.Common.Dtos;

public class TestResultDto
{
    public int Id { get; init; }
    
    public string Result { get; init; }

    public string Description { get; init; }
    
    public string TestName { get; init; }

    public decimal Score { get; init; }
}