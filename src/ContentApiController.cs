using System;

using Flaeng.Umbraco.ContentAPI.Handlers;
using Flaeng.Umbraco.ContentAPI.Models;
using Flaeng.Umbraco.ContentAPI.Options;
using Flaeng.Umbraco.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Web.Common.Controllers;

namespace Flaeng.Umbraco.ContentAPI;

[ApiController, Route("api/contentapi")]
public class ContentApiController : UmbracoApiController
{
    protected readonly ILogger<ContentApiController> logger;
    protected readonly AppCaches cache;
    protected readonly IContentApiOptions options;
    protected readonly IResponseBuilder responseBuilder;
    protected readonly IRequestInterpreter requestInterpreter;

    public record BadRequestResponse(string error, string message);
    protected string Culture { get => Request.Headers.ContentLanguage.ToString(); }

    public ContentApiController(
            ILogger<ContentApiController> logger,
            AppCaches cache,
            IOptions<ContentApiOptions> options,
            IResponseBuilder responseBuilder,
            IRequestInterpreter requestInterpreter
            )
    {
        this.logger = logger;
        this.cache = cache;
        this.options = options.Value;
        this.responseBuilder = responseBuilder;
        this.requestInterpreter = requestInterpreter;
    }

    public override OkObjectResult Ok([ActionResultObjectValue] object value)
    {
        var result = new OkObjectResult(value);
        result.ContentTypes.Clear();
        result.ContentTypes.Add("application/hal+json");
        return result;
    }

    [HttpGet, Route("{*path}")]
    public ActionResult<ILinksContainer> Get(string path)
    {
        try
        {
            ILinksContainer result = options.EnableCaching
                ? cache.RuntimeCache.Get<ILinksContainer>(
                        key: $"{Culture}_{path}",
                        factory: () => GetResult(path),
                        timeout: options.CacheTimeout,
                        isSliding: options.SlidingCache)
                : GetResult(path);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (HalException e)
        {
            logger.LogInformation(e, "ContentAPI request returned non-successful response");
            return BadRequest(new BadRequestResponse(e.SystemMessage, e.Message));
        }
        catch (Exception e)
        {
            logger.LogInformation(e, "ContentAPI request returned non-successful response");
            return StatusCode(500);
        }
    }

    private ILinksContainer GetResult(string path)
    {
        var result = requestInterpreter.Interprete(path);

        switch (result)
        {
            case RootInterpreterResult rir:
                return responseBuilder.Build(rir);

            case Handlers.ObjectInterpreterResult oir:
                return oir.Value == null ? null : responseBuilder.Build(oir);

            case CollectionInterpreterResult cir:
                return responseBuilder.Build(cir);

            case null:
                return null;

            default:
                throw new Exception("Unknown interpretor result");
        }
    }

}