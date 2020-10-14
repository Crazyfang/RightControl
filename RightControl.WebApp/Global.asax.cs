using System;
using Autofac;
using Autofac.Integration.Mvc;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using FreeSql;
using AutoMapper;
using System.Collections.Generic;

namespace RightControl.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Log4Net.config")));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //创建autofac管理注册类的容器实例
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);
            //使用Autofac提供的RegisterControllers扩展方法来对程序集中所有的Controller一次性的完成注册 支持属性注入
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            var fsql = new FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, @"Data Source=.;Initial Catalog=rightcontrol;Persist Security Info=True;Connect Timeout=300;User ID=sa;Password=waj")
                .UseAutoSyncStructure(true) //自动迁移实体的结构到数据库
                .Build(); //请务必定义成 Singleton 单例模式
            
            builder.RegisterInstance(fsql).SingleInstance();

            // 把容器装入到微软默认的依赖注入容器中
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
                {
                    setting.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                    setting.DateFormatString = "yyyy-MM-dd";
                    setting.NullValueHandling = NullValueHandling.Ignore;
                    return setting;
                });
        }

        private static void SetupResolveRules(ContainerBuilder builder)
        {
            //WebAPI只用引用services和repository的接口，不用引用实现的dll。
            //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll
            var iServices = Assembly.Load("RightControl.IService");
            var services = Assembly.Load("RightControl.Service");
            var iRepository = Assembly.Load("RightControl.IRepository");
            var repository = Assembly.Load("RightControl.Repository");

            builder.RegisterModule(new AutoMapperModule(services));

            //根据名称约定（服务层的接口和实现均以Services结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(iServices, services)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces().PropertiesAutowired();

            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(iRepository, repository)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().PropertiesAutowired();
        }

        public class AutoMapperModule : Autofac.Module
        {
            private readonly IEnumerable<Assembly> assembliesToScan;

            public AutoMapperModule(IEnumerable<Assembly> assembliesToScan) => this.assembliesToScan = assembliesToScan;

            public AutoMapperModule(params Assembly[] assembliesToScan) : this((IEnumerable<Assembly>)assembliesToScan) { }

            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                var assembliesToScan = this.assembliesToScan as Assembly[] ?? this.assembliesToScan.ToArray();

                var allTypes = assembliesToScan
                              .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                              .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                              .SelectMany(a => a.DefinedTypes)
                              .ToArray();

                var openTypes = new[] {
                            typeof(IValueResolver<,,>),
                            typeof(IMemberValueResolver<,,,>),
                            typeof(ITypeConverter<,>),
                            typeof(IValueConverter<,>),
                            typeof(IMappingAction<,>)
            };

                foreach (var type in openTypes.SelectMany(openType =>
                     allTypes.Where(t => t.IsClass && !t.IsAbstract && ImplementsGenericInterface(t.AsType(), openType))))
                {
                    builder.RegisterType(type.AsType()).InstancePerDependency();
                }

                builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps(assembliesToScan)));

                builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();
            }

            private static bool ImplementsGenericInterface(Type type, Type interfaceType)
                      => IsGenericType(type, interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => IsGenericType(@interface, interfaceType));

            private static bool IsGenericType(Type type, Type genericType)
                       => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }
    }
}
