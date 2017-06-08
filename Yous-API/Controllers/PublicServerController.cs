using Newtonsoft.Json.Linq;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YousAPI.Models;
using YousAPI.Utility;


namespace YousAPI.Controllers
{
    /// <summary>
    /// 公用服务类（900-）
    /// </summary>
    public class PublicServerController : ApiController
    {

        #region wechat业务90000001
        /// <summary>
        ///  微信发送方法
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("PublicServer/90000001")]
        [HttpPost]
        public ResponseJson SendWeiXinMassageToOne(RequestJson parameters)
        {
            /*--
             * oJ86ixMHdlXgKN6MKoJJawCCOg-w 刘tok
                oJ86ixFqymlEiqxzkoGE7i_cPesE  du


                {
                    "touser": "oJ86ixMHdlXgKN6MKoJJawCCOg-w", 
                    "msgtype": "news", 
                    "news": {
                        "articles": [
                            {
                                "title": "Happy Day", 
                                "description": "Is Really A HappyDay", 
                                "url": "www.baidu.com", 
                                "picurl": "http://images.cnitblog.com/blog/22790/201310/24144112-8a154829fb9742ebb149b7acfb5a61f4.png"
                            }
                        ]
                    }
                }--*/
            JObject o = JObject.Parse(parameters.Parameters);
            //step1 TODO-check param参数，参数不对，直接抛出业务异常

            string AppID = System.Configuration.ConfigurationManager.AppSettings["AppID"].ToString();
            string AppSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"].ToString();

            //推送给客户信息
            var openId = o["touser"].ToString();
            //var msgtype = o["msgtype"].ToString();
            var mtitle = o["title"].ToString();
            var mdescription = o["description"].ToString();
            var murl = o["url"].ToString();
            var mpicurl = o["picurl"].ToString();

            List<Article> articles = new List<Article>()
            {
                new Article() {
                Title = mtitle,
                Description = mdescription,
                Url = murl,
                PicUrl = mpicurl,
                }
            };
            var resulttoken = AccessTokenContainer.TryGetAccessToken(AppID, AppSecret);
            Senparc.Weixin.Entities.WxJsonResult result = Custom.SendNews(resulttoken, openId, articles);


            //step3 返回结果
            ResponseJson responseJson = new ResponseJson { success = true, data = result, message = "" };
            return responseJson;
        }
        
        [Route("PublicServer/90000201")]
        [HttpPost]
        public ResponseJson AddWeChatUser(RequestJson parameters)
        {
            JObject o = JObject.Parse(parameters.Parameters);

            /*---
             {
                "parameters": {
                    "openid": "oJ86ixMHdlXgKN6MKoJJawCCOg-w",
                    "phone": "13426242626",
                    "systemid": "11111111-1111-1111-1111-111111111111",
                    "wechatid": "11111111-1111-1111-1111-111111111111",
                    },
					"foreEndType": 2,
					"code": "90000201"
                }
            ---*/

            //推送给客户信息
            var openId = o["openid"].ToString();
            var phone = o["phone"].ToString();
            string systemid = o["systemid"].ToString();
            string wechatid = o["wechatid"].ToString();

            //todo 根据电话号 系统id获取系统用户id
            string userid = string.Empty;//获取用户id

            WechatLogic.AddWechatUser(openId, phone, systemid, userid, wechatid);

            string result = "添加成功";
            //step3 返回结果
            ResponseJson responseJson = new ResponseJson { success = true, data = result, message = "ok" };
            return responseJson;
        }

        #endregion

        #region 短信服务
        /// <summary>
        ///  获取Area信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("PublicServer/90000101")]
        [HttpPost]
        public ResponseJson SendMassageToOne(RequestJson parameters)
        {
            JObject o = JObject.Parse(parameters.Parameters);

            /*---
             {
                "parameters": {
                    "touser": "13426242626",
						"code": "123456"

                    },
					"foreEndType": 2,
					"code": "90000101"

                }
            ---*/

            //推送给客户信息
            var openId = o["touser"].ToString();
            //var msgtype = o["msgtype"].ToString();
            var code = o["code"].ToString();

            var result = SmsHelper.SendMassage(openId, code);


            //step3 返回结果
            ResponseJson responseJson = new ResponseJson { success = true, data = result, message = "" };
            return responseJson;
        }


