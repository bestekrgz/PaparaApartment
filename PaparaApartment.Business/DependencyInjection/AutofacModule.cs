using ApartmentManagement.DataAccess.Concrete.EntityFramework;
using ApartmentManagement.DataAccess.Concrete.Mongo;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Business.Concrete;
using PaparaApartment.Core.Utilities.Interceptors;
using PaparaApartment.Core.Utilities.Security;
using PaparaApartment.Core.Utilities.Security.JWT;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Data.Concrete.EntityFramework;
using System.Reflection;
using Module = Autofac.Module;


namespace PaparaApartment.Business.DependencyInjection
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExpenseTypeAdmin>().As<IExpenseTypeService>().InstancePerRequest();
            builder.RegisterType<EfExpenseTypeDal>().As<IExpenseTypeDal>();

            builder.RegisterType<UserAdmin>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<EfUserDetailDal>().As<IUserDetailDal>();
            builder.RegisterType<UserDetailAdmin>().As<IUserDetailService>();

            builder.RegisterType<EfBlockDal>().As<IBlockDal>();
            builder.RegisterType<BlockAdmin>().As<IBlockService>();

            builder.RegisterType<EfApartmentDal>().As<IApartmentDal>();
            builder.RegisterType<ApartmentAdmin>().As<IApartmentService>();

            builder.RegisterType<EfApartmentExpenseDal>().As<IApartmentExpenseDal>();
            builder.RegisterType<ApartmentExpenseAdmin>().As<IApartmentExpenseService>();

            builder.RegisterType<EfMessageDal>().As<IMessageDal>();
            builder.RegisterType<MessageManager>().As<IMessageService>();

            builder.RegisterType<EfUserMessageDal>().As<IUserMessageDal>();
            builder.RegisterType<UserMessageManager>().As<IUserMessageService>();

            builder.RegisterType<EfCarDal>().As<ICarDal>();
            builder.RegisterType<CarAdmin>().As<ICarService>();

            builder.RegisterType<EfUserClaimDal>().As<IUserClaimDal>();
            builder.RegisterType<UserClaimAdmin>().As<IUserClaimService>();

            builder.RegisterType<EfClaimDal>().As<IClaimDal>();
            builder.RegisterType<ClaimAdmin>().As<IClaimService>();

            builder.RegisterType<EfExpenseDal>().As<IExpenseDal>();
            builder.RegisterType<ExpenseAdmin>().As<IExpenseService>();

            builder.RegisterType<MPaymentDal>().As<IPaymentDal>();
            builder.RegisterType<PaymentAdmin>().As<IPaymentService>();

            builder.RegisterType<AuthAdmin>().As<IAuthService>();

            builder.RegisterType<JWTHelper>().As<ITokenHelper>();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
