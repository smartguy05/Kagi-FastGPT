# Kagi GPT

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