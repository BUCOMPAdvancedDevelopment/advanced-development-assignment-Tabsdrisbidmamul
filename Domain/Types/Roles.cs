using Domain.Extensions;

namespace Domain.Types
{
    public enum Roles: int
    {
        [StringValue("admin")]
        Admin = 0,
        [StringValue("user")]
        User = 1,
        
    }
}