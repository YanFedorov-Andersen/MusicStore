using Autofac;
using Autofac.Integration.Mvc;
using MusicStore.Business.Interfaces;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.DataAccess.Realization;
using System.Web.Mvc;
using MusicStore.Domain;
using MusicStore.Domain.Mappers;
using MusicStore.Domain.Models;

namespace MusicStore.Web
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

            builder.RegisterType<MapSong>().As<IMapper<Song, SongDTO>>();
            builder.RegisterType<MapUserAccount>().As<IMapper<User, UserAccountDTO>>();

            builder.RegisterType<MusicStoreService>().As<IMusicStoreService>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();

            IMapper<Song, SongDTO> songMapper = container.Resolve<IMapper<Song, SongDTO>>();
            IMapper<User, UserAccountDTO> userAccountMapper = container.Resolve<IMapper<User, UserAccountDTO>>();

            IMusicStoreService musicStoreService = container.Resolve<IMusicStoreService>();
        }
    }
}