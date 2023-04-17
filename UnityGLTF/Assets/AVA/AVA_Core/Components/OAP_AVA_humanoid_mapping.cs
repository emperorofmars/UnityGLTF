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
			{"Hip", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.Hips)},
			{"Spine", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.Spine)},
			{"Chest", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.Chest)},
			{"UpperChest", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.UpperChest)},
			{"Neck", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.Neck)},
			{"Head", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.Head)},
			{"Jaw", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.Jaw)},
			{"EyeLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftEye)},
			{"EyeRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightEye)},
			{"ShoulderLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftShoulder)},
			{"UpperArmLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftUpperArm)},
			{"LowerArmLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftLowerArm)},
			{"HandLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftHand)},
			{"ShoulderRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightShoulder)},
			{"UpperArmRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightUpperArm)},
			{"LowerArmRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightLowerArm)},
			{"HandRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightHand)},
			{"UpperLegLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftUpperLeg)},
			{"LowerLegLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftLowerLeg)},
			{"FootLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftFoot)},
			{"ToesLeft", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftToes)},
			{"UpperLegRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightUpperLeg)},
			{"LowerLegRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightLowerLeg)},
			{"FootRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightFoot)},
			{"ToesRight", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightToes)},
			{"FingerThumb1Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftThumbProximal)},
			{"FingerThumb2Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftThumbIntermediate)},
			{"FingerThumb3Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftThumbDistal)},
			{"FingerIndex1Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftIndexProximal)},
			{"FingerIndex2Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftIndexIntermediate)},
			{"FingerIndex3Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftIndexDistal)},
			{"FingerMiddle1Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftMiddleProximal)},
			{"FingerMiddle2Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftMiddleIntermediate)},
			{"FingerMiddle3Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftMiddleDistal)},
			{"FingerRing1Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftRingProximal)},
			{"FingerRing2Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftRingIntermediate)},
			{"FingerRing3Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftRingDistal)},
			{"FingerLittle1Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftLittleProximal)},
			{"FingerLittle2Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftLittleIntermediate)},
			{"FingerLittle3Left", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.LeftLittleDistal)},
			{"FingerThumb1Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightThumbProximal)},
			{"FingerThumb2Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightThumbIntermediate)},
			{"FingerThumb3Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightThumbDistal)},
			{"FingerIndex1Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightIndexProximal)},
			{"FingerIndex2Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightIndexIntermediate)},
			{"FingerIndex3Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightIndexDistal)},
			{"FingerMiddle1Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightMiddleProximal)},
			{"FingerMiddle2Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightMiddleIntermediate)},
			{"FingerMiddle3Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightMiddleDistal)},
			{"FingerRing1Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightRingProximal)},
			{"FingerRing2Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightRingIntermediate)},
			{"FingerRing3Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightRingDistal)},
			{"FingerLittle1Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightLittleProximal)},
			{"FingerLittle2Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightLittleIntermediate)},
			{"FingerLittle3Right", Enum.GetName(typeof(HumanBodyBones), HumanBodyBones.RightLittleDistal)},
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