        /// <summary>
        ///  获取短信码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [Route("PublicServer/90000102")]
        [HttpPost]
        public ResponseJson GetMobileCode(RequestJson parameters)
        {

            /*---//发送注册验证码
             * VerifiationCCodeType：发送短信类型 1001=注册；2001=创建用户查询商圈表单；
             {
                "parameters": {
                    "Col_telephone": "13426242626",
					"VerifiationCCodeType": "1001"
                    },
					"foreEndType": 2,
					"code": "90000102"

                }
             {
                "parameters": {
                    "Col_telephone": "13426242626",
					"VerifiationCCodeType": "2001"
                    },
					"foreEndType": 2,
					"code": "90000102"

                }
            ---*/
            JObject o = JObject.Parse(parameters.Parameters);
            string Mobile = o["Col_telephone"].ToString(); ;
            string VerifiationCCodeType = o["VerifiationCCodeType"].ToString(); ;
            string ImageNo = o["ImageNo"]!=null? o["ImageNo"].ToString():string.Empty ;


            ResponseJson result = new ResponseJson { success = true, message = "" };
            Random rm = new Random();
            string s = string.Empty;                //置空字符串.
            string code = Utility.Utility.random_1(s, rm);
            result.data = code;

            try
            {
                using (var redisClient = RedisHelper.GetRedisClient())
                {
                    IRedisTypedClient<UserPhoneCode> irClient = redisClient.As<UserPhoneCode>();

                    //判断对象是否存在
                    //删除同一类型的用户短信请求
                    UserPhoneCode data = irClient.GetById(Mobile + "_" + VerifiationCCodeType);
                    if (data != null)
                    {
                        irClient.DeleteById(data.Id);
                    }
       
                    UserPhoneCode pcode = new UserPhoneCode()
                    {
                        Id = Mobile + "_" + VerifiationCCodeType,
                        phone = Mobile,
                        VerifiationCCodeType = VerifiationCCodeType,
                        code = code,
                        passtimespan = 3600,
                        getcodedate = DateTime.Now
                    };
                    irClient.Store(pcode);

                    //test 发送短信 测试阶段不发送短信
                    //SmsHelp.SendMassage(pcode.phone, pcode.code);
                }
            }
            catch (Exception ex) { }
            return result;
        }


        #endregion

    }





    #region weixin
    public static class WechatLogic
    {

        internal static bool AddWechatUser(string openId, string phone, string systemid, string userid, string wechatid)
        {
            tb_wechat_user user = new tb_wechat_user()
            {
                fd_isenable = 1,
                fd_desc = "",
                fd_openid = openId,
                fd_phone = phone,
                fd_system_id = systemid,
                fd_user_id = userid,
                fd_wechat_id = wechatid
            };
            tb_wechat_user userget = (new MyWXDbHelperDB()).FirstOrDefault<tb_wechat_user>(@"
select * from tb_wechat_user where fd_openid=@0
                ", openId);
            if (userget != null)
            {
                user.id = userget.id;
                (new MyWXDbHelperDB()).Update(user);

            }
            else
            {
                (new MyWXDbHelperDB()).Insert(user);
            }

            return true;
        }
    }
    /// <summary>
    /// 客服接口
    /// </summary>
    public static class Custom
    {
        private const string URL_FORMAT = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static WxJsonResult SendText(string accessToken, string openId, string content)
        {
            var data = new
            {
                touser = openId,
                msgtype = "text",
                text = new
                {
                    content = content
                }
            };
            return CommonJsonSend.Send(accessToken, URL_FORMAT, data);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static WxJsonResult SendImage(string accessToken, string openId, string mediaId)
        {
            var data = new
            {
                touser = openId,
                msgtype = "image",
                image = new
                {
                    media_id = mediaId
                }
            };
            return CommonJsonSend.Send(accessToken, URL_FORMAT, data);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static WxJsonResult SendVoice(string accessToken, string openId, string mediaId)
        {
            var data = new
            {
                touser = openId,
                msgtype = "voice",
                voice = new
                {
                    media_id = mediaId
                }
            };
            return CommonJsonSend.Send(accessToken, URL_FORMAT, data);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="thumbMediaId"></param>
        /// <returns></returns>
        public static WxJsonResult SendVideo(string accessToken, string openId, string mediaId, string thumbMediaId)
        {
            var data = new
            {
                touser = openId,
                msgtype = "video",
                video = new
                {
                    media_id = mediaId,
                    thumb_media_id = thumbMediaId
                }
            };
            return CommonJsonSend.Send(accessToken, URL_FORMAT, data);
        }

        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">视频缩略图的媒体ID</param>
        /// <returns></returns>
        public static WxJsonResult SendMusic(string accessToken, string openId, string title, string description,
                                    string musicUrl, string hqMusicUrl, string thumbMediaId)
        {
            var data = new
            {
                touser = openId,
                msgtype = "music",
                music = new
                {
                    title = title,
                    description = description,
                    musicurl = musicUrl,
                    hqmusicurl = hqMusicUrl,
                    thumb_media_id = thumbMediaId
                }
            };
            return CommonJsonSend.Send(accessToken, URL_FORMAT, data);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">视频缩略图的媒体ID</param>
        /// <returns></returns>
        public static WxJsonResult SendNews(string accessToken, string openId, List<Article> articles)
        {
            var data = new
            {
                touser = openId,
                msgtype = "news",
                news = new
                {
                    articles = articles.Select(z => new
                    {
                        title = z.Title,
                        description = z.Description,
                        url = z.Url,
                        picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                    }).ToList()
                }
            };
            return CommonJsonSend.Send(accessToken, URL_FORMAT, data);
        }
    }
    #endregion
}
