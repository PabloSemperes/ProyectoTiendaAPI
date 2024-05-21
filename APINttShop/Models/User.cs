using System;
using System.Collections.Generic;

namespace APINttShop.Models;

public partial class User
{
    public int PkUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname1 { get; set; } = null!;

    public string? Surname2 { get; set; }

    public string? Adress { get; set; }

    public string? Province { get; set; }

    public string? Town { get; set; }

    public string? PostalCode { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string? Language { get; set; }

    public int? Rate { get; set; }
}
