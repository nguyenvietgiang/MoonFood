
namespace MoonBussiness.CommonBussiness.Dapper
{
    public interface IDataAcess
    {
        Task<IEnumerable<T>> GetData<T, P>(string query, P parameters, string connectionId = "ApiCodeFirst");

        Task SaveData<P>(string query, P parameters, string connectionId = "ApiCodeFirst");
    }
}
