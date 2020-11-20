using System.Data.SqlClient;

namespace SchoolDiarySystem.Interface
{
    public interface IBaseConvert<T>
    {
        T ToObject(SqlDataReader dataReader);
    }
}
