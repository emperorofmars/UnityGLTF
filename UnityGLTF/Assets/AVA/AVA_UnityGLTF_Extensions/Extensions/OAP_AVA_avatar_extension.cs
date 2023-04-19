
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
using UnityGLTF.Cache;

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
			jo.Add(new JProperty("license-link", license_link));
			jo.Add(new JProperty("license", license));
			jo.Add(new JProperty("viewport-parent-uuid", viewport_parent_uuid));
			jo.Add(new JProperty("viewport-position", new JArray(viewport_position.x, viewport_position.y, viewport_position.z)));
			jo.Add(new JProperty("voice-parent-uuid", voice_parent_uuid));
			jo.Add(new JProperty("voice-position", new JArray(voice_position.x, voice_position.y, voice_position.z)));

			return jProperty;
		}

		public String avatar_name;
		public String avatar_version;
		public TextureInfo icon;
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
				JToken viewport_position = extensionToken.Value["viewport-position"];
				JToken voice_position = extensionToken.Value["voice-position"];

				extension.avatar_name = extensionToken.Value["name"]?.Value<String>();
				extension.avatar_version = extensionToken.Value["version"]?.Value<String>();

				extension.icon = extensionToken.Value["icon"]?.DeserializeAsTexture(root);
				/*var icon = extensionToken.Value["icon"];
				if(icon != null)
				{
					Debug.Log("AAAAAAAAAAAAAAAAAAAA");
					extension.icon = TextureInfo.Deserialize(root, icon.CreateReader());
				}*/

				extension.author = extensionToken.Value["author"]?.Value<String>();
				extension.license_link = extensionToken.Value["license-link"]?.Value<String>();
				extension.license = extensionToken.Value["license"]?.Value<String>();
				extension.viewport_parent_uuid = extensionToken.Value["viewport-parent-uuid"]?.Value<String>();
				if(viewport_position != null)
				{
					var vec3 = ((JArray)viewport_position).CreateReader().ReadAsVector3();
					extension.viewport_position = new Vector3(vec3.X, vec3.Y, vec3.Z);
				}
				extension.voice_parent_uuid = extensionToken.Value["voice-parent-uuid"]?.Value<String>();
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

		public async Task ConstructComponent(GameObject nodeObj, IExtension extension, AssetCache _assetCache, Func<NodeId, Task<GameObject>> getNode) {
			OAP_AVA_avatar_extension _extension = (OAP_AVA_avatar_extension)extension;

			var component = nodeObj.AddComponent<OAP_AVA_avatar>();
			component.avatar_name = _extension.avatar_name;
			component.avatar_version = _extension.avatar_version;
			if(_extension.icon != null) component.icon = _assetCache.TextureCache[_extension.icon.Index.Id]?.Texture;
			foreach(var cache in _assetCache.TextureCache)
			{
				Debug.Log(cache);
			}
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
