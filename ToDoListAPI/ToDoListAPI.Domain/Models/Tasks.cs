using System;
using System.Collections.Generic;

namespace ToDoListAPI.Domain.Models;

public partial class Tasks
{
    public long Id { get; set; }

    public int State { get; set; }

    public string Description { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string? ModifiedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }
}
