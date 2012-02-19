﻿using System;
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
    public partial class Service : ServiceBase, ILogger
    {
        public const int DEFAULT_SERVICE_PORT = 37207;

        private ServiceHost host;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (host != null)
                host.Close();

            IPHostEntry ipHost;
            ipHost = Dns.GetHostEntry(Dns.GetHostName());
            Uri uri = null;
            foreach (var ip in ipHost.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    uri = new Uri(string.Format("http://{0}:{1}/", ip.ToString(), DEFAULT_SERVICE_PORT));
                    break;
                }
            }

            if (uri == null)
                uri = new Uri(string.Format("{0}:{1}", @"http://localhost", DEFAULT_SERVICE_PORT));

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
            host.Description.Endpoints[0].ListenUri = uri;
                        
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

        public void Write(string message, EventLogEntryType messageType)
        {
            eventLogger.WriteEntry(message, messageType);
        }
    }
}
