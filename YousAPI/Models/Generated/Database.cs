
// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `MySqlDbHelper`
//     Provider:               `MySql.Data.MySqlClient`
//     Connection String:      `Server=116.62.71.76;Database=youshi;Uid=root;Pwd=youshi2015;charset=utf8;pooling=true;`
//     Schema:                 ``
//     Include Views:          `False`

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace YousAPI.Models
{
	public partial class MySqlDbHelperDB : Database
	{
		public MySqlDbHelperDB() 
			: base("MySqlDbHelper")
		{
			CommonConstruct();
		}

		public MySqlDbHelperDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			MySqlDbHelperDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static MySqlDbHelperDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new MySqlDbHelperDB();
        }

		[ThreadStatic] static MySqlDbHelperDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        
		public class Record<T> where T:new()
		{
			public static MySqlDbHelperDB repo { get { return MySqlDbHelperDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }
			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }
			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }
		}
	}
	

    
	[TableName("youshi.新表")]
	[PrimaryKey("Id")]
	[ExplicitColumns]
    public partial class 新表 : MySqlDbHelperDB.Record<新表>  
    {
		[Column] public int Id { get; set; }
	}
    
	[TableName("youshi.tb_building")]
	[PrimaryKey("TB_BuildingInfo_ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class tb_building : MySqlDbHelperDB.Record<tb_building>  
    {
		[Column] public int TB_BuildingInfo_ID { get; set; }
		[Column] public string Col_Name { get; set; }
		[Column] public string Col_KFSInfo { get; set; }
		[Column] public string Col_Address { get; set; }
		[Column] public decimal? Col_BuildAreage { get; set; }
		[Column] public decimal? Col_KFSAreage { get; set; }
		[Column] public decimal? Col_YZAreage { get; set; }
		[Column] public decimal? Col_SYAreage { get; set; }
		[Column] public int? Col_AllNum { get; set; }
		[Column] public int? Col_KFSNum { get; set; }
		[Column] public int? Col_YZNum { get; set; }
		[Column] public int? Col_SYNum { get; set; }
		[Column] public DateTime? Col_KPDate { get; set; }
		[Column] public string Col_Test { get; set; }
	}
    
	[TableName("youshi.tb_buildingdetail")]
	[PrimaryKey("TB_BuildingDetail_ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class tb_buildingdetail : MySqlDbHelperDB.Record<tb_buildingdetail>  
    {
		[Column] public int TB_BuildingDetail_ID { get; set; }
		[Column] public int? TB_Building_ID { get; set; }
		[Column] public string Col_No { get; set; }
		[Column] public string Col_Key1 { get; set; }
		[Column] public string Col_Key2 { get; set; }
		[Column] public string Col_Key3 { get; set; }
		[Column] public string Col_Key4 { get; set; }
		[Column] public string Col_Key5 { get; set; }
		[Column] public int? Col_FloorNum { get; set; }
		[Column] public int? Col_FloorNum_SJ { get; set; }
		[Column] public decimal? Col_FloorHeight { get; set; }
		[Column] public string Col_Property { get; set; }
		[Column] public decimal? Col_FloorHeight_J { get; set; }
		[Column] public string Col_Elevator_PP { get; set; }
		[Column] public int? Col_Elevator_KT_Num { get; set; }
		[Column] public int? Col_Elevator_HT_Num { get; set; }
		[Column] public string Col_Elevator_Floor_QF { get; set; }
		[Column] public decimal? Col_Elevator_KT_L { get; set; }
		[Column] public decimal? Col_Elevator_KT_W { get; set; }
		[Column] public decimal? Col_Elevator_KT_H { get; set; }
		[Column] public decimal? Col_Elevator_HT_L { get; set; }
		[Column] public decimal? Col_Elevator_HT_W { get; set; }
		[Column] public decimal? Col_Elevator_HT_H { get; set; }
		[Column] public decimal? Col_BuildAreage { get; set; }
		[Column] public decimal? Col_KFSAreage { get; set; }
		[Column] public decimal? Col_YZAreage { get; set; }
		[Column] public decimal? Col_FloorAreage { get; set; }
		[Column] public decimal? Col_KZAreage { get; set; }
		[Column] public byte[] Col_BDImage { get; set; }
		[Column] public int? Col_AllNum { get; set; }
		[Column] public int? Col_KZNum { get; set; }
		[Column] public int? Col_SYNum { get; set; }
		[Column] public decimal? Col_KZ_ZB { get; set; }
		[Column] public decimal? Col_SC_Price { get; set; }
		[Column] public decimal? Col_LS_Price { get; set; }
		[Column] public decimal? Col_SC_M_Price { get; set; }
		[Column] public decimal? Col_LS_M_Price { get; set; }
		[Column] public decimal? Col_SC_Money { get; set; }
		[Column] public sbyte? Col_IsZPDept { get; set; }
		[Column] public string Col_ZP_HZFS { get; set; }
		[Column] public decimal? Col_ZP_FY { get; set; }
		[Column] public string Col_ZP_FYTime { get; set; }
		[Column] public string Col_ZP_Address { get; set; }
		[Column] public string Col_ZP_Phone { get; set; }
		[Column] public sbyte? Col_IsDelete { get; set; }
		[Column] public string Col_State { get; set; }
		[Column] public int? TB_Person_ID_Create { get; set; }
		[Column] public DateTime? Col_CreateDate { get; set; }
		[Column] public int? TB_Person_ID_Update { get; set; }
		[Column] public DateTime? Col_UpdateDate { get; set; }
	}
    
	[TableName("youshi.tb_buildingdetail_house")]
	[PrimaryKey("TB_BuildingDetail_House_ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class tb_buildingdetail_house : MySqlDbHelperDB.Record<tb_buildingdetail_house>  
    {
		[Column] public int TB_BuildingDetail_House_ID { get; set; }
		[Column] public int? TB_Building_ID { get; set; }
		[Column] public int? TB_BuildingDetail_ID { get; set; }
		[Column] public string Col_NO { get; set; }
		[Column] public string Col_MaxFloor { get; set; }
		[Column] public string Col_LowFloor { get; set; }
		[Column] public string Col_HouseNo { get; set; }
		[Column] public decimal? Col_HouseAreage { get; set; }
		[Column] public string Col_HouseJG { get; set; }
		[Column] public decimal? Col_HouseFloorHeight { get; set; }
		[Column] public decimal? Col_HouseWidth { get; set; }
		[Column] public decimal? Col_HouseLength { get; set; }
		[Column] public byte[] Col_HouseImageModel { get; set; }
		[Column] public byte[] Col_HouseImage { get; set; }
		[Column] public string Col_HouseDirection { get; set; }
		[Column] public string Col_JYModel { get; set; }
		[Column] public sbyte? Col_IsThis { get; set; }
		[Column] public sbyte? Col_Is_ZH { get; set; }
		[Column] public decimal? Col_KZ_Days { get; set; }
		[Column] public string Col_ZPPirv { get; set; }
		[Column] public string Col_House_YS { get; set; }
		[Column] public string Col_House_LS { get; set; }
		[Column] public sbyte? Col_IsDelete { get; set; }
		[Column] public string Col_State { get; set; }
		[Column] public int? TB_Person_ID_Create { get; set; }
		[Column] public DateTime? Col_CreateDate { get; set; }
		[Column] public int? TB_Person_ID_Update { get; set; }
		[Column] public DateTime? Col_UpdateDate { get; set; }
	}
    
	[TableName("youshi.tb_buildingdetail_house_dl")]
	[PrimaryKey("TB_BuildingDetail_House_DL_ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class tb_buildingdetail_house_dl : MySqlDbHelperDB.Record<tb_buildingdetail_house_dl>  
    {
		[Column] public int TB_BuildingDetail_House_DL_ID { get; set; }
		[Column] public int? TB_Person_ID { get; set; }
		[Column] public string Col_PersonName { get; set; }
		[Column] public string Col_PersonSex { get; set; }
		[Column] public string Col_PersonTel { get; set; }
		[Column] public string Col_PersonTZ { get; set; }
		[Column] public sbyte? Col_IsDelete { get; set; }
		[Column] public string Col_State { get; set; }
		[Column] public int? Col_Person_ID_Create { get; set; }
		[Column] public DateTime? Col_CreateDate { get; set; }
		[Column] public int? Col_Person_ID_Update { get; set; }
		[Column] public DateTime? Col_UpdateDate { get; set; }
		[Column] public int? TB_BuildingDetail_House_ID { get; set; }
	}
    
	[TableName("youshi.tb_buildingdetail_houseprice")]
	[PrimaryKey("TB_BuildingDetail_HousePrice_ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class tb_buildingdetail_houseprice : MySqlDbHelperDB.Record<tb_buildingdetail_houseprice>  
    {
		[Column] public int TB_BuildingDetail_HousePrice_ID { get; set; }
		[Column] public int? TB_BuildingDetail_House_ID { get; set; }
		[Column] public decimal? Col_Price { get; set; }
		[Column] public DateTime? Col_CreateDate { get; set; }
		[Column] public sbyte? Col_IsStart { get; set; }
	}
    
	[TableName("youshi.tb_user")]
	[PrimaryKey("id")]
	[ExplicitColumns]
    public partial class tb_user : MySqlDbHelperDB.Record<tb_user>  
    {
		[Column] public int id { get; set; }
		[Column] public string Col_username { get; set; }
		[Column] public string Col_displayname { get; set; }
		[Column] public string Col_password { get; set; }
		[Column] public string Col_address { get; set; }
		[Column] public string Col_email { get; set; }
		[Column] public string Col_telephone { get; set; }
		[Column] public string Col_desc { get; set; }
		[Column] public string Col_remark { get; set; }
		[Column] public int? Col_isenable { get; set; }
		[Column] public DateTime create_time { get; set; }
		[Column] public DateTime update_time { get; set; }
	}
    
	[TableName("youshi.tb_user_request_building")]
	[PrimaryKey("id")]
	[ExplicitColumns]
    public partial class tb_user_request_building : MySqlDbHelperDB.Record<tb_user_request_building>  
    {
		[Column] public int id { get; set; }
		[Column] public string Col_orderid { get; set; }
		[Column] public string Col_telephone { get; set; }
		[Column] public string Col_city { get; set; }
		[Column] public string Col_business { get; set; }
		[Column] public decimal Col_measure { get; set; }
		[Column] public decimal Col_rent { get; set; }
		[Column] public string Col_desc { get; set; }
		[Column] public int? Col_isenable { get; set; }
		[Column] public DateTime create_time { get; set; }
		[Column] public DateTime update_time { get; set; }
	}
}
