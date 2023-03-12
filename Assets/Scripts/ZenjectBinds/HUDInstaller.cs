using Defender.HUD;
using UnityEngine;
using Zenject;

namespace ZenjectBinds
{
    public class HUDInstaller: MonoInstaller
    {
        [SerializeField] private DefenderHUD _defenderHUD;
        
        public override void InstallBindings()
        {
            InstallDefender();
        }

        private void InstallDefender()
        {
            Container.Bind<DefenderHUD>().FromInstance(_defenderHUD).AsSingle();
            Container.QueueForInject(_defenderHUD);
        }
    }
}