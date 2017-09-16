using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.IRepository.FinancialModule;
using IntegrateManagement.MiddleBaseService.B1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:48:04
	===============================================================================================================================*/
    public class JournalRelationMapApp: IJournalRelationMapApp
    {
        private readonly IJournalRelationMapRepository _JournalRelationMapRepository;
        public JournalRelationMapApp(IJournalRelationMapRepository IJournalRelationMapRepository)
        {
            this._JournalRelationMapRepository = IJournalRelationMapRepository;
        }
        public async Task<List<JournalRelationMap>> GetJournalRelationMapListAsync(QueryParam QueryParam)
        {
            return await _JournalRelationMapRepository.GetJournalRelationMapList(QueryParamHandle.ParamHanle(QueryParam));
        }

        public async Task<SaveResult> SaveJournalRelationMapAsync(JournalRelationMap JournalRelationMap)
        {
           return  await _JournalRelationMapRepository.SaveJournalRelationMap(JournalRelationMap);
        }

        public async Task<bool> UpdateJournalRelationMapMinusStatuAsync(DocumentSync SyncData)
        {
            return await _JournalRelationMapRepository.ModifyJournalRelationMapMinus(SyncData);
        }

        public async Task<bool> UpdateJournalRelationMapPositiveStatuAsync(DocumentSync SyncData)
        {
            return await _JournalRelationMapRepository.ModifyJournalRelationMapPositive(SyncData);
        }

        public async Task<bool> UpdateJournalRelationMapStatuAsync(DocumentSync SyncData)
        {
            return await _JournalRelationMapRepository.ModifyJournalRelationMapStatus(SyncData);
        }

        public async Task<string> CreateJournalEntry(List<JournalRelationMap> JournalRelationMaps)
        {
            StringBuilder resultStr = new StringBuilder();
            foreach (var item in JournalRelationMaps)
            {
                if (item.IsApart == "Y")
                {
                    if (item.IsPositiveSync == "N")
                    {
                        var result = JournalEntryService.ApartPositiveJournal(item);
                        if (result.SyncResult == "N")
                            resultStr.Append(result.SyncMsg + ";");
                        await this.UpdateJournalRelationMapPositiveStatuAsync(result);
                    }
                    if (item.IsMinusSync == "N")
                    {
                        var result = JournalEntryService.ApartMinusJournal(item);
                        if (result.SyncResult == "N")
                            resultStr.Append(result.SyncMsg + ";");
                        await this.UpdateJournalRelationMapMinusStatuAsync(result);
                    }
                }
                else
                {
                    if (item.IsSync == "N")
                    {
                        var result = JournalEntryService.CreateJournal(item);
                        if (result.SyncResult == "N")
                            resultStr.Append(result.SyncMsg + ";");
                        await this.UpdateJournalRelationMapStatuAsync(result);
                    }
                }

            }
            if (String.IsNullOrEmpty(resultStr.ToString()))
                return "数据同步至财务系统全部成功";
            else
                return resultStr.ToString();
        }
        
    }
}
