using System;
using Data.Towers;
using Defender.HUD;
using Defender.Towers;
using Helpers;
using Unity.Mathematics;
using UnityEngine;

public class TowerBuilder : Singleton<TowerBuilder>
{
    public event Action<Tower> TowerBuilt;
    public event Action<Tower> CancelBuild;

    private TilePlacement[] _tiles;
    private Camera _mainCamera;

    private Tower _towerGhost;
    private TilePlacement _assumedTilePlacement;
    private bool _canBuild;

    private LayerMask _groundLayer;
    private LayerMask _groundLayerMask;

    private void Awake()
    {
        _tiles = FindObjectsOfType<TilePlacement>();

        _mainCamera = Camera.main;

        _groundLayer = LayerMask.NameToLayer("Ground");
        _groundLayerMask = (1 << _groundLayer);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        HUDManager.Instance.BuildTowerTapped += OnBuildTower;
    }

    private void OnBuildTower(TowerData towerData)
    {
        ShowTileStates();

        _towerGhost = Instantiate(towerData.Tower, _mainCamera.ScreenPointToRay(Input.mousePosition).direction,
            Quaternion.identity);
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
                if (_canBuild)
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
        Debug.DrawRay(_mainCamera.transform.position,
            _mainCamera.ScreenPointToRay(Input.mousePosition).direction * 100f, Color.red);

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

                if (_assumedTilePlacement.CurrentState != PlacementTileState.Filled)
                    _canBuild = true;
                else
                    _canBuild = false;
            }
            else
            {
                _towerGhost.transform.position = hit.point;
                
                _towerGhost.SetState(PlacementTowerState.Unavailable);
                _canBuild = false;
            }
        }
    }

    private void PlaceTower()
    {
        Tower tower = Instantiate(_towerGhost, _towerGhost.transform.position, quaternion.identity);
        tower.HideState();

        _assumedTilePlacement.SetState(PlacementTileState.Filled);
        HideTileStates();

        Destroy(_towerGhost.gameObject);

        TowerBuilt?.Invoke(tower);
    }
    
    private void CancelBuilding()
    {
        HideTileStates();
        Destroy(_towerGhost.gameObject);

        CancelBuild?.Invoke(_towerGhost);
    }
}