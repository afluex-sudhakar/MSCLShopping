using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class Address
    {
        public string HouseNo { get; set; }
        public string CustomerId { get; set; }
        public string Locality { get; set; }
        public string LandMark { get; set; }
        public string PinCode { get; set; }

        public string AddressType { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Name { get; set; }
        public string ContatNo { get; set; }
        public string IsDefault { get; set; }

        public DataSet InsertAddress()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_CustomerID", CustomerId),
                                      new SqlParameter("@HouseNo", HouseNo),
                                       new SqlParameter("@Locality", Locality),
                                       new SqlParameter("@Landmark", LandMark),
                                       new SqlParameter("@Pincode", PinCode),
                                       new SqlParameter("@AddedBy", CustomerId),
                                        new SqlParameter("@AddressType", AddressType),
                                         new SqlParameter("@Name", Name),
                                          new SqlParameter("@ContatNo", ContatNo),
                new SqlParameter("@IsDefault", IsDefault)

                                    };
            DataSet ds = Connection.ExecuteQuery("SaveCustomerAddress", para);
            return ds;
        }
    }

    public class GetAddress
    {
        public string Status { get; set; }

        public List<adressdata> lstaddress { get; set; }
        public string CustomerId { get; set; }

        public DataSet GetAddressData()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_CustomerID", CustomerId),


                                    };
            DataSet ds = Connection.ExecuteQuery("GetCustomerAddress", para);
            return ds;
        }
    }

    public class adressdata
    {

        public string Title { get; set; }

        public List<AddressDetails> Address { get; set; }



    }

    public class AddressDetails
    {
        public string HouseNo { get; set; }
        public string Pk_AddressId { get; set; }
        public string Locality { get; set; }
        public string LandMark { get; set; }
        public string PinCode { get; set; }

        public string AddressType { get; set; }
        public string IsDefault { get; set; }
        public string Name { get; set; }
        public string ContatNo { get; set; }
    }

    public class Stock
    {
        public string Quantity { get; set; }
        public string ProductInfoCode { get; set; }
        public string FK_VendorID { get; set; }

        public string Status { get; set; }

        public string ErrorMessage { get; set; }

        public DataSet CheckStock()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Quantity", Quantity),
                                         new SqlParameter("@ProductInfoCode", ProductInfoCode),
                new SqlParameter("@FK_VendorID", FK_VendorID),

                                    };
            DataSet ds = Connection.ExecuteQuery("CheckStock", para);
            return ds;
        }
    }
}