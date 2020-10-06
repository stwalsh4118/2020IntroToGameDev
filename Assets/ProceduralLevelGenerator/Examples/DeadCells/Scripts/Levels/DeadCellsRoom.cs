using System.Collections.Generic;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.DeadCells.Scripts.Levels
{
    public class DeadCellsRoom : RoomBase
    {
        public DeadCellsRoomType Type;

        public bool Outside;

        public override List<GameObject> GetRoomTemplates()
        {
            // We do not need any room templates here because they are resolved based on the type of the room.
            return null;
        }

        public override string GetDisplayName()
        {
            // Use the type of the room as its display name.
            return Type.ToString();
        }
    }
}
