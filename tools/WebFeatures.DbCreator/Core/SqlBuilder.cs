using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebFeatures.DbCreator.Core
{
	internal static class SqlBuilder
	{
		public static string CreateDatabase(string databaseName)
		{
			return $"CREATE DATABASE {databaseName};";
		}

		public static string DropDatabase(string databaseName)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(
				@$"SELECT pg_terminate_backend(pid) 
				FROM pg_stat_activity
				WHERE datname = '{databaseName}' AND pid<>
				pg_backend_pid();
				");

			sb.AppendLine($"DROP DATABASE IF EXISTS {databaseName};");

			return sb.ToString();
		}

		public static IEnumerable<(string Name, string Body)> GetDbSchemaScripts()
		{
			return GetTableScripts().Concat(GetFunctionScripts()).Concat(GetViewScripts());
		}

		private static IEnumerable<(string Name, string Body)> GetTableScripts()
		{
			string[] tables =
			{
				"files",
				"users",
				"roles",
				"userroles",
				"rolepermissions",
				"countries",
				"cities",
				"manufacturers",
				"categories",
				"brands",
				"products",
				"productcomments",
				"productreviews",
				"productpictures",
				"shippers"
			};

			Dictionary<string, (string Name, string Body)> scripts = GetScripts("Core\\Scripts\\Schema\\Tables")
			   .ToDictionary(x => Path.GetFileNameWithoutExtension(x.Name), x => x);

			foreach (string table in tables)
			{
				(string Name, string Body) script = scripts[table];

				yield return script;
			}
		}

		private static IEnumerable<(string Name, string Body)> GetFunctionScripts()
		{
			return GetScripts("Core\\Scripts\\Schema\\Functions");
		}

		private static IEnumerable<(string Name, string Body)> GetViewScripts()
		{
			return GetScripts("Core\\Scripts\\Schema\\Views");
		}

		private static IEnumerable<(string Name, string Body)> GetScripts(string path)
		{
			string[] files = Directory.GetFiles(path, "*.sql", SearchOption.AllDirectories);

			foreach (string file in files)
			{
				yield return (file, File.ReadAllText(file));
			}
		}

		public static string SeedInitialData(string environment)
		{
			return File.ReadAllText($"Core/Scripts/Data/initial_data_{environment}.sql");
		}

		public static string RefreshViews()
		{
			string[] views =
			{
				"get_products_list"
			};

			StringBuilder sb = new StringBuilder();

			foreach (string view in views)
			{
				sb.Append($"REFRESH MATERIALIZED VIEW {view};").AppendLine();
			}

			return sb.ToString();
		}
	}
}
