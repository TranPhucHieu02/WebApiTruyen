using System;
using System.Collections.Generic;

namespace DoAnAndroid.Models;

public partial class DocGia
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string TenDg { get; set; } = null!;

    public string Email { get; set; }

    public string? Sdt { get; set; }

    public bool? GioiTinh { get; set; }

    public virtual ICollection<TuongTac> TuongTacs { get; } = new List<TuongTac>();
}
