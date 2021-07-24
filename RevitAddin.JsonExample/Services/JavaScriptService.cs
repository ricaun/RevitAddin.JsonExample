using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private class ElementIdConverter : JavaScriptConverter
        {
            private const string ElementIdKey = "ElementId";

            public override IEnumerable<Type> SupportedTypes
            {
                get
                {
                    return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(ElementId) }));
                }
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                if (dictionary == null)
                    throw new ArgumentNullException("Dictionary");
                if (type == typeof(ElementId))
                {
                    return new ElementId((int)dictionary[ElementIdKey]);
                }
                return null;
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                ElementId elementId = obj as ElementId;
                var result = new Dictionary<string, object>();
                if (elementId != null)
                {
                    result[ElementIdKey] = elementId.IntegerValue;
                }
                return result;
            }
        }
    }
}