using System;

namespace Flaeng.Umbraco.ContentAPI.Options;

public interface IContentApiOptions : IFluentContentApiOptions
{
    static string ConfigurationName = "ContentApiOptions";
    bool EnableCaching { get; set; }
    TimeSpan? CacheTimeout { get; set; }
    bool SlidingCache { get; set; }
    bool HideLinks { get; set; }
    bool PrependAbsolutePathToLinks { get; set; }
    // string JwtPrivateKey { get; set; }
    // UmbracoOptions UmbracoOptions { get; }
    // CrudOptions CreationOptions { get; }
    // CrudOptions EditingOptions { get; }
    // CrudOptions DeletionOptions { get; }
}
public partial class ContentApiOptions : IContentApiOptions
{
    public bool EnableCaching { get; set; }
    public TimeSpan? CacheTimeout { get; set; }
    public bool SlidingCache { get; set; }

    public bool HideLinks { get; set; }
    public bool PrependAbsolutePathToLinks { get; set; }
    // public string JwtPrivateKey { get; set; }

    // public UmbracoOptions UmbracoOptions { get; } = new UmbracoOptions();

    // public CrudOptions CreationOptions { get; } = new CrudOptions();
    // public CrudOptions EditingOptions { get; } = new CrudOptions();
    // public CrudOptions DeletionOptions { get; } = new CrudOptions();
}