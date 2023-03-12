using Defender.HUD;
using Models;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    public class TowerBuilder : MonoBehaviour
    {
        [SerializeField] private DefenderHUD _defenderHUD;

        private TilePlacement[] _tiles;
        private Camera _mainCamera;

        private TowerGhost _towerGhost;
        private TilePlacement _assumedTilePlacement;
        private bool _isTilePlacementEmpty;

        private LayerMask _groundLayerMask;
        private const string GroundLayer = "Ground";

        [Inject] private Wallet _wallet;
        [Inject] private TowerFactory _towerFactory;

        private void Awake()
        {
            _tiles = FindObjectsOfType<TilePlacement>();
            _mainCamera = Camera.main;
            
            _groundLayerMask = 1 << LayerMask.NameToLayer(GroundLayer);
            
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _defenderHUD.BuildTowerTapped += OnBuildTowerStart;
        }

        private void OnBuildTowerStart(Tower tower)
        {
            if (_towerGhost == null && _wallet.IsEnoughMoney(tower.TowerData.Cost))
            {
                ShowTileStates();

                _towerGhost = _towerFactory.GetTowerGhost(tower);
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
            if (_towerGhost != null)
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
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 50f, _groundLayerMask))
            {
                var tilePlacement = hit.collider.gameObject.GetComponent(typeof(TilePlacement)) as TilePlacement;
                
                if (tilePlacement != null && tilePlacement.CurrentState == PlacementTileState.Empty)
                {
                    _assumedTilePlacement = tilePlacement;
                    _towerGhost.SetState(PlacementTowerState.Available);
                    _towerGhost.transform.position = _assumedTilePlacement.CenterPosition;

                    _isTilePlacementEmpty = true;
                }
                else
                {
                    _towerGhost.transform.position = hit.point;
                
                    _towerGhost.SetState(PlacementTowerState.Unavailable);
                    _isTilePlacementEmpty = false;
                }
            }
        }

        private void PlaceTower()
        {
            _wallet.Purchase(_towerGhost.Tower.TowerData.Cost);
            
            _assumedTilePlacement.SetState(PlacementTileState.Filled);
            HideTileStates();
            
            _towerFactory.Reclaim(_towerGhost);
        }
    
        private void CancelBuilding()
        {
            HideTileStates();
            _towerFactory.Reclaim(_towerGhost.Tower);
        }
    }
}