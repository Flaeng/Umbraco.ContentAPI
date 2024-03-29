using System;

namespace Flaeng.Umbraco.ContentAPI.Options;

public interface IFluentContentApiOptions
{
    // IContentApiOptions ExposeMedia();
    // IContentApiOptions ExposeMembers();
    // IContentApiOptions ExposeMemberGroups();
    // IContentApiOptions ExposeUsers();
    // IContentApiOptions ExposeForms();
    // IContentApiOptions ExposeTranslationDictionary();
    IContentApiOptions UseCachingWithTimeout(TimeSpan? timeout);
    IContentApiOptions UseSlidingCachingWithTimeout(TimeSpan? timeout);
    IContentApiOptions UseAbsolutePathInLinks();
    IContentApiOptions IgnoreLinks();
    // IContentApiOptions UseJwtWithPrivateKey(string privateKey);
    // IAllowCrudOperation AllowCreationOf(string contentTypeAlias);
    // IAllowCrudOperation AllowEditingOf(string contentTypeAlias);
    // IAllowCrudOperation AllowDeletionOf(string contentTypeAlias);
}
public partial class ContentApiOptions : IFluentContentApiOptions//, IAllowCrudOperation
{
    #region ExposeUmbracoFeature
    // public IContentApiOptions ExposeMedia()
    // {
    //     UmbracoOptions.ExposeMedia = true;
    //     return this;
    // }

    // public IContentApiOptions ExposeMembers()
    // {
    //     UmbracoOptions.ExposeMembers = true;
    //     return this;
    // }

    // public IContentApiOptions ExposeMemberGroups()
    // {
    //     UmbracoOptions.ExposeMemberGroups = true;
    //     return this;
    // }

    // public IContentApiOptions ExposeUsers()
    // {
    //     UmbracoOptions.ExposeUsers = true;
    //     return this;
    // }

    // public IContentApiOptions ExposeForms()
    // {
    //     UmbracoOptions.ExposeForms = true;
    //     return this;
    // }

    // public IContentApiOptions ExposeTranslationDictionary()
    // {
    //     UmbracoOptions.ExposeTranslationDictionary = true;
    //     return this;
    // }
    #endregion

    #region Caching
    public IContentApiOptions UseCachingWithTimeout(TimeSpan? timeout)
    {
        EnableCaching = true;
        SlidingCache = false;
        CacheTimeout = timeout;
        return this;
    }
    public IContentApiOptions UseSlidingCachingWithTimeout(TimeSpan? timeout)
    {
        EnableCaching = true;
        SlidingCache = true;
        CacheTimeout = timeout;
        return this;
    }
    #endregion

    public IContentApiOptions IgnoreLinks()
    {
        this.HideLinks = true;
        return this;
    }

    public IContentApiOptions UseAbsolutePathInLinks()
    {
        this.PrependAbsolutePathToLinks = true;
        return this;
    }

    // public IContentApiOptions UseJwtWithPrivateKey(string privateKey)
    // {
    //     this.JwtPrivateKey = privateKey;
    //     return this;
    // }

    #region ContentTypeCrud
    // ContentTypeCrudOption LastCreatedCrudOption = null;
    // public IAllowCrudOperation AllowCreationOf(string contentTypeAlias)
    // {
    //     LastCreatedCrudOption = new ContentTypeCrudOption
    //     {
    //         ContentTypeAlias = contentTypeAlias
    //     };
    //     CreationOptions.Add(LastCreatedCrudOption);
    //     return this;
    // }
    // public IAllowCrudOperation AllowEditingOf(string contentTypeAlias)
    // {
    //     LastCreatedCrudOption = new ContentTypeCrudOption
    //     {
    //         ContentTypeAlias = contentTypeAlias
    //     };
    //     EditingOptions.Add(LastCreatedCrudOption);
    //     return this;
    // }
    // public IAllowCrudOperation AllowDeletionOf(string contentTypeAlias)
    // {
    //     LastCreatedCrudOption = new ContentTypeCrudOption
    //     {
    //         ContentTypeAlias = contentTypeAlias
    //     };
    //     DeletionOptions.Add(LastCreatedCrudOption);
    //     return this;
    // }
    // public IAllowCrudOperation If(Func<ICrudRequest, bool> condition)
    // {
    //     LastCreatedCrudOption.Authorize = condition;
    //     return this;
    // }
    #endregion
}