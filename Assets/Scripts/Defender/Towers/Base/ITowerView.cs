namespace Defender.Towers.Base
{
    public interface ITowerView
    {
        void HideState();
        void ShowState();
        void SetPlacementState(PlacementTowerState state);
    }
}