
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
	public class OAP_STF_uuid_extension : GLTFProperty, IExtension
	{
		public OAP_STF_uuid_extension() { }

		public OAP_STF_uuid_extension(OAP_STF_uuid_extension ext, GLTFRoot root) : base(ext, root) { }

		public IExtension Clone(GLTFRoot gltfRoot)
		{
			return new OAP_STF_uuid_extension(this, gltfRoot);
		}

		public JProperty Serialize()
		{
			var jo = new JObject();
			JProperty jProperty = new JProperty(OAP_STF_uuidFactory.EXTENSION_NAME, jo);

			jo.Add(new JProperty(nameof(uuid), uuid));

			return jProperty;
		}

		public String uuid;
	}

	public class OAP_STF_uuidFactory : ExtensionFactory
	{
		public const string EXTENSION_NAME = "OAP_STF_uuid";

		public OAP_STF_uuidFactory()
		{
			ExtensionName = EXTENSION_NAME;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			if (extensionToken != null)
			{
				var extension = new OAP_STF_uuid_extension();
				JToken uuid = extensionToken.Value[nameof(OAP_STF_uuid_extension.uuid)];
				extension.uuid = uuid.Value<String>();
				return extension;
			}

			return null;
		}
	}

	public class Construct_OAP_STF_uuid : IExtensionConstructor {
		public Construct_OAP_STF_uuid() {}

		public async Task ConstructComponent(GameObject nodeObj, IExtension extension, Func<NodeId, Task<GameObject>> getNode) {
			OAP_STF_uuid_extension _extension = (OAP_STF_uuid_extension)extension;

			var component = nodeObj.AddComponent<OAP_STF_uuid>();
			component.uuid = _extension.uuid;
		}
	}
	
	[InitializeOnLoad]
	public class Register_OAP_STF_uuid {
		static Register_OAP_STF_uuid()
		{
			var definition = new ExtensionDefinition(ExtensionType.NODE, new OAP_STF_uuidFactory(), new Construct_OAP_STF_uuid());
			ExtensionRegistry.registerExtension(definition);
		}
	}
}

#endif
