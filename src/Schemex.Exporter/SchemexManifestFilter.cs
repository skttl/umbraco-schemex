using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.Schemex.Exporter
{
    internal class SchemexManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            var assembly = typeof(SchemexManifestFilter).Assembly;

            manifests.Add(new PackageManifest
            {
                PackageName = "Schemex.Exporter",
                Version = assembly.GetName()?.Version?.ToString(3) ?? "0.1.0",
                AllowPackageTelemetry = true,
                Scripts = new string[] {
                    // List any Script files
                    // Urls should start '/App_Plugins/Schemex/' not '/wwwroot/Schemex/', e.g.
                    // "/App_Plugins/Schemex/Scripts/scripts.js"
                },
                Stylesheets = new string[]
                {
                    // List any Stylesheet files
                    // Urls should start '/App_Plugins/Schemex/' not '/wwwroot/Schemex/', e.g.
                    // "/App_Plugins/Schemex/Styles/styles.css"
                }
            });
        }
    }
}
