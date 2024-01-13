using System.Text;
using Ason.Ason.Ast;
using Ason.Ason.Enums;
using Ason.Ason.Utilities;
using System.Text.RegularExpressions;

using static Ason.Ason.Match.AsonRegex;

namespace Ason.Ason;

public class Ason(AsonFormatting formatting = AsonFormatting.None, char delimiter = ';')
{
    private static readonly Regex SectionPattern = SectionRegex();
    private static readonly Regex StringPattern = StringRegex();
    private static readonly Regex StringArrayPattern = StringArrayRegex();
    private static readonly Regex IntPattern = IntRegex();
    private static readonly Regex IntArrayPattern = IntArrayRegex();
    private static readonly Regex FloatPattern = FloatRegex();
    private static readonly Regex FloatArrayPattern = FloatArrayRegex();
    private static readonly Regex BoolPattern = BoolRegex();
    private static readonly Regex BoolArrayPattern = BoolArrayRegex();
    private static readonly Regex NullPattern = NullRegex();

    public AsonObject Parse(string input)
    {
        var sections = new List<AsonSection>();
        var lines = input.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

        AsonSection? currentSection = null;

        foreach (var line in lines)
        {
            var sectionMatch = SectionPattern.Match(line);
            if (sectionMatch.Success)
            {
                currentSection = new AsonSection { Name = sectionMatch.Groups[1].Value, Nodes = [] };
                sections.Add(currentSection);
                continue;
            }

            var stringMatch = StringPattern.Match(line);
            if (stringMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing string value, no section found.");
                currentSection.Nodes.Add(new AsonNode
                {
                    Type = AsonNodeType.String,
                    Name = stringMatch.Groups[1].Value,
                    Value = stringMatch.Groups[2].Value
                });
                continue;
            }

            var intMatch = IntPattern.Match(line);
            if (intMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing int value, no section found.");
                if (int.TryParse(intMatch.Groups[2].Value, out var intValue))
                    currentSection.Nodes.Add(new AsonNode
                    { Type = AsonNodeType.Int, Name = intMatch.Groups[1].Value, Value = intValue });
                else
                    throw new AsonException($"Error parsing int value in section {currentSection.Name}");
                continue;
            }

            var floatMatch = FloatPattern.Match(line);
            if (floatMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing float value, no section found.");
                if (float.TryParse(floatMatch.Groups[2].Value, out var floatValue))
                    currentSection.Nodes.Add(new AsonNode
                    { Type = AsonNodeType.Float, Name = floatMatch.Groups[1].Value, Value = floatValue });
                else
                    throw new AsonException($"Error parsing float value in section {currentSection.Name}");
                continue;
            }

            var boolMatch = BoolPattern.Match(line);
            if (boolMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing boolean value, no section found.");
                if (bool.TryParse(boolMatch.Groups[2].Value, out var boolValue))
                    currentSection.Nodes.Add(new AsonNode
                    { Type = AsonNodeType.Bool, Name = boolMatch.Groups[1].Value, Value = boolValue });
                else
                    throw new AsonException($"Error parsing bool value in section {currentSection.Name}");
                continue;
            }

            var nullMatch = NullPattern.Match(line);
            if (nullMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing null value, no section found.");
                currentSection.Nodes.Add(new AsonNode
                { Type = AsonNodeType.Null, Name = nullMatch.Groups[1].Value, Value = null });
            }

            var stringArrayMatch = StringArrayPattern.Match(line);
            if (stringArrayMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing string array value, no section found.");
                var values = stringArrayMatch.Groups[2].Value
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                currentSection.Nodes.Add(new AsonNode
                { Type = AsonNodeType.StringArray, Name = stringArrayMatch.Groups[1].Value, Value = values });
                continue;
            }

            var intArrayMatch = IntArrayPattern.Match(line);
            if (intArrayMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing int array value, no section found.");
                var values = intArrayMatch.Groups[2].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()).ToArray();
                var intValues = new int[values.Length];
                for (var i = 0; i < values.Length; i++)
                    if (int.TryParse(values[i], out var intValue))
                        intValues[i] = intValue;
                    else
                        throw new AsonException($"Error parsing int value in section {currentSection.Name}");
                currentSection.Nodes.Add(new AsonNode
                { Type = AsonNodeType.IntArray, Name = intArrayMatch.Groups[1].Value, Value = intValues });
                continue;
            }

            var floatArrayMatch = FloatArrayPattern.Match(line);
            if (floatArrayMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing float array value, no section found.");
                var values = floatArrayMatch.Groups[2].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()).ToArray();
                var floatValues = new float[values.Length];
                for (var i = 0; i < values.Length; i++)
                    if (float.TryParse(values[i], out var floatValue))
                        floatValues[i] = floatValue;
                    else
                        throw new AsonException($"Error parsing float value in section {currentSection.Name}");
                currentSection.Nodes.Add(new AsonNode
                { Type = AsonNodeType.FloatArray, Name = floatArrayMatch.Groups[1].Value, Value = floatValues });
                continue;
            }

            var boolArrayMatch = BoolArrayPattern.Match(line);
            if (boolArrayMatch.Success)
            {
                if (currentSection == null)
                    throw new AsonException("Error parsing bool array value, no section found.");
                var values = boolArrayMatch.Groups[2].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()).ToArray();
                var boolValues = new bool[values.Length];
                for (var i = 0; i < values.Length; i++)
                    if (bool.TryParse(values[i], out var boolValue))
                        boolValues[i] = boolValue;
                    else
                        throw new AsonException($"Error parsing bool value in section {currentSection.Name}");
                currentSection.Nodes.Add(new AsonNode
                { Type = AsonNodeType.BoolArray, Name = boolArrayMatch.Groups[1].Value, Value = boolValues });
            }
        }

        return new AsonObject { Sections = sections };
    }

    public string Unparse(AsonObject asonObject)
    {
        var output = new StringBuilder();

        switch (formatting)
        {
            case AsonFormatting.None:
                foreach (var section in asonObject.Sections)
                {
                    output.AppendLine($"{{ \"{section.Name}\" }}{delimiter}");

                    foreach (var node in section.Nodes)
                    {
                        switch (node.Type)
                        {
                            case AsonNodeType.String:
                                output.AppendLine($"str -> \"{node.Name}\": \"{node.Value}\"{delimiter}");
                                break;
                            case AsonNodeType.Int:
                                output.AppendLine($"integer -> \"{node.Name}\": {node.Value}{delimiter}");
                                break;
                            case AsonNodeType.Float:
                                output.AppendLine($"flt -> \"{node.Name}\": {node.Value}{delimiter}");
                                break;
                            case AsonNodeType.Bool:
                                output.AppendLine($"bool -> \"{node.Name}\": {node.Value}{delimiter}");
                                break;
                            case AsonNodeType.Null:
                                output.AppendLine($"null -> \"{node.Name}\": null{delimiter}");
                                break;
                            case AsonNodeType.StringArray:
                                output.AppendLine(
                                    $"str[] -> \"{node.Name}\": [{string.Join(",", ((string[])node.Value).Select(v => $"\"{v}\""))}]{delimiter}");
                                break;
                            case AsonNodeType.IntArray:
                                output.AppendLine(
                                    $"int[] -> \"{node.Name}\": [{string.Join(", ", (int[])node.Value)}]{delimiter}");
                                break;
                            case AsonNodeType.FloatArray:
                                output.AppendLine(
                                    $"flt[] -> \"{node.Name}\": [{string.Join(", ", (float[])node.Value)}]{delimiter}");
                                break;
                            case AsonNodeType.BoolArray:
                                output.AppendLine(
                                    $"bool[] -> \"{node.Name}\": [{string.Join(", ", (bool[])node.Value)}]{delimiter}");
                                break;
                            default:
                                output.AppendLine($"unknown -> \"{node.Name}\": {node.Value}{delimiter}");
                                break;
                        }
                    }
                }

                break;
            case AsonFormatting.Indented:
                foreach (var section in asonObject.Sections)
                {
                    output.AppendLine($"{{ \"{section.Name}\" }}{delimiter}");

                    foreach (var node in section.Nodes)
                    {
                        switch (node.Type)
                        {
                            case AsonNodeType.String:
                                output.AppendLine($"str -> \"{node.Name}\": \"{node.Value}\"{delimiter}");
                                break;
                            case AsonNodeType.Int:
                                output.AppendLine($"integer -> \"{node.Name}\": {node.Value}{delimiter}");
                                break;
                            case AsonNodeType.Float:
                                output.AppendLine($"flt -> \"{node.Name}\": {node.Value}{delimiter}");
                                break;
                            case AsonNodeType.Bool:
                                output.AppendLine($"bool -> \"{node.Name}\": {node.Value}{delimiter}");
                                break;
                            case AsonNodeType.Null:
                                output.AppendLine($"null -> \"{node.Name}\": null{delimiter}");
                                break;
                            case AsonNodeType.StringArray:
                                output.AppendLine(
                                    $"str[] -> \"{node.Name}\": [\n\t{string.Join(",\n\t", ((string[])node.Value).Select(v => $"\"{v}\""))}\n]{delimiter}");
                                break;
                            case AsonNodeType.IntArray:
                                output.AppendLine(
                                    $"int[] -> \"{node.Name}\": [\n\t{string.Join(",\n\t", (int[])node.Value)}\n]{delimiter}");
                                break;
                            case AsonNodeType.FloatArray:
                                output.AppendLine(
                                    $"flt[] -> \"{node.Name}\": [\n\t{string.Join(",\n\t", (float[])node.Value)}\n]{delimiter}");
                                break;
                            case AsonNodeType.BoolArray:
                                output.AppendLine(
                                    $"bool[] -> \"{node.Name}\": [\n\t{string.Join(",\n\t", (bool[])node.Value)}\n]{delimiter}");
                                break;
                            default:
                                output.AppendLine($"unknown -> \"{node.Name}\": {node.Value}{delimiter}");
                                break;
                        }
                    }
                }
                break;
            default:
                throw new AsonException("Formatting entry not found", new Exception("Error"));
        }
        return output.ToString();
    }
}