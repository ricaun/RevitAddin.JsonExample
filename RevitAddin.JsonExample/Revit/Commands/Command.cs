using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.JsonExample.Services;
using System.Collections.Generic;

namespace RevitAddin.JsonExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;

            JavaScriptService javaScriptService = new JavaScriptService();

            var obj = new MyClass() { Name = "Luiz", Last = "Cassettari", ElementId = new ElementId(100000) };

            var str = javaScriptService.Serialize(obj);

            System.Console.WriteLine(str);

            System.Console.WriteLine(javaScriptService.Serialize(javaScriptService.Deserialize<MyClass>(str)));

            return Result.Succeeded;
        }

        class MyClass
        {
            public string Name { get; set; }
            public string Last { get; set; }
            public ElementId ElementId { get; set; }
        }

        class MySkills
        {
            public string Name { get; set; }
            public int Level { get; set; }
        }
    }
}
