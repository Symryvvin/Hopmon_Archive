using System.Collections;
using Assets.Scripts.Gameobjects.Actors.Movements;
using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Gameobjects.Levels;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Structures {
    public class Gate : MonoBehaviour, IDestructable {
        private Game game;
        private Node current;

        void Start() {
            game = GameObject.Find("Game").GetComponent<Game>();
            StartCoroutine(WaitForStartGame());
        }

        private IEnumerator WaitForStartGame() {
            while (true) {
                if (game.status == GameStatus.STARTED) {
                    Level level = game.level;
                    foreach (var node in level.movement.nodes) {
                        if (node.position + Vector3.up * 0.1f == transform.position) {
                            current = node;
                            current.ChangeTypeToRestore(NodeType.NORMAL);
                        }
                    }
                    break;
                }
                yield return null;
            }
        }

        public void Hit(int damage) {
            Dead();
        }

        public void Dead() {
            current.RestoreType();
            Destroy(gameObject);
        }
    }
}