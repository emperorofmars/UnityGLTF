
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

		public static GameObject convertTree(GameObject rootAVA)
		{
			var nodeDict = new Dictionary<GameObject, GameObject>();
			var targetRoot = deepcopyTree(rootAVA, rootAVA, null, nodeDict);
			convertComponentsInTree(rootAVA, targetRoot, targetRoot, nodeDict);
			return targetRoot;
		}

		private static GameObject deepcopyTree(GameObject rootAVA, GameObject nodeAVA, GameObject targetRoot, Dictionary<GameObject, GameObject> nodeDict)
		{
			GameObject targetNode = new GameObject();
			nodeDict.Add(nodeAVA, targetNode);
			targetNode.name = nodeAVA.name;

			/*OAP_STF_uuid uuidAVA = nodeAVA.GetComponent<OAP_STF_uuid>();
			if(uuidAVA)
			{
				OAP_STF_uuid uuidVRC = targetNode.AddComponent<OAP_STF_uuid>();
				uuidVRC.uuid = uuidAVA.uuid;
			}
			*/
			for(int i = 0; i < nodeAVA.transform.childCount; i++)
			{
				GameObject child = deepcopyTree(rootAVA, nodeAVA.transform.GetChild(i).gameObject, targetRoot != null ? targetRoot : targetNode, nodeDict);
				child.transform.parent = targetNode.transform;
			}
			return targetNode;
		}

		private static void convertComponentsInTree(GameObject rootAVA, GameObject targetRoot, GameObject targetNode, Dictionary<GameObject, GameObject> nodeDict)
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

			convertComponents(rootAVA, nodeAVA, targetRoot, targetNode, nodeDict);
			for(int i = 0; i < targetNode.transform.childCount; i++)
			{
				convertComponentsInTree(rootAVA, targetRoot, targetNode.transform.GetChild(i).gameObject, nodeDict);
			}
		}

		private static void convertComponents(GameObject rootAVA, GameObject nodeAVA, GameObject targetRoot, GameObject targetNode, Dictionary<GameObject, GameObject> nodeDict)
		{
			foreach(Type componentType in CommonConverters.getSupportedTypes())
			{
				Component[] components = nodeAVA.GetComponents(componentType);
				foreach(Component component in components)
				{
					CommonConverters.getConverter(componentType).convert(component, targetNode, targetRoot, rootAVA, nodeDict);
				}
			}
		}
	}
}
