using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace oap.ava
{
	public interface IAVAImporter
	{
		void run(GameObject go);
	}

	public class RegisteredImporters
	{
		private static List<IAVAImporter> importerList = new List<IAVAImporter>();

		public static void addImporter(IAVAImporter importer)
		{
			importerList.Add(importer);
		}

		public static void run(GameObject go)
		{
			Debug.Log("Running AVA Importers");
			foreach(IAVAImporter importer in importerList) // parallelize this
			{
				importer.run(go);
			}
		}
	}
}
