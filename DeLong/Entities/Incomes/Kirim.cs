using DeLong.Entities.Informs;
using DeLong.Entities.Roles;

namespace DeLong.Entities.Incomes;

public class Kirim : Auditable
{
    public string Ombornomi{ get; set; }
    public string Yetkazuvchi { get; set; }
    public int JamiSoni { get; set; }
    public int Jaminarxi { get; set; }
    public Inform Inform { get; set; }
    public Role Role { get; set; }
    
}
