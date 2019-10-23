using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDAO
    {
        private Dbcontext db = null;
        public MenuDAO()
        {
            db = new Dbcontext();
        }

        public List<Menu> GetListByID(int typeID)
        {
            var cities = GetCities();
            var districts = GetDistricts();
            
            SaveProfileAddress(cities[0], districts[0]);
            
            
            var cities = GetCities();
            var districts = GetDistricts();
            var hotCity = cities[0];
            var hotDistrict = districts[0];
            
            SaveProfileAddress(hotCity, hotDistrict);
            
            
            
            
            
            
            
            return db.Menus.Where(x => x.MenuTypeID == typeID).ToList();
        }
    }
}
