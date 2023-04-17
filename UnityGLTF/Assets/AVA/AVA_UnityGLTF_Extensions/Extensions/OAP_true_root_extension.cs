
#if UNITY_EDITOR

using System;
using Newtonsoft.Json.Linq;
using GLTF.Schema;
using UnityEditor;
using UnityEngine;
using UnityGLTF;
using System.Threading.Tasks;
using oap.stf.Components;

namespace oap.stf.Extensions
{
	public class OAP_true_root_extension : GLTFProperty, IExtension
	{
		public OAP_true_root_extension() { }

		public OAP_true_root_extension(OAP_true_root_extension ext, GLTFRoot root) : base(ext, root) { }

		public IExtension Clone(GLTFRoot gltfRoot)
		{
			return new OAP_true_root_extension(this, gltfRoot);
		}

		public JProperty Serialize()
		{
			var jo = new JObject();
			JProperty jProperty = new JProperty(OAP_true_rootFactory.EXTENSION_NAME, jo);

			return jProperty;
		}
	}

	public class OAP_true_rootFactory : ExtensionFactory
	{
		public const string EXTENSION_NAME = "OAP_true_root";

		public OAP_true_rootFactory()
		{
			ExtensionName = EXTENSION_NAME;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			if (extensionToken != null)
			{
				var extension = new OAP_true_root_extension();
				return extension;
			}

			return null;
		}
	}

	public class Construct_OAP_true_root : IExtensionConstructor {
		public Construct_OAP_true_root() {}

		public async Task ConstructComponent(GameObject nodeObj, IExtension extension, Func<NodeId, Task<GameObject>> getNode) {
			OAP_true_root_extension _extension = (OAP_true_root_extension)extension;

			var component = nodeObj.AddComponent<OAP_true_root>();
		}
	}
	
	[InitializeOnLoad]
	public class Register_OAP_true_root {
		static Register_OAP_true_root()
		{
			var definition = new ExtensionDefinition(ExtensionType.NODE, new OAP_true_rootFactory(), new Construct_OAP_true_root());
			ExtensionRegistry.registerExtension(definition);
		}
	}
}

#endif
