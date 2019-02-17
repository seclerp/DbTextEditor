using Ninject;
using Ninject.Modules;

namespace DbTextEditor.Shared.DependencyInjection
{
    public class CompositionRoot
    {
        private static IKernel _ninjectKernel;

        public static void Wire(INinjectModule module)
        {
            _ninjectKernel = new StandardKernel(module);
        }

        public static T Resolve<T>()
        {
            return _ninjectKernel.Get<T>();
        }

        public static T Resolve<T>(string name)
        {
            return _ninjectKernel.Get<T>(name);
        }
    }
}