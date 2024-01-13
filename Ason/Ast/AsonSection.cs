namespace Ason.Ason.Ast;

public class AsonSection
{
    public string Name { get; set; }
    public List<AsonNode> Nodes { get; set; } = [];
}