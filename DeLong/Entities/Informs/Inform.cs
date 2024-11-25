namespace DeLong.Entities.Informs;

public class Inform:Auditable
{
    public string TovarNomi { get; set; }
    public decimal KirishSummasi { get; set; }
    public decimal SotilishNarxi { get; set; }
    public int Soni { get; set; }
    public int Prosent { get; set; }
}
