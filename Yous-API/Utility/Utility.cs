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

namespace YousAPI.Utility
{
    public class Utility
    {

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