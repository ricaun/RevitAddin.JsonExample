using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.JsonExample.Services;
using System.Collections.Generic;

namespace RevitAddin.JsonExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandDictionary : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;

            JavaScriptService javaScriptService = new JavaScriptService();

            var obj = new MyClass();

            for (int i = 0; i < 2; i++)
            {
                obj.Dictionary.Add($"Key {i}", $"Value {i}");
            }

            obj.List.Add(new ClassKey() { Key = BuiltInCategory.OST_ElectricalFixtures, Value = $"Value" });

            //obj.Category.Add(BuiltInCategory.OST_ElectricalFixtures, "T");

            var str = javaScriptService.Serialize(obj);

            System.Console.WriteLine(str);

            System.Console.WriteLine(javaScriptService.Serialize(javaScriptService.Deserialize<MyClass>(str)));

            return Result.Succeeded;
        }

        class MyClass
        {
            public Dictionary<string, string> Dictionary { get; set; } = new Dictionary<string, string>();

            public List<ClassKey> List { get; set; } = new List<ClassKey>();

            //public Dictionary<BuiltInCategory, string> Category { get; set; } = new Dictionary<BuiltInCategory, string>();
        }

        public class ClassKey
        {
            public BuiltInCategory Key { get; set; }
            public string Value { get; set; }
        }
    }
}
