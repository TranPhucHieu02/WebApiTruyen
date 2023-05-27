using System;
using System.Collections.Generic;

namespace DoAnAndroid.Models;

public partial class TuongTac
{
    public string MaTruyen { get; set; } = null!;

    public string Username { get; set; } = null!;

    public int? DanhGia { get; set; }

    public bool? YeuThich { get; set; }

    public virtual Truyen MaTruyenNavigation { get; set; } = null!;

    public virtual DocGia UsernameNavigation { get; set; } = null!;
}
