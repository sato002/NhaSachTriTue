using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.ViewModel;
using System.Data.Entity;

namespace Model.DAO
{
    public class OrderDAO
    {
        private Dbcontext db;
        public OrderDAO() {
            db = new Dbcontext();
        }

        public bool DeleteOrderDetail(long id) {
            try
            {
                var res = (IList<OrderDetail>)db.OrderDetails.Where(x => x.OrderID == id).ToList();
                foreach (var item in res)
                {
                    res.Remove(item);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<Order> GetListWithSearchString(string searchString,int page, int pageS) {
            IQueryable<Order> model = db.Orders.OrderBy(x => x.OrderID);
            long number;
            DateTime date;
            if (searchString != null)
            {
                bool isconvertToLong = Int64.TryParse(searchString, out number);
                bool isconvertToDatetime = DateTime.TryParse(searchString, out date);
                if (isconvertToLong)
                {
                    model = model.Where(x => x.OrderID == number);
                }
                else if (isconvertToDatetime)
                {
                    model = model.Where(x=> DbFunctions.TruncateTime(x.CreatedDate) == date );
                }
                else {
                    model = model.Where(x => x.shipName.Contains(searchString));
                }
            }

            return model.ToPagedList(page, pageS);
        }
        public long Create(Order order)
        {
            try
            {
                order.Status = false;
                order.CreatedDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return order.OrderID;
            }
            catch
            {
                return -1;
            }

        }

        public long Delete(long id) {
            try
            {
                var model = db.Orders.Find(id);
                db.Orders.Remove(model);
                db.SaveChanges();
                return id;
            }
            catch
            {
                return -1;
            }
        }

        public bool ChangeStatus(long id) {
            var model = db.Orders.Find(id);
            model.Status = !model.Status;
            db.SaveChanges();
            return model.Status;
        }

        public OrderViewModel ViewDetail(long id) {
            var model = new OrderViewModel();
            model.order = db.Orders.Find(id);
            model.details = new OrderDetailDAO().GetList(id);
            return model;
        }
    }
}
