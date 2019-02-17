using System.Data.SQLite;
using System.Text;
using Dapper;
using DbTextEditor.Model.DAL;
using DbTextEditor.Model.Entities;
using NUnit.Framework;

namespace DbTextEditor.Model.Tests.Integration
{
    [TestFixture]
    public class DbFilesTests
    {
        private readonly IRepository<DbFileEntity> _repository;
        private const string ConnectionString = TestConstants.TestConnectionString;

        public DbFilesTests()
        {
            _repository = new DbFilesRepository(ConnectionString);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute(@"DROP TABLE IF EXISTS files;
                                         CREATE TABLE files (
                                           Id       NVARCHAR(36) PRIMARY KEY,
                                           Name     NVARCHAR,
                                           Revision INTEGER,
                                           Contents BLOB
                                         );");
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DROP TABLE IF EXISTS files;");
            }
        }

        [Test]
        public void Create_Success_SimpleFile1()
        {
            var expectedContents = "SimpleContents";
            var fileName = "SimpleFile1.txt";

            _repository.Create(new DbFileEntity
            {
                Name = fileName,
                Contents = Encoding.UTF8.GetBytes(expectedContents)
            });

            Assert.AreEqual(Encoding.UTF8.GetString(_repository.Get(fileName).Contents), expectedContents);
        }

        [Test]
        public void Get_Success_SimpleFile2()
        {
            var expectedContents = "SimpleContents";
            var fileName = "SimpleFile1.txt";

            _repository.Create(new DbFileEntity
            {
                Name = fileName,
                Contents = Encoding.UTF8.GetBytes(expectedContents)
            });
            var entity = _repository.Get(fileName);

            Assert.AreEqual(Encoding.UTF8.GetString(entity.Contents), expectedContents);
        }

        [Test]
        public void Update_Success_SimpleFile3()
        {
            var expectedContents = "SimpleUpdatedContents";
            var fileName = "SimpleFile1.txt";

            _repository.Create(new DbFileEntity
            {
                Name = fileName,
                Contents = Encoding.UTF8.GetBytes("SimpleContents")
            });
            _repository.Update(new DbFileEntity
            {
                Name = fileName,
                Contents = Encoding.UTF8.GetBytes(expectedContents)
            });

            Assert.AreEqual(Encoding.UTF8.GetString(_repository.Get(fileName).Contents), expectedContents);
        }

        [Test]
        public void Delete_Success_SimpleFile4()
        {
            var fileName = "SimpleFile1.txt";
            _repository.Create(new DbFileEntity
            {
                Name = fileName,
                Contents = Encoding.UTF8.GetBytes("SimpleContents")
            });
            _repository.Delete(fileName);

            Assert.IsFalse(_repository.Exists(fileName));
        }
    }
}