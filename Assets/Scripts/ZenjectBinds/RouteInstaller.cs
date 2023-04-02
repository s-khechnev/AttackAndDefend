using Attackers.Movement;
using UnityEngine;
using Zenject;

namespace ZenjectBinds
{
    public class RouteInstaller : MonoInstaller
    { 
        [SerializeField] private Route _mainRoute;  
        
        public override void InstallBindings()
        {
            Container.Bind<Route>().FromInstance(_mainRoute).AsSingle();
        }
    }
}