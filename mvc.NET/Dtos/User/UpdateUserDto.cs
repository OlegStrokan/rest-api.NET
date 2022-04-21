namespace mvc.NET.Dtos.User
{

    public record UpdateUserDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}