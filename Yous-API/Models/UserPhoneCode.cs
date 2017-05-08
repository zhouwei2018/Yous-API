using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yous_API.Models
{
    public class UserPhoneCode
    {
        public string id { get; set; }
        public string phone { get; set; }
        public string code { get; set; }
        /// <summary>
        /// 1-注册；2-修改密码；3-忘记密码；4-绑定手机号；5-动态登陆；6-查询订单
        /// </summary>
        public string VerifiationCCodeType { get; set; }
        public DateTime getcodedate { get; set; }
        /// <summary>
        /// 过期时效S
        /// </summary>
        public int passtimespan { get; set; }
    }
}