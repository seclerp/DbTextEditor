using System.Configuration;
using DbTextEditor.Model;
using DbTextEditor.Model.DAL;
using DbTextEditor.Model.DAL.Interfaces;
using DbTextEditor.Model.Infrastructure;
using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Model.Interfaces;
using DbTextEditor.ViewModel;
using DbTextEditor.ViewModel.Interfaces;
using Ninject;
using Ninject.Modules;

namespace DbTextEditor.Configuration
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            RegisterDal();
            RegisterViewModel();
            RegisterModel();
        }

        private void RegisterDal()
        {
            Bind<ILocalFilesRepository>()
                .To<LocalFilesRepository>();

            Bind<IDbFilesRepository>()
                .To<DbFilesRepository>()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Main"].ConnectionString);

            Bind<IFilesAdapter>()
                .To<LocalFilesAdapter>()
                .Named("LocalFilesAdapter");

            Bind<IFilesAdapter>()
                .To<DbFilesAdapter>()
                .Named("DbFilesAdapter");
        }

        private void RegisterViewModel()
        {
            Bind<IDatabaseViewViewModel>()
                .To<DatabaseViewViewModel>();

            Bind<IEditorViewModel>()
                .To<EditorViewModel>();

            Bind<IMainViewModel>()
                .To<MainViewModel>();
        }

        private void RegisterModel()
        {
            Bind<IDatabaseModel>()
                .To<DatabaseModel>()
                .InSingletonScope();

            Bind<FileModel>()
                .ToSelf()
                .Named("LocalFileModel");

            Bind<FileModel>()
                .ToSelf()
                .Named("DbFileModel");
        }
    }
}