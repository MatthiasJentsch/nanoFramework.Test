using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;

namespace nanoFramework.Tools.UnitTester
{
	class Program
	{
		private static void Main(string[] args)
		{
			// Read the configuration of the test devices
			List<ConnectedDevice> connectedDevices = new List<ConnectedDevice>();
			foreach (JToken entry in JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"))["TestEngine"]["ConnectedDevices"].AsJEnumerable())
			{
				connectedDevices.Add(entry.ToObject<ConnectedDevice>());
			}

			// assert that the directories exists or create the results directory
			DirectoryInfo jobDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["JobDirectory"]);
			if (!jobDirectory.Exists)
			{
				throw new DirectoryNotFoundException(jobDirectory.FullName);
			}
			DirectoryInfo resultsDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["ResultsDirectory"]);
			if (!resultsDirectory.Exists)
			{
				resultsDirectory.Create();
			}
			DirectoryInfo testAssembliesDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["TestAssembliesRoot"]);
			if (!testAssembliesDirectory.Exists)
			{
				throw new DirectoryNotFoundException(testAssembliesDirectory.FullName);
			}

			// All .json files in the jobs directory are job's
			foreach (FileInfo jobFile in jobDirectory.GetFiles("*.json"))
			{
				// read the job
				TestJob job = JsonConvert.DeserializeObject<TestJob>(File.ReadAllText(jobFile.FullName));
				// execute the job
				IList<UnitTestAssemblyResult> results = job.Execute(connectedDevices, testAssembliesDirectory.FullName);
				// store the results
				File.WriteAllText(Path.Combine(resultsDirectory.FullName, string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), "#", job.TestId, ".json")), JsonConvert.SerializeObject(results, Formatting.Indented));
				// delete the job file
				jobFile.Delete();
			}
		}
	}
}
