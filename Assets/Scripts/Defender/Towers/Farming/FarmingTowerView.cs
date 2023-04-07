using  Defender.Towers.Base;
using UnityEngine;

namespace Defender.Towers.Farming
{
    public class FarmingTowerView : MonoBehaviour, ITowerView
    {
        private TowerGhost _towerGhost;

        private void Awake()
        {
            _towerGhost = new TowerGhost(GetComponentsInChildren<Renderer>());
        }

        public void SetPlacementState(PlacementTowerState state)
        {
            _towerGhost.SetState(state);
        }

        public void HideState()
        {
            _towerGhost.Hide();
        }

        public void ShowState()
        {
            _towerGhost.Show();
        }
    }
}