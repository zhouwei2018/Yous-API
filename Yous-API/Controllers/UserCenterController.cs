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
    /// UserCenter用户中心(100-)
    /// </summary>
    public class UserCenterController : ApiController
    {
        /// <summary>
        ///  用户登录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [Route("UserCenter/10000001")]
        [HttpPost]
        public ResponseJson Login(RequestJson parameters)
        {
            /*---//登陆
             {
                "parameters": {
					"Col_telephone": "13426242626",
					"Col_password": "123456"
                    },
					"foreEndType": 2,
					"code": "10000001"
                }
            ---*/
            JObject o = JObject.Parse(parameters.Parameters);
            //step1 TODO-check param参数，参数不对，直接抛出业务异常
            var phone = o["Col_telephone"].ToString();
            var pwd = o["Col_password"].ToString();

            MySqlDbHelperDB dbhelper = new MySqlDbHelperDB();

            tb_user info = dbhelper.FirstOrDefault<tb_user>(@"select * from tb_user 
where Col_telephone=@0 and Col_password=@1", phone.ToString(), pwd.ToString());

            ResponseJson result = new ResponseJson();
            result.success = false;
            result.message = "用户名不存在或者密码不对";
            if (info != null)
            {
                result.success = true;
                result.message = "获取到用户.";
                result.data = info;
            }
            return result;
        }
        /// <summary>
        ///  用户注册
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [Route("UserCenter/10000002")]
        [HttpPost]
        public ResponseJson UserRegister(RequestJson parameters)
        {
            JObject o = JObject.Parse(parameters.Parameters);
            //step1 TODO-check param参数，参数不对，直接抛出业务异常
            /*---//注册
             * VerifiationCCodeType：发送短信类型 1001=注册；
             {
                "parameters": {
					"VerifiationCCodeType": "1001",
					"InputCode": "InputCode",
                    "Col_telephone": "13426242626",
					"Col_username": "kaka",
					"Col_password": "123456",
					"Col_address": "Col_address",
					"Col_displayname": "king",
					"Col_email": "Col_email",
					"Col_remark": "Col_remark",
					"Col_desc": "Col_desc",
                    },
					"foreEndType": 2,
					"code": "10000002"
                }
            ---*/

            //string CultureName = o["CultureName"].ToString();  
            string VerifiationCCodeType = o["VerifiationCCodeType"].ToString();  

            string InputCode= o["InputCode"].ToString();

            MySqlDbHelperDB dbhelper = new MySqlDbHelperDB();
            ResponseJson result = new ResponseJson();


            if (dbhelper.Fetch<tb_user>("select * from tb_user where Col_telephone = @0", o["Col_telephone"].ToString()).Count > 0)
            {
                result.success = false;
                result.message = "该手机号已经注册.";
                return result;
            }

            tb_user user = new tb_user()
            {
                Col_address = o["Col_address"] != null ? o["Col_address"].ToString() : string.Empty,
                Col_desc = o["Col_desc"] != null ? o["Col_username"].ToString() : string.Empty,
                Col_displayname = o["Col_displayname"] != null ? o["Col_displayname"].ToString() : string.Empty,
                Col_email = o["Col_email"] != null ? o["Col_email"].ToString() : string.Empty,
                Col_password = o["Col_password"].ToString(),
                Col_isenable = 1,
                Col_remark = o["Col_remark"] != null ? o["Col_remark"].ToString() : string.Empty,
                Col_telephone = o["Col_telephone"] != null ? o["Col_telephone"].ToString() : string.Empty,
                Col_username = o["Col_username"] != null ? o["Col_username"].ToString() : string.Empty,
                create_time = DateTime.Now,
                update_time = DateTime.Now,
            };

            #region //校验注册验证码
            try
            {
                using (var redisClient = RedisHelper.GetRedisClient())
                {
                    IRedisTypedClient<UserPhoneCode> irClient = redisClient.As<UserPhoneCode>();

                    UserPhoneCode data = irClient.GetById(user.Col_telephone + "_" + VerifiationCCodeType);
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
                            dbhelper.Insert(user);
                            result.success = true;
                            //返回用户
                            result.data = dbhelper.FirstOrDefault<tb_user>("select * from tb_user where Col_telephone = @0", user.Col_telephone);
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
