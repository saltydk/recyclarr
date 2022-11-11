using System.ComponentModel;
using System.Text;

namespace Recyclarr.Cli.Helpers;

[AttributeUsage(AttributeTargets.Property)]
public class EnumDescriptionAttribute<TEnum> : DescriptionAttribute
    where TEnum: Enum
{
    public override string Description { get; }

    public EnumDescriptionAttribute(string description)
    {
        var validValues = string.Join(", ", Enum.GetNames(typeof(TEnum)));
        var str = new StringBuilder(description.Trim());
        str.Append($" (Valid Values: {validValues})");
        Description = str.ToString();
    }
}
