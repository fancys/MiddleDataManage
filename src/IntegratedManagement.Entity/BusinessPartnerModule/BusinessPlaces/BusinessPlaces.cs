using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.BusinessPartnerModule.BusinessPlaces
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 17:33:52
	===============================================================================================================================*/
    public class BusinessPlaces
    {
       public int BPLId { get; set; }
public string BPLName { get; set; }
public string TaxIdNum { get; set; }
public string RepName { get; set; } 
public string Industry { get; set; }
public string  Business { get; set; }
        public string  Address { get; set; }
        public string  MainBPL { get; set; }
        public string  TxOffcNo { get; set; }
        public string  Disabled { get; set; }
        public string  DflCust { get; set; }
        public string  DflVendor { get; set; }
        public string  DflWhs { get; set; }
        public string  DfltResWhs { get; set; }
        public string  AliasName { get; set; }
        public string  AddrType { get; set; }
        public string  Street { get; set; }
        public string  StreetNo { get; set; }
        public string  Building { get; set; }
        public string  ZipCode { get; set; }
        public string  Block { get; set; }
        public string  City { get; set; }
        public string  State { get; set; }
        public string  County { get; set; }
        public string  Country { get; set; }
        public string  PmtClrAct { get; set; }
        public string  GlblLocNum { get; set; }
    }
}
