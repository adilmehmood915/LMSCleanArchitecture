using Microsoft.AspNetCore.Identity;

public interface IJwtTokenService
{
    string GenerateToken(IdentityUser user, IList<string> roles, int? studentId, int? instructorId);
}