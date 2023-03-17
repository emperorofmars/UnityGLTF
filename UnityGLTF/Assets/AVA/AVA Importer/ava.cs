using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityGLTF;

namespace oap
{
	public class ava : ScriptableObject
	{

	}

	public class AVAImporter : AfterImportCallback
	{
		public void afterImport()
		{
			Debug.Log("AAAAAAAAAAAAA");
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
