namespace Farm.Utility;

public class Utils
{
    private static readonly float BAD_DOUBLE = float.MinValue;
    private static readonly string BAD_Color = string.Empty;

    public static float ConvertInputFloat(string value)
    {
        if (value is null) return BAD_DOUBLE;
        else
        {
            if (float.TryParse(value, out float f)) return f;
            else return BAD_DOUBLE;
        }
    }

    public static string ConvertInputColor(string value)
    {
        if (value is null) return string.Empty;
        else
        {
            if (Color.TryParse(value, out Color color)) return color.AsPaint().ToString();
            else return BAD_Color;
        }
    }
}
