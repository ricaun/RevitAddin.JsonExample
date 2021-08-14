using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace RevitAddin.JsonExample.Services
{
    public class JavaScriptService
    {
        private readonly JavaScriptSerializer javaScriptSerializer;
        public JavaScriptService()
        {
            javaScriptSerializer = new JavaScriptSerializer();
            RegisterConverter(new ElementIdConverter());
        }

        public void RegisterConverter(JavaScriptConverter javaScriptConverter)
        {
            javaScriptSerializer.RegisterConverters(new List<JavaScriptConverter>() { javaScriptConverter });
        }

        public string Serialize(object obj)
        {
            return javaScriptSerializer.Serialize(obj);
        }

        public T Deserialize<T>(string str)
        {
            return javaScriptSerializer.Deserialize<T>(str);
        }
    }
}