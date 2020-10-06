using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.Doors;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateInitializers;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.EnterTheGungeon.Scripts
{
    public class GungeonRoomTemplateInitializer : RoomTemplateInitializerBase
    {
        public override void Initialize()
        {
            base.Initialize();

            // Custom behaviour
            gameObject.AddComponent<GungeonRoomManager>();
        }

        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            var tilemapLayersHandler = ScriptableObject.CreateInstance<GungeonTilemapLayersHandler>();
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);

            // Custom behaviour
            tilemapsRoot.transform.Find("Floor").gameObject.AddComponent<GungeonCurrentRoomHandler>();
        }

        protected override void InitializeDoors()
        {
            base.InitializeDoors();

            var doors = gameObject.GetComponent<Doors>();
            doors.DistanceFromCorners = 2;
            doors.DoorLength = 2;
        }

        
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Dungeon generator/Examples/Enter The Gungeon/Room template")]
        public static void CreateRoomTemplatePrefab()
        {
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<GungeonRoomTemplateInitializer>();
        }
#endif
    }
}