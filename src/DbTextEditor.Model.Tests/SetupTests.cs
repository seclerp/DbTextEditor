using DbTextEditor.Model.Tests.Configuration;
using DbTextEditor.Shared.DependencyInjection;
using NUnit.Framework;

namespace DbTextEditor.Model.Tests
{
    [TestFixture]
    public class SetupTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            CompositionRoot.Wire(new TestApplicationModule());
        }
    }
}