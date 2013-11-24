using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;


namespace DataBaseGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=localhost;Integrated Security=True;MultipleActiveResultSets=True");
            Server server = new Server(new ServerConnection(sqlConnection));

            Assembly assembly = Assembly.GetExecutingAssembly();
            try
            {
                if (args.Contains("-schema"))
                {
                    #region
                    using (Stream resourceStream = assembly.GetManifestResourceStream("DataBaseGeneration.Scripts.preparescript.sql"))
                    {
                        Console.WriteLine("Droping prev DB and creating new...");

                        server.ConnectionContext.ExecuteNonQuery(ReadAllText(resourceStream));

                        Console.WriteLine("Empty DB created!");
                    }

                    using (Stream resourceStream = assembly.GetManifestResourceStream("DataBaseGeneration.Scripts.schema.sql"))
                    {
                        Console.WriteLine("Creating schema...");

                        server.ConnectionContext.ExecuteNonQuery(ReadAllText(resourceStream));

                        Console.WriteLine("Schema created!");
                    }

                    using (Stream resourceStream = assembly.GetManifestResourceStream("DataBaseGeneration.Scripts.defaultdata.sql"))
                    {
                        Console.WriteLine("Adding default data...");

                        server.ConnectionContext.ExecuteNonQuery(ReadAllText(resourceStream));

                        Console.WriteLine("Default data added!");
                    }
                    #endregion
                }
                if (args.Contains("-test"))
                {
                    Console.WriteLine("Filling test data...");
                    
                    // UNIVERSITY STRUCTURE
                    using (Stream resourceStream = assembly.GetManifestResourceStream("DataBaseGeneration.Scripts.universitystructure.sql"))
                    {
                        Console.WriteLine("Adding university structure..");

                        server.ConnectionContext.ExecuteNonQuery(ReadAllText(resourceStream));
                    }

                    // USERS AND USER INFORMATIONS
                    using (Stream resourceStream = assembly.GetManifestResourceStream("DataBaseGeneration.Scripts.users.sql"))
                    {
                        Console.WriteLine("Adding users..");

                        server.ConnectionContext.ExecuteNonQuery(ReadAllText(resourceStream));
                    }



                    Console.WriteLine("Data filled!");
                }

                Console.WriteLine("Done!");

                if (args.Contains("-wait"))
                {
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private static string ReadAllText(Stream stream)
        {
            // it's good for unicode
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
