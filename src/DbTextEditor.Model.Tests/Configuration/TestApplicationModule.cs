using DbTextEditor.Model.DAL;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure;
using Ninject.Modules;

namespace DbTextEditor.Model.Tests.Configuration
{
    public class TestApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<LocalFileEntity>>()
                .To<LocalFilesRepository>();

            Bind<IRepository<DbFileEntity>>()
                .To<DbFilesRepository>()
                .WithConstructorArgument("connectionString", TestConstants.TestConnectionString);

            Bind<IFilesAdapter>()
                .To<LocalFilesAdapter>()
                .Named("LocalFilesAdapter");

            Bind<IFilesAdapter>()
                .To<DbFilesAdapter>()
                .Named("DbFilesAdapter");
        }
    }
}