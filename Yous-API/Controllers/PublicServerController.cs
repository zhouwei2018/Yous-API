using Newtonsoft.Json.Linq;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yous_Api.Models;

namespace Yous_API.Controllers
{
    /// <summary>
    /// 公用服务类
    /// </summary>
    public class PublicServerController : ApiController
    {
        /// <summary>
        ///  获取Area信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("PublicServer/90000001")]
        [HttpPost]
        public ResponseJson SendWeiXinMassageToOne(RequestJson parameters)
        {
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


        #region weixin
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
}
