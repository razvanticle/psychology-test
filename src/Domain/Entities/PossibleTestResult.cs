using Domain.Common;

namespace Domain.Entities;

public class PossibleTestResult : AuditableEntity
{
    /// <summary>
    ///     The name of the result.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The description of the test result.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     The minimum score of the result.
    /// </summary>
    public decimal MinScore { get; set; }

    /// <summary>
    ///     The maximum score of the result.
    /// </summary>
    public decimal MaxScore { get; set; }

    public TestTemplate TestTemplate { get; set; }

    public int TestTemplateId { get; set; }
}