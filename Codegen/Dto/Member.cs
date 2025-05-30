namespace Codegen.Dto;

public class Member(string name, string clrType, string annotation)
{
    public string Name { get; set; } = name;
    public string ClrType { get; set; } = clrType;
    public string Annotation { get; set; } = annotation;

    public override string ToString()
    {
        return $"{Annotation}{ClrType} {Name}";
    }
}