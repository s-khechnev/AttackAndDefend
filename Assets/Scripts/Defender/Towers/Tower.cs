﻿using Data.Towers;
using UnityEngine;

namespace Defender.Towers
{
    public enum PlacementTowerState
    {
        Available,
        Unavailable
    }
    
    public class Tower : MonoBehaviour
    {
        public TowerData TowerData => _towerData;
        public PlacementTowerState CurrentState { get; private set; }

        [SerializeField] private TowerData _towerData;
        [SerializeField] private Renderer _renderer;

        private Color _availablePlaceColor = Color.green;
        private Color _unavailablePlaceColor = Color.red;
        private Color _normalColor = Color.white;

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
        }

        public void HideState()
        {
            _renderer.material.color = _normalColor;
        }

        public void SetState(PlacementTowerState newState)
        {
            CurrentState = newState;
            
            switch (newState)
            {
                case PlacementTowerState.Available:
                    _renderer.material.color = _availablePlaceColor;
                    break;
                case PlacementTowerState.Unavailable:
                    _renderer.material.color = _unavailablePlaceColor;
                    break;
            }
        }
    }
}