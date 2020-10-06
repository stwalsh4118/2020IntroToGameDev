using System.Collections;
using System.Diagnostics;
using ProceduralLevelGenerator.Unity.Examples.Common;
using ProceduralLevelGenerator.Unity.Examples.Example1.Scripts;
using ProceduralLevelGenerator.Unity.Generators.PlatformerGenerator;
using ProceduralLevelGenerator.Unity.Pro;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.Platformer3.Scripts
{
    /// <summary>
    /// Example of a simple game manager that uses the DungeonGeneratorRunner to generate levels.
    /// </summary>
    public class Platformer3GameManager : GameManagerBase<Example1GameManager>
    {
        // To make sure that we do not start the generator multiple times
        private bool isGenerating;

        public void Update()
        {
            if (Input.GetKey(KeyCode.G) && !isGenerating)
            {
                LoadNextLevel();
            }
        }

        public override void LoadNextLevel()
        {
            isGenerating = true;

            // Show loading screen
            ShowLoadingScreen("Platformer 3", "loading..");

            // Find the generator runner
            var generator = GameObject.Find("Platformer Generator").GetComponent<PlatformerGenerator>();

            // Start the generator coroutine
            StartCoroutine(GeneratorCoroutine(generator));
        }

        /// <summary>
        /// Coroutine that generates the level.
        /// It is sometimes useful to yield return before we hide the loading screen to make sure that
        /// all the scripts that were possibly created during the process are properly initialized.
        /// </summary>
        private IEnumerator GeneratorCoroutine(PlatformerGenerator generator)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var generatorCoroutine = this.StartSmartCoroutine(generator.GenerateCoroutine());

            yield return generatorCoroutine.Coroutine;

            yield return null;

            stopwatch.Stop();

            isGenerating = false;

            generatorCoroutine.ThrowIfNotSuccessful();

            SetLevelInfo($"Generated in {stopwatch.ElapsedMilliseconds/1000d:F}s");
            HideLoadingScreen();
        }
    }
}