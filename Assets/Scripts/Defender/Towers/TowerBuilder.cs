using System;
using Defender.HUD;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    public class TowerBuilder : MonoBehaviour
    {
        public event Action<TowerView> TowerPlaced;

        private TilePlacement[] _tiles;
        private Camera _mainCamera;

        private TowerView _buildingTowerView;
        private TilePlacement _assumedTilePlacement;
        private bool _isTilePlacementEmpty;

        private LayerMask _groundLayerMask;
        private const string GroundLayer = "Ground";

        [Inject] private TowerFactory _towerFactory;

        private void Awake()
        {
            _tiles = FindObjectsOfType<TilePlacement>();
            _mainCamera = Camera.main;

            _groundLayerMask = 1 << LayerMask.NameToLayer(GroundLayer);
        }

        public void StartBuildTower(Tower tower)
        {
            if (_buildingTowerView == null)
            {
                DefenderGUIManager.SetState(DefenderGameState.Building);

                ShowTileStates();

                _buildingTowerView = _towerFactory.GetTowerView(tower);
            }
        }

        private void ShowTileStates()
        {
            foreach (var tile in _tiles)
            {
                tile.ShowState();
            }
        }

        private void HideTileStates()
        {
            foreach (var tile in _tiles)
            {
                tile.HideState();
            }
        }

        private void Update()
        {
            if (_buildingTowerView != null)
            {
                Building();

                if (Input.GetMouseButton(0))
                {
                    if (_isTilePlacementEmpty)
                    {
                        PlaceTower();
                    }
                    else
                    {
                        CancelBuilding();
                    }
                }
            }
        }

        private void Building()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 50f, _groundLayerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out TilePlacement tilePlacement) &&
                    tilePlacement.CurrentState == PlacementTileState.Empty)
                {
                    _assumedTilePlacement = tilePlacement;
                    _buildingTowerView.SetPlacementState(PlacementTowerState.Available);
                    _buildingTowerView.transform.position = _assumedTilePlacement.CenterPosition;

                    _isTilePlacementEmpty = true;
                }
                else
                {
                    _buildingTowerView.transform.position = hit.point;

                    _buildingTowerView.SetPlacementState(PlacementTowerState.Unavailable);
                    _isTilePlacementEmpty = false;
                }
            }
        }

        private void PlaceTower()
        {
            TowerPlaced?.Invoke(_buildingTowerView);

            _assumedTilePlacement.SetState(PlacementTileState.Filled);
            HideTileStates();

            _buildingTowerView.Tower.enabled = true;
            _buildingTowerView.HideState();
            _buildingTowerView = null;

            DefenderGUIManager.SetState(DefenderGameState.Normal);
        }

        private void CancelBuilding()
        {
            HideTileStates();
            _towerFactory.Reclaim(_buildingTowerView.Tower);
            DefenderGUIManager.SetState(DefenderGameState.Normal);
        }
    }
}