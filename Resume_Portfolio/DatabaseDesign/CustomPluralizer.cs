using Resume_Portfolio.Interfaces;

namespace Resume_Portfolio.DatabaseDesign;

public class CustomPluralizer : IPluralizer
{

    public string Pluralize(string name)
    {

        // Simple pluralization rules for demonstration purposes
        if (string.IsNullOrEmpty(name))
            return name;

        if (name.EndsWith("s") || name.EndsWith("x") || name.EndsWith("z"))
            return name + "es";

        if (name.EndsWith("y"))
            return name.Remove(name.Length - 1) + "ies";

        return name + "s";
    }

    public string Singularize(string name)
    {

        // Simple singularization rules for demonstration purposes
        if (string.IsNullOrEmpty(name))
            return name;

        if (name.EndsWith("es") && name.Length > 2)
            return name.Remove(name.Length - 2);

        if (name.EndsWith("ies"))
            return name.Remove(name.Length - 3) + "y";

        if (name.EndsWith("s"))
            return name.Remove(name.Length - 1);

        return name;
    }
}
