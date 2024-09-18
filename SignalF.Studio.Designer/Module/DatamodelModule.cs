using Autofac;
using Scotec.Transactions;
using Scotec.XMLDatabase;
using Scotec.XMLDatabase.ReaderWriter.Xml;

namespace SignalF.Studio.Designer.Module
{
    public class DatamodelModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<ITransactionManager>(c => new TransactionManager())
                // Create default transaction handler.
                .OnActivating(handler => handler.Instance.CreateTransactionHandler(""))
                .Named<ITransactionManager>("Transactions.DefaultTransactionManager")
                .InstancePerLifetimeScope();

            builder.Register<IDataDocument>(c => new XmlDataDocument())
                .OnActivating(handler =>
                    handler.Instance.TransactionManager = handler.Context.ResolveNamed<ITransactionManager>("Transactions.DefaultTransactionManager"))
                .Named<IDataDocument>("XMLDatabase.ControllerConfigurationDataDocument")
                .InstancePerLifetimeScope();

            builder.Register<IBusinessDocument>(c =>
                    new BusinessDocument(
                        c.ResolveNamed<IDataDocument>("XMLDatabase.ControllerConfigurationDataDocument")))
                //.Named<IBusinessDocument>("XMLDatabase.ControllerConfigurationInfoBusinessDocument")
                .As<IBusinessDocument>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<ConfigurationFactory>()
            //    .As<IConfigurationFactory>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<DocumentManager>()
                .InstancePerLifetimeScope();
        }
    }
}
