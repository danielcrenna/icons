using System.Globalization;
using System.Text;

var iconTypes = new HashSet<string>();
await GenerateIconComponentsAsync(iconTypes, @"..\\..\\..\\..\\..\\icons", @"..\\..\\..\\..\\..\\blazor\\twittericons");
await GenerateIconTypes(iconTypes);
return;

async Task GenerateIconComponentsAsync(ISet<string> typeSet, string sourceDir, string outputDir)
{
    foreach (var filePath in Directory.EnumerateFiles(sourceDir))
    {
        var filename = Path.GetFileNameWithoutExtension(filePath);

        var iconType = ConvertToTitleCase(filename).Replace("Fill", "")
            .Replace("0", "Zero")
            .Replace("1", "One")
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eight")
            .Replace("9", "Nine")
            ;

        typeSet.Add(iconType);

        var style = filename.Contains("-fill") ? "Solid" : "Outline";

        Console.WriteLine(iconType + style);

        var sb = new StringBuilder();
        sb.AppendLine($"@namespace twittericons.{style}");
        sb.AppendLine("@inherits Icon");

        var svg = await File.ReadAllTextAsync(filePath);
        svg = svg.Replace("<svg xmlns=\"http://www.w3.org/2000/svg\"", "<svg xmlns=\"http://www.w3.org/2000/svg\" @attributes=\"@AdditionalAttributes\"");
        sb.AppendLine(svg);

        var component = sb.ToString();
        var componentName = $"{iconType}Icon.razor";
        var subDir = style.ToLowerInvariant();

        await File.WriteAllTextAsync(Path.Combine(outputDir, subDir, componentName), component);
    }
}

async Task GenerateIconTypes(HashSet<string> hashSet)
{
    var sb = new StringBuilder();
    sb.AppendLine("namespace twittericons;");
    sb.AppendLine();
    sb.AppendLine("public enum IconType");
    sb.AppendLine("{");
    foreach (var iconType in hashSet)
    {
        sb.Append("    ");
        sb.Append(iconType);
        sb.Append(',');
        sb.AppendLine();
    }

    sb.AppendLine("}");
    await File.WriteAllTextAsync(@"..\\..\\..\\..\\..\\blazor\\twittericons\\IconType.cs", sb.ToString());
}

static string ConvertToTitleCase(string input)
{
    var textInfo = new CultureInfo("en-US", false).TextInfo;
    var replaced = input.Replace('-', ' ');
    var titleCased = textInfo.ToTitleCase(replaced);
    return titleCased.Replace(" ", "");
}
