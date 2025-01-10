using System.IO;

namespace NReferenceManager.Cli.Tests;

public class ReferenceManagerTests
{
    ReferenceManager _Manager;
    string _TestFolder;

    [SetUp]
    public void Setup()
    {
        this._Manager = new ReferenceManager();

        this._TestFolder = Path.Combine(Environment.CurrentDirectory, nameof(ReferenceManagerTests));
        if (!Directory.Exists(this._TestFolder))
            Directory.CreateDirectory(this._TestFolder);

    }

    #region Tests on ParseArguments
    [Test]
    public void ParseArguments_DoesNotThrowEx_IfOptionIsNotManaged()
    {
        //***************** GIVEN
        List<string> args = ["-Not-Managed=blabla"];

        //***************** WHEN
        this._Manager.ParseArguments(args);

        //***************** ASSERT
        Assert.That(_Manager.IsInitialized, Is.False);
        Assert.Pass();
    }

    [Test]
    public void ParseArguments_DoesNotThrowEx_IfSettingsAreNotValid()
    {
        //***************** GIVEN
        List<string> args = [];

        //***************** WHEN
        this._Manager.ParseArguments(args);

        //***************** ASSERT
        Assert.That(_Manager.IsInitialized, Is.False);
        Assert.Pass();
    }

    [Test]
    public void ParseArguments_InitializesManager_IfSettingsAreValid()
    {
        //***************** GIVEN
        List<string> args = ["-r='C:\\Temp\\Project'"];
        this._Manager.ParseArguments(args);
        Assert.That(_Manager.IsInitialized, Is.True);

        //***************** WHEN
        this._Manager.ParseArguments(args);

        //***************** ASSERT
        var result = this._Manager.Run();

        //***************** ASSERT
        Assert.That(result, Is.EqualTo(0));
        Assert.Pass();
    }

    #endregion

    #region Tests on Run

    [Test]
    public void Run_ReturnsMinusTwo_IfIsNotInitialized()
    {
        //***************** GIVEN
        List<string> args = [];
        this._Manager.ParseArguments(args);
        Assert.That(_Manager.IsInitialized, Is.False);

        //***************** WHEN
        var result = this._Manager.Run();

        //***************** ASSERT
        Assert.That(result, Is.EqualTo(-2));
        Assert.Pass();
    }

    [Test]
    public void Run_ReturnsZero_IfJsonFileHasNotBeenFound()
    {
        //***************** GIVEN
        List<string> args = ["-r='C:\\Temp\\Project'"];

        //***************** WHEN
        this._Manager.ParseArguments(args);

        //***************** ASSERT
        Assert.That(_Manager.IsInitialized, Is.True);
        Assert.Pass();
    }

    [Test]
    public void Run_ReturnZero_AndCsProj_Is_Updated()
    {
        //***************** GIVEN
        CreateTestProjFile(Path.Combine(this._TestFolder, "Project1.csproj"));
        CreatePakageJsonFile(Path.Combine(this._TestFolder, "packages.centralized.json"));
        List<string> args = [$"-r='{this._TestFolder}'"];
        this._Manager.ParseArguments(args);

        //***************** WHEN
        var result = this._Manager.Run();

        //***************** ASSERT
        Assert.That(result, Is.EqualTo(0));
        string updated = File.ReadAllText(Path.Combine(this._TestFolder, "Project1.csproj"));
        string expectedFileContent = @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <!-- <Nullable>enable</Nullable> -->
    <GenerateDocumentationFile>True</GenerateDocumentationFile>
  </PropertyGroup>

  <PropertyGroup>
    <Description>Library to implement Domain of Notification</Description>
    <RepositoryType>git</RepositoryType>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""Microsoft.Extensions.DependencyInjection"" Version=""9.0.0"" />
    <PackageReference Include=""Microsoft.Extensions.Logging"" Version=""9.0.0"" />
    <PackageReference Include=""Umbrella.Infrastructure.Firestore"" Version=""2.0.0.2836"" />
  </ItemGroup>

</Project>";
        Assert.That(updated, Is.EqualTo(expectedFileContent));
        Assert.Pass();

    }
    #endregion

    void CreateTestProjFile(string filePath)
    {
        string fileContent = @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <!-- <Nullable>enable</Nullable> -->
    <GenerateDocumentationFile>True</GenerateDocumentationFile>
  </PropertyGroup>

  <PropertyGroup>
    <Description>Library to implement Domain of Notification</Description>
    <RepositoryType>git</RepositoryType>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""Microsoft.Extensions.DependencyInjection"" Version=""7.0.0"" />
    <PackageReference Include=""Microsoft.Extensions.Logging"" Version=""7.0.0"" />
    <PackageReference Include=""Umbrella.Infrastructure.Firestore"" Version=""2.0.0.2836"" />
  </ItemGroup>

</Project>";
        File.WriteAllText(filePath, fileContent);
    }

    void CreatePakageJsonFile(string filePath)
    {
        string fileContent = @"{
  ""References"": [
        { ""PackageName"": ""Microsoft.Extensions.DependencyInjection"", ""PackageVersion"": ""9.0.0"" },
        { ""PackageName"": ""Microsoft.Extensions.Logging"", ""PackageVersion"": ""9.0.0"" }
    ]
}";
        File.WriteAllText(filePath, fileContent);
    }
}