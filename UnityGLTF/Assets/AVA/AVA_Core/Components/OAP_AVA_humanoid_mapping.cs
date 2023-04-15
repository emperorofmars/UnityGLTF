using System;
using System.Collections.Generic;
using UnityEngine;

namespace oap.ava.Components
{
	public class OAP_AVA_humanoid_mapping : MonoBehaviour
	{
		public string locomotion_type;
		public Dictionary<string, string> mappings = new Dictionary<string, string>();

		/*public OAP_AVA_humanoid_mapping()
		{
			foreach(string bone in Enum.GetValues(typeof(HumanBodyBones)))
			{
				mappings.Add(bone, null);
			}
		}*/
	}
}
