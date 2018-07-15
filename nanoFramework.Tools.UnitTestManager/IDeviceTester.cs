using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nanoFramework.Tools.UnitTestManager
{
    interface IDeviceTester
    {
		void DownloadAllArtifacts();
		void FlashNanoClr();
		void StartDebugger();
		void DeployAssemblies();
    }
}
