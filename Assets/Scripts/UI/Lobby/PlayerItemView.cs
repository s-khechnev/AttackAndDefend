using Models;
using TMPro;
using UnityEngine;

namespace UI.Lobby
{
    public class PlayerItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;

        public void Render(Player player)
        {
            _playerName.text = player.Name;
        }
    }
}