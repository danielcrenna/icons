using Microsoft.AspNetCore.Components;

namespace twittericons;

public abstract class Icon : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object?> AdditionalAttributes { get; set; } = new();
}
