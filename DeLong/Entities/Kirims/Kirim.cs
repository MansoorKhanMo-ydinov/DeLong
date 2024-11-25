using DeLong.Entities.Informs;
using DeLong.Entities.Roles;

namespace DeLong.Entities.Kirims;

public class Kirim:Auditable
{
    public string OmborNomi { get; set; }
    public string Yetkazuvchi { get; set; }
    public decimal JamiNarxi { get; set; }
    public decimal JamiSoni { get; set; }
    public List<Inform> Informs { get; set; }
    public List<Role> Roles { get; set; }
}
