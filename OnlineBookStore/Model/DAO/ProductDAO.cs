using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductDAO
    {
        private Dbcontext db = null;
        public ProductDAO()
        {
            db = new Dbcontext();
        }

        public void IncreaseView(long id) {
            var model = db.Products.Find(id);
            model.ViewCount += 1;
            db.SaveChanges();
        }

        public IEnumerable<ProductCategory> GetListAll()
        {
            return db.ProductCategories;
        }

        public IEnumerable<ProductViewModel> GetListProductViewModel(string searchString)
        {
            var model = from a in db.Products
                        join b in db.ProductCategories
                        on a.CategoryID equals b.CategoryID
                        select new ProductViewModel()
                        {
                            ProductID = a.ProductID,
                            CategoryID = a.CategoryID,
                            CategoryName = b.CategoryName,
                            ProductName = a.ProductName,
                            ProductCode = a.ProductCode,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Image = a.Image,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            Quanlity = a.Quanlity,
                            Detail = a.Detail,
                            Status = a.Status,
                            CreatedDate = a.CreatedDate,
                         //   View = a.View,
                            Author = a.Author,
                            Publisher = a.Publisher
                        };
            long number;
            if (searchString != null) {
                bool res = Int64.TryParse(searchString,out number);
                if (res) {
                    model = model.Where(x => x.ProductID == number);
                }
                else
                {
                    model = model.Where(x => x.ProductName.Contains(searchString));
                }
            }
            return model.OrderBy(x => x.ProductID).ToList();
        }

        public List<Product> GetListOrderByCreatedDate(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Where(x=>x.Status == true).Take(top).ToList();
        }

        public List<Product> GetListOrderByPrice(int top)
        {
            return db.Products.OrderBy(x => x.Price).Where(x => x.Status == true).Take(top).ToList();
        }

        public List<Product> GetListOrderByView(int top) {
            return db.Products.OrderByDescending(x => x.ViewCount).Where(x => x.Status == true).Take(top).ToList();
        }

        public bool Create(Product model)
        {
            db.Products.Add(model);
            db.SaveChanges();
            return true;
        }

        public Product GetByID(long id)
        {
            return db.Products.Find(id);
        }

        public List<Product> GetSuggestionList(long cateID, int top,long productID) {
            return db.Products.Where(x => x.CategoryID == cateID && x.ProductID != productID).OrderBy(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> GetListByCategory(long cateID,int top)
        {
            return db.Products.Where(x => x.CategoryID == cateID).OrderBy(x=>x.CreatedDate).Take(top).ToList();
        }

        public bool Edit(Product product,bool isImage)
        {
            try
            {
                var newProduct = db.Products.Find(product.ProductID);
                newProduct.CategoryID = product.CategoryID;
                newProduct.ProductName = product.ProductName;
                newProduct.ProductCode = product.ProductCode;
                newProduct.MetaTitle = product.MetaTitle;
                newProduct.Description = product.Description;
                if(isImage == true)
                {
                    newProduct.Image = product.Image;
                }
                newProduct.Author = product.Author;
                newProduct.Publisher = product.Publisher;
                newProduct.Price = product.Price;
                newProduct.PromotionPrice = product.PromotionPrice;
                newProduct.Quanlity = product.Quanlity;
                newProduct.Detail = product.Detail;
                newProduct.Status = product.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        // list by books hot
        public List<Product> GetHotList(int top)
        {
            var query =
               (from p in db.Products
                let totalQuantity = (from op in db.OrderDetails
                                     where op.ProductID == p.ProductID
                                     select op.Quanlity).Sum()
                where totalQuantity > 0
                orderby totalQuantity descending
                select p).Take(top).ToList();
            return query;
        }
        //return all products
        public List<Product> listProducts(ref int totalRecord, int page = 1, int pageSize = 4)
        {
            totalRecord = db.Products.Count();
            return db.Products.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        // get name to seach books
        public List<Product> listByName(string name, ref int totalRecord, int page = 1, int pageSize = 4)
        {
            totalRecord = db.Products.Where(x => x.ProductName.Contains(name)).Count();
            return db.Products.Where(x => x.ProductName.Contains(name)).OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
