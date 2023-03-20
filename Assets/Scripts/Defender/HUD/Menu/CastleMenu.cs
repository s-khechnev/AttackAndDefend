using System;
using Defender.HUD.Bar;
using UnityEngine;

namespace Defender.HUD.Menu
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