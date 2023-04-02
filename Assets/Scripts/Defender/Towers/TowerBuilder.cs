using Defender.HUD;
using Defender.Towers.Factories;
using Models;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    public class TowerBuilder : MonoBehaviour
    {
        private TilePlacement[] _tiles;
        private Camera _mainCamera;

        private TowerView _buildingTowerView;
        private TilePlacement _assumedTowerPlacement;
        private bool _isTilePlacementEmpty;

        private LayerMask _groundLayerMask;
        private const string GroundLayer = "Ground";
        private const float MaxRaycastDistance = 50f;

        private const int MouseLeftButton = 0;

        private bool _isRelocating;
        private TilePlacement _tileBeforeMoving;

        private global::Factories.IFactory<TowerView> _towerViewFactory;
        private Wallet _wallet;

        [Inject]
        private void Construct(ITowerViewFactory towerViewFactory, Wallet wallet)
        {
            _towerViewFactory = towerViewFactory;
            _wallet = wallet;
        }

        private void Awake()
        {
            _tiles = FindObjectsOfType<TilePlacement>();
            _mainCamera = Camera.main;

            _groundLayerMask = 1 << LayerMask.NameToLayer(GroundLayer);
        }

        public void StartBuildTower(TowerView towerView)
        {
            if (_buildingTowerView != null) return;
            
            DefenderGUIManager.SetState(DefenderGameState.Building);
            
            ShowTileStates();
            
            _buildingTowerView = _towerViewFactory.Create(towerView, Vector3.zero);
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
            if (_buildingTowerView == null) return;

            MoveTower();
            if (Input.GetMouseButton(MouseLeftButton))
            {
                if (_isTilePlacementEmpty)
                    PlaceTower();
                else
                    CancelBuilding();
            }
        }

        private void MoveTower()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, MaxRaycastDistance, _groundLayerMask)) return;

            if (hit.collider.gameObject.TryGetComponent(out TilePlacement tilePlacement) &&
                tilePlacement.CurrentState == PlacementTileState.Empty)
            {
                _assumedTowerPlacement = tilePlacement;
                _isTilePlacementEmpty = true;

                _buildingTowerView.SetPlacementState(PlacementTowerState.Available);
                _buildingTowerView.transform.position = _assumedTowerPlacement.CenterPosition;
            }
            else
            {
                _isTilePlacementEmpty = false;

                _buildingTowerView.transform.position = hit.point;
                _buildingTowerView.SetPlacementState(PlacementTowerState.Unavailable);
            }
        }

        private void PlaceTower()
        {
            if (!_isRelocating)
            {
                _wallet.Purchase(_buildingTowerView.Tower.TowerData.Cost);
                _buildingTowerView.HideState();
            }
            else
            {
                _wallet.Purchase(_buildingTowerView.Tower.TowerData.CostToRelocate);
            }

            _assumedTowerPlacement.SetState(PlacementTileState.Filled);
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
                _towerViewFactory.Destroy(_buildingTowerView);
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
            if (!Physics.Raycast(towerView.transform.position, Vector3.down, out var hit, MaxRaycastDistance,
                    _groundLayerMask)) return;
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