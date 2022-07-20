﻿using Domain.Common;

namespace Domain.Entities;

public class TestAnswer : AuditableEntity
{
    /// <summary>
    ///     The content of the answer.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     The score that will be used in calculating the test result.
    /// </summary>
    public int Score { get; set; }
}