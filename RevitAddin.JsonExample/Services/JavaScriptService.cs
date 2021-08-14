using Newtonsoft.Json;

namespace RevitAddin.JsonExample.Services
{
    public class JavaScriptService
    {
        public JavaScriptService()
        {

        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}