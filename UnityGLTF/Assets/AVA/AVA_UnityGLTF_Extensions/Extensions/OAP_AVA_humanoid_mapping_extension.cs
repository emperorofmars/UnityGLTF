
#if UNITY_EDITOR

using System;
using Newtonsoft.Json.Linq;
using GLTF.Schema;
using UnityEditor;
using UnityEngine;
using UnityGLTF;
using System.Threading.Tasks;
using System.Collections.Generic;
using oap.ava.Components;
using Newtonsoft.Json;
using GLTF;
using UnityGLTF.Cache;

namespace oap.ava.Extensions
{
	public class OAP_AVA_humanoid_mapping_extension : GLTFProperty, IExtension
	{
		public OAP_AVA_humanoid_mapping_extension() { }

		public OAP_AVA_humanoid_mapping_extension(OAP_AVA_humanoid_mapping_extension ext, GLTFRoot root) : base(ext, root) { }

		public IExtension Clone(GLTFRoot gltfRoot)
		{
			return new OAP_AVA_humanoid_mapping_extension(this, gltfRoot);
		}

		public JProperty Serialize()
		{
			var jo = new JObject();
			JProperty jProperty = new JProperty(OAP_AVA_humanoid_mappingFactory.EXTENSION_NAME, jo);

			jo.Add(new JProperty("locomotion-type", locomotion_type));
			var mappings_jo = new JObject("mappings", mappings);
			jo.Add(mappings_jo);
			foreach(var mapping in mappings)
			{
				mappings_jo.Add(new JProperty(mapping.bone, mapping.uuid));
			}

			return jProperty;
		}

		public String locomotion_type;
		public List<BoneMappingPair> mappings = new List<BoneMappingPair>();
	}

	public class OAP_AVA_humanoid_mappingFactory : ExtensionFactory
	{
		public const string EXTENSION_NAME = "OAP_AVA_humanoid_mapping";

		public OAP_AVA_humanoid_mappingFactory()
		{
			ExtensionName = EXTENSION_NAME;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			if (extensionToken != null)
			{
				var extension = new OAP_AVA_humanoid_mapping_extension();
				JToken locomotion_type = extensionToken.Value["locomotion-type"];
				extension.locomotion_type = locomotion_type.Value<String>();

				extension.mappings = new List<BoneMappingPair>();
				JObject mappings = (JObject)JToken.ReadFrom(extensionToken.Value["mappings"].CreateReader());
				foreach(JToken child in mappings.Children())
				{
					if (child.Type != JTokenType.Property) throw new GLTFParseException("FML");
					JProperty childAsJProperty = (JProperty)child;
					extension.mappings.Add(new BoneMappingPair(childAsJProperty.Name, childAsJProperty.Value.Value<String>()));
				}

				return extension;
			}
			return null;
		}
	}

	public class Construct_OAP_AVA_humanoid_mapping : IExtensionConstructor {
		public Construct_OAP_AVA_humanoid_mapping() {}

		public async Task ConstructComponent(GameObject nodeObj, IExtension extension, AssetCache _assetCache, Func<NodeId, Task<GameObject>> getNode) {
			OAP_AVA_humanoid_mapping_extension _extension = (OAP_AVA_humanoid_mapping_extension)extension;

			var component = nodeObj.AddComponent<OAP_AVA_humanoid_mapping>();
			component.locomotion_type = _extension.locomotion_type;
			component.mappings = _extension.mappings;
		}
	}
	
	[InitializeOnLoad]
	public class Register_OAP_AVA_humanoid_mapping {
		static Register_OAP_AVA_humanoid_mapping()
		{
			var definition = new ExtensionDefinition(ExtensionType.NODE, new OAP_AVA_humanoid_mappingFactory(), new Construct_OAP_AVA_humanoid_mapping());
			ExtensionRegistry.registerExtension(definition);
		}
	}
}

#endif
