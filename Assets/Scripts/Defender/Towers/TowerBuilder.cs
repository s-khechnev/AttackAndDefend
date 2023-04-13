using Defender.HUD;
using Defender.Towers.Base;
using Defender.Towers.Factories;
using Models;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    /// <summary>
    /// Component responsible for the construction and relocation of towers
    /// </summary>
    public class TowerBuilder : MonoBehaviour
    {
        private TilePlacement[] _tiles;
        private Camera _mainCamera;

        private BaseTower _buildingTower;
        private TilePlacement _assumedTowerPlacement;
        private bool _isAssumedTileEmpty;

        private LayerMask _groundLayerMask;
        private const string GroundLayer = "Ground";
        private const float MaxRaycastDistance = 50f;

        private const int MouseLeftButton = 0;

        private bool _isRelocating;
        private TilePlacement _tileBeforeMoving;

        private ITowerFactory _towerFactory;
        private Wallet _wallet;

        [Inject]
        private void Construct(ITowerFactory towerFactory, Wallet wallet)
        {
            _towerFactory = towerFactory;
            _wallet = wallet;
        }

        private void Awake()
        {
            _tiles = FindObjectsOfType<TilePlacement>();
            _mainCamera = Camera.main;

            _groundLayerMask = 1 << LayerMask.NameToLayer(GroundLayer);
        }

        /// <summary>
        /// Start build the tower
        /// </summary>
        /// <param name="towerToBuild">tower to build</param>
        public void StartBuildTower(BaseTower towerToBuild)
        {
            if (_buildingTower != null) return;

            DefenderGameManager.SetState(DefenderGameState.Building);

            ShowTileStates();

            _buildingTower = _towerFactory.Create(towerToBuild, Vector3.zero);
            _buildingTower.enabled = false;
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
            if (_buildingTower == null) return;

            MoveTower();
            if (Input.GetMouseButton(MouseLeftButton))
            {
                if (_isAssumedTileEmpty)
                    PlaceTower();
                else
                    CancelBuilding();
            }
        }

        /// <summary>
        /// Move the tower behind the mouse cursor and set the placement state
        /// </summary>
        private void MoveTower()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, MaxRaycastDistance, _groundLayerMask)) return;

            if (hit.collider.gameObject.TryGetComponent(out TilePlacement tilePlacement) &&
                tilePlacement.CurrentState == PlacementTileState.Empty)
            {
                _assumedTowerPlacement = tilePlacement;
                _isAssumedTileEmpty = true;

                _buildingTower.transform.position = _assumedTowerPlacement.CenterPosition;
                _buildingTower.TowerView.SetPlacementState(PlacementTowerState.Available);
            }
            else
            {
                _isAssumedTileEmpty = false;

                _buildingTower.transform.position = hit.point;
                _buildingTower.TowerView.SetPlacementState(PlacementTowerState.Unavailable);
            }
        }

        /// <summary>
        /// Place tower to assumed tile
        /// </summary>
        private void PlaceTower()
        {
            if (_isRelocating)
            {
                if (_assumedTowerPlacement == _tileBeforeMoving)
                {
                    CancelBuilding();
                    return;
                }

                _wallet.Purchase(_buildingTower.BaseTowerData.CostToRelocate);
            }
            else
            {
                _wallet.Purchase(_buildingTower.BaseTowerData.Cost);
                _buildingTower.TowerView.HideState();
            }

            _assumedTowerPlacement.SetState(PlacementTileState.Filled);
            HideTileStates();

            _buildingTower.enabled = true;
            _buildingTower = null;

            _isRelocating = false;

            DefenderGameManager.SetState(DefenderGameState.Normal);
        }

        /// <summary>
        /// Cancel building of the tower
        /// </summary>
        private void CancelBuilding()
        {
            HideTileStates();
            DefenderGameManager.SetState(DefenderGameState.Normal);

            if (!_isRelocating)
            {
                _towerFactory.Destroy(_buildingTower);
            }
            else
            {
                _isRelocating = false;

                _buildingTower.TowerView.SetPlacementState(PlacementTowerState.Available);
                _buildingTower.transform.position = _tileBeforeMoving.CenterPosition;
                _buildingTower.enabled = true;
                _buildingTower = null;

                _tileBeforeMoving.SetState(PlacementTileState.Filled);
            }
        }

        /// <summary>
        /// Start relocate tower
        /// </summary>
        /// <param name="towerToRelocate">tower to relocate</param>
        public void RelocateTower(BaseTower towerToRelocate)
        {
            if (_buildingTower != null) return;
            if (!Physics.Raycast(towerToRelocate.transform.position, Vector3.down, out var hit,
                    MaxRaycastDistance, _groundLayerMask)) return;
            if (!hit.collider.gameObject.TryGetComponent(out TilePlacement tilePlacement)) return;

            DefenderGameManager.SetState(DefenderGameState.Building);
            _isRelocating = true;

            _tileBeforeMoving = tilePlacement;
            tilePlacement.SetState(PlacementTileState.Empty);
            ShowTileStates();

            towerToRelocate.enabled = false;
            _buildingTower = towerToRelocate;
        }
    }
}