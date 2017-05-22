using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Sms.Model.V20160927;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Yous_API.Utility
{
    public class SmsHelper
    {

        public static bool SendOrderMassageToBack(string recNum, string content)
        {
            return ALiYunSendOrderMassageToBack(recNum, content);
        }
        public static bool SendMassage(string RecNum, string code)
        {
            return ALiYunSendMassage(RecNum, code);
        }
        public static bool SendOrderOverMassage(string recNum, string code)
        {
            return ALiYunSendOrderOverMassage(recNum, code);
        }
        private static bool ALiYunSendOrderOverMassage(string recNum, string code)
        {
            //尊敬的客户，您的订单${code}已经完成处理，您可通过手机访问手机手机幼狮空间，在“我的订单”点击“确认订单”后进行评价；若有疑问请您选择"联系客服"，我们会尽快给您回复。
            //SMS_30415034
            string appkey = "LTAIYzGb4MlggBDc";
            string appsecret = "aGHxEWOuaw1DbGD9iVBc5Kh2TPmFkr";
            string SmsTemplateCode = "SMS_30400020";

            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SendMassage"]))
            {
                appkey = System.Configuration.ConfigurationManager.AppSettings["SendOrderOverMassage"].Split(',')[0];
                appsecret = System.Configuration.ConfigurationManager.AppSettings["SendOrderOverMassage"].Split(',')[1];
                SmsTemplateCode = System.Configuration.ConfigurationManager.AppSettings["SendOrderOverMassage"].Split(',')[2];
            }
            IClientProfile profile = DefaultProfile.GetProfile("cn-beijing", appkey, appsecret);
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendSmsRequest request = new SingleSendSmsRequest();
            try
            {
                request.SignName = "幼狮空间";
                request.TemplateCode = SmsTemplateCode;
                request.RecNum = recNum;
                String json = "{\"code\":\"" + code + "\""
                    + "}";//用于替换短信模板中的变量  
                request.ParamString = json;
                SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
            }
            catch (ServerException e)
            {
                //e.printStackTrace();
            }
            catch (ClientException e)
            {
                //e.printStackTrace();
            }




            return true;

        }

        private static bool ALiYunSendOrderMassageToBack(string recNum, string content)
        {
            //您有新的订单，请及时接单。
            //${ content}
            string appkey = "LTAIYzGb4MlggBDc";
            string appsecret = "aGHxEWOuaw1DbGD9iVBc5Kh2TPmFkr";
            string SmsTemplateCode = "SMS_30400020";

            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SendMassage"]))
            {
                appkey = System.Configuration.ConfigurationManager.AppSettings["SendOrderMassage"].Split(',')[0];
                appsecret = System.Configuration.ConfigurationManager.AppSettings["SendOrderMassage"].Split(',')[1];
                SmsTemplateCode = System.Configuration.ConfigurationManager.AppSettings["SendOrderMassage"].Split(',')[2];
            }
            IClientProfile profile = DefaultProfile.GetProfile("cn-beijing", appkey, appsecret);
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendSmsRequest request = new SingleSendSmsRequest();
            try
            {
                request.SignName = "幼狮空间";
                request.TemplateCode = SmsTemplateCode;

                request.RecNum = recNum;
                String json = "{\"content\":\"" + content + "\"}";//用于替换短信模板中的变量  
                request.ParamString = json;
                SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
            }
            catch (ServerException e)
            {
                //e.printStackTrace();
            }
            catch (ClientException e)
            {
                //e.printStackTrace();
            }

            return true;
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="RecNum">手机号</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static bool ALiYunSendMassage(string RecNum, string code)
        {
            string appkey = "LTAIYzGb4MlggBDc";
            string appsecret = "aGHxEWOuaw1DbGD9iVBc5Kh2TPmFkr";
            string SmsTemplateCode = "SMS_27350218";

            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SendMassage"]))
            {
                appkey = System.Configuration.ConfigurationManager.AppSettings["SendMassage"].Split(',')[0];
                appsecret = System.Configuration.ConfigurationManager.AppSettings["SendMassage"].Split(',')[1];
                SmsTemplateCode = System.Configuration.ConfigurationManager.AppSettings["SendMassage"].Split(',')[2];
            }
            IClientProfile profile = DefaultProfile.GetProfile("cn-beijing", appkey, appsecret);
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendSmsRequest request = new SingleSendSmsRequest();
            try
            {
                request.SignName = "幼狮空间";
                request.TemplateCode = SmsTemplateCode;
                request.RecNum = RecNum;
                String json = "{\"code\":\"" + code + "\"}";//用于替换短信模板中的变量  
                request.ParamString = json;
                SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
            }
            catch (ServerException e)
            {
                //e.printStackTrace();
            }
            catch (ClientException e)
            {
                //e.printStackTrace();
            }


            return true;
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="RecNum">手机号</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static bool DaYuSendMassage(string RecNum, string code)
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
            req.RecNum = RecNum.Trim();
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
    public class SmsHelp
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