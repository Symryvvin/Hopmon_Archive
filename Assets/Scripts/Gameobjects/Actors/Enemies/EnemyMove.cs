using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gameobjects.Actors.Players;
using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Gameobjects.Levels;
using Assets.Scripts.Rules.Movement;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class EnemyMove : MonoBehaviour {
        public MoveState moveState;
        public MoveDirection direction;
        public Node current;
        public Node end;
        private Game game;
        private Rigidbody enemyRigidbody;
        [SerializeField] private float moveSpeed;

        protected virtual void Start() {
            moveState = MoveState.STAND;
            direction = MoveDirection.STOP;
            enemyRigidbody = GetComponent<Rigidbody>();
            game = GameObject.Find("Game").GetComponent<Game>();
            StartCoroutine(WaitForStartGame());
        }

        private IEnumerator WaitForStartGame() {
            while (true) {
                if (game.status == GameStatus.STARTED) {
                    Level level = game.level;
                    foreach (var node in level.nodes) {
                        if (node.position + new Vector3(0, transform.position.y, 0) == transform.position) {
                            current = node;
                            current.ChangeType(NodeType.BLOCKED_FOR_ENEMY);
                        }
                    }
                    break;
                }
                yield return null;
            }
        }

        protected virtual void Update() {
            switch (moveState) {
            case MoveState.STAND:
                Move();
                break;
            case MoveState.WALK:
                CurrentDirection();
                break;
            }
        }

        protected virtual Node CalculateMoveDirection(IDictionary<Vector3, Node> directions) {
            // Сначала удаляем из словаря узлы, которые заблокированы другими врагами или заблокированы для врагов
            var copy = new Dictionary<Vector3, Node>(directions);
            foreach (var entry in copy) {
                if (entry.Value.isBlockedForEnemy())
                    directions.Remove(entry.Key);
            }
            if (directions.Count != 1) {
                switch (direction) {
                case MoveDirection.FORWARD:
                    directions.Remove(NodeDirection.BACK);
                    break;
                case MoveDirection.BACK:
                    directions.Remove(NodeDirection.FORWARD);
                    break;
                case MoveDirection.LEFT:
                    directions.Remove(NodeDirection.RIGHT);
                    break;
                case MoveDirection.RIGHT:
                    directions.Remove(NodeDirection.LEFT);
                    break;
                }
            }
            return directions.Count != 0 ? directions.ElementAt(Random.Range(0, directions.Count)).Value : null;
        }

        protected void CurrentDirection() {
            if (transform.position.x < end.position.x)
                direction = MoveDirection.RIGHT;
            if (transform.position.x > end.position.x)
                direction = MoveDirection.LEFT;
            if (transform.position.z > end.position.z)
                direction = MoveDirection.BACK;
            if (transform.position.z < end.position.z)
                direction = MoveDirection.FORWARD;
        }

        protected virtual void Move() {
            var directions = new Dictionary<Vector3, Node>(current.directions);
            end = CalculateMoveDirection(directions);
            if (end != null) {
                current.RestoreType();
                end.ChangeType(NodeType.BLOCKED_FOR_ENEMY);
                current = end;
                StartCoroutine(MoveTo(end.position + new Vector3(0, transform.position.y, 0)));
            }
        }

        protected IEnumerator MoveTo(Vector3 position) {
            if (gameObject.name.Contains("JellyFish"))
                print(position);
            float distance = (transform.position - position).sqrMagnitude;
            while (distance > float.Epsilon) {
                transform.position = Vector3.MoveTowards(enemyRigidbody.position, position, moveSpeed * Time.deltaTime);
                moveState = MoveState.WALK;
                distance = (transform.position - position).sqrMagnitude;
                yield return null;
            }
            moveState = MoveState.STAND;
        }
    }
}