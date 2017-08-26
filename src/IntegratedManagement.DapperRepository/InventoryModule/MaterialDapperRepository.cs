using IntegratedManagement.IRepository.InventoryModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.InventoryModule.Material;
using IntegratedManagement.Entity.Result;
using System.Data;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;
using IntegratedManagement.Entity.Param;

namespace IntegratedManagement.RepositoryDapper.InventoryModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 13:23:42
	===============================================================================================================================*/
    public class MaterialDapperRepository : IMaterialRepository
    {
        public async Task<List<Material>> GetMaterial(QueryParam queryParam)
        {
            List<Material> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  top {queryParam.limit} {queryParam.select} FROM T_Materials t0  {queryParam.filter + " " + queryParam.orderby} ";
                try
                {
                    var materialList = await conn.QueryAsync<Material>(sql);
                    collection = materialList.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return collection;
            }
        }

        public Material GetMaterial(string ItemCode)
        {
            Material material = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM T_Materials t0  where t0.ItemCode = @ItemCode ";
                try
                {
                    material = conn.Query<Material>(sql,new { ItemCode = ItemCode}).ToList().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return material;
            }
        }

        public async Task<SaveResult> Save(Material Material)
        {
            SaveResult saveRlt = new SaveResult();
            saveRlt.UniqueKey = Material.ItemCode;//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"INSERT INTO T_Materials (ItemCode,ItemName,VatGourpSa,VatGourpPu,InitialCost,RealCost,SalesPrice,InvntItem,Consignment,Vendor,OMSGroupNum,CreateDate,UpdateDate)
                                            VALUES (@ItemCode,@ItemName,@VatGourpSa,@VatGourpPu,@InitialCost,@RealCost,@SalesPrice,@InvntItem,@Consignment,@Vendor,@OMSGroupNum,@CreateDate,@UpdateDate)";
                    await conn.ExecuteScalarAsync(insertSql,
                        new
                        {
                            ItemCode = Material.ItemCode,
                            ItemName = Material.ItemName,
                            VatGourpSa = Material.VatGourpSa,
                            VatGourpPu = Material.VatGourpPu,
                            InitialCost = Material.InitialCost,
                            RealCost = Material.RealCost,
                            SalesPrice = Material.SalesPrice,
                            InvntItem = Material.InvntItem,
                            Consignment = Material.Consignment,
                            Vendor = Material.Vendor,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            OMSGroupNum = Material.OMSGroupNum
                        }, dbTransaction);
                    saveRlt.ReturnUniqueKey = Material.ItemCode;//回传保存订单的主键
                    dbTransaction.Commit();
                    saveRlt.Code = 0;
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    saveRlt.Code = 1;
                    saveRlt.Message = ex.Message;
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return saveRlt;
            }
        }

        public async Task<bool> Update(Material material)
        {
            if (material == null || string.IsNullOrEmpty(material.ItemCode))
                throw new Exception("the material is null or the itemcode of material is null.");
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = @"update T_Materials set  ItemName = @ItemName
	                                                  ,VatGourpSa = @VatGourpSa
	                                                  ,VatGourpPu = @VatGourpPu
	                                                  ,InitialCost =@InitialCost 
	                                                  ,RealCost = @RealCost
	                                                  ,SalesPrice = @SalesPrice
	                                                  ,InvntItem = @InvntItem
	                                                  ,Consignment = @Consignment
	                                                  ,Vendor = @Vendor
	                                                  ,UpdateDate = @UpdateDate
	                                                  ,UpDator = @UpDator
	                                                  ,IsDelete = @IsDelete
	                                                  ,IsSync = @IsSync 
                                                      ,OMSGroupNum = @OMSGroupNum where ItemCode = @ItemCode";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new {
                        ItemName = material.ItemName,
                        VatGourpSa = material.VatGourpSa,
                        VatGourpPu = material.VatGourpPu,
                        InitialCost = material.InitialCost,
                        RealCost = material.RealCost,
                        SalesPrice = material.SalesPrice,
                        InvntItem = material.InvntItem,
                        Consignment = material.Consignment,
                        Vendor = material.Vendor,
                        UpdateDate = DateTime.Now,
                        UpDator = material.UpDator,
                        IsDelete = material.IsDelete,
                        IsSync = "N",
                        ItemCode = material.ItemCode,
                        OMSGroupNum = material.OMSGroupNum,
                    });
                    if (rtCount == 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the customer by cardcode:{material.ItemCode}");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return isSuccessOperate;
            }
        }

        public async Task<bool> UpdateSyncData(string ItemCode)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = $"update T_Materials set IsSync = 'Y',SyncDate = '{DateTime.Now}'  where ItemCode = @ItemCode";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new { ItemCode = ItemCode });
                    if (rtCount == 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the customer by cardcode:{ItemCode}");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return isSuccessOperate;
            }
        }
    }
}
