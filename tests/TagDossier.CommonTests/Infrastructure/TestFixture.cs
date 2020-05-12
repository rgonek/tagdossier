using System.Linq;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;

namespace TagDossier.CommonTests.Infrastructure
{
    public class TestFixture
    {
        public static readonly Fixture F = new Fixture();

        static TestFixture()
        {
        }

        public static IFixture Fixture(params ISpecimenBuilder[] specimenBuilders)
        {
            var fixture = new Fixture();

            if (specimenBuilders != null && specimenBuilders.Any())
            {
                foreach (var specimenBuilder in specimenBuilders)
                {
                    fixture.Customizations.Insert(0, specimenBuilder);
                }
            }

            return fixture;
        }

        public static T Create<T>(params ISpecimenBuilder[] specimenBuilders)
        {
            var fixture = Fixture(specimenBuilders);

            return Create<T>(fixture);
        }

        public static T Create<T>(IFixture fixture)
        {
            try
            {
                return fixture.Create<T>();
            }
            catch (ObjectCreationException e)
            {
                if (e.InnerException is TargetInvocationException exception)
                    if (exception.InnerException != null)
                        throw exception.InnerException;
                throw;
            }
        }
    }
}