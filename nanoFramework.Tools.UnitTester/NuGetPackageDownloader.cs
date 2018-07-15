//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using NuGet.Common;
using NuGet.Configuration;
using NuGet.PackageManagement;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.ProjectManagement;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace nanoFramework.Tools.UnitTester
{
	/// <summary>
	/// Class that handles the download of the nuget packages
	/// </summary>
	internal class NuGetPackageDownloader
	{
		#region fields
		private readonly Logger _logger = null;
		private readonly List<Lazy<INuGetResourceProvider>> _providers = null;
		private readonly PackageSource _packageSource = null;
		private readonly SourceRepository _sourceRepository = null;
		private readonly PackageSearchResource _searchResource = null;
		private readonly ISettings _settings = null;
		private readonly FolderNuGetProject _project = null;
		private readonly NuGetPackageManager _packageManager = null;
		private readonly INuGetProjectContext _projectContext = null;
		private readonly SearchFilter _searchFilter = null;
		private readonly ResolutionContext _resolutionContext = null;
		private readonly List<SourceRepository> _secondarySources = null;
		#endregion

		#region constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="packageSource">package source</param>
		/// <param name="destinationPath">destination path where the packages should be downloaded</param>
		internal NuGetPackageDownloader(string packageSource, string destinationPath)
		{
			_logger = new Logger();
			_providers = new List<Lazy<INuGetResourceProvider>>();
			_providers.AddRange(Repository.Provider.GetCoreV3());
			_packageSource = new PackageSource(packageSource);
			_sourceRepository = new SourceRepository(_packageSource, _providers);
			_searchResource = _sourceRepository.GetResource<PackageSearchResource>();
			_settings = Settings.LoadDefaultSettings(destinationPath);
			_project = new FolderNuGetProject(destinationPath);
			_packageManager = new NuGetPackageManager(new SourceRepositoryProvider(new PackageSourceProvider(_settings), _providers), _settings, destinationPath);
			_projectContext = new ProjectContext(_logger);
			_searchFilter = new SearchFilter(true, SearchFilterType.IsAbsoluteLatestVersion);
			_resolutionContext = new ResolutionContext(NuGet.Resolver.DependencyBehavior.Ignore, false, false, VersionConstraints.None);
			_secondarySources = new List<SourceRepository>();
		}
		#endregion

		#region internal methods
		/// <summary>
		/// Cleans the destination directory
		/// </summary>
		internal void CleanDestinationDirectory()
		{
			DirectoryInfo destination = new DirectoryInfo(_project.Root);
			if (destination.Exists)
			{
				destination.Delete(true);
			}
			destination.Create();
		}

		/// <summary>
		/// Downloads and extracts the absolute latest pre-release version of a nuget package
		/// </summary>
		/// <param name="packageName">nuget package name</param>
		/// <returns>The directory where the downloaded assembly is extracted</returns>
		internal DirectoryInfo DownloadAndExtractPackage(string packageName)
		{
			// find the absolute latest pre-release version of the given package
			PackageIdentity latestPackage = _searchResource.SearchAsync(packageName, _searchFilter, 0, 1, _logger, CancellationToken.None).Result.First().Identity;
			// install the package
			_packageManager.InstallPackageAsync(_project, latestPackage, _resolutionContext, _projectContext, _sourceRepository, _secondarySources, CancellationToken.None).GetAwaiter().GetResult();
			// find the lib subdirectory in the extracted package
			DirectoryInfo extractedFilesDirectory = new DirectoryInfo(Path.Combine(_project.Root, latestPackage.ToString(), "lib"));
			if (extractedFilesDirectory.Exists)
			{
				// is below the "lib" subdirectory another subdirectory? (only one!)
				DirectoryInfo[] subDirectories = extractedFilesDirectory.GetDirectories();
				// then the extracted assemblies are in this subdirectory; otherwise the assemblies are in the "lib" subdirectory
				return (subDirectories != null && subDirectories.Length == 1) ? subDirectories[0] : extractedFilesDirectory;
			}
			else
			{
				// "lib" subdirectory not found
				return null;
			}
		}

		/// <summary>
		/// Downloads all referenced packages for a given library file
		/// </summary>
		/// <param name="libraryFile">The library file (dll or exe; not the pe-file!)</param>
		/// <param name="knownAssemblies">The list of the already known assemblies; key = package name; value = extracted "lib" assembly directory</param>
		/// <returns>The list of the referenced assemblies; key = package name; value = extracted "lib" assembly directory</returns>
		internal IDictionary<string, DirectoryInfo> DownloadReferencedAssemblyPackages(FileInfo libraryFile, IDictionary<string, DirectoryInfo> knownAssemblies)
		{
			// load the library for reflection and iterate through all referenced assemblies
			Assembly library = Assembly.ReflectionOnlyLoadFrom(libraryFile.FullName);
			AssemblyName[] references = library.GetReferencedAssemblies();
			IDictionary<string, DirectoryInfo> referencedAssemblies = new Dictionary<string, DirectoryInfo>();
			if (references != null && references.Length > 0)
			{
				foreach (AssemblyName reference in references)
				{
					// reference already known?
					DirectoryInfo allreadyKnown;
					if (knownAssemblies.TryGetValue(reference.Name, out allreadyKnown))
					{
						if (!referencedAssemblies.ContainsKey(reference.Name))
						{
							referencedAssemblies.Add(reference.Name, allreadyKnown);
						}
						continue;
					}
					// download the package and add it to the known assembly list
					DirectoryInfo referencedPackage = DownloadAndExtractPackage(reference.Name);
					knownAssemblies.Add(reference.Name, referencedPackage);
					referencedAssemblies.Add(reference.Name, referencedPackage);
					// find the extracted dll of this assembly
					FileInfo[] referencedLibraryFiles = referencedPackage.GetFiles(reference.Name + ".dll");
					// found? => Download all references packages recursively
					if (referencedLibraryFiles != null && referencedLibraryFiles.Length == 1)
					{
						IDictionary<string, DirectoryInfo> referencedReferenceAssemblies = DownloadReferencedAssemblyPackages(referencedLibraryFiles[0], knownAssemblies);
						foreach (KeyValuePair<string, DirectoryInfo> assembly in referencedReferenceAssemblies)
						{
							if (!referencedAssemblies.ContainsKey(assembly.Key))
							{
								referencedAssemblies.Add(assembly);
							}
						}
					}
				}
			}
			return referencedAssemblies;
		}
		#endregion
	}

	#region helper classes for making the NuGetPackageManager happy
	/// <summary>
	/// Logger class
	/// </summary>
	public class Logger : ILogger
	{
		#region methods
		public void LogDebug(string data) => Console.WriteLine($"DEBUG: {data}");
		public void LogVerbose(string data) => Console.WriteLine($"VERBOSE: {data}");
		public void LogInformation(string data) => Console.WriteLine($"INFORMATION: {data}");
		public void LogMinimal(string data) => Console.WriteLine($"MINIMAL: {data}");
		public void LogWarning(string data) => Console.WriteLine($"WARNING: {data}");
		public void LogError(string data) => Console.WriteLine($"ERROR: {data}");
		public void LogSummary(string data) => Console.WriteLine($"SUMMARY: {data}");
		public void LogInformationSummary(string data) => Console.WriteLine($"INFORMATION_SUMMARY: {data}");
		public void LogErrorSummary(string data) => Console.WriteLine($"ERROR_SUMMARY: {data}");
		public void Log(LogLevel level, string data) => Console.WriteLine($"{level.ToString()}: {data}");
		public Task LogAsync(LogLevel level, string data) { return Task.Run(() => { Console.WriteLine($"{level.ToString()}: {data}"); }); }
		public void Log(ILogMessage message) => Console.WriteLine($"{message.Level.ToString()}: {message.Message}");
		public Task LogAsync(ILogMessage message) { return Task.Run(() => { Console.WriteLine($"{message.Level.ToString()}: {message.Message}"); }); }
		#endregion
	}

	/// <summary>
	/// ProjectContext class
	/// </summary>
	public class ProjectContext : INuGetProjectContext
	{
		#region fields
		private readonly ILogger _logger;
		#endregion

		#region properties
		public PackageExtractionContext PackageExtractionContext { get; set; }
		public NuGet.ProjectManagement.ExecutionContext ExecutionContext { get; }
		public XDocument OriginalPackagesConfig { get; set; }
		public ISourceControlManagerProvider SourceControlManagerProvider { get; set; }
		public NuGetActionType ActionType { get; set; }
		PackageExtractionContext INuGetProjectContext.PackageExtractionContext { get; set; }
		XDocument INuGetProjectContext.OriginalPackagesConfig { get; set; }
		public Guid OperationId { get; set; }
		#endregion

		#region constructor
		public ProjectContext(ILogger logger)
		{
			_logger = logger;
		}
		#endregion

		#region methods
		public void Log(MessageLevel level, string message, params object[] args)
		{
			_logger.LogInformation(string.Format(message, args));
		}

		public FileConflictAction ResolveFileConflict(string message)
		{
			_logger.LogError(message);
			return FileConflictAction.Ignore;
		}

		public void ReportError(string message)
		{
			_logger.LogError(message);
		}
		#endregion
	}
	#endregion
}
