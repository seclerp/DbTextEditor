using System.IO;
using DbTextEditor.Model.DAL;
using DbTextEditor.Model.DAL.Interfaces;
using DbTextEditor.Model.Entities;
using NUnit.Framework;

namespace DbTextEditor.Model.Tests.Integration
{
    [TestFixture]
    public class LocalFilesTests
    {
        private const string TempDirectory = "/local_files_tests";
        private readonly IRepository<LocalFileEntity> _localFilesRepository;

        public LocalFilesTests()
        {
            _localFilesRepository = new LocalFilesRepository();
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            if (Directory.Exists(TempDirectory))
                Directory.Delete(TempDirectory, true);
            else
                Directory.CreateDirectory(TempDirectory);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Directory.Delete(TempDirectory, true);
        }

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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
    }
}