using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.SAPBOneCommon
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/31 16:47:03
	===============================================================================================================================*/
    public class BOneCommon
    {
        /// <summary>
        /// 根据OMSGroupNum获取B1中物料组代码
        /// </summary>
        /// <param name="OMSGroupNum"></param>
        /// <returns></returns>
        public static int GetItemGroupCodeByOMSGroupNum(string OMSGroupNum)
        {
            if (string.IsNullOrEmpty(OMSGroupNum)) throw new ArgumentNullException("组代码为空");
            SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            string sql = $"SELECT ItmsGrpCod FROM OITB  WHERE U_OMSGroupNum = '{OMSGroupNum}'";
            res.DoQuery(sql);
            if (res.RecordCount != 1)
                throw new Exception("根据OMS商品分类无法找到唯一的组代码");
            return res.Fields.Item("ItmsGrpCod").Value;
        }

        public static int GetCustomerGroupCodeByPlateformCode(string PlateformCode)
        {
            SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string sql = $"SELECT A1.U_BPGroupCode FROM [@AVA_PLATFORMCODE] A1 WHERE A1.CODE = '{PlateformCode}'";
            res.DoQuery(sql);
            if (res.RecordCount != 1)
                throw new Exception("根据PlateformCode无法找到唯一的组代码");
            return Convert.ToInt32( res.Fields.Item("U_BPGroupCode").Value);
        }

        /// <summary>
        /// 根据平台code获取业务伙伴类型
        /// </summary>
        /// <returns></returns>
        public static int GetCustomerTypeByPLATFORMCODE(string PlatformCode)
        {
            SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string sql = $"SELECT A1.BPGroupCode FROM @AVA_PLATFORMCODE A1 WHERE A1.CODE = '{PlatformCode}'";
            res.DoQuery(sql);
            if (res.RecordCount != 1)
                throw new Exception("根据OMS商品分类无法找到唯一的组代码");
            return res.Fields.Item("BPGroupCode").Value;
        }


        public static string GetAccCodeByBusinessType(string BusinessType)
        {
            SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string sql = $"SELECT A1.U_AcctCode FROM @AVA_INVNTRNSTYPE A1 WHERE A1.CODE = '{BusinessType}'";
            res.DoQuery(sql);
            if (res.RecordCount != 1)
                throw new Exception("根据业务类型无法找到唯一的科目代码");
            return res.Fields.Item("U_AcctCode").Value;
        }

        /// <summary>
        /// 获取物料的税额
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns>
        /// item1 vatGroupPu
        /// item2 vatGroupSa
        /// </returns>
        public static Tuple<string,string> GetVatGroupbyItemCode(string ItemCode)
        {
            SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string sql = $"SELECT A1.VatGourpSa,A1.VatGroupPu FROM OITM A1 WHERE A1.ItemCode = '{ItemCode}'";
            res.DoQuery(sql);
            if (res.RecordCount != 1)
                throw new Exception("根据物料编码无法找到唯一的vatGroup");
            string vatGroupPu = res.Fields.Item("VatGroupPu").Value;
            string vatGroupSa = res.Fields.Item("VatGourpSa").Value;
            return new Tuple<string, string>(vatGroupPu, vatGroupSa);
        }

        #region 查询单据是否已经生成
        public static bool IsExistDocument(string tableName,int omsDocEntry,out int DocEnty)
        {
            try
            {
                DocEnty = 0;
                SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sql = $"select DocEntry from {tableName} where U_OMSDocEntry = '{omsDocEntry}'";
                res.DoQuery(sql);
                if (res.RecordCount == 1)
                {
                    DocEnty = res.Fields.Item("DocEntry").Value;
                    return true;
                }
                else
                    return false;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region refund
        /// <summary>
        /// 根据退款原因判断退款单是否为特殊单据
        /// </summary>
        /// <param name="RefundReason">退款原因</param>
        /// <returns></returns>
        public static string GetRefundReason(string RefundReason)
        {
            try
            {
                SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sql = $@"select U_SpecialType from [@AVA_REFUNDREASON] where CODE = '{RefundReason}'";
                res.DoQuery(sql);
                if (res.RecordCount != 1)
                    throw new Exception("根据退款原因无法确认退款单是否为特色订单，请检查配置表@AVA_REFUNDREASON");
                if (string.IsNullOrEmpty(res.Fields.Item("U_SpecialType").Value))
                    throw new Exception("根据退款原因找到的退款单类型字段[U_SpecialType]值为空。");
                return res.Fields.Item("U_SpecialType").Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 根据支付方式获取支付科目与描述
        /// </summary>
        /// <param name="payMethod">支付方式</param>
        /// <returns>item1  支付科目</returns>
        /// <returns>item2  描述</returns>
        public static Tuple<string,string> GetAccountByPayMethod(string payMethod)
        {
            try
            {
                SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sql = $@"select U_ACCOUNT ,U_SHORTNAME from [@AVA_PAYMETHOD] where code = '{payMethod}'";
                res.DoQuery(sql);
                if (res.RecordCount != 1)
                    throw new Exception($"根据支付方式[{payMethod}]无法找到支付科目，请检查配置表@AVA_PAYMETHOD");
                string accountCode = res.Fields.Item("U_ACCOUNT").Value;
                string shortName = res.Fields.Item("U_SHORTNAME").Value;
                return new Tuple<string, string>(accountCode, shortName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据退款原因查询科目  
        /// </summary>
        /// <param name="refundReason"></param>
        /// <returns></returns>
        public static string GetAccountByRefundReason(string refundReason)
        {
            try
            {
                SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sql = $@"select U_AcctCode FROM @AVA_REFUNDREASON A1 WHERE A1.CODE = '{refundReason}'";
                res.DoQuery(sql);
                if (res.RecordCount != 1)
                    throw new Exception($"根据退款原因[{refundReason}]无法找到科目，请检查配置表[@AVA_REFUNDREASON]");
                return res.Fields.Item("U_AcctCode").Value;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据物料查找科目
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static string GetAccountByItemCode(string itemCode)
        {
            try
            {
                SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sql = $@"SELECT A2.U_SalesAccount as Account FROM OITM A1 INNER JOIN OITB A2 ON A2.ItmsGrpCod = A1.ItmsGrpCod WHERE A1.ItemCode = '{itemCode}'";
                res.DoQuery(sql);
                if (res.RecordCount != 1)
                    throw new Exception($"根据物料[{itemCode}]无法找到科目");
                return res.Fields.Item("Account").Value;
            }catch(Exception ex)
            {
                throw ex;
            }
        }


        public static string GetShortNameByItemCode(string itemCode)
        {
            try
            {
                SAPbobsCOM.IRecordset res = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sql = $@"SELECT A1.U_Vendor FROM OITM A1 WHERE A1.ItemCode = '{itemCode}'";
                res.DoQuery(sql);
                if (res.RecordCount != 1)
                    throw new Exception($"根据物料[{itemCode}]无法找到科目描述信息.");
                return res.Fields.Item("U_Vendor").Value;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
