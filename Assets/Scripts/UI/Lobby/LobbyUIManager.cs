﻿using System;
using System.Collections.Generic;
using Data;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {
        public static LobbyUIManager Instance { get; private set; }

        public event Action StartGame;

        [SerializeField] private Button _startButton;

        [SerializeField] private PlayerItemView _playerView;
        [SerializeField] private PlayerItemView _botView;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            InitPlayersView();
        }

        private void InitPlayersView()
        {
            //_playerView.Render(GameManager.Instance.Player);
            //_botView.Render(GameManager.Instance.Bot);
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartClick);
        }

        private void OnStartClick()
        {
            SceneManager.LoadSceneAsync("GameScene");
            StartGame?.Invoke();
        }
    }
}