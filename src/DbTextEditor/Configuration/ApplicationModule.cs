using System.Configuration;
using DbTextEditor.Model.DAL;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure;
using Ninject.Modules;

namespace DbTextEditor.Configuration
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<LocalFileEntity>>()
                .To<LocalFilesRepository>();

            Bind<IRepository<DbFileEntity>>()
                .To<DbFilesRepository>()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Main"]);

            Bind<IFilesAdapter>()
                .To<LocalFilesAdapter>()
                .Named("LocalFilesAdapter");

            Bind<IFilesAdapter>()
                .To<DbFilesAdapter>()
                .Named("DbFilesAdapter");
        }
    }
}