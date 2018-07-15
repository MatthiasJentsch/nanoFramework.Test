using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nanoFramework.Tools.UnitTestManager.Models;

namespace nanoFramework.Tools.UnitTestManager.Controllers
{
	[Route("[controller]")]
	public class DiscoveryController : Controller
	{
		private readonly ITestService _testService;

		public DiscoveryController(ITestService testService)
		{
			_testService = testService;
		}

		[HttpGet]
		public List<AvailableTestDevice> GetTestDevices()
		{
			List<AvailableTestDevice> devices = new List<AvailableTestDevice>();
			foreach (string deviceType in _testService.ConnectedDeviceTypes)
			{
				AvailableTestDevice device = new AvailableTestDevice
				{
					DeviceType = deviceType,
					IsReadyForNewTest = _testService.IsDeviceTypeAvailable(deviceType)
				};
				devices.Add(device);
			}
			return devices;
		}
	}

	[Route("[controller]")]
	public class StartController : Controller
	{
		private readonly ITestService _testService;

		public StartController(ITestService testService)
		{
			_testService = testService;
		}

		[HttpGet("{deviceType}")]
		public RunningTest StartTest(string deviceType, string artifactname = null, string artifactDownloadPath = null)
		{
			return _testService.StartNewTest(deviceType, artifactname, artifactDownloadPath);
		}
	}

	[Route("[controller]")]
	public class ResultController : Controller
	{
		private readonly ITestService _testService;

		public ResultController(ITestService testService)
		{
			_testService = testService;
		}

		[HttpGet("{testId}")]
		public TestResult GetResult(Guid testId)
		{
			return _testService.GetTestResults(testId);
		}
    }
}
