using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public partial class Time
{
    public int TimeId { get; set; }

    public int ClassGroupId { get; set; }

    public byte Slot { get; set; }

    public TimeSpan Time1 { get; set; }

    public virtual ClassGroup ClassGroup { get; set; } = null!;

    [NotMapped]
    //Thuộc tính [NotMapped] yêu cầu Entity Framework bỏ qua thuộc tính này khi ánh xạ tới cơ sở dữ liệu.
    public string Time1String
    {
        get => Time1.ToString();
        set => Time1 = TimeSpan.Parse(value);
    }
}
