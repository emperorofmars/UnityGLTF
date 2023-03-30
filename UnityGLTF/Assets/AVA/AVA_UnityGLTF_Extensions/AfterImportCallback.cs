using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityGLTF;

namespace oap.ava
{
	public class AVAImporter : AfterImportCallback
	{
		public void afterImport()
		{
			RegisteredImporters.run();
		}
	}



	[InitializeOnLoad]
	public class Register_UnityGLTF_afterimport_callback {
		static Register_UnityGLTF_afterimport_callback()
		{
			GLTFImporter.afterImportCallback = new AVAImporter();
		}
	}
}
