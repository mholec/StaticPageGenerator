[![Build status](https://mholec.visualstudio.com/DEV/_apis/build/status/NuGet%20Packages/StaticPageGenerator)](https://mholec.visualstudio.com/DEV/_build/latest?definitionId=53)

# Static Page Generator
Tento projekt umí vygenerovat statické webové stránky na základě předdefinované struktury. Projekt je uvolněn i v podobě NuGet balíčku a lze jím nainstalovat tzv. [global tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools).

## Instalace 

NuGet balíček je [dostupný v repositáři nuget.org](https://www.nuget.org/packages/StaticPageGenerator/).


```c#
dotnet tool install --global StaticPageGenerator --version 1.2.1
```

Po instalaci globálního nástroje lze zavolat generátor z příkazové řádky pomocí:

```c#
spg c:\cesta-k-adresari
```

Pokud se má generování provádět periodicky (do nekonečna), lze volat nástroj s parametrem **r** (repeat):

```c#
spg c:\cesta-k-adresari r
```

## Struktura adresáře
- **assets**
  - css
  - img
  - js
  - less
  - lib
- **includes**
- **layouts**
  - layout.html
- **pages**
  - index.md
  - page.html
- **posts**

Pozn.: posts aktuálně nejsou podporovány. Lze generovat jen statické pages.

# Algoritmus generátoru

1. Najde cestu ke všem souborům (PathFinder)
2. Načte obsah všech souborů (ContentLoader)
3. Sestaví instanci třídy s obsahem všech souborů (ContentHolder)
4. Sestaví všechny soubory do podoby HTML (ContentHolder.Build())
5. Uloží seskládané HTML stránky do složky **_site** ve stejném adresáři
6. Provede kompilaci assetů (pouze less soubory)
7. Vykopíruje assety do výstupní složky **_site**

## Systémové proměnné

Lze použít systémové proměnné, které jsou při generování dynamicky nahrazeny.

- {{system.date}} - aktuální datum
- {{system.datetime}} - aktuální datum a čas
- {{system.spg.version}} - aktuální verze generátoru SPG

