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

            obj.Category.Add(BuiltInCategory.OST_ElectricalFixtures, "T");
            obj.Category.Add(BuiltInCategory.OST_LightingDevices, "L");

            var str = javaScriptService.Serialize(obj);

            System.Console.WriteLine(str);

            var myClass = javaScriptService.Deserialize<MyClass>(str);

            System.Console.WriteLine(myClass.ElementId);

            System.Console.WriteLine(javaScriptService.Serialize(javaScriptService.Deserialize<MyClass>(str)));

            TaskDialog.Show("Json", javaScriptService.Serialize(javaScriptService.Deserialize<MyClass>(str)));

            return Result.Succeeded;
        }

        class MyClass
        {
            public Dictionary<string, string> Dictionary { get; set; } = new Dictionary<string, string>();
            public ElementId ElementId { get; set; } = new ElementId(200002);
            public Dictionary<BuiltInCategory, string> Category { get; set; } = new Dictionary<BuiltInCategory, string>();
        }

    }
}
