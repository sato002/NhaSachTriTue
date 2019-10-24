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
            return db.Menus.Where(x => x.MenuTypeID == typeID).ToList();
        }
    }
    
    public class SupportCenter
    {
        public void SendMailToCustomers()
        {
            GetCustomers();
            ProcessData();
            SendEmail();
        }
        
        public List<Client> GetCustomers();
        public void ProcessData();
        public void SendEmail();
    }
    
    public class ReportGeneration
    {
        public string ReportType { get; set; }

        public void GenerateReport(Employee em)
        {
            if (ReportType == "CRS")
            {
                 // 300 line code here
            }
            if (ReportType == "PDF")
            {
                // 500 line code here.
            }
            if (ReportType == "Excel")
            {
                // 200 line code here
            }
            if (ReportType == "Word")
            {
                // 700 line code here
            }
         }
     }
}
