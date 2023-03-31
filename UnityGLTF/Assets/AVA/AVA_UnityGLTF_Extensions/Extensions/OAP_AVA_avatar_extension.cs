
#if UNITY_EDITOR

using System;
using Newtonsoft.Json.Linq;
using GLTF.Schema;
using UnityEditor;
using UnityEngine;
using UnityGLTF;
using System.Threading.Tasks;
using oap.ava.Components;

namespace oap.ava.Extensions
{
	public class OAP_AVA_avatar_extension : GLTFProperty, IExtension
	{
		public OAP_AVA_avatar_extension() { }

		public OAP_AVA_avatar_extension(OAP_AVA_avatar_extension ext, GLTFRoot root) : base(ext, root) { }

		public IExtension Clone(GLTFRoot gltfRoot)
		{
			return new OAP_AVA_avatar_extension(this, gltfRoot);
		}

		public JProperty Serialize()
		{
			var jo = new JObject();
			JProperty jProperty = new JProperty(OAP_AVA_avatarFactory.EXTENSION_NAME, jo);

			jo.Add(new JProperty("name", avatar_name));
			jo.Add(new JProperty("version", avatar_version));

			return jProperty;
		}

		public String avatar_name;
		public String avatar_version;
	}

	public class OAP_AVA_avatarFactory : ExtensionFactory
	{
		public const string EXTENSION_NAME = "OAP_AVA_avatar";

		public OAP_AVA_avatarFactory()
		{
			ExtensionName = EXTENSION_NAME;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			if (extensionToken != null)
			{
				var extension = new OAP_AVA_avatar_extension();
				JToken avatar_name = extensionToken.Value["name"];
				JToken avatar_version = extensionToken.Value["version"];
				extension.avatar_name = avatar_name.Value<String>();
				extension.avatar_version = avatar_version.Value<String>();
				return extension;
			}

			return null;
		}
	}

	public class Construct_OAP_AVA_avatar : IExtensionConstructor {
		public Construct_OAP_AVA_avatar() {}

		public async Task ConstructComponent(GameObject nodeObj, IExtension extension, Func<NodeId, Task<GameObject>> getNode) {
			OAP_AVA_avatar_extension _extension = (OAP_AVA_avatar_extension)extension;

			var component = nodeObj.AddComponent<OAP_AVA_avatar>();
			component.avatar_name = _extension.avatar_name;
			component.avatar_version = _extension.avatar_version;
		}
	}
	
	[InitializeOnLoad]
	public class Register_OAP_AVA_avatar {
		static Register_OAP_AVA_avatar()
		{
			var definition = new ExtensionDefinition(ExtensionType.NODE, new OAP_AVA_avatarFactory(), new Construct_OAP_AVA_avatar());
			ExtensionRegistry.registerExtension(definition);
		}
	}
}

#endif
