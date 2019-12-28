using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

using Dapper;
using CustomerCRUD.Repository;
using CustomerCRUD.Models;

namespace CustomerCRUD.Dapper
{
    public class OrderRepository : IRepository
    {

        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindDB"].ConnectionString);

        public Order find(int id)
        {
            string query = "Select id,  nroinvoice, companyid, customer, ammount, nroproducts, datecreate from Orders where Id = @Id";
            return this.db.Query<Order>(query, new { Id = id }).SingleOrDefault();
        }

        public Order Add(Order order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", value: order.OrderID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@nroinvoice", value: order.ShipCity);
            parameters.Add("@company", value: order.ShipCountry);
            parameters.Add("@customer", value: order.ShipName);

            var id = this.db.Execute("InsertOrder", parameters, commandType: CommandType.StoredProcedure);
            order.OrderID = parameters.Get<int>("@id");
            return order;
        }


        public List<Order> GetAll()
        {

            string query = "Select id,  nroinvoice, company, customer, datecreate from dbo.Orders; " +
                           "Select id, idInvoice, productname, quantity, unitprice, subtotal from dbo.OrderDetails; ";

            using (var multipleResults = this.db.QueryMultiple(query))
            {
                var orders = multipleResults.Read<Order>().ToList();
                var orderdetails = multipleResults.Read<OrderDetail>().ToList();

                foreach (var order in orders)
                {
                    order.OrderDetails.AddRange(orderdetails.Where(x => x.OrderID == order.OrderID).ToList());
                }

                return orders;

            }

        }

        public Order GetFullOrder(int id)
        {
            using (var multipleResults = this.db.QueryMultiple("GetFullOrder", new { id }, commandType: CommandType.StoredProcedure))
            {
                var order = multipleResults.Read<Order>().SingleOrDefault();
                var orderdetails = multipleResults.Read<OrderDetail>().ToList();

                if (order != null && orderdetails != null)
                {
                    order.OrderDetails.AddRange(orderdetails);
                }

                return order;

            }

        }

        public void Remove(int id)
        {
            this.db.Execute("DELETE FROM dbo.OrderDetails where OdrderID = @id; DELETE FROM dbo.Orders where id = @id ", new { id });
        }


        public void Save(Order order)
        {
            using (var txScope = new TransactionScope())
            {
                this.Add(order);                

                foreach (var detail in order.OrderDetails)
                {
                    detail.OrderID = order.OrderID;
                    this.Add(detail);
                }

                txScope.Complete();

            }
        }

        public Order Update(Order order)
        {
            var sql = " UPDATE Orders " +
                "SET    nroinvoice = @nroinvoice, " +
                "       companyid = @companyid, " +
                "       customer = @customer " +
                "WHERE id = @id ";

            this.db.Execute(sql, order);

            return order;
        }

        #region Details

        public OrderDetail Add(OrderDetail orderdetail)
        {
            var sql = "insert into dbo.OrderDetails (idInvoice, productname, quantity, unitprice, subtotal) " +
                       "values (@idInvoice, @productname, @quantity, @unitprice, @subtotal); " +
                        "Select cast(scope_identity() as int)";

            var id = this.db.Query<int>(sql, orderdetail).Single();
            orderdetail.OrderID = id;
            return orderdetail;
        }

        public void RemoveDetail(int id)
        {
            this.db.Execute("DELETE FROM OrderDetails where id = @id", new { id });
        }

        #endregion

    }
}
