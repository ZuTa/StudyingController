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
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Configuration;
using System.Net;

namespace StudyingControllerWindowsService
{
    public partial class Service : ServiceBase
    {
        public const string SERVICE_NAME = "Studying Controller Service";

        private ServiceHost host;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.ServiceName = SERVICE_NAME;

            if (host != null)
                host.Close();

            IPHostEntry ipHost;
            string localIP = "";
            string publicIP = "";
            ipHost = Dns.GetHostEntry(Dns.GetHostName());
            Uri uri = null;
            foreach (var ip in ipHost.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    localIP = ip.ToString();
            }

            //var uri = new Uri("http://localhost:37207/");

            //ControllerService service = new ControllerService();

            //SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            //connectionStringBuilder.DataSource = "195.68.211.25";
            //connectionStringBuilder.InitialCatalog = "UniversityDB";
            //connectionStringBuilder.IntegratedSecurity = true;

            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            //ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            //connectionStringsSection.ConnectionStrings["UniversityEntities"].ConnectionString = "";// connectionStringBuilder.ToString();

            //config.Save(ConfigurationSaveMode.Modified);

            host = new ServiceHost(typeof(ControllerService));
                        
            //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //smb.HttpGetEnabled = true;
            //host.Description.Behaviors.Add(smb);

            //ContractDescription cd = ContractDescription.GetContract(typeof(IControllerService));
            //BasicHttpBinding bnd = new BasicHttpBinding(BasicHttpSecurityMode.None);
            //bnd.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            //bnd.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            //host.AddServiceEndpoint(typeof(IControllerService), bnd, "ControllerService");

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
