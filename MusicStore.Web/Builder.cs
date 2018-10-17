using Autofac;
using Autofac.Integration.Mvc;
using MusicStore.Business.Interfaces;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.DataAccess.Realization;
using MusicStore.Domain;
using MusicStore.Domain.Mappers;
using System.Web.Mvc;
using MusicStore.Domain.DataTransfer;
using MusicStore.Business;

namespace MusicStore.Web
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<Pagination<Domain.DataTransfer.Album>>().As<IPagination<Domain.DataTransfer.Album>>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

            builder.RegisterType<MapSong>().As<IMapper<DataAccess.Song, Domain.DataTransfer.Song>>();
            builder.RegisterType<MapAlbum>().As<IMapper<DataAccess.Album, Domain.DataTransfer.Album>>();
            builder.RegisterType<MapBoughtSong>().As<IMapper<DataAccess.BoughtSong, Domain.DataTransfer.BoughtSong>>();
            builder.RegisterType<MapUserAccount>().As<IMapper<User, UserAccount>>();

            builder.RegisterType<MusicStoreService>().As<IMusicStoreService>();
            builder.RegisterType<UserAccountService>().As<IUserAccountService>();
            builder.RegisterType<AdminService>().As<IAdminService>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            IPagination<Domain.DataTransfer.Album> albumPagination = container.Resolve<IPagination<Domain.DataTransfer.Album>>();
            IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();

            var songMapper = container.Resolve<IMapper<DataAccess.Song, Domain.DataTransfer.Song>>();
            var boughtSongMapper = container.Resolve<IMapper<DataAccess.BoughtSong, Domain.DataTransfer.BoughtSong>>();
            var userAccountMapper = container.Resolve<IMapper<User, UserAccount>>();
            var albumMapper = container.Resolve<IMapper<DataAccess.Album, Domain.DataTransfer.Album>>();

            IMusicStoreService musicStoreService = container.Resolve<IMusicStoreService>();
            IUserAccountService userAccountService = container.Resolve<IUserAccountService>();
            IAdminService adminService = container.Resolve<IAdminService>();
        }
    }
}