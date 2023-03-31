
#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using oap.ava.Components;
using oap.stf.Components;
using UnityEngine;

namespace oap.ava.importer.common
{
	public class ConvertTree: ScriptableObject
	{
		static Type[] SUPPORTED_TYPES = {typeof(OAP_AVA_avatar), typeof(OAP_STF_twist_constraint), typeof(SkinnedMeshRenderer)};

		public static GameObject convertTree(GameObject rootAVA, Dictionary<Type, IComponentConverter> appConverters)
		{
			Dictionary<Type, IComponentConverter> converters = new Dictionary<Type, IComponentConverter>();
			foreach(var item in appConverters) converters.Add(item.Key, item.Value);
			foreach(var key in CommonConverters.getSupportedTypes())
			{
				if(!converters.ContainsKey(key)) converters.Add(key, CommonConverters.getConverter(key));
			}

			var nodeDict = new Dictionary<GameObject, GameObject>();
			var targetRoot = deepcopyTree(rootAVA, rootAVA, null, nodeDict);
			convertComponentsInTree(rootAVA, targetRoot, targetRoot, nodeDict, converters);
			return targetRoot;
		}

		private static GameObject deepcopyTree(GameObject rootAVA, GameObject nodeAVA, GameObject targetRoot, Dictionary<GameObject, GameObject> nodeDict)
		{
			GameObject targetNode = new GameObject();
			nodeDict.Add(nodeAVA, targetNode);
			targetNode.name = nodeAVA.name;

			for(int i = 0; i < nodeAVA.transform.childCount; i++)
			{
				GameObject child = deepcopyTree(rootAVA, nodeAVA.transform.GetChild(i).gameObject, targetRoot != null ? targetRoot : targetNode, nodeDict);
				child.transform.parent = targetNode.transform;
			}
			return targetNode;
		}

		private static void convertComponentsInTree(GameObject rootAVA, GameObject targetRoot, GameObject targetNode, Dictionary<GameObject, GameObject> nodeDict, Dictionary<Type, IComponentConverter> converters)
		{
			GameObject nodeAVA = null;
			foreach(var item in nodeDict)
			{
				if(item.Value == targetNode)
				{
					nodeAVA = item.Key;
					break;
				}
			}

			convertComponents(rootAVA, nodeAVA, targetRoot, targetNode, nodeDict, converters);
			for(int i = 0; i < targetNode.transform.childCount; i++)
			{
				convertComponentsInTree(rootAVA, targetRoot, targetNode.transform.GetChild(i).gameObject, nodeDict, converters);
			}
		}

		private static void convertComponents(GameObject rootAVA, GameObject nodeAVA, GameObject targetRoot, GameObject targetNode, Dictionary<GameObject, GameObject> nodeDict, Dictionary<Type, IComponentConverter> converters)
		{
			foreach(var item in converters)
			{
				Component[] components = nodeAVA.GetComponents(item.Key);
				foreach(Component component in components)
				{
					item.Value.convert(component, targetNode, targetRoot, rootAVA, nodeDict);
				}
			}
		}
	}
}

#endif
