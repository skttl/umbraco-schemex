# Schemex.Importer

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.Schemex.Importer?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.Schemex.Importer/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.Schemex.Importer?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.Schemex.Importer)
[![GitHub license](https://img.shields.io/github/license/skttl/umbraco-schemex?color=8AB803)](../LICENSE)

Package for importing all schema data and content from an Umbraco project running [Schemex.Exporter](https://www.nuget.org/packages/Umbraco.Community.Schemex.Importer). Use it as a baseline installer, instead of having to manage a Starter Kit package.

The package is installed in your destination project, eg. a new blank project you've created, where you want to import your default schema setup into.

Hit the `/umbraco/schemex/import/push?source=[hostname]` endpoint on the destination project to trigger the import. In the `source` parameter, you define the hostname of the Umbraco project containing the Exporter package. Eg. `baseline.skttl.dk`.

The importer will then download the package.xml of the source site, and import it into the destination project. For safety reasons, the import will not run if the destination project already contains Content Types. But you can override this by adding `&force=true` to the import endpoint url.