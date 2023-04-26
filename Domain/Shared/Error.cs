namespace ChatApplication.Domain.Shared
{
    public class Error
    {
       public string Message { get; set; }
       public string Code { get; set; }

        public Error(string code, string message )
        {
            Code = code;
            Message = message;
        }

        public static Error None = null;
    }
}
