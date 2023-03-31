using System;
using System.Collections.Generic;
using UnityEngine;

namespace oap.ava.importer.common
{
	public interface IComponentConverter
	{
		void convert(Component component, GameObject target, GameObject root, GameObject rootAVA, Dictionary<GameObject, GameObject> nodeDict);
	}

	public class CommonConverters
	{
		static Dictionary<Type, IComponentConverter> converters = new Dictionary<Type, IComponentConverter>();

		public static void addConverter(Type type, IComponentConverter converter)
		{
			converters.Add(type, converter);
		}

		public static IComponentConverter getConverter(Type type)
		{
			return converters[type];
		}

		public static Type[] getSupportedTypes()
		{
			Type[] arr = new Type[converters.Keys.Count];
			converters.Keys.CopyTo(arr, 0);
			return arr;
		}
	}
}