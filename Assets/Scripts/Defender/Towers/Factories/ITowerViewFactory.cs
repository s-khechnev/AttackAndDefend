using System;
using Factories;

namespace Defender.Towers.Factories
{
    public interface ITowerViewFactory : IFactory<TowerView>
    {
        public event Action<Tower> TowerTapped;
    }
}