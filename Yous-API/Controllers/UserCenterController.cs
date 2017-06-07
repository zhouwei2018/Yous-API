using Newtonsoft.Json.Linq;
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
            JObject o = JObject.Parse(parameters.Parameters.ToLower());
            //step1 TODO-check param参数，参数不对，直接抛出业务异常
            var area_id = o["area_id"].ToString();

            //step2 TODO-业务组装sql语句
            MySqlDbHelperDB dbhelper = new MySqlDbHelperDB();
            var result = dbhelper.Fetch<tb_user>("select * from tb_user where Col_Id=@0", area_id);

            //step3 返回结果
            ResponseJson responseJson = new ResponseJson { success = true, data = result, message = "" };
            return responseJson;
        }
        /// <summary>
        ///  用户注册
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [Route("UserCenter/10000001")]
        [HttpPost]
        public ResponseJson Regesite(RequestJson parameters)
        {
            JObject o = JObject.Parse(parameters.Parameters.ToLower());
            //step1 TODO-check param参数，参数不对，直接抛出业务异常
            var area_id = o["area_id"].ToString();

            //step2 TODO-业务组装sql语句
            MySqlDbHelperDB dbhelper = new MySqlDbHelperDB();
            var result = dbhelper.Fetch<tb_user>("select * from tb_user where Col_Id=@0", area_id);

            //step3 返回结果
            ResponseJson responseJson = new ResponseJson { success = true, data = result, message = "" };
            return responseJson;
        }
    }
}
