using System;

namespace Defender.Towers.Base
{
    public interface ILevelChanger
    {
        public event Action<int, int> LevelChanged;
        public int CurrentLevel { get; }
        public int MaxLevel { get; }
    }
}