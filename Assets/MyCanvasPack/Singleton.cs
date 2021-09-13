using UnityEngine;

namespace MyCanvasPack
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance != null) return _instance;
				// Search for existing instance.
				_instance = (T) FindObjectOfType(typeof(T));

				// Create new instance if one doesn't already exist.
				if (_instance != null) return _instance;
				// Need to create a new GameObject to attach the singleton to.
				var singletonObject = new GameObject();
				_instance = singletonObject.AddComponent<T>();
				singletonObject.name = typeof(T) + " (Singleton)";

				// Make instance persistent.
				// DontDestroyOnLoad(singletonObject);

				return _instance;
			}
		}
	}
}