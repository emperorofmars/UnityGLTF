using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace oap.ava
{
    public class AVA_Importer_VRC : IAVAImporter
    {

        public void run()
        {
            Debug.Log("Running AVA VRChat importer");

            // Construct Prefab/Scene with a functioning VRChat avatar
        }
    }
    
	[InitializeOnLoad]
    public class Register_AVA_Importer_VRC
    {
        static Register_AVA_Importer_VRC()
        {
            RegisteredImporters.addImporter(new AVA_Importer_VRC());
        }
    }
}
