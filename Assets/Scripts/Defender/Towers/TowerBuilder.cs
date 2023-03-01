﻿using Data.Towers;
using Defender.HUD;
using Models;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    public class TowerBuilder : MonoBehaviour
    {
        [SerializeField] private HUDManager _hudManager;

        private TilePlacement[] _tiles;
        private Camera _mainCamera;

        private Tower _towerGhost;
        private TilePlacement _assumedTilePlacement;
        private bool _isTilePlacementEmpty;

        private LayerMask _groundLayerMask;
        private const string GroundLayer = "Ground";

        [Inject] private Wallet _wallet; 

        private void Awake()
        {
            _tiles = FindObjectsOfType<TilePlacement>();

            _mainCamera = Camera.main;
            
            _groundLayerMask = 1 << LayerMask.NameToLayer(GroundLayer);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _hudManager.BuildTowerTapped += OnBuildTowerStart;
        }

        private void OnBuildTowerStart(TowerData towerData)
        {
            if (_towerGhost == null)
            {
                ShowTileStates();

                _towerGhost = Instantiate(towerData.Tower, _mainCamera.ScreenPointToRay(Input.mousePosition).direction,
                    Quaternion.identity);
            }
        }

        private void ShowTileStates()
        {
            foreach (var tile in _tiles)
            {
                tile.ShowState();
            }
        }

        private void HideStateTiles()
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
                    if (_isTilePlacementEmpty && _wallet.IsEnoughMoney(_towerGhost.TowerData.Cost))
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
                var hitObj = hit.collider.gameObject;

                if (hitObj.GetComponent(typeof(TilePlacement)) != null)
                {
                    _towerGhost.SetState(PlacementTowerState.Available);

                    _assumedTilePlacement = hitObj.GetComponent(typeof(TilePlacement)) as TilePlacement;
                    var groundBlockCenter = _assumedTilePlacement.transform.GetChild(0).transform.position;
                    _towerGhost.transform.position = groundBlockCenter;

                    _isTilePlacementEmpty = _assumedTilePlacement.CurrentState != PlacementTileState.Filled;
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
            _wallet.Purchase(_towerGhost.TowerData.Cost);
            
            _assumedTilePlacement.SetState(PlacementTileState.Filled);
            HideStateTiles();
        
            _towerGhost.HidePlacementState();
            _towerGhost = null;
        }
    
        private void CancelBuilding()
        {
            HideStateTiles();
            Destroy(_towerGhost.gameObject);
        }
    }
}