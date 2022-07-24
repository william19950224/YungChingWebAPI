using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace RepositoryService {
	public class BaseRepository {
        public static string ConnString {
            get
            {
                //var test = ConfigurationManager.ConnectionStrings["ConString"];
                return ConnectionStringValue;
            }
        }
        public static string ConnectionStringValue {
            get
            {

                var cfgFileMap = new ExeConfigurationFileMap();
                var codeBaseUri = new Uri(Assembly.GetExecutingAssembly().CodeBase);
                cfgFileMap.ExeConfigFilename = codeBaseUri.LocalPath + ".config";

                if (!File.Exists(cfgFileMap.ExeConfigFilename))
                    throw new Exception("Configuration file not found: " + cfgFileMap.ExeConfigFilename);
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(cfgFileMap, ConfigurationUserLevel.None);
                foreach (ConnectionStringSettings connectionString in configuration.ConnectionStrings.ConnectionStrings) {
                    if (connectionString.Name.Equals("ConnectionString", StringComparison.OrdinalIgnoreCase)) {
                        return connectionString.ConnectionString;
                    }
                }

                return string.Empty;
            }
        }
    }
}
