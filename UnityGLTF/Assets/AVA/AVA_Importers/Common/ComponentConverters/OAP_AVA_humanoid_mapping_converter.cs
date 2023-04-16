
#if UNITY_EDITOR

using System.Collections.Generic;
using oap.ava.Components;
using oap.stf.Components;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace oap.ava.importer.common
{
    public class OAP_AVA_humanoid_mapping_converter : IComponentConverter
    {
		public bool cleanup()
		{
			return true;
		}
		
        public void convert(GameObject node, GameObject root, Component originalComponent)
        {
			OAP_AVA_humanoid_mapping component = (OAP_AVA_humanoid_mapping)originalComponent;
			
			Animator animator = root.GetComponent<Animator>();
			if(!animator)
			{
				animator = root.AddComponent<Animator>();
			}
			animator.applyRootMotion = true;
			animator.updateMode = AnimatorUpdateMode.Normal;
			animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;

			Debug.Log(component.mappings.Count);

			var mappings = component.mappings.FindAll(mapping => mapping.bone != null && mapping.bone.Length > 0 && mapping.uuid != null && mapping.uuid.Length > 0);
			foreach(var mapping in mappings)
			{
				mapping.uuid = TreeUtils.findByUUID(root, mapping.uuid).name;
				mapping.bone = component.translateHumanoidAVAtoUnity(mapping.bone);
			}

			var humanDescription = new HumanDescription
			{
				human = mappings.FindAll(mapping => mapping.bone != null && mapping.bone.Length > 0 && mapping.uuid != null && mapping.uuid.Length > 0).Select(mapping =>
				{
					var bone = new HumanBone {humanName = mapping.bone, boneName = mapping.uuid};
					Debug.Log(bone.humanName + " : " + bone.boneName);
					bone.limit.useDefaultValues = true;
					return bone;
				}).ToArray()
			};
			/*var humanDescription = new HumanDescription();
			humanDescription.human = new HumanBone[] {};
			foreach(var mapping in component.mappings)
			{
				if(mapping.bone != null && mapping.bone.Length > 0 && mapping.uuid != null && mapping.uuid.Length > 0)
				{
					var boneGO = TreeUtils.findByUUID(root, mapping.uuid);
					var humanName = component.translateHumanoidAVAtoUnity(mapping.bone);
					if(humanName != null && humanName.Length > 0)
					{
						Debug.Log(humanName + " : " + boneGO.name);
						var bone = new HumanBone {humanName = humanName, boneName = boneGO.name};
						bone.limit.useDefaultValues = true;
						humanDescription.human.Append(bone);
					}
				}
			}*/

			var avatar = AvatarBuilder.BuildHumanAvatar(root, humanDescription);
			avatar.name = root.name;
			if (!avatar.isValid)
			{
				Debug.LogError("Invalid humanoid avatar");
			}
			animator.avatar = avatar;

			var path = $"Assets/{avatar.name.Replace(':', '_')}.ht";
			AssetDatabase.CreateAsset(avatar, path);
        }
    }

	[InitializeOnLoad]
	class Register_OAP_AVA_humanoid_mapping_converter
	{
		static Register_OAP_AVA_humanoid_mapping_converter()
		{
			CommonConverters.addConverter(typeof(OAP_AVA_humanoid_mapping), new OAP_AVA_humanoid_mapping_converter());
		}
	}
}

#endif
