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
    
    class ReportGeneration
    {
        public void GenerateReport(string reportType)
        {
            if (reportType == "PDF")
            {
                // 500 line code here.
            }
            if (reportType == "Excel")
            {
                // 200 line code here
            }
            if (reportType == "Word")
            {
                // 700 line code here
            }
         }
     }
    
    interface IReportHelper
    {
        void GenerateReport();
    }
    
    class PDFReportHelper : IReportHelper
    {
        public void GenerateReport()
        {
            // Logic generate PDF report
        }
    }
    class ExcelReportHelper : IReportHelper
    {
        public void GenerateReport()
        {
            // Logic generate Excel report
        }
    }
    
    class ReportManager
    {
        private readonly IReportHelper ReportHelper;

        public ReportManager(IReportHelper reportHelper)
        {
            ReportHelper = reportHelper;
        }

        public void ExportReport()
        {
            ReportHelper.GenerateReport();
        }
    }
    
    interface IManager 
    {
        void Approve();
    }
    
    class Employee
    {
        void Working() 
        {
            // Working logic code
        }
    }
    
    class Developer : Employee
    {
        
    }
    
    class Manager : Employee, IManager
    {
        // need implement method Approve()
    }
    
    class MySqlProvider
    {
        public IList ExecuteQuery();
        public int ExecuteNonQuery();
        public object ExecuteScalar();
    }
    
    class DLCustomer
    {
        private MySqlProvider _mysqlProvider = new MySqlProvider();
    }
    
    interface IDBProvider
    {
        IList ExecuteQuery();
        int ExecuteNonQuery();
        object ExecuteScalar();
    }
    
    class MySqlProvider : IDBProvider
    {
        public IList ExecuteQuery();
        public int ExecuteNonQuery();
        public object ExecuteScalar();
    }
    
    class NoSqlProvider : IDBProvider
    {
        public IList ExecuteQuery();
        public int ExecuteNonQuery();
        public object ExecuteScalar();
    }
    
    class DLCustomer
    {
        private IDBProvider _dbProvider;
        public DLCustomer(IDBProvider dbProvider) 
        {
            _dbProvider = dbProvider;
        }
    }
}
