using DbTextEditor.Model.DAL;
using DbTextEditor.Model.DAL.Interfaces;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure;
using DbTextEditor.Model.Infrastructure.Interfaces;
using Ninject.Modules;

namespace DbTextEditor.Model.Tests.Configuration
{
    public class TestApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocalFilesRepository>()
                .To<LocalFilesRepository>();

            Bind<IDbFilesRepository>()
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