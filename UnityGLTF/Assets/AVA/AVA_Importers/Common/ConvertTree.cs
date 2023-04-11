
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

		public static GameObject convertTree(GameObject rootAVA, Dictionary<Type, IComponentConverter> appConverters)
		{
			Dictionary<Type, IComponentConverter> converters = new Dictionary<Type, IComponentConverter>();
			foreach(var item in appConverters) converters.Add(item.Key, item.Value);
			foreach(var key in CommonConverters.getSupportedTypes())
			{
				if(!converters.ContainsKey(key)) converters.Add(key, CommonConverters.getConverter(key));
			}
			var targetRoot = Instantiate(rootAVA);
			removeTrash(targetRoot);
			convertComponents(targetRoot, targetRoot, converters);
			cleanupComponents(targetRoot, targetRoot, converters);
			return targetRoot;
		}
		
		private static void removeTrash(GameObject node)
		{
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
				foreach(var childchild in children)
				{
					removeTrash(childchild);
				}
			}
			else
			{
				for(int i = 0; i < node.transform.childCount; i++)
				{
					removeTrash(node.transform.GetChild(i).gameObject);
				}
			}
			return;
		}

		private static void convertComponents(GameObject node, GameObject root, Dictionary<Type, IComponentConverter> converters)
		{
			foreach(var item in converters)
			{
				Component[] components = node.GetComponents(item.Key);
				foreach(Component component in components)
				{
					item.Value.convert(node, root, component);
				}
			}
			for(int i = 0; i < node.transform.childCount; i++)
			{
				convertComponents(node.transform.GetChild(i).gameObject, root, converters);
			}
		}

		private static void cleanupComponents(GameObject node, GameObject root, Dictionary<Type, IComponentConverter> converters)
		{
			foreach(var item in converters)
			{
				if(node.GetComponent(item.Key) != null && item.Value.cleanup())
				{
					Debug.Log(item.Key);
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
