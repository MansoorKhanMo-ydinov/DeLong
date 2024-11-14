namespace DeLong.Entities.Roles;

public class Role:Auditable
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
