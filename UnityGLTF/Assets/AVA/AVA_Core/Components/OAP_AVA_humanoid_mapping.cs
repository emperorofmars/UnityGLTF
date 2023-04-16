using System;
using System.Collections.Generic;
using UnityEngine;

namespace oap.ava.Components
{
	[Serializable]
	public class BoneMappingPair
	{
		public BoneMappingPair(string bone, string uuid)
		{
			this.bone = bone;
			this.uuid = uuid;
		}

		public string bone;
		public string uuid;
	}

	public class OAP_AVA_humanoid_mapping : MonoBehaviour
	{
		private static Dictionary<string, string> translations = new Dictionary<string, string> {
			{"Hip", "Hips"},
			{"Spine", "Spine"},
			{"Chest", "Chest"},
			{"UpperChest", "UpperChest"},
			{"Neck", "Neck"},
			{"Head", "Head"},
			{"Jaw", "Jaw"},
			{"EyeLeft", "LeftEye"},
			{"EyeRight", "RightEye"},
			{"ShoulderLeft", "LeftShoulder"},
			{"UpperArmLeft", "LeftUpperArm"},
			{"LowerArmLeft", "LeftLowerArm"},
			{"HandLeft", "LeftHand"},
			{"ShoulderRight", "RightShoulder"},
			{"UpperArmRight", "RightUpperArm"},
			{"LowerArmRight", "RightLowerArm"},
			{"HandRight", "RightHand"},
			{"UpperLegLeft", "LeftUpperLeg"},
			{"LowerLegLeft", "LeftLowerLeg"},
			{"FootLeft", "LeftFoot"},
			{"ToesLeft", "LeftToes"},
			{"UpperLegRight", "RightUpperLeg"},
			{"LowerLegRight", "RightLowerLeg"},
			{"FootRight", "RightFoot"},
			{"ToesRight", "RightToes"},
			{"FingerThumb1Left", ""},
			{"FingerThumb2Left", ""},
			{"FingerThumb3Left", ""},
			{"FingerIndex1Left", ""},
			{"FingerIndex2Left", ""},
			{"FingerIndex3Left", ""},
			{"FingerMiddle1Left", ""},
			{"FingerMiddle2Left", ""},
			{"FingerMiddle3Left", ""},
			{"FingerRing1Left", ""},
			{"FingerRing2Left", ""},
			{"FingerRing3Left", ""},
			{"FingerLittle1Left", ""},
			{"FingerLittle2Left", ""},
			{"FingerLittle3Left", ""},
			{"FingerThumb1Right", ""},
			{"FingerThumb2Right", ""},
			{"FingerThumb3Right", ""},
			{"FingerIndex1Right", ""},
			{"FingerIndex2Right", ""},
			{"FingerIndex3Right", ""},
			{"FingerMiddle1Right", ""},
			{"FingerMiddle2Right", ""},
			{"FingerMiddle3Right", ""},
			{"FingerRing1Right", ""},
			{"FingerRing2Right", ""},
			{"FingerRing3Right", ""},
			{"FingerLittle1Right", ""},
			{"FingerLittle2Right", ""},
			{"FingerLittle3Right", ""},
		};

		public string locomotion_type;
		public List<BoneMappingPair> mappings = new List<BoneMappingPair>();

		public OAP_AVA_humanoid_mapping()
		{
			foreach(string avaBone in translations.Keys)
			{
				mappings.Add(new BoneMappingPair(avaBone, null));
			}
		}

		public string translateHumanoidAVAtoUnity(string avaName)
		{
			if(translations[avaName] != null) return translations[avaName];
			return avaName;
		}
	}
}
