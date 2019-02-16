using System.IO;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure;
using DbTextEditor.Model.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbTextEditor.Model.Tests.Integration
{
    [TestClass]
    public class LocalFilesTests
    {
        private const string TempDirectory = "/local_files_tests";
        private readonly IRepository<LocalFileEntity, string> _localFilesRepository;

        public LocalFilesTests()
        {
            _localFilesRepository = new LocalFilesRepository();
        }

        [ClassInitialize]
        public static void TearUp(TestContext ctx)
        {
            if (Directory.Exists(TempDirectory))
            {
                Directory.Delete(TempDirectory, true);
            }
            else
            {
                Directory.CreateDirectory(TempDirectory);
            }
        }

        [ClassCleanup]
        public static void TearDown()
        {
            Directory.Delete(TempDirectory, true);
        }

        [TestMethod]
        public void Create_Success_SimpleFile1()
        {
            var expectedContents = "SimpleContents";
            var filePath = Path.Combine(TempDirectory, "SimpleFile1.txt");

            _localFilesRepository.Create(new LocalFileEntity
            {
                Path = filePath,
                Contents = expectedContents
            });

            Assert.AreEqual(File.ReadAllText(filePath), expectedContents);
        }

        [TestMethod]
        public void Get_Success_SimpleFile2()
        {
            var expectedContents = "SimpleContents";
            var filePath = Path.Combine(TempDirectory, "SimpleFile2.txt");

            _localFilesRepository.Create(new LocalFileEntity
            {
                Path = filePath,
                Contents = expectedContents
            });
            var entity = _localFilesRepository.Get(filePath);

            Assert.AreEqual(entity.Contents, expectedContents);
        }

        [TestMethod]
        public void Update_Success_SimpleFile3()
        {
            var expectedContents = "UpdatedContents";
            var filePath = Path.Combine(TempDirectory, "SimpleFile3.txt");

            _localFilesRepository.Create(new LocalFileEntity
            {
                Path = filePath,
                Contents = "SimpleContents"
            });
            _localFilesRepository.Update(new LocalFileEntity
            {
                Path = filePath,
                Contents = expectedContents
            });

            Assert.AreEqual(File.ReadAllText(filePath), expectedContents);
        }

        [TestMethod]
        public void Delete_Success_SimpleFile4()
        {
            var filePath = Path.Combine(TempDirectory, "SimpleFile4.txt");

            _localFilesRepository.Create(new LocalFileEntity
            {
                Path = filePath,
                Contents = "SimpleContents"
            });
            _localFilesRepository.Delete(filePath);

            Assert.IsFalse(File.Exists(filePath));
        }
    }
}
