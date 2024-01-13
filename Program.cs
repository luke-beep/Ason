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
        var asonObject = new AsonObject();

        asonObject.Sections.Add(new AsonSection
        {
            Name = "Normal",
            Nodes =
            [
                new AsonNode
                {
                    Name = "String",
                    Type = AsonNodeType.String,
                    Value = "String"
                },
                new AsonNode
                {
                    Name = "Integer",
                    Type = AsonNodeType.Int,
                    Value = 2
                },
                new AsonNode
                {
                    Name = "Float",
                    Type = AsonNodeType.Float,
                    Value = 3.14f
                },
                new AsonNode
                {
                    Name = "Boolean",
                    Type = AsonNodeType.Bool,
                    Value = true
                },
                new AsonNode
                {
                    Name = "Null",
                    Type = AsonNodeType.Null,
                    Value = null
                }
            ]
        });

        asonObject.Sections.Add(new AsonSection
        {
            Name = "Array",
            Nodes =
            [
                new AsonNode
                {
                    Name = "StringArray",
                    Type = AsonNodeType.StringArray,
                    Value = new[]
                    {
                        "String1",
                        "String2",
                        "String3"
                    }
                },

                new AsonNode
                {
                    Name = "IntegerArray",
                    Type = AsonNodeType.IntArray,
                    Value = new[]
                    {
                        1,
                        2,
                        3
                    }
                },

                new AsonNode
                {
                    Name = "FloatArray",
                    Type = AsonNodeType.FloatArray,
                    Value = new[]
                    {
                        1.1f,
                        2.2f,
                        3.3f
                    }
                },

                new AsonNode
                {
                    Name = "BooleanArray",
                    Type = AsonNodeType.BoolArray,
                    Value = new[]
                    {
                        true,
                        false,
                        true
                    }
                }
            ]
        });

        var unparsed = ason.Unparse(asonObject);
        Console.WriteLine(unparsed);

        var parsed = ason.Parse(unparsed);
        foreach (var section in parsed.Sections)
        {
            Console.WriteLine($"Section - {section.Name}");
            foreach (var node in section.Nodes)
            {
                switch (node.Type)
                {
                    case AsonNodeType.String:
                        Console.WriteLine($"{node.Name}: {node.Value}");
                        break;
                    case AsonNodeType.StringArray:
                        Console.WriteLine($"{node.Name}: {string.Join(", ", (string[])node.Value)}");
                        break;
                    case AsonNodeType.Int:
                        Console.WriteLine($"{node.Name}: {node.Value}");
                        break;
                    case AsonNodeType.IntArray:
                        Console.WriteLine($"{node.Name}: {string.Join(", ", (int[])node.Value)}");
                        break;
                    case AsonNodeType.Float:
                        Console.WriteLine($"{node.Name}: {node.Value}");
                        break;
                    case AsonNodeType.FloatArray:
                        Console.WriteLine($"{node.Name}: {string.Join(", ", (float[])node.Value)}");
                        break;
                    case AsonNodeType.Bool:
                        Console.WriteLine($"{node.Name}: {node.Value}");
                        break;
                    case AsonNodeType.BoolArray:
                        Console.WriteLine($"{node.Name}: {string.Join(", ", (bool[])node.Value)}");
                        break;
                    case AsonNodeType.Null:
                        Console.WriteLine($"{node.Name}: {node.Value}");
                        break;
                    default:
                        throw new AsonException();
                }
            }
        }
    }
}