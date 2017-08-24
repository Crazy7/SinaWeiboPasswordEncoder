using System;
using Jint;
using System.IO;
using SinaWeiboPasswordEncoder.Abstractions;

namespace SinaWeiboPasswordJsEncoder
{
    public class JsEncoder : ISinaWeiboPasswordEncoder
    {
        const string JsFilePath = "encoder.js";
        private static readonly Engine JsEngine;

        static JsEncoder()
        {
            if (!File.Exists(JsFilePath)) JsEngine = null;

            try
            {
                var js = File.ReadAllText(JsFilePath);
                if(!string.IsNullOrWhiteSpace(js))
                {
                    JsEngine = new Engine().Execute(js);
                }
            }
            catch
            {
                JsEngine = null;
            }
        }

        public string EncodePassword(string pwd, string serverTime, string nonce, string rsaPubkey)
        {
            try
            {
                var encrypted = JsEngine.Invoke("encodePwd", pwd, serverTime, nonce, rsaPubkey);

                return encrypted.ToString();
            }
            catch
            {
                throw;
            }
        }
    }
}
