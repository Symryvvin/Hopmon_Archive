﻿using System;
using Assets.Scripts.Gameobjects.Actors.Players;
using Assets.Scripts.Gameobjects.Levels;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.EventMessages;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Games {
    public class Game : MonoBehaviour {
        public GameStatus status;
        public Level level;
        public Player player;
        public HUD hud;
        private LevelStats stats;

        protected void Start() {
            status = GameStatus.INITIALIZE;
            hud.StartListeners();
            EventMessenger.StartListener(GameEvents.START_GAME, StartGame);
            EventMessenger.StartListener(GameEvents.DEFEATE, Defeat);
            EventMessenger.StartListener(GameEvents.VICTORY, Victory);
            EventMessenger.StartListener(GameEvents.UPDATE_CRISTAL_COUNT, UpdateCristalCount);
            status = GameStatus.READY;
        }

        private void StartGame() {
            level.Build();
            InitStatsAndHUD();
            InitPlayer();
            status = GameStatus.STARTED;
        }

        private void InitPlayer() {
            player.MoveToStart(level);
            player.ResetPlayer();
            foreach (var node in level.nodes) {
                if (node.position  + Vector3.up * 0.1f == player.transform.position) {
                    player.SetNodeForPlayer(node);
                }
            }
        }

        private void InitStatsAndHUD() {
            stats = new LevelStats(level);
            hud.InitHUD(stats);
            EventMessenger.TriggerEvent(GameEvents.UPDATE_HUD);
        }

        private void UpdateCristalCount() {
            stats.cristals--;
            EventMessenger.TriggerEvent(GameEvents.UPDATE_HUD);
            if (stats.cristals == 0) {
                EventMessenger.TriggerEvent(GameEvents.VICTORY);
            }
        }

        public void NextLevel() {
            level.nodes.Erase();
            level = LevelManager.NextLevel(level);
            StartGame();
        }

        public void PrevLevel() {
            level.nodes.Erase();
            level = LevelManager.PrevLevel(level);
            StartGame();
        }

        private void Victory() {
            Invoke("NextLevel", 3f);
        }

        private void Defeat() {
            Invoke("StartGame", 1f);
            level.nodes.Erase();
        }
    }
}