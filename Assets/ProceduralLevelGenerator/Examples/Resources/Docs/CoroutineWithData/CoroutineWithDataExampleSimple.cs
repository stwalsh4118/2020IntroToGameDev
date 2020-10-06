using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.Resources.Docs.CoroutineWithData
{
    public class CoroutineWithDataExampleSimple : MonoBehaviour
    {
        public void Start()
        {
            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
            StartCoroutine(generator.GenerateCoroutine());
        }
    }
}
