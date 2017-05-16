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
using Yous;
using Yous_Api.Models;

namespace Yous_API.Controllers
{
    public class APIController : ApiController
    {
       
        [Route("api/GetServiceApiResult")]
        [HttpPost]
        public IHttpActionResult GetServiceApiResult(dynamic inputParame)
        {
            Dictionary<string, string> controllerNameKeyValue = new Dictionary<string, string> {   
                {"100", "http://" + Url.Request.Headers.Host + "/UserCenter"}, //100-用户中心路由
                {"200", "http://" + Url.Request.Headers.Host + "/Portals"},     //200-网站门户路由
            };
       
            string ret = String.Empty;
            RequestJson parame = GetInParametersCondition(inputParame);

            #region 发送Request请求
            DateTime reqeustdt = DateTime.Now;
            var controllerName = controllerNameKeyValue[parame.Code.Substring(0, 3)];
            HttpWebRequest proxyRequest = HttpWebRequest.Create(controllerName + "/" + parame.Code) as HttpWebRequest;
            proxyRequest.Method = "POST";
            proxyRequest.KeepAlive = false;
            proxyRequest.ContentType = "application/json";
            proxyRequest.Timeout = 200000;

            var parameters = Newtonsoft.Json.JsonConvert.SerializeObject(parame);
            byte[] aryBuf = Encoding.GetEncoding("utf-8").GetBytes(parameters);
            proxyRequest.ContentLength = aryBuf.Length;
            using (Stream writer = proxyRequest.GetRequestStream())
            {
                writer.Write(aryBuf, 0, aryBuf.Length);
                writer.Close();
                writer.Dispose();
            }
        
            #endregion

            #region 返回Response
            using (WebResponse response = proxyRequest.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                ret = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
            }
            #endregion

            return new TextResult(ret, Request);
        }


        #region 入参对象
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputParameters"></param>
        /// <returns></returns>
        private RequestJson GetInParametersCondition(dynamic InputParameters)
        {
            if (InputParameters.Parameters == null)
                InputParameters.Parameters = InputParameters.parameters;
            if (InputParameters.ForeEndType == null)
                InputParameters.ForeEndType = InputParameters.foreEndType;
            if (InputParameters.Code == null)
                InputParameters.Code = InputParameters.code;
            return new RequestJson
            {
                Parameters = InputParameters.Parameters == null ? string.Empty : InputParameters.Parameters.ToString().ToLower(), //Action对应的传入参数
                ForeEndType = (int)InputParameters.ForeEndType,     //前端类型 1：IOS、2：Android、3：H5
                Method = "POST",                                    //默认只支持POST提交
                Code = InputParameters.Code                         //前台传的编码
            };
        }
        #endregion

    }
}
