using Domain.Common;

namespace Domain.Entities;

public class TestResult : AuditableEntity
{
    /// <summary>
    ///     The name of the result.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The minimum score of the result.
    /// </summary>
    public int MinScore { get; set; }

    /// <summary>
    ///     The maximum score of the result.
    /// </summary>
    public int MaxScore { get; set; }
}