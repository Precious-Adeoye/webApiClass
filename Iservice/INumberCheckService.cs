using webApiClass.model;

namespace webApiClass.Iservice
{
    public interface INumberCheckService
    {
        Task<Root> GetCountryNumber(string number);
    }
}
