create view T_view_warehouse as select * from [DB_SunPie].[dbo].[AVA_SP_VIEW_WAREHOUSE];
create view T_VIEW_ProfitCenters as select * from [DB_SunPie].[dbo].[AVA_SP_VIEW_COSTCENTER];
 create view T_View_businessPartner as
 select * from [DB_SunPie].[dbo].[AVA_SP_VIEW_BUSINESSPARTNER] where 
 cardcode in (select UniqueKey from [DB_SunPie].[dbo].[AVA_SP_TASKLIST] where BusinessType = '2' 
  and CreateDate >= convert(varchar,dateadd(dd,-day(getdate())+1,getdate()),111));
   create view T_View_BusinessAddress as select * From
 [DB_SunPie].[dbo].[AVA_SP_VIEW_BUSINESSPARTNERLINE];
  create view T_View_BusinessContacts as select * From
 [DB_SunPie].[dbo].[AVA_SP_VIEW_BUSINESSPARTNER_CONTACTSLINE]