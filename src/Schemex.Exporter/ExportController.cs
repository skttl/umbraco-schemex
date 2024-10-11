using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Packaging;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Umbraco.Community.Schemex.Exporter;

[PluginController("Schemex")]
public class ExportController : UmbracoApiController
{
    private readonly IContentService _contentService;
    private readonly IContentTypeService _contentTypeService;
    private readonly IDataTypeService _dataTypeService;
    private readonly ILocalizationService _localizationService;
    private readonly IMediaTypeService _mediaTypeService;
    private readonly IFileService _fileService;
    private readonly IPackagingService _packagingService;

    public ExportController(
        IContentService contentService,
        IContentTypeService contentTypeService,
        IDataTypeService dataTypeService,
        ILocalizationService localizationService,
        IMediaTypeService mediaTypeService,
        IFileService fileService,
        IPackagingService packagingService)
    {
        _contentService = contentService;
        _contentTypeService = contentTypeService;
        _dataTypeService = dataTypeService;
        _localizationService = localizationService;
        _mediaTypeService = mediaTypeService;
        _fileService = fileService;
        _packagingService = packagingService;
    }


    [HttpGet]
    public IActionResult Get()
    {
        var packageDefinition = new PackageDefinition
        {
            ContentNodeId = _contentService.GetRootContent().FirstOrDefault()?.Id.ToString(),
            ContentLoadChildNodes = true,
            DataTypes = _dataTypeService.GetAll().Select(x => x.Id.ToString()).ToList(),
            DocumentTypes = _contentTypeService.GetAll().Select(x => x.Id.ToString()).ToList(),
            MediaTypes = _mediaTypeService.GetAll().Select(x => x.Id.ToString()).ToList(),
            Stylesheets = _fileService.GetStylesheets().Select(x => x.Id.ToString()).ToList(),
            Templates = _fileService.GetTemplates().Select(x => x.Id.ToString()).ToList()
        };

        foreach (var dictionaryItem in _localizationService.GetRootDictionaryItems())
        {
            packageDefinition.DictionaryItems.Add(dictionaryItem.Id.ToString());

            var descendants = _localizationService.GetDictionaryItemDescendants(dictionaryItem.Key).Select(x => x.Id.ToString()).ToList();
            foreach (var descendant in descendants)
            {
                packageDefinition.DictionaryItems.Add(descendant);
            }
        }

        packageDefinition.PackageId = Guid.NewGuid();
        packageDefinition.Name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name ?? packageDefinition.PackageId.ToString();
        packageDefinition.Id = 1;

        var fileName = _packagingService.ExportCreatedPackage(packageDefinition);
        var file = System.IO.File.ReadAllBytes(fileName);
        return File(file, "text/xml", "package.xml");
    }

}
