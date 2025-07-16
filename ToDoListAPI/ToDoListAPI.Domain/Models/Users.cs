using System;
using System.Collections.Generic;

namespace ToDoListAPI.Domain.Models;

public partial class Users
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }
}
