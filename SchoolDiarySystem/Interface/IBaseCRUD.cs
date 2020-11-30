using System.Collections.Generic;

namespace SchoolDiarySystem.Interface
{
    public interface IBaseCRUD<T>
    {
        bool Create(T model);
        bool Update(T model);
        bool Delete(int id);
        //bool Search()
        List<T> GetAll();
        T Get(int id);
    }
}
