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
    }
}
