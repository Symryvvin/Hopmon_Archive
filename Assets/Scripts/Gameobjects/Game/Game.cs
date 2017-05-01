using Assets.Scripts.Gameobjects.Actors.Player;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Game {
    public class Game : MonoBehaviour {
        public GameStatus status;
        public Level.Level level;
        public Player player;
        public HUD hud;
        private LevelStats stats;

        void Start() {
            status = GameStatus.INITIALIZE;
            hud.StartListeners();
            EventManager.StartListener(GameEvents.START_GAME, StartGame);
            EventManager.StartListener(GameEvents.WARP_CRISTAL, WarpCristal);
            EventManager.StartListener(GameEvents.DEFEATE, Defeat);
            EventManager.StartListener(GameEvents.VICTORY, Victory);
        }

        private void StartGame() {
            status = GameStatus.STARTING;
            level.Build();
            InitStatsAndHUD();
            InitPlayer();
            status = GameStatus.STARTED;
        }

        private void InitPlayer() {
            player.MoveToStart(level);
            player.ResetPlayer();
            foreach (var node in level.movement.nodes) {
                if (node.position  + Vector3.up * 0.1f == player.transform.position) {
                    player.SetNodeForPlayer(node);
                }
            }
        }

        private void InitStatsAndHUD() {
            stats = new LevelStats(level);
            hud.InitHUD(stats);
            EventManager.TriggerEvent(GameEvents.UPDATE_HUD);
        }

        private void WarpCristal() {
            stats.cristals--;
            EventManager.TriggerEvent(GameEvents.UPDATE_HUD);
            if (stats.cristals == 0) {
                EventManager.TriggerEvent(GameEvents.VICTORY);
            }
        }

        public void NextLevel() {
            level.movement.Erase();
            level = LevelManager.NextLevel(level);
            StartGame();
        }

        public void PrevLevel() {
            level.movement.Erase();
            level = LevelManager.PrevLevel(level);
            StartGame();
        }

        private void Victory() {
            Invoke("NextLevel", 3f);
        }

        private void Defeat() {
            Invoke("StartGame", 1f);
            level.movement.Erase();
        }
    }
}