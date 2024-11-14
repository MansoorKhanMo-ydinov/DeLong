namespace DeLong.Entities.Informs;

public class Inform:Auditable
{
    public string TovarNomi { get; set; }
    public int Soni { get; set; }
    public decimal SotibOlishNarxi { get; set; }
    public decimal KirimSummasi { get; set; }
    public int Foizi { get; set; }
    public decimal SotishNarxi { get; set; }
    public decimal SotishSummasi { get; set; }
}
