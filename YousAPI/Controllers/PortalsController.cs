using Newtonsoft.Json.Linq;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Routing;
using YousAPI.Models;
using YousAPI.Utility;

namespace YousAPI.Controllers
{
    /// <summary>
    /// 门户首页路由(200-)
    /// </summary>
    public class PortalsController : ApiController
    {
        /// <summary>
        ///  获取网站首页信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("Portals/20000001")]
        [HttpPost]
        public ResponseJson GetHomePageArea(RequestJson parameters)
        {
            JObject o = JObject.Parse(parameters.Parameters.ToLower());
            //step1 TODO-check param参数，参数不对，直接抛出业务异常
            var area_id = o["area_id"].ToString();

            //step2 TODO-业务组装sql语句
            MySqlDbHelperDB dbhelper = new MySqlDbHelperDB();
            var result = dbhelper.Fetch<tb_user>("select * from tb_user where C_Id=@0", area_id);

            //step3 返回结果
            ResponseJson responseJson = new ResponseJson { success = true, data = result, message = "" };
            return responseJson;
        }
        [Route("Portals/20000002")]
        [HttpPost]
        public ResponseJson InsertUserBuildingRequest(RequestJson parameters)
        {
            JObject o = JObject.Parse(parameters.Parameters);
            //step1 TODO-check param参数，参数不对，直接抛出业务异常
            /*---//注册
             * VerifiationCCodeType：发送短信类型 1001=注册；2001=创建用户查询商圈表单；
             {
                "parameters": {
					"VerifiationCCodeType": "2001",
					"InputCode": "InputCode",
                    "Col_telephone": "13426242626",
					"Col_city": "北京",
					"Col_business": "国贸",
					"Col_measure": "1000.1",
					"Col_rent": "1000.2",
					"Col_desc": "Col_desc",
                    },
					"foreEndType": 2,
					"code": "10000002"
                }
            ---*/

            //string CultureName = o["CultureName"].ToString();  
            string VerifiationCCodeType = o["VerifiationCCodeType"].ToString();

            string InputCode = o["InputCode"].ToString();

            MySqlDbHelperDB dbhelper = new MySqlDbHelperDB();
            ResponseJson result = new ResponseJson();

            tb_user_request_building info = new tb_user_request_building()
            {
                Col_orderid= DateTime.Now.ToString("yyyyMMddHHmmss"),
                Col_desc = o["Col_desc"] != null ? o["Col_username"].ToString() : string.Empty,
                Col_isenable = 1,
                Col_telephone = o["Col_telephone"] != null ? o["Col_telephone"].ToString() : string.Empty,
                Col_business= o["Col_business"] != null ? o["Col_business"].ToString() : string.Empty,
                Col_city = o["Col_city"] != null ? o["Col_city"].ToString() : string.Empty,
                Col_measure = o["Col_measure"] != null ? decimal.Parse( o["Col_measure"].ToString()) : 0,
                Col_rent = o["Col_rent"] != null ? decimal.Parse(o["Col_rent"].ToString()) : 0,
                
                create_time = DateTime.Now,
                update_time = DateTime.Now,
            };

            #region //校验注册验证码
            try
            {
                using (var redisClient = RedisHelper.GetRedisClient())
                {
                    IRedisTypedClient<UserPhoneCode> irClient = redisClient.As<UserPhoneCode>();

                    UserPhoneCode data = irClient.GetById(info.Col_telephone + "_" + VerifiationCCodeType);
                    if (data != null)
                    {
                        TimeSpan span = DateTime.Now - data.getcodedate;
                        if (span.TotalSeconds > data.passtimespan)
                        {
                            //过期
                            result.success = false;
                            result.message = "验证码超时.";
                        }
                        if (InputCode == data.code)
                        {
                            //验证通过
                            result.success = true;
                            result.message = "验证通过.";
                            //插入用户表
                            dbhelper.Insert(info);
                            //返回数据
                            result.data = dbhelper.Fetch<tb_user_request_building>("select * from tb_user_request_building where Col_telephone = @0 and Col_orderid=@1", info.Col_telephone,info.Col_orderid);
                        }
                        else
                        {
                            //验证失败
                            result.success = false;
                            result.message = "验证失败.";
                        }
                    }
                    else
                    {
                        //验证失败
                        result.success = false;
                        result.message = "未发现手机验证码.";
                    }
                }
            }
            catch (Exception ex)
            {
                //验证失败
                result.success = false;
                result.message = "失败.";
            }
            #endregion


            return result;
        }
    }
}
