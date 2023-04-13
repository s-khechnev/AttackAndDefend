using UnityEngine;

namespace Factories
{
    /// <summary>
    /// Base interface for factories
    /// </summary>
    /// <typeparam name="T">The type of objects that the factory will create and destroy</typeparam>
    public interface IFactory<T>
    {
        T Create(T prefab, Vector3 position);
        void Destroy(T gameObjectToDestroy);
    }
}