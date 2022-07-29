namespace Application.Common.Dtos;

public class TestResultDto
{
    public int Id { get; set; }
    
    public string Result { get; set; }

    public string Description { get; set; }

    public decimal Score { get; set; }
}