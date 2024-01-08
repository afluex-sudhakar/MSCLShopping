using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class Complains : Common
    {
        
        public string EncrptNo { get; set; }
        public string FK_UserID { get; set; }
        public string LoginID { get; set; }
        public string Issue { get; set; }
        public string PK_ComplainID { get; set; }
        public string ComplainID { get; set; }
        public string ComplainDate { get; set; }
        public string ComplainStatus1 { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Reply { get; set; }
        public string ReplyPerson { get; set; }
        public string ReplyDate { get; set; }
        public List<Complains> lstComplains { get; set; }
        public List<Complains> lstComplainResponse { get; set; }

        public DataSet InsertComplain()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", FK_UserID),
                                    new SqlParameter("@LoginID", LoginID),
                                    new SqlParameter("@Issue", Issue) };
            DataSet ds = Connection.ExecuteQuery("InsertComplain", para);
            return ds;
        }

        public DataSet GetAllComplains()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", FK_UserID),
                                    new SqlParameter("@Status", ComplainStatus1), };
            DataSet ds = Connection.ExecuteQuery("GetAllComplains", para);
            return ds;
        }

        public DataSet GetAllComplainsAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID),
                                    new SqlParameter("@Status", ComplainStatus1),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate), };
            DataSet ds = Connection.ExecuteQuery("GetAllComplainsAdmin", para);
            return ds;
        }

        public DataSet GetComplainResponseAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@PK_ComplainID", PK_ComplainID), };
            DataSet ds = Connection.ExecuteQuery("GetComplainResponse", para);
            return ds;
        }

        public DataSet ReplyAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@FK_ComplainID", PK_ComplainID),
                                    new SqlParameter("@Reply", Reply),
                                    new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = Connection.ExecuteQuery("ComplainReplyAdmin", para);
            return ds;
        }

        public DataSet ReplyAssociate()
        {
            SqlParameter[] para = { new SqlParameter("@FK_ComplainID", PK_ComplainID),
                                    new SqlParameter("@Reply", Reply),
                                    new SqlParameter("@FK_UserID", FK_UserID), };
            DataSet ds = Connection.ExecuteQuery("ComplainReplyAssociate", para);
            return ds;
        }

    }
}