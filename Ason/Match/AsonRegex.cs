using System.Text.RegularExpressions;

namespace Ason.Ason.Match;

public partial class AsonRegex
{
    [GeneratedRegex(@"\{ ""(.*)"" \}", RegexOptions.Compiled)]
    public static partial Regex SectionRegex();

    [GeneratedRegex(@"str -> ""(.*)"": ""(.*)""", RegexOptions.Compiled)]
    public static partial Regex StringRegex();

    [GeneratedRegex(@"str\[\] -> ""(.*)"": \[([\s\S]*?)\]", RegexOptions.Compiled)]
    public static partial Regex StringArrayRegex();

    [GeneratedRegex(@"int -> ""(.*)"": (\d+)", RegexOptions.Compiled)]
    public static partial Regex IntRegex();

    [GeneratedRegex(@"int\[\] -> ""(.*)"": \[([\s\S]*?)\]", RegexOptions.Compiled)]
    public static partial Regex IntArrayRegex();

    [GeneratedRegex(@"flt -> ""(.*)"": (\d+(\.\d+)?)", RegexOptions.Compiled)]
    public static partial Regex FloatRegex();

    [GeneratedRegex(@"flt\[\] -> ""(.*)"": \[([\s\S]*?)\]", RegexOptions.Compiled)]
    public static partial Regex FloatArrayRegex();

    [GeneratedRegex(@"bool -> ""(.*)"": (true|false)", RegexOptions.Compiled)]
    public static partial Regex BoolRegex();

    [GeneratedRegex(@"bool\[\] -> ""(.*)"": \[([\s\S]*?)\]", RegexOptions.Compiled)]
    public static partial Regex BoolArrayRegex();

    [GeneratedRegex(@"null -> ""(.*)"": null", RegexOptions.Compiled)]
    public static partial Regex NullRegex();
}