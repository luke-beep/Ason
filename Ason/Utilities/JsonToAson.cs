using System.Text.Json;
using System.Text.Json.Serialization;
using Ason.Ason.Ast;
using Ason.Ason.Enums;

namespace Ason.Ason.Utilities;

public class JsonToAson
{
    public AsonObject Parse(string json)
    {
        var ason = new AsonObject();
        var sections = new List<AsonSection>();

        JsonDocument doc = JsonDocument.Parse(json);

        foreach (JsonProperty property in doc.RootElement.EnumerateObject())
        {
            var section = new AsonSection
            {
                Name = property.Name
            };

            var nodes = new List<AsonNode>();

            foreach (JsonProperty node in property.Value.EnumerateObject())
            {
                var asonNode = new AsonNode
                {
                    Name = node.Name
                };

                switch (node.Value.ValueKind)
                {
                    case JsonValueKind.String:
                        asonNode.Type = AsonNodeType.String;
                        asonNode.Value = node.Value.GetString();
                        break;
                    case JsonValueKind.Number:
                        if (node.Value.TryGetInt32(out var intValue))
                        {
                            asonNode.Type = AsonNodeType.Int;
                            asonNode.Value = intValue;
                        }
                        else if (node.Value.TryGetSingle(out var floatValue))
                        {
                            asonNode.Type = AsonNodeType.Float;
                            asonNode.Value = floatValue;
                        }

                        break;
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        asonNode.Type = AsonNodeType.Bool;
                        asonNode.Value = node.Value.GetBoolean();
                        break;
                    case JsonValueKind.Null:
                        asonNode.Type = AsonNodeType.Null;
                        asonNode.Value = null;
                        break;
                    case JsonValueKind.Array:
                        switch (node.Value[0].ValueKind)
                        {
                            case JsonValueKind.String:
                                asonNode.Type = AsonNodeType.StringArray;
                                var strArray = new string[node.Value.GetArrayLength()];
                                for (var i = 0; i < node.Value.GetArrayLength(); i++)
                                {
                                    strArray[i] = node.Value[i].GetString();
                                }

                                asonNode.Value = strArray;
                                break;
                            case JsonValueKind.Number:
                                if (node.Value[0].TryGetInt32(out var intArrayValue))
                                {
                                    asonNode.Type = AsonNodeType.IntArray;
                                    var intArray = new int[node.Value.GetArrayLength()];
                                    for (var i = 0; i < node.Value.GetArrayLength(); i++)
                                    {
                                        intArray[i] = node.Value[i].GetInt32();
                                    }

                                    asonNode.Value = intArray;
                                }
                                else if (node.Value[0].TryGetSingle(out var floatArrayValue))
                                {
                                    asonNode.Type = AsonNodeType.FloatArray;
                                    var floatArray = new float[node.Value.GetArrayLength()];
                                    for (var i = 0; i < node.Value.GetArrayLength(); i++)
                                    {
                                        floatArray[i] = node.Value[i].GetSingle();
                                    }

                                    asonNode.Value = floatArray;
                                }

                                break;
                            case JsonValueKind.True:
                            case JsonValueKind.False:
                                asonNode.Type = AsonNodeType.BoolArray;
                                var boolArray = new bool[node.Value.GetArrayLength()];
                                for (var i = 0; i < node.Value.GetArrayLength(); i++)
                                {
                                    boolArray[i] = node.Value[i].GetBoolean();
                                }

                                asonNode.Value = boolArray;
                                break;
                        }
                        break;
                }
                nodes.Add(asonNode);
            }
            section.Nodes = nodes;
            sections.Add(section);
        }
        ason.Sections = sections;
        return ason;
    }
}
