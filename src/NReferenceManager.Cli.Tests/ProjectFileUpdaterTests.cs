using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NReferenceManager.Cli.Tests
{
    public class ProjectFileUpdaterTests
    {
        string _TestFolder;

        [SetUp]
        public void Setup()
        {
            this._TestFolder = Path.Combine(Environment.CurrentDirectory, nameof(ProjectFileUpdaterTests));
            if (!Directory.Exists(this._TestFolder))
                Directory.CreateDirectory(this._TestFolder);

        }

        [Test]
        public void CsProj_is_Updated()
        {
            //***************** GIVEN
            var testRootFolder = Path.Combine(this._TestFolder, nameof(CsProj_is_Updated));
            if (!Directory.Exists(testRootFolder))
                Directory.CreateDirectory(testRootFolder);
            var tragetFile = Path.Combine(testRootFolder, "test.csproj");
            ReferenceManagerTests.CreateTestProjFile(tragetFile);
            ReferenceDto reference= new()
            {
                PackageName = "Microsoft.Extensions.DependencyInjection",
                PackageVersion="MyVersion"
            };

            //***************** WHENa
            ProjectFileUpdater.UpdatePackageReferenceVersion(reference, new FileInfo(tragetFile));

            //***************** ASSERT
            var fileContent = File.ReadAllText(tragetFile);
            Assert.That(fileContent.Contains(reference.PackageVersion), Is.True)s;
        }
    }
}