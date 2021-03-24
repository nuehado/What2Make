using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.DbAccess
{
    public interface IDbAccess
    {
        Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        Task<int> SaveData<U>(string storedProcedure, U parameters, string connectionStringName);
    }
}