using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using StudyingControllerService;
using System.ServiceModel.Description;
using System.IO;

namespace StudyingControllerWindowsService
{
    public partial class Service : ServiceBase
    {
        private ServiceHost host;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (host != null)
                host.Close();

            var uri = new Uri("http://localhost:37207/");
            
            host = new ServiceHost(typeof(ControllerService), new[] { uri });
            
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            ContractDescription cd = ContractDescription.GetContract(typeof(IControllerService));
            BasicHttpBinding bnd = new BasicHttpBinding(BasicHttpSecurityMode.None);
            bnd.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            bnd.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            host.AddServiceEndpoint(typeof(IControllerService), bnd, "ControllerService");

            host.Open();
        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
                ((IDisposable)host).Dispose();

                host = null;
            }
        }
    }
}
