using UnityEngine;

namespace Defender.HUD
{
    public enum DefenderGameState
    {
        Normal,
        Pause,
        Building
    }

    [RequireComponent(typeof(Canvas))]
    public class DefenderGUIManager : MonoBehaviour
    {
        private Canvas _canvas;

        public static DefenderGameState GameState { get; private set; }

        private void Awake()
        {
            GameState = DefenderGameState.Normal;
            _canvas = GetComponent<Canvas>();
        }

        public static void SetState(DefenderGameState newState)
        {
            GameState = newState;
        }

        public void InitCamera(Camera uiCamera)
        {
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = uiCamera;
        }
    }
}