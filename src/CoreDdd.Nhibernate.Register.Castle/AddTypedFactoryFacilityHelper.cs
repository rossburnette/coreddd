using System.Linq;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;

namespace CoreDdd.Nhibernate.Register.Castle
{
    internal static class AddTypedFactoryFacilityHelper
    {
        public static void TryAddTypedFactoryFacility(IWindsorContainer container)
        {
            var facilities = container.Kernel.GetFacilities();
            if (facilities.All(x => x.GetType() != typeof(TypedFactoryFacility)))
            {
                container.AddFacility<TypedFactoryFacility>();
            }
        }
    }
}