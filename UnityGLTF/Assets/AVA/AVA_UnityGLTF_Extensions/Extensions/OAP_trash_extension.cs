
#if UNITY_EDITOR

using System;
using Newtonsoft.Json.Linq;
using GLTF.Schema;
using UnityEditor;
using UnityEngine;
using UnityGLTF;
using System.Threading.Tasks;
using oap.stf.Components;
using UnityGLTF.Cache;

namespace oap.stf.Extensions
{
	public class OAP_trash_extension : GLTFProperty, IExtension
	{
		public OAP_trash_extension() { }

		public OAP_trash_extension(OAP_trash_extension ext, GLTFRoot root) : base(ext, root) { }

		public IExtension Clone(GLTFRoot gltfRoot)
		{
			return new OAP_trash_extension(this, gltfRoot);
		}

		public JProperty Serialize()
		{
			var jo = new JObject();
			JProperty jProperty = new JProperty(OAP_trashFactory.EXTENSION_NAME, jo);

			return jProperty;
		}
	}

	public class OAP_trashFactory : ExtensionFactory
	{
		public const string EXTENSION_NAME = "OAP_trash";

		public OAP_trashFactory()
		{
			ExtensionName = EXTENSION_NAME;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			if (extensionToken != null)
			{
				var extension = new OAP_trash_extension();
				return extension;
			}

			return null;
		}
	}

	public class Construct_OAP_trash : IExtensionConstructor {
		public Construct_OAP_trash() {}

		public async Task ConstructComponent(GameObject nodeObj, IExtension extension, AssetCache _assetCache, Func<NodeId, Task<GameObject>> getNode) {
			OAP_trash_extension _extension = (OAP_trash_extension)extension;

			var component = nodeObj.AddComponent<OAP_trash>();
		}
	}
	
	[InitializeOnLoad]
	public class Register_OAP_trash {
		static Register_OAP_trash()
		{
			var definition = new ExtensionDefinition(ExtensionType.NODE, new OAP_trashFactory(), new Construct_OAP_trash());
			ExtensionRegistry.registerExtension(definition);
		}
	}
}

#endif
