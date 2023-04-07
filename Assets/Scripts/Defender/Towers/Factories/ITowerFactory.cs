using System;
using Defender.Towers.Base;
using Factories;

namespace Defender.Towers.Factories
{
    public interface ITowerFactory : IFactory<BaseTower>
    {
        public event Action<BaseTower> TowerTapped;
    }
}