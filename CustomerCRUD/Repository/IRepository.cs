using System.Collections.Generic;
using CustomerCRUD.Models;

namespace CustomerCRUD.Repository
{
    public interface IRepository
    {
        Order find(int id);
        List<Order> GetAll();
        Order Add(Order order);
        Order Update(Order order);
        void Remove(int id);
        Order GetFullOrder(int id);
        void Save(Order order);
        void RemoveDetail(int id);
    }
}
