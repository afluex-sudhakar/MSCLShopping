using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class QuickView
    {


        public string DeliveryCharge { get; set; }
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string MRP { get; set; }
        public string OfferedPrice { get; set; }
        public string ProductID { get; set; }
        public string ProductInfoCode { get; set; }
        public string ProductName { get; set; }

        public string ShortDescription { get; set; }

        public string Status { get; set; }

        public List<Varients> Varients { get; set; }


        public List<Imagesdata> lstimages { get; set; }
        public string SizeID { get; set; }
        public string ColorID { get; set; }
        public string FlavorID { get; set; }
        public string RamID { get; set; }
        public string StorageID { get; set; }
        public string MaterialID { get; set; }
        public string LastSelected { get; set; }
        public string VendorName { get; set; }
        public string Fk_vendorId { get; set; }
        public string ShareURL { get; set; }
        public string IsAvailable { get; set; }
        public string RedeemPrice { get; set; }
        public string IsCart { get; set; }

        public string FK_UserId { get; set; }

        public DataSet GetQuickView()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_ProductID", ProductID),
                                      new SqlParameter("@Fk_ColorId", ColorID),
                                       new SqlParameter("@Fk_SizeId", SizeID),
                                       new SqlParameter("@FK_RamId", RamID),
                                       new SqlParameter("@FK_StorageId", StorageID),
                                       new SqlParameter("@FK_FlavourId", FlavorID),
                                       new SqlParameter("@Fk_MaterialId", MaterialID),
                                       new SqlParameter("@FK_UserId", FK_UserId),
                                       new SqlParameter("@LastSelected", LastSelected),
                                    };
            DataSet ds = Connection.ExecuteQuery("QuickViewNew", para);
            return ds;
        }
        public DataSet GetVendorForProduct()
        {
            SqlParameter[] para = { new SqlParameter("@FK_ProductID", ProductID),
                new SqlParameter("@Fk_RamId", RamID),
                new SqlParameter("@Fk_StorageId", StorageID),
                new SqlParameter("@Fk_ColorId", ColorID),
                new SqlParameter("@Fk_SizeId", SizeID),
                new SqlParameter("@FK_FlavourId", FlavorID),
            };
            DataSet ds = Connection.ExecuteQuery("GetVendorForProduct", para);
            return ds;
        }

    }


    public class sizedata
    {

        public string Title { get; set; }

        public List<SizeDetails> SizeDetails { get; set; }



    }
    public class SizeDetails
    {
        public string SizeID { get; set; }
        public string SizeName { get; set; }
        public string Status { get; set; }
    }

    public class Varients
    {
        public string Title { get; set; }
        public List<SizeDetails> lstsize { get; set; }

        public List<ColorDetails> lstcolor { get; set; }

        public List<FlavourDetails> lstflavour { get; set; }

        public List<RamDetails> lstram { get; set; }

        public List<StorageDetails> lststorage { get; set; }

        public List<ImagesDetails> lstimages { get; set; }

        public List<MaterialDetails> lstmaterial { get; set; }
    }

    public class Colordata
    {

        public string Title { get; set; }

        public List<ColorDetails> ColorDetails { get; set; }



    }
    public class ColorDetails
    {
        public string ColorID { get; set; }
        public string ColorCode { get; set; }
        public string ColorName { get; set; }
        public string Status { get; set; }
    }
    public class Flavourdata
    {
        public string Title { get; set; }

        public List<FlavourDetails> FlavourDetails { get; set; }

    }
    public class FlavourDetails
    {
        public string FlavorID { get; set; }
        public string FlavorName { get; set; }

        public string Status { get; set; }
    }

    public class Ramdata
    {
        public string Title { get; set; }

        public List<RamDetails> RamDetails { get; set; }

    }
    public class RamDetails
    {
        public string RamID { get; set; }
        public string RamName { get; set; }

        public string Status { get; set; }
    }


    public class Storagedata
    {
        public string Title { get; set; }

        public List<StorageDetails> StorageDetails { get; set; }

    }
    public class StorageDetails
    {
        public string StorageID { get; set; }
        public string StorageName { get; set; }

        public string Status { get; set; }
    }

    public class Materialdata
    {
        public string Title { get; set; }

        public List<MaterialDetails> MaterialDetails { get; set; }

    }
    public class MaterialDetails
    {
        public string MaterialID { get; set; }
        public string MaterialName { get; set; }

        public string Status { get; set; }
    }

    public class Imagesdata
    {
        public string Title { get; set; }

        public List<ImagesDetails> ImagesDetails { get; set; }

    }
    public class ImagesDetails
    {
        public string ImagePath { get; set; }

    }
}