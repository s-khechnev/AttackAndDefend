using System;
using Defender.HUD;
using UnityEngine;

namespace Defender.Towers.Base
{
    [SelectionBase, RequireComponent(typeof(BoxCollider), typeof(ITowerView))]
    public abstract class BaseTower : MonoBehaviour
    {
        public event Action<BaseTower> TowerTapped;

        public abstract BaseTowerData BaseTowerData { get; }
        public abstract ITowerView TowerView { get; }

        private void OnMouseDown()
        {
            if (!isActiveAndEnabled || DefenderGUIManager.GameState == DefenderGameState.Building)
                return;

            TowerView.ShowState();
            TowerTapped?.Invoke(this);
        }
    }
}