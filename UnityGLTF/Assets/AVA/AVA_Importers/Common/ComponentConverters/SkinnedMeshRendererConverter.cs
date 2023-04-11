
#if UNITY_EDITOR

using System.Collections.Generic;
using oap.stf.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace oap.ava.importer.common
{
    public class SkinnedMeshRenderer_converter : IComponentConverter
    {
		public bool cleanup()
		{
			return false;
		}

		public void convert(GameObject node, GameObject root, Component originalComponent)
        {
			// handle custom material system
        }
    }

	[InitializeOnLoad]
	class Register_SkinnedMeshRenderer_converter
	{
		static Register_SkinnedMeshRenderer_converter()
		{
			CommonConverters.addConverter(typeof(SkinnedMeshRenderer), new SkinnedMeshRenderer_converter());
		}
	}
}

#endif
