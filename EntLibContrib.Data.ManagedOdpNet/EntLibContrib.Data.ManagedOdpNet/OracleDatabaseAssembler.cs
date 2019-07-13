//===============================================================================
// Microsoft patterns & practices Enterprise Library Contribution
// Data Access Application Block
//===============================================================================

using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using EntLibContrib.Data.ManagedOdpNet.Configuration;

namespace EntLibContrib.Data.ManagedOdpNet
{
	/// <summary>
	/// Represents the process to build an instance of <see cref="OracleDatabase"/>
	/// described by configuration information.
	/// </summary>
	public class OracleDatabaseAssembler : IDatabaseAssembler
	{
		#region Public Methods
		/// <summary>
		/// Builds an instance of the concrete subtype of <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.Database"/> the receiver knows how to build, based on
		/// the provided connection string and any configuration information that might be contained by the
		/// <paramref name="configurationSource"/>.
		/// </summary>
		/// <param name="name">The name for the new database instance.</param>
		/// <param name="connectionStringSettings">The connection string for the new database instance.</param>
		/// <param name="configurationSource">The source for any additional configuration information.</param>
		/// <returns>The new database instance.</returns>
    public Database Assemble(string name, ConnectionStringSettings connectionStringSettings, IConfigurationSource configurationSource)
    {
			OracleConnectionSettings oracleConnectionSettings = OracleConnectionSettings.GetSettings(configurationSource);
			if (oracleConnectionSettings != null)
			{
				OracleConnectionData oracleConnectionData = oracleConnectionSettings.OracleConnectionsData.Get(name);
				if (oracleConnectionData != null)
				{
					IOraclePackage[] packages = new IOraclePackage[oracleConnectionData.Packages.Count];
					int i = 0;
					foreach (IOraclePackage package in oracleConnectionData.Packages)
					{
						packages[i++] = package;
					}

					return new OracleDatabase(connectionStringSettings.ConnectionString, packages);
				}
			}

			return new OracleDatabase(connectionStringSettings.ConnectionString);
		}
		#endregion
	}
}