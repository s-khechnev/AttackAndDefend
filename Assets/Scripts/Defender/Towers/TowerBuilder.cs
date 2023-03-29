using System;
using Defender.HUD;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    public class TowerBuilder : MonoBehaviour
    {
        public event Action<TowerView> TowerBuilt;
        public event Action<TowerView> TowerRelocated;

        private TilePlacement[] _tiles;
        private Camera _mainCamera;

        private TowerView _buildingTowerView;
        private TilePlacement _assumedTilePlacement;
        private bool _isTilePlacementEmpty;

        private LayerMask _groundLayerMask;
        private const string GroundLayer = "Ground";

        [Inject] private TowerFactory _towerFactory;

        private bool _isRelocating = false;
        private TilePlacement _tileBeforeMoving;

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
            if (!_isRelocating)
            {
                TowerBuilt?.Invoke(_buildingTowerView);
                _buildingTowerView.HideState();
            }
            else
            {
                TowerRelocated?.Invoke(_buildingTowerView);
            }

            _assumedTilePlacement.SetState(PlacementTileState.Filled);
            HideTileStates();

            _buildingTowerView.Tower.enabled = true;
            _buildingTowerView = null;

            _isRelocating = false;

            DefenderGUIManager.SetState(DefenderGameState.Normal);
        }

        private void CancelBuilding()
        {
            HideTileStates();
            DefenderGUIManager.SetState(DefenderGameState.Normal);

            if (!_isRelocating)
            {
                _towerFactory.Reclaim(_buildingTowerView.Tower);
            }
            else
            {
                _isRelocating = false;

                _buildingTowerView.SetPlacementState(PlacementTowerState.Available);
                _buildingTowerView.transform.position = _tileBeforeMoving.CenterPosition;
                _buildingTowerView.Tower.enabled = true;
                _buildingTowerView = null;

                _tileBeforeMoving.SetState(PlacementTileState.Filled);
            }
        }

        public void RelocateTower(TowerView towerView)
        {
            if (_buildingTowerView != null) return;
            if (!Physics.Raycast(towerView.transform.position, Vector3.down, out var hit, 5f, _groundLayerMask)) return;
            if (!hit.collider.gameObject.TryGetComponent(out TilePlacement tilePlacement)) return;

            DefenderGUIManager.SetState(DefenderGameState.Building);
            _isRelocating = true;

            _tileBeforeMoving = tilePlacement;
            tilePlacement.SetState(PlacementTileState.Empty);
            ShowTileStates();

            towerView.Tower.enabled = false;
            _buildingTowerView = towerView;
        }
    }
}