using Ason.Ason.Ast;
using Ason.Ason.Enums;
using Ason.Ason.Utilities;

namespace Ason;

internal class Program
{

    // Test case:
    private static void Main(string[] args)
    {
        var ason = new Ason.Ason(AsonFormatting.Indented);

        var asontojson = new AsonToJson();
        var jsontoason = new JsonToAson();

    }
}