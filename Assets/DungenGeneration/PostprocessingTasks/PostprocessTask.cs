using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;

[CreateAssetMenu(menuName = "Dungeon generator/postprocess", fileName = "PostprocessTask")]
public class PostprocessTask : DungeonGeneratorPostProcessBase
{
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        MovePlayerToSpawn(level);
        SetTilemapSortingLayer("Tilemap");
        SetCompositeColliderTag("Boundary");
    }

    private void MovePlayerToSpawn(GeneratedLevel level)
    {
        foreach (var roomInstance in level.GetRoomInstances())
        {
            var room = roomInstance.Room;
            var roomTemplateInstance = roomInstance.RoomTemplateInstance;

            Transform spawnPosition = roomTemplateInstance.transform.Find("Spawn");
            if (spawnPosition != null)
            {
                Transform player = GameObject.FindObjectOfType<Player>().transform.parent.transform;
                player.transform.position = spawnPosition.position;
                Debug.Log(spawnPosition.position);

                break;
            }

        }
    }

    private void SetTilemapSortingLayer(string name)
    {
        foreach (TilemapRenderer tmr in FindObjectsOfType<TilemapRenderer>())
        {
            if (tmr.transform.name != "Background")
            {
                tmr.sortingLayerName = name;
            }
        }
    }

    private void SetCompositeColliderTag(string tag)
    {
        foreach (CompositeCollider2D coll in FindObjectsOfType<CompositeCollider2D>())
        {
            GameObject col = coll.gameObject;
            col.tag = tag;
        }
    }
}
