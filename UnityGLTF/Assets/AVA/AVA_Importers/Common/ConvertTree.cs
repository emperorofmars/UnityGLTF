
#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using oap.ava.Components;
using oap.stf.Components;
using UnityEngine;
using UnityGLTF;

namespace oap.ava.importer.common
{
	public class ConvertTree: ScriptableObject
	{
		static Type[] SUPPORTED_TYPES = {typeof(OAP_AVA_avatar), typeof(OAP_STF_twist_constraint), typeof(SkinnedMeshRenderer)};

		public static GameObject convertTree(GameObject rootAVA, Dictionary<Type, IComponentConverter> appConverters, string assetName)
		{
			/*Dictionary<Type, IComponentConverter> converters = new Dictionary<Type, IComponentConverter>();
			foreach(var item in CommonConverters.getConverters()) converters.Add(item.Key, item.Value);
			foreach(var item in appConverters) converters.Add(item.Key, item.Value);
			foreach(var key in CommonConverters.getSupportedTypes())
			{
				if(!converters.ContainsKey(key)) converters.Add(key, CommonConverters.getConverter(key));
			}*/
			
			GameObject targetRoot;
			if(rootAVA.transform.childCount == 1)
			{
				var child = rootAVA.transform.GetChild(0).gameObject;
				if(child.GetComponent<OAP_true_root>() != null)
				{
					targetRoot = Instantiate(child);
					DestroyImmediate(targetRoot.GetComponent<OAP_true_root>());
				}
				else
				{
					targetRoot = Instantiate(rootAVA);
				}
			}
			else targetRoot = Instantiate(rootAVA);
			removeTrash(targetRoot);

			convertComponents(targetRoot, targetRoot, CommonConverters.getConverters(), assetName);
			convertComponents(targetRoot, targetRoot, appConverters, assetName);

			cleanupComponents(targetRoot, targetRoot, CommonConverters.getConverters());
			cleanupComponents(targetRoot, targetRoot, appConverters);
			return targetRoot;
		}
		
		private static bool removeTrash(GameObject node)
		{
			var self_removed = false;
			if(node.GetComponent<OAP_trash>() != null)
			{
				var children = new List<GameObject>();
				for(int i = 0; i < node.transform.childCount; i++)
				{
					if(node.transform.parent != null)
					{
						var child = node.transform.GetChild(i).gameObject;
						child.transform.SetParent(node.transform.parent.transform);
						children.Add(child);
						i--;
					}
				}
				DestroyImmediate(node);
				self_removed = true;
				foreach(var childchild in children)
				{
					removeTrash(childchild);
				}
			}
			else
			{
				for(int i = 0; i < node.transform.childCount; i++)
				{
					if(removeTrash(node.transform.GetChild(i).gameObject)) i--;
				}
			}
			return self_removed;
		}

		private static void convertComponents(GameObject node, GameObject root, Dictionary<Type, IComponentConverter> converters, string assetName)
		{
			foreach(var item in converters)
			{
				Component[] components = node.GetComponents(item.Key);
				foreach(Component component in components)
				{
					item.Value.convert(node, root, component, assetName);
				}
			}
			for(int i = 0; i < node.transform.childCount; i++)
			{
				convertComponents(node.transform.GetChild(i).gameObject, root, converters, assetName);
			}
		}

		private static void cleanupComponents(GameObject node, GameObject root, Dictionary<Type, IComponentConverter> converters)
		{
			foreach(var item in converters)
			{
				if(node.GetComponent(item.Key) != null && item.Value.cleanup())
				{
					DestroyImmediate(node.GetComponent(item.Key));
				}
			}
			if(node.GetComponent<OAP_STF_uuid>() != null)
			{
				DestroyImmediate(node.GetComponent<OAP_STF_uuid>());
			}
			if(node.GetComponent<DefaultComponent>() != null)
			{
				DestroyImmediate(node.GetComponent<DefaultComponent>());
			}
			for(int i = 0; i < node.transform.childCount; i++)
			{
				cleanupComponents(node.transform.GetChild(i).gameObject, root, converters);
			}
		}
	}
}

#endif
