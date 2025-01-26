using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NReferenceManager.Cli.Tests
{
    public class ProjectFileFinderTests
    {
        string _TestFolder;

        [SetUp]
        public void Setup()
        {
            this._TestFolder = Path.Combine(Environment.CurrentDirectory, nameof(ProjectFileFinderTests));
            if (!Directory.Exists(this._TestFolder))
                Directory.CreateDirectory(this._TestFolder);

        }

        [Test]
        public void RepoWithOneProjInTheRoot_IsFound()
        {
            //***************** GIVEN
            var testRootFolder = Path.Combine(this._TestFolder, nameof(RepoWithOneProjInTheRoot_IsFound));
            if (!Directory.Exists(testRootFolder))
                Directory.CreateDirectory(testRootFolder);
            var tragetFile = Path.Combine(testRootFolder, "test.csproj");
            ReferenceManagerTests.CreateTestProjFile(tragetFile);

            //***************** WHENa
            var files = ProjectFileFinder.GetCsprojFiles(testRootFolder).ToList();

            //***************** ASSERT
            Assert.That(files, Has.Count.EqualTo(1));
        }

        [Test]
        public void RepoWithOneProjInTheSUbFOlder_IsFound()
        {
            //***************** GIVEN
            var testRootFolder = Path.Combine(this._TestFolder, nameof(RepoWithOneProjInTheSUbFOlder_IsFound));
            if (!Directory.Exists(testRootFolder))
                Directory.CreateDirectory(testRootFolder);
            var targetSubFolder = Path.Combine(testRootFolder, "Folder1");
            if (!Directory.Exists(targetSubFolder))
                Directory.CreateDirectory(targetSubFolder);
            var targetFile = Path.Combine(targetSubFolder, "test.csproj");
            ReferenceManagerTests.CreateTestProjFile(targetFile);
            targetFile = Path.Combine(testRootFolder, "test.csproj");
            ReferenceManagerTests.CreateTestProjFile(targetFile);


            //***************** WHENa
            var files = ProjectFileFinder.GetCsprojFiles(testRootFolder).ToList();

            //***************** ASSERT
            Assert.That(files, Has.Count.EqualTo(2));
        }

        [Test]
        public void RepoWitComplexProjetTree_IsFound()
        {
            //***************** GIVEN
            var testRootFolder = Path.Combine(this._TestFolder, nameof(RepoWitComplexProjetTree_IsFound));
            if (!Directory.Exists(testRootFolder))
                Directory.CreateDirectory(testRootFolder);
            var targetSubFolder = Path.Combine(testRootFolder, "src");
            if (!Directory.Exists(targetSubFolder))
                Directory.CreateDirectory(targetSubFolder);

            targetSubFolder = Path.Combine(testRootFolder, "src", "Folder1");
            if (!Directory.Exists(targetSubFolder))
                Directory.CreateDirectory(targetSubFolder);
            var targetFile = Path.Combine(targetSubFolder, "test.csproj");
            ReferenceManagerTests.CreateTestProjFile(targetFile);

            targetSubFolder = Path.Combine(testRootFolder, "src", "Folder2");
            if (!Directory.Exists(targetSubFolder))
                Directory.CreateDirectory(targetSubFolder);
            targetFile = Path.Combine(targetSubFolder, "test.csproj");
            ReferenceManagerTests.CreateTestProjFile(targetFile);
            
            targetSubFolder = Path.Combine(testRootFolder, "src", "Folder3");
            if (!Directory.Exists(targetSubFolder))
                Directory.CreateDirectory(targetSubFolder);
            targetFile = Path.Combine(targetSubFolder, "test.csproj");
            ReferenceManagerTests.CreateTestProjFile(targetFile);
            
            targetFile = Path.Combine(testRootFolder, "test.csproj");
            ReferenceManagerTests.CreateTestProjFile(targetFile);


            //***************** WHENa
            var files = ProjectFileFinder.GetCsprojFiles(testRootFolder).ToList();

            //***************** ASSERT
            Assert.That(files, Has.Count.EqualTo(4));
        }
    }
}