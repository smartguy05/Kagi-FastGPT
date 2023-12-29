# Kagi GPT

## How to Deploy
 - Build exe from IDE
 - Copy contents of Kagi_gpt/bin/Release/net7.0 to your desired directory
 - Add your Kagi API key to the ApiKey property in appsettings.json
 - To access from command line:
   - Add kagi directory to PATH environment variable
   - Update exe to be named Kagi.exe instead of Kagi_pgt.exe

## How to Use
 - Press Windows key (or WinKey + r for run) and type Kagi
 - Enter your query into the console window
 - Your results will display in the console

## Plugins

### Required for plugins

Replace ProjectReference in plugin project .csproj

+ `
<ItemGroup>
    <ProjectReference Include="..\Kagi_gpt_Common\Kagi_gpt_Common.csproj">
        <Private>false</Private>
        <ExcludeAssets>runtime</ExcludeAssets>
    </ProjectReference>
</ItemGroup>
`

Add EnableDynamicLoading to PropertyGroup

+ `<PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <EnableDynamicLoading>true</EnableDynamicLoading>
  </PropertyGroup>`