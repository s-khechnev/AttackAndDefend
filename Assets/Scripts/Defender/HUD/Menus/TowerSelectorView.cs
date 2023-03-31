using System;
using System.Collections.Generic;
using Defender.Towers;
using Defender.Towers.TargetSelectors;
using TMPro;
using UnityEngine;

namespace Defender.HUD.Menus
{
    public class TowerSelectorView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private ITargetSelector _targetSelector;

        private Dictionary<Type, string> selectorNames = new()
        {
            { typeof(NearestTargetSelector), "ближайший" },
            { typeof(MaxHealthTargetSelector), "макс. здоровье" },
            { typeof(MinHealthTargetSelector), "мин. здоровье" }
        };

        public void Init(ITargetSelector targetSelector)
        {
            _targetSelector = targetSelector;
            _text.text = selectorNames[targetSelector.GetType()];
        }
    }
}