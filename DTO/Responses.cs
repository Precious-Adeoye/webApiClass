namespace webApiClass.DTO
{
    public class Responses
    {
        public record class GeneralResponse(bool flag, string message); 

        public record class LogInResponse(bool flag,string token, string message);
    }
}
