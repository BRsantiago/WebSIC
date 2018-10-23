[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebSIC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebSIC.App_Start.NinjectWebCommon), "Stop")]

namespace WebSIC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Repository.Interface;
    using Repository.Repository;
    using Service.Interface;
    using Service.Service;
    using Services.Service;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAeroportoService>().To<AeroportoService>();
            kernel.Bind<IApoliceService>().To<ApoliceService>();
            kernel.Bind<IAreaService>().To<AreaService>();
            kernel.Bind<ICargoService>().To<CargoService>();
            kernel.Bind<IContratoService>().To<ContratoService>();
            kernel.Bind<ICredencialService>().To<CredencialService>();
            kernel.Bind<ICursoService>().To<CursoService>();
            kernel.Bind<IEmpresaService>().To<EmpresaService>();
            kernel.Bind<IOcorrenciaService>().To<OcorrenciaService>();
            kernel.Bind<IPessoaService>().To<PessoaService>();
            kernel.Bind<IPortaoAcessoService>().To<PortaoAcessoService>();
            kernel.Bind<IScheduleService>().To<ScheduleService>();
            kernel.Bind<ISolicitacaoService>().To<SolicitacaoService>();
            kernel.Bind<ITipoEmpresaService>().To<TipoEmpresaService>();
            kernel.Bind<ITipoSolicitacaoService>().To<TipoSolicitacaoService>();
            kernel.Bind<ITurmaService>().To<TurmaService>();
            kernel.Bind<IUsuarioService>().To<UsuarioService>();
            kernel.Bind<IVeiculoService>().To<VeiculoService>();

            kernel.Bind<IAeroportoRepository>().To<AeroportoRepository>();
            kernel.Bind<IApoliceRepository>().To<ApoliceRepository>();
            kernel.Bind<IAreaRepository>().To<AreaRepository>();
            kernel.Bind<ICargoRepository>().To<CargoRepository>();
            kernel.Bind<IContratoRepository>().To<ContratoRepository>();
            kernel.Bind<ICredencialRepository>().To<CredencialRepository>();
            kernel.Bind<ICursoRepository>().To<CursoRepository>();
            kernel.Bind<ICursoSemTurmaRepository>().To<CursoSemTurmaRepository>();
            kernel.Bind<IEmpresaRepository>().To<EmpresaRepository>();
            kernel.Bind<IOcorrenciaRepository>().To<OcorrenciaRepository>();
            kernel.Bind<IPessoaRepository>().To<PessoaRepository>();
            kernel.Bind<IPortaoAcessoRepository>().To<PortaoAcessoRepository>();
            kernel.Bind<IScheduleRepository>().To<ScheduleRepository>();
            kernel.Bind<ISolicitacaoRepository>().To<SolicitacaoRepository>();
            kernel.Bind<ITipoEmpresaRepository>().To<TipoEmpresaRepository>();
            kernel.Bind<ITipoSolicitacaoRepository>().To<TipoSolicitacaoRepository>();
            kernel.Bind<ITurmaRepository>().To<TurmaRepository>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();
            kernel.Bind<IVeiculoRepository>().To<VeiculoRepository>();
        }        
    }
}
