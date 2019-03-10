	dotnet new console

Use C# 7.1

See .csproj and LangVersion

	<Project Sdk="Microsoft.NET.Sdk">
	
	  <PropertyGroup>
	    <OutputType>Exe</OutputType>
	    <TargetFramework>netcoreapp2.2</TargetFramework>
	    <LangVersion>latest</LangVersion>
	  </PropertyGroup>
	
	</Project>
