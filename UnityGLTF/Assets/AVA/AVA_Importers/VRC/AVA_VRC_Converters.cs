#if VRCSDK3_FOUND

using System.Collections.Generic;
using oap.ava.Components;
using oap.ava.importer.common;
using oap.stf.Components;
using UnityEditor;
using UnityEngine;

namespace oap.ava.importer.vrc
{
    public class OAP_AVA_avatar_converter : IComponentConverter
    {
        public void convert(Component component, GameObject target, GameObject root, GameObject rootAVA, Dictionary<GameObject, GameObject> nodeDict)
        {
        }
    }

	[InitializeOnLoad]
	class Register_OAP_AVA_avatar_converter
	{
		static Register_OAP_AVA_avatar_converter()
		{
			AVA_Importer_VRC.addConverter(typeof(OAP_AVA_avatar), new OAP_AVA_avatar_converter());
		}
	}
}

#endif
