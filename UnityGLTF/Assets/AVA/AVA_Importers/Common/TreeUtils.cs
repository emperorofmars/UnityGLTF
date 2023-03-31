
#if UNITY_EDITOR

using oap.stf.Components;
using UnityEngine;

namespace oap.ava.importer.common
{
	public class TreeUtils
	{
		public static GameObject findByUUID(GameObject go, string uuid)
		{
			OAP_STF_uuid c = go.GetComponent<OAP_STF_uuid>();
			if(c != null && c.uuid == uuid)
			{
				return go;
			}
			else
			{
				for(int i = 0; i < go.transform.childCount; i++)
				{
					GameObject ret = findByUUID(go.transform.GetChild(i).gameObject, uuid);
					if(ret != null) return ret;
				}
			}
			return null;
		}
	}
}

#endif
