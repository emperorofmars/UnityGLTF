
#if UNITY_EDITOR

using System.Collections.Generic;
using oap.stf.Components;
using UnityEditor;
using UnityEngine;

namespace oap.ava.importer.common
{
    public class OAP_STF_twist_constraint_converter : IComponentConverter
    {
        public void convert(Component component, GameObject target, GameObject root, GameObject rootAVA, Dictionary<GameObject, GameObject> nodeDict)
        {
			OAP_STF_twist_constraint s = (OAP_STF_twist_constraint)component;
            var ret = target.AddComponent<UnityEngine.Animations.RotationConstraint>();
			ret.weight = s.weight;
			ret.rotationAxis = UnityEngine.Animations.Axis.Y;

			var source = new UnityEngine.Animations.ConstraintSource();
			source.weight = 1;
			GameObject sourceTransformGO = TreeUtils.findByUUID(rootAVA, s.source_uuid);
			if(sourceTransformGO != null) source.sourceTransform = nodeDict[sourceTransformGO].transform;

			ret.AddSource(source);
			ret.locked = true;
			ret.constraintActive = true;
        }
    }

	[InitializeOnLoad]
	class Register_OAP_STF_twist_constraint_converter
	{
		static Register_OAP_STF_twist_constraint_converter()
		{
			CommonConverters.addConverter(typeof(OAP_STF_twist_constraint), new OAP_STF_twist_constraint_converter());
		}
	}
}

#endif
