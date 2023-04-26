namespace ChatApplication.Application.Users.Commands.Register
{
    public record RegisterUserDto(string username, string email, string password, string confirmPassword);
}
