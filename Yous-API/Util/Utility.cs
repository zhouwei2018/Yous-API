using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Yous_API.Util
{
    public class Utility
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="RecNum">手机号</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static bool SendMassage(string RecNum, string code)
        {

            string url = "http://gw.api.taobao.com/router/rest?";

            string appkey = "23522849";
            string appsecret = "a3c33ff3dfcb91394f576061d66c6fce";
            string SmsTemplateCode = "SMS_25615003";

            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SendMassage"]))
            {
                appkey = System.Configuration.ConfigurationManager.AppSettings["SendMassage"].Split(',')[0];
                appsecret = System.Configuration.ConfigurationManager.AppSettings["SendMassage"].Split(',')[1];
                SmsTemplateCode = System.Configuration.ConfigurationManager.AppSettings["SendMassage"].Split(',')[2];
            }
            ITopClient client = new DefaultTopClient(url, appkey, appsecret);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = "";
            req.SmsType = "normal";
            req.SmsFreeSignName = "幼狮空间";
            req.SmsParam = "{code:'" + code + "'}";
            req.RecNum = RecNum;
            req.SmsTemplateCode = SmsTemplateCode;
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);

            return true;
        }


        /// <summary>
        /// 方法一，生成随机数。
        /// </summary>
        /// <param name="s">接收生成的随机数</param>
        /// <param name="rm">random的实例</param>
        /// <returns>生成的随机数</returns>
        public static string random_1(string s, Random rm)
        {
            for (int i = 0; i < 4; i++)
            {
                int k = rm.Next();
                char j = (char)('0' + (char)(k % 10));
                s += j.ToString();
            }
            return s;
        }
        /// <summary>
        /// 方法二，生成随机数。
        /// </summary>
        /// <param name="s">接收生成的随机数</param>
        /// <param name="rm">random的实例</param>
        /// <returns>生成的随机数</returns>
        public static string random_2(string s, Random rm)
        {
            s = Convert.ToString(rm.Next(1000, 9999));
            return s;
        }
        
    }
}