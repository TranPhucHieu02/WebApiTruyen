using System;
using System.Collections.Generic;

namespace DoAnAndroid.Models;

public partial class Truyen
{
    public string MaTruyen { get; set; } = null!;

    public string TenTruyen { get; set; } = null!;

    public string TheLoai { get; set; } = null!;

    public string TacGia { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime NgayXb { get; set; }

    public string NoiDung { get; set; } = null!;

    public int? LuotDoc { get; set; }

    public int? LuotDanhGia { get; set; }

    public double? SoSao { get; set; }

    public virtual ICollection<TuongTac> TuongTacs { get; } = new List<TuongTac>();
}
