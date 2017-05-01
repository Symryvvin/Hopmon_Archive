using System;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Levels {
    public class LevelBuilder : MonoBehaviour {
        private static Transform parent;
        private static PrefabLoader prefabLoader;

        void Start() {
            prefabLoader = GetComponent<PrefabLoader>();
            parent = transform;
        }

        public static void BuildLevel(Level level, bool partOnly) {
            DestroyLevel();
            var world = (World) Enum.Parse(typeof(World), level.world);
            if (partOnly) {
                InstantiateTiles(level.tiles.parts, world);
                CenteredLevel(level);
            }
            else {
                InstantiateTiles(level.tiles.parts, world);
                InstantiateTiles(level.tiles.structures, world);
                InstantiateTiles(level.tiles.enemies, world);
                ChangeMusic(world);
            }
        }

        public static void DestroyLevel() {
            if (parent.childCount == 0) return;
            foreach (Transform t in parent) {
                Destroy(t.gameObject);
            }
        }


        private static void InstantiateTiles(Tile[] tiles, World world) {
            foreach (var tile in tiles) {
                InstantiateTile(tile, world);
            }
        }

        private static void InstantiateTile(Tile tile, World world) {
            var position = tile.position;
            var rotation = tile.rotation;
            var prefab = prefabLoader.GetPrefabFromTile(tile, world);
            if (prefab != null) {
                Instantiate(prefab,
                        new Vector3(position.x, prefab.transform.position.y, position.z),
                        Quaternion.Euler(0, rotation.y, 0))
                    .transform.SetParent(parent);
            }
            else {
                Debug.LogError("Error. Prefab with name " + tile.name + " is null");
            }
        }

        private static void CenteredLevel(Level level) {
            parent.position = new Vector3(parent.position.x - (float) level.size.width / 2, 0,
                parent.position.z - (float) level.size.length / 2);
        }

        private static void ChangeMusic(World world) {
            switch (world) {
            case World.TEMPLE:
                AudioManager.instance.TempleMusic();
                break;
            case World.JUNGLE:
                AudioManager.instance.JungleMusic();
                break;
            case World.SPACE:
                AudioManager.instance.SpaceMusic();
                break;
            }
        }
    }
}