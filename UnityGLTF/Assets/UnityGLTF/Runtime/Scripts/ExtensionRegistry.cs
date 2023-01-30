using System.Collections.Generic;
using GLTF.Schema;
using System;
using UnityEngine;
using System.Threading.Tasks;

namespace UnityGLTF
{
	public enum ExtensionType {
		NODE, MESH, MATERIAL
	}

	public interface IExtensionConstructor
	{
		Task ConstructComponent(GameObject nodeObj, IExtension extension, Func<NodeId, Task<GameObject>> getNode);
	}

	public class ExtensionDefinition
	{
		public ExtensionDefinition(ExtensionType type, ExtensionFactory factory, IExtensionConstructor constructor)
		{
			this.type = type;
			this.factory = factory;
			this.constructor = constructor;
		}

		public ExtensionType type;
		public ExtensionFactory factory;
		public IExtensionConstructor constructor;
	}

    public class ExtensionRegistry
    {
        private static Dictionary<ExtensionType, Dictionary<string, ExtensionDefinition>> _extensionRegistry = new Dictionary<ExtensionType, Dictionary<string, ExtensionDefinition>>();

		public static void registerExtension(ExtensionDefinition ext)
		{
			lock (_extensionRegistry)
			{
				if(!_extensionRegistry.ContainsKey(ext.type))
					_extensionRegistry.Add(ext.type, new Dictionary<string, ExtensionDefinition>());
				_extensionRegistry[ext.type].Add(ext.factory.ExtensionName, ext);
				GLTFRoot.RegisterExtension(ext.factory);
			}
		}

		public static bool hasExtension(ExtensionType type, string name) {
			return _extensionRegistry.ContainsKey(type) && _extensionRegistry[type].ContainsKey(name);
		}

		public static IExtensionConstructor getConstructor(ExtensionType type, string name)
		{
			return _extensionRegistry[type][name].constructor;
		}
    }
}