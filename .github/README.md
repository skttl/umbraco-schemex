# Schemex

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.Schemex.Exporter?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.Schemex.Exporter/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.Schemex.Exporter?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.Schemex.Exporter)
[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.Schemex.Importer?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.Schemex.Importer/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.Schemex.Importer?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.Schemex.Importer)
[![GitHub license](https://img.shields.io/github/license/skttl/umbraco-schemex?color=8AB803)](../LICENSE)

Umbraco Package for exporting and importing all schema data and content from one project to another. Use it as a baseline installer, instead of having to manage a Starter Kit package.

## Exporter

The [exporter package](https://www.nuget.org/packages/Umbraco.Community.Schemex.Exporter) is installed in your source project, where you keep your baseline schema data. It exposes an endpoint (`/umbraco/schemex/export/get`) that returns a `package.xml` file containing all Content Types, Media Types, Data Types, Stylesheets, Templates, Dictionary Items, and the first content node and its descendants.

This file can either be used in a [starter kit package](https://docs.umbraco.com/umbraco-cms/extending/packages#starter-kits) made for Umbraco, or imported with a package like [Dragonfly Schema Importer](https://dragonflylibraries.com/umbraco-packages/schema-importer/). Or you can use the Importer package of Schemex as described below.

Note. This package essentially exposes all of your schema data publicly. Don't install this package on a sensitive Umbraco project, or make sure to delete it, once you don't need it anymore!

## Importer
The [importer package](https://www.nuget.org/packages/Umbraco.Community.Schemex.Importer) is installed in your destination project, eg. a new blank project you've created, where you want to import your default schema setup into.

Hit the `/umbraco/schemex/import/push?source=[hostname]` endpoint on the destination project to trigger the import. In the `source` parameter, you define the hostname of the Umbraco project containing the Exporter package. Eg. `baseline.skttl.dk`.

The importer will then download the package.xml of the source site, and import it into the destination project. For safety reasons, the import will not run if the destination project already contains Content Types. But you can override this by adding `&force=true` to the import endpoint url.

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).

## Acknowledgments

[Lotte Pitcher](https://github.com/lottepitcher) - for the [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter), you can create an Umbraoc package in 5 minutes with this!
[Heather Floyd](https://github.com/hfloyd) - for the [Dragonfly Schema Importer](https://github.com/hfloyd/Dragonfly.Umbraco10.SchemaImporter) package, which was a huge inspiration for this.