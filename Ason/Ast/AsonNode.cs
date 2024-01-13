using Ason.Ason.Enums;

namespace Ason.Ason.Ast;

public class AsonNode
{
    public AsonNodeType Type { get; set; }
    public string Name { get; set; }
    public object Value { get; set; } = new();
}