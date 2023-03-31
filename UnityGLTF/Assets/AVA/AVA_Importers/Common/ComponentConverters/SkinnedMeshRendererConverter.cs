
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
        public void convert(Component component, GameObject target, GameObject root, GameObject rootAVA, Dictionary<GameObject, GameObject> nodeDict)
        {
			SkinnedMeshRenderer original = (SkinnedMeshRenderer)component;
			var c = target.AddComponent<SkinnedMeshRenderer>();
			ComponentUtility.CopyComponent(original);
            ComponentUtility.PasteComponentValues(c);

			Debug.Log("BBBBBBBBBBBBBBBBBBBBBBB");
			Debug.Log(original.sharedMesh);
			Debug.Log(AssetDatabase.GetAssetPath(original.sharedMesh));
			Debug.Log(AssetDatabase.GetAssetPath(original.GetInstanceID()));
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
