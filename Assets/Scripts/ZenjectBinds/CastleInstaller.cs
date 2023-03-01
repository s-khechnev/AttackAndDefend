using Models;
using UnityEngine;
using Zenject;

namespace ZenjectBinds
{
    public class CastleInstaller : MonoInstaller
    {
        [SerializeField] private Castle _castle;
        
        public override void InstallBindings()
        {
            Container.Bind<Castle>().FromInstance(_castle).AsSingle();
        }
    }
}
