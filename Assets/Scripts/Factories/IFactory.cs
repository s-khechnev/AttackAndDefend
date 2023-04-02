using UnityEngine;

namespace Factories
{
    public interface IFactory<T>
    {
        T Create(T prefab, Vector3 position);
        void Destroy(T gameObjectToDestroy);
    }
}