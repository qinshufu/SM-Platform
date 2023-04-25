using System.Collections.Specialized;

namespace SmPlatform.Server.Models
{
    /// <summary>
    /// 短信
    /// </summary>
    public class ShortMessage
    {
        /// <summary>
        /// 配置
        /// </summary>
        public Guid Configuration { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        public Guid Template { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public Guid Signature { get; set; }

        /// <summary>
        /// 模板参数
        /// </summary>
        public NameValueCollection TemplateParams { get; set; }
    }
}
