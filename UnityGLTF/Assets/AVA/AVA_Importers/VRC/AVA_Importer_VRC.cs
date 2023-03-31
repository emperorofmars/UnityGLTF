using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using oap.ava.importer.common;

namespace oap.ava.importer.vrc
{
#if VRCSDK3_FOUND

	public class AVA_Importer_VRC : ScriptableObject, IAVAImporter
	{
		const string IMPORT_FOLDER_VRC = "VRC";

		static Dictionary<Type, IComponentConverter> converters = new Dictionary<Type, IComponentConverter>();

		public static void addConverter(Type type, IComponentConverter converter)
		{
			converters.Add(type, converter);
		}

		public void run(GameObject go)
		{
			Debug.Log("Running AVA VRChat importer");

			FolderManager.ensureAVAFolderExists();
			if(!AssetDatabase.IsValidFolder(FolderManager.AVA_IMPORT_FOLDER + "/" + IMPORT_FOLDER_VRC)) AssetDatabase.CreateFolder(FolderManager.AVA_IMPORT_FOLDER, IMPORT_FOLDER_VRC);
			AssetDatabase.DeleteAsset(FolderManager.AVA_IMPORT_FOLDER + "/" + IMPORT_FOLDER_VRC + "/" + go.name + ".prefab");

			GameObject root = ConvertTree.convertTree(go, converters);
			PrefabUtility.SaveAsPrefabAsset(root, FolderManager.AVA_IMPORT_FOLDER + "/" + IMPORT_FOLDER_VRC + "/" + go.name + ".prefab");
			DestroyImmediate(root);
		}
	}
	
	[InitializeOnLoad]
	public class Register_AVA_Importer_VRC
	{
		static Register_AVA_Importer_VRC()
		{
			RegisteredImporters.addImporter(ScriptableObject.CreateInstance<AVA_Importer_VRC>());
		}
	}

#endif

	[InitializeOnLoad]
	public class Detect_VRC
	{
		const string VRCSDK3_FOUND = "VRCSDK3_FOUND";
		static Detect_VRC()
		{
			if(Directory.GetFiles(Path.GetDirectoryName(Application.dataPath), "VRCAvatarDescriptorEditor3.cs", SearchOption.AllDirectories).Length > 0)
			{
				Debug.Log("Found VRC SDK 3");
				ScriptDefinesManager.AddDefinesIfMissing(BuildTargetGroup.Standalone, VRCSDK3_FOUND);
			}
			else
			{
				Debug.Log("Didn't find VRC SDK 3");
				ScriptDefinesManager.RemoveDefines(VRCSDK3_FOUND);
			}
		}
	}
}
