using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateInitializers;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateOutline;
using ProceduralLevelGenerator.Unity.Generators.PlatformerGenerator;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ProceduralLevelGenerator.Unity.Examples.DeadCells.Scripts
{
    public class DeadCellsRoomTemplateInitializer : RoomTemplateInitializerBase
    {
        public override void Initialize()
        {
            base.Initialize();

            var outlineHandler = gameObject.AddComponent<BoundingBoxOutlineHandler>();
            outlineHandler.PaddingTop = 3;
        }

        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            var tilemapLayersHandlers = new PlatformerTilemapLayersHandler();
            tilemapLayersHandlers.InitializeTilemaps(tilemapsRoot);
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Dungeon generator/Examples/Dead Cells/Room template outside")]
        public static void CreateRoomTemplatePrefab()
        {
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<DeadCellsRoomTemplateInitializer>();
        }
#endif
    }
}