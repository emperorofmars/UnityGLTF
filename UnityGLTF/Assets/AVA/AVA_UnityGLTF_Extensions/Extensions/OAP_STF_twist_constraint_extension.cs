
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
	public class OAP_STF_twist_constraint_extension : GLTFProperty, IExtension
	{
		public OAP_STF_twist_constraint_extension() { }

		public OAP_STF_twist_constraint_extension(OAP_STF_twist_constraint_extension ext, GLTFRoot root) : base(ext, root) { }

		public IExtension Clone(GLTFRoot gltfRoot)
		{
			return new OAP_STF_twist_constraint_extension(this, gltfRoot);
		}

		public JProperty Serialize()
		{
			var jo = new JObject();
			JProperty jProperty = new JProperty(OAP_STF_twist_constraintFactory.EXTENSION_NAME, jo);

			jo.Add(new JProperty("source", source_uuid));
			jo.Add(new JProperty(nameof(weight), weight));

			return jProperty;
		}

		public string source_uuid;
		public float weight;
	}

	public class OAP_STF_twist_constraintFactory : ExtensionFactory
	{
		public const string EXTENSION_NAME = "OAP_STF_twist_constraint";

		public OAP_STF_twist_constraintFactory()
		{
			ExtensionName = EXTENSION_NAME;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			if (extensionToken != null)
			{
				var extension = new OAP_STF_twist_constraint_extension();

				JToken source_uuid = extensionToken.Value["source_uuid"];
				JToken weight = extensionToken.Value[nameof(OAP_STF_twist_constraint_extension.weight)];

				extension.source_uuid = source_uuid.Value<string>();
				extension.weight = weight.Value<float>();

				return extension;
			}

			return null;
		}
	}

	public class Construct_OAP_STF_twist_constraint : IExtensionConstructor {
		public Construct_OAP_STF_twist_constraint() {}

		public async Task ConstructComponent(GameObject nodeObj, IExtension extension, AssetCache _assetCache, Func<NodeId, Task<GameObject>> getNode) {
			OAP_STF_twist_constraint_extension _extension = (OAP_STF_twist_constraint_extension)extension;
			var component = nodeObj.AddComponent<OAP_STF_twist_constraint>();
			component.source_uuid = _extension.source_uuid;
			component.weight = _extension.weight;
		}
	}
	
	[InitializeOnLoad]
	public class Register_OAP_STF_twist_constraint {
		static Register_OAP_STF_twist_constraint()
		{
			var definition = new ExtensionDefinition(ExtensionType.NODE, new OAP_STF_twist_constraintFactory(), new Construct_OAP_STF_twist_constraint());
			ExtensionRegistry.registerExtension(definition);
		}
	}
}

#endif
