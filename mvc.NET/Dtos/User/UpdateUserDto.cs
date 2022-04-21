namespace mvc.NET.Dtos.User
{

    public record UpdateUserDto
    {
        public string UserName { get; init; }
        public string FullName { get; init; }
        public int Age { get; init; }
    }
}