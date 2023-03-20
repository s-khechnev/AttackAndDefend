using System;
using Defender.HUD.Bars;
using UnityEngine;

namespace Defender.HUD.Menus
{
    [Serializable]
    public class CastleMenu : GUIMenuBase
    {
        [SerializeField] private CastleHealthBar _castleHealthBar;

        public override void Init()
        {
        }
    }
}