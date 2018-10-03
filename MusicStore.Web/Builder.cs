using Autofac;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.DataAccess.Realization;

namespace MusicStore.Web
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<UserAccountRepository>().As<IRepository<User>>();

            var container = builder.Build();

            IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();
        }
    }
}