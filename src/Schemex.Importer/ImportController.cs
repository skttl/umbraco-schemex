using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Umbraco.Community.Schemex.Importer;

[PluginController("Schemex")]
public class ImportController : UmbracoApiController
{
    private readonly IPackagingService _packagingService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IContentTypeService _contentTypeService;

    public ImportController(IPackagingService packagingService, IHttpClientFactory httpClientFactory, IContentTypeService contentTypeService)
    {
        _packagingService = packagingService;
        _httpClientFactory = httpClientFactory;
        _contentTypeService = contentTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> Push(string source, bool force = false)
    {
        if (force == false && _contentTypeService.GetAll().Any())
        {
            throw new Exception("This project already has Content Types - to continue add &force=true to the url");
        }

        var client = _httpClientFactory.CreateClient("SchemaImporter");
        client.BaseAddress = new Uri(source.EnsureStartsWith("https://"));
        var packageString = await client.GetStringAsync("/umbraco/schemex/export/get");
        var packageXml = XDocument.Parse(packageString);
        _packagingService.InstallCompiledPackageData(packageXml);

        return Ok("Schema installed from " + source);
    }
}
