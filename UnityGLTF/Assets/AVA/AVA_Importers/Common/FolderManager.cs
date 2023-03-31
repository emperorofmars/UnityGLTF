
using UnityEditor;

namespace oap.ava.importer.common
{
	public class FolderManager
	{
		public const string AVA_IMPORT_FOLDER = "Assets/AVA_import";

		public static void ensureAVAFolderExists()
		{
			if(!AssetDatabase.IsValidFolder("Assets/AVA_import")) AssetDatabase.CreateFolder("Assets", "AVA_import");
		}
	}
}