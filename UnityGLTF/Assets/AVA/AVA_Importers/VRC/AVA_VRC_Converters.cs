
#if UNITY_EDITOR
#if VRCSDK3_FOUND

using System.Collections.Generic;
using oap.ava.Components;
using oap.ava.importer.common;
using oap.stf.Components;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace oap.ava.importer.vrc
{
    public class OAP_AVA_avatar_converter : IComponentConverter
    {
		public bool cleanup()
		{
			return true;
		}
		
        public void convert(GameObject node, GameObject root, Component originalComponent)
        {
			var animator = node.AddComponent<Animator>();
			var avatarDescriptor = node.AddComponent<VRCAvatarDescriptor>();
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
#endif
