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
using MusicStore.Business.Services.Statistics;

namespace MusicStore.Web
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

            builder.RegisterType<MapSong>().As<IMapper<DataAccess.Song, Domain.DataTransfer.Song>>().SingleInstance();
            builder.RegisterType<MapAlbum>().As<IMapper<DataAccess.Album, Domain.DataTransfer.Album>>().SingleInstance();
            builder.RegisterType<MapBoughtSong>().As<IMapper<DataAccess.BoughtSong, Domain.DataTransfer.BoughtSong>>().SingleInstance();
            builder.RegisterType<MapUserAccount>().As<IMapper<User, UserAccount>>().SingleInstance();

            builder.RegisterType<MusicStoreService>().As<IMusicStoreService>().SingleInstance();
            builder.RegisterType<MusicStoreDisplayService>().As<IMusicStoreDisplayService>().SingleInstance(); 
            builder.RegisterType<UserAccountService>().As<IUserAccountService>().SingleInstance(); 
            builder.RegisterType<AdminService>().As<IAdminService>().SingleInstance(); 
            builder.RegisterType<UserStatisticService>().As<IUserStatisticService>().SingleInstance(); 
            builder.RegisterType<AdminStatisticService>().As<IAdminStatisticService>().SingleInstance(); 
            builder.RegisterType<DiscountService>().As<IDiscountService>().SingleInstance();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();

            var songMapper = container.Resolve<IMapper<DataAccess.Song, Domain.DataTransfer.Song>>();
            var boughtSongMapper = container.Resolve<IMapper<DataAccess.BoughtSong, Domain.DataTransfer.BoughtSong>>();
            var userAccountMapper = container.Resolve<IMapper<User, UserAccount>>();
            var albumMapper = container.Resolve<IMapper<DataAccess.Album, Domain.DataTransfer.Album>>();

            IMusicStoreDisplayService musicStoreDisplayService = container.Resolve<IMusicStoreDisplayService>();
            IMusicStoreService musicStoreService = container.Resolve<IMusicStoreService>();
            IUserAccountService userAccountService = container.Resolve<IUserAccountService>();
            IAdminService adminService = container.Resolve<IAdminService>();
            IUserStatisticService userStatisticService = container.Resolve<IUserStatisticService>();
            IAdminStatisticService adminStatisticService = container.Resolve<IAdminStatisticService>();
            IDiscountService discountService = container.Resolve<IDiscountService>();
        }
    }
}