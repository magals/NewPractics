using System.Reflection;
using System.Runtime.Loader;

namespace ConsoleSG
{
	partial class Program
	{
		static void Main(string[] args)
		{
			string[] pathdrivers = Directory.GetFiles("C:\\Users\\ITEng\\source\\repos\\NewPractics\\LibrarySC\\bin\\Debug\\netstandard2.0", "*.dll", SearchOption.AllDirectories);
			List<FileInfo> fileInfos = new();
			foreach (var file in pathdrivers)
			{
				FileInfo info = new(file);
				fileInfos.Add(info);
			}

			foreach (var item in fileInfos)
			{
				var context = new AssemblyLoadContext(name: item.Name, isCollectible: true);
				try
				{
					Assembly assembly = context.LoadFromAssemblyPath(item.FullName);
					// if true need unload
					bool FlagUnload = true;


				}
				catch (Exception ex)
				{
					context.Unload();
				}
			}

			HelloFrom("Generated Code");
			Console.Read();
		}

		static partial void HelloFrom(string name);
	}
}