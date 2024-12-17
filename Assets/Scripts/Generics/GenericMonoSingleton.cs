#region Summary
/// <summary>
/// GenericMonoSingleton<T> is a generic singleton class that ensures only one instance of a given class (derived from GenericMonoSingleton) exists throughout the lifecycle of the game.
/// It provides a static property to access the instance and ensures that the instance persists across scenes.
/// - The Awake method handles instance creation and ensures the singleton pattern is respected by destroying any duplicate instances.
/// - If the instance already exists, it destroys the new instance and logs a warning.
/// </summary>
#endregion
using UnityEngine;

namespace HouseDefence.Generic
{
    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
                DontDestroyOnLoad(gameObject);
                Debug.Log($"{typeof(T)} instance created.");
            }
            else
            {
                Destroy(gameObject);
                Debug.LogWarning($"{typeof(T)} already exists. Destroying duplicate on {gameObject.name}");
            }
        }
    }
}
