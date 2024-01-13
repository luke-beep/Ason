using System.Text;
using Ason.Ason.Ast;
using Ason.Ason.Enums;

namespace Ason.Ason.Utilities;

public class AsonToJson
{
    public string Parse(AsonObject obj)
    {
        var sb = new StringBuilder();
        sb.Append("{");

        for (var i = 0; i < obj.Sections.Count; i++)
        {
            var section = obj.Sections[i];
            sb.Append($"\n\t\"{section.Name}\": {{\n");

            for (var j = 0; j < section.Nodes.Count; j++)
            {
                var node = section.Nodes[j];
                sb.Append($"\t    \"{node.Name}\": ");

                switch (node.Type)
                {
                    case AsonNodeType.String:
                        sb.Append($"\"{node.Value}\"");
                        break;
                    case AsonNodeType.Int:
                        sb.Append($"{node.Value}");
                        break;
                    case AsonNodeType.Float:
                        sb.Append($"{node.Value}");
                        break;
                    case AsonNodeType.Bool:
                        sb.Append($"{node.Value.ToString().ToLower()}");
                        break;
                    case AsonNodeType.Null:
                        sb.Append($"null");
                        break;
                    case AsonNodeType.StringArray:
                        sb.Append("[\n");
                        var strArray = (string[]) node.Value;
                        for (var k = 0; k < strArray.Length; k++)
                        {
                            sb.Append($"\t\t\"{strArray[k]}\"");
                            if (k < strArray.Length - 1)
                                sb.Append(",\n");
                        }

                        sb.Append("\n\t    ]");
                        break;
                    case AsonNodeType.IntArray:
                        sb.Append("[\n");
                        var intArray = (int[]) node.Value;
                        for (var k = 0; k < intArray.Length; k++)
                        {
                            sb.Append($"\t\t{intArray[k]}");
                            if (k < intArray.Length - 1)
                                sb.Append(",\n");
                        }

                        sb.Append("\n\t    ]");
                        break;
                    case AsonNodeType.FloatArray:
                        sb.Append("[\n");
                        var floatArray = (float[]) node.Value;
                        for (var k = 0; k < floatArray.Length; k++)
                        {
                            sb.Append($"\t\t{floatArray[k]}");
                            if (k < floatArray.Length - 1)
                                sb.Append(",\n");
                        }

                        sb.Append("\n\t    ]");
                        break;
                    case AsonNodeType.BoolArray:
                        sb.Append("[\n");
                        var boolArray = (bool[]) node.Value;
                        for (var k = 0; k < boolArray.Length; k++)
                        {
                            sb.Append($"\t\t{boolArray[k].ToString().ToLower()}");
                            if (k < boolArray.Length - 1)
                                sb.Append(",\n");
                        }

                        sb.Append("\n\t    ]");
                        break;
                }

                if (j < section.Nodes.Count - 1)
                    sb.Append(",\n");
            }

            sb.Append("\n\t}");

            if (i < obj.Sections.Count - 1)
                sb.Append(",");
        }
        sb.Append("\n}");
        return sb.ToString();
    }
}