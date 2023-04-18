
#if UNITY_EDITOR

using System;
using Newtonsoft.Json.Linq;
using GLTF.Schema;
using UnityEditor;
using UnityEngine;
using UnityGLTF;
using System.Threading.Tasks;
using oap.ava.Components;
using System.Collections.Generic;
using GLTF.Extensions;

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
			//jo.Add(new JProperty("icon", icon));
			jo.Add(new JProperty("author", author));
			jo.Add(new JProperty("license_link", license_link));
			jo.Add(new JProperty("license", license));
			jo.Add(new JProperty("viewport_parent_uuid", viewport_parent_uuid));
			jo.Add(new JProperty("viewport_position", viewport_position));
			jo.Add(new JProperty("voice_parent_uuid", voice_parent_uuid));
			jo.Add(new JProperty("voice_position", voice_position));

			return jProperty;
		}

		public String avatar_name;
		public String avatar_version;
		public Texture2D icon;
		public String author;
		public String license_link;
		public String license;
		public String viewport_parent_uuid;
		public Vector3 viewport_position;
		public String voice_parent_uuid;
		public Vector3 voice_position;
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
				//JToken icon = extensionToken.Value["icon"];
				JToken author = extensionToken.Value["author"];
				JToken license_link = extensionToken.Value["license-link"];
				JToken license = extensionToken.Value["license"];
				JToken viewport_parent_uuid = extensionToken.Value["viewport-parent-uuid"];
				JToken viewport_position = extensionToken.Value["viewport-position"];
				JToken voice_parent_uuid = extensionToken.Value["voice-parent-uuid"];
				JToken voice_position = extensionToken.Value["voice-position"];
				extension.avatar_name = avatar_name.Value<String>();
				extension.avatar_version = avatar_version.Value<String>();
				//extension.icon = avatar_version.Value<GLTFTexture>();
				if(author != null) extension.author = author.Value<String>();
				if(license_link != null) extension.license_link = license_link.Value<String>();
				if(license != null) extension.license = license.Value<String>();
				if(viewport_parent_uuid != null) extension.viewport_parent_uuid = viewport_parent_uuid.Value<String>();
				if(viewport_position != null)
				{
					var vec3 = ((JArray)viewport_position).CreateReader().ReadAsVector3();
					extension.viewport_position = new Vector3(vec3.X, vec3.Y, vec3.Z);
				}
				if(voice_parent_uuid != null) extension.voice_parent_uuid = voice_parent_uuid.Value<String>();
				if(voice_position != null)
				{
					var vec3 = ((JArray)voice_position).CreateReader().ReadAsVector3();
					extension.voice_position = new Vector3(vec3.X, vec3.Y, vec3.Z);
				}
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
			component.icon = _extension.icon;
			component.author = _extension.author;
			component.license_link = _extension.license_link;
			component.license = _extension.license;
			component.viewport_parent_uuid = _extension.viewport_parent_uuid;
			component.viewport_position = _extension.viewport_position;
			component.voice_parent_uuid = _extension.voice_parent_uuid;
			component.voice_position = _extension.voice_position;
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
