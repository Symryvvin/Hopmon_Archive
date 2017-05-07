using Assets.Scripts.Gameobjects.Actors.Enemies;
using Assets.Scripts.Gameobjects.Actors.Players;
using Assets.Scripts.Gameobjects.Actors.Shells;
using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Gameobjects.Structures;
using Assets.Scripts.Managers.EventMessages;
using UnityEngine;

namespace Assets.Scripts.Rules {
    public class CollisionHandler {
        private static CollisionHandler handler;

        public static CollisionHandler instance {
            get { return handler ?? (handler = new CollisionHandler()); }
        }

        private CollisionHandler() {
        }

        public void StartRules() {
            EventMessenger<Enemy, Collider>.StartListener(GameEvents.ENEMY_TAKE_DAMAGE, EnemyTakeDamage);
            EventMessenger<Player, Collider>.StartListener(GameEvents.PLAYER_TAKE_DAMAGE, PlayerTakeDamage);
            EventMessenger<Shell, Collision>.StartListener(GameEvents.DESTROY_SHELL, DestroyShell);
            EventMessenger<Gate, Collision>.StartListener(GameEvents.GATE_TAKE_DAMAGE, DestroyGate);
            EventMessenger<Collector, Collider>.StartListener(GameEvents.COLLECT_CRISTAL, CollectCristal);
            EventMessenger<Collector, Collider>.StartListener(GameEvents.WARP_CRISTAL, WarpCristal);
            EventMessenger<Collector, Collider>.StartListener(GameEvents.STOP_WARP, StopWarp);
        }

        public void StopRules() {
            EventMessenger<Enemy, Collider>.StopListener(GameEvents.ENEMY_TAKE_DAMAGE, EnemyTakeDamage);
            EventMessenger<Player, Collider>.StopListener(GameEvents.PLAYER_TAKE_DAMAGE, PlayerTakeDamage);
            EventMessenger<Shell, Collision>.StopListener(GameEvents.DESTROY_SHELL, DestroyShell);
            EventMessenger<Gate, Collision>.StopListener(GameEvents.GATE_TAKE_DAMAGE, DestroyGate);
            EventMessenger<Collector, Collider>.StopListener(GameEvents.COLLECT_CRISTAL, CollectCristal);
            EventMessenger<Collector, Collider>.StopListener(GameEvents.WARP_CRISTAL, WarpCristal);
            EventMessenger<Collector, Collider>.StopListener(GameEvents.STOP_WARP, StopWarp);
        }

        private void EnemyTakeDamage(Enemy enemy, Collider collider) {
            PlayerShell shell = collider.GetComponent<PlayerShell>();
            if (shell) {
                enemy.Hit(1);
                shell.Explosion();
            }
        }

        private void PlayerTakeDamage(Player player, Collider collider) {
            if (collider.CompareTag("Enemy")) {
                player.Hit();
            }
        }

        private void DestroyShell(Shell shell, Collision collision) {
            int layer = collision.gameObject.layer;
            if (layer == 8 &&
                collision.gameObject.transform != shell.transform.parent) {
                shell.Destroy();
            }
        }

        private void DestroyGate(Gate gate, Collision collision) {
            PlayerShell shell = collision.gameObject.GetComponent<PlayerShell>();
            if (shell) {
                gate.Hit(1);
                shell.Explosion();
            }
        }

        private void CollectCristal(Collector collector, Collider collider) {
            if (collider.CompareTag("Collectible")) {
                collector.Pick(collider.transform);
            }
        }

        private void WarpCristal(Collector collector, Collider collider) {
            if (collider.CompareTag("WarpZone")) {
                Debug.Log("release");
                collector.Release();
            }
        }

        private void StopWarp(Collector collector, Collider collider) {
            if (collider.CompareTag("WarpZone")) {
                collector.StopAllCoroutines();
            }
        }
    }
}