using System;
using Newtonsoft.Json.Linq;
using GLTF.Schema;
using UnityEditor;
using UnityEngine;
using UnityGLTF;
using System.Threading.Tasks;

namespace oap.stf.Extensions
{
	public class OAP_STF_twist_constraint : GLTFProperty, IExtension
	{
		public OAP_STF_twist_constraint() { }

		public OAP_STF_twist_constraint(OAP_STF_twist_constraint ext, GLTFRoot root) : base(ext, root) { }

		public IExtension Clone(GLTFRoot gltfRoot)
		{
			return new OAP_STF_twist_constraint(this, gltfRoot);
		}

		public JProperty Serialize()
		{
			var jo = new JObject();
			JProperty jProperty = new JProperty(OAP_STF_twist_constraintFactory.EXTENSION_NAME, jo);

			jo.Add(new JProperty(nameof(source), source));
			jo.Add(new JProperty(nameof(weight), weight));

			return jProperty;
		}

		public NodeId source;
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
				var extension = new OAP_STF_twist_constraint();

				JToken source = extensionToken.Value[nameof(OAP_STF_twist_constraint.source)];
				JToken weight = extensionToken.Value[nameof(OAP_STF_twist_constraint.weight)];

				extension.source = NodeId.Deserialize(root, source.CreateReader());
				extension.weight = weight.Value<float>();

				return extension;
			}

			return null;
		}
	}

	public class Construct_Register_OAP_STF_twist_constraint : IExtensionConstructor {
		public Construct_Register_OAP_STF_twist_constraint() {}

		public async Task ConstructComponent(GameObject nodeObj, IExtension extension, Func<NodeId, Task<GameObject>> getNode) {
			OAP_STF_twist_constraint _extension = (OAP_STF_twist_constraint)extension;

			var component = nodeObj.AddComponent<UnityEngine.Animations.RotationConstraint>();
			component.weight = _extension.weight;
			component.rotationAxis = UnityEngine.Animations.Axis.Y;

			var source = new UnityEngine.Animations.ConstraintSource();
			source.weight = 1;
			source.sourceTransform = (await getNode(_extension.source)).transform;

			component.AddSource(source);
			component.locked = true;
			component.constraintActive = true;
		}
	}
	
	[InitializeOnLoad]
	public class Register_OAP_STF_twist_constraint {
		static Register_OAP_STF_twist_constraint()
		{
			var definition = new ExtensionDefinition(ExtensionType.NODE, new OAP_STF_twist_constraintFactory(), new Construct_Register_OAP_STF_twist_constraint());
			ExtensionRegistry.registerExtension(definition);
		}
	}
}