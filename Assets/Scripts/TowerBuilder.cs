using System;
using Data.Towers;
using Defender.HUD;
using Defender.Towers;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
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
        GameManager.Instance.StartBuildTower += OnBuildTower;
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
        _assumedTilePlacement.SetState(PlacementTileState.Filled);
        HideStateTiles();
        
        _towerGhost.HidePlacementState();

        TowerBuilt?.Invoke(_towerGhost);
        _towerGhost = null;
    }
    
    private void CancelBuilding()
    {
        HideStateTiles();
        Destroy(_towerGhost.gameObject);

        CancelBuild?.Invoke(_towerGhost);
    }
}