namespace twittericons;

public static class IconFactory
{
    public static IconType DefaultIconType = IconType.QuestionCircle;
    public static IconStyle DefaultIconStyle = IconStyle.Outline;

    public static Type DefaultIcon = typeof(Outline.QuestionCircleIcon);

    public static Type GetIconType(IconType type, IconStyle style)
    {
        var typeName = $"{typeof(Icon).Namespace}.{style}.{type}Icon";
        var iconType = System.Type.GetType(typeName, false, true);
        return iconType ?? DefaultIcon;
    }
}
