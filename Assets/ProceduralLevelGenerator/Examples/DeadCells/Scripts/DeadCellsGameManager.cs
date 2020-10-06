﻿using System;
using System.Collections;
using System.Diagnostics;
using ProceduralLevelGenerator.Unity.Examples.Common;
using ProceduralLevelGenerator.Unity.Examples.DeadCells.Scripts.Levels;
using ProceduralLevelGenerator.Unity.Generators.PlatformerGenerator;
using ProceduralLevelGenerator.Unity.Pro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ProceduralLevelGenerator.Unity.Examples.DeadCells.Scripts
{
    public class DeadCellsGameManager : GameManagerBase<DeadCellsGameManager>
    {
        public DeadCellsLevelType LevelType;
        private long generatorElapsedMilliseconds;
        
        // To make sure that we do not start the generator multiple times
        private bool isGenerating;

        public static readonly string LevelMapLayer = "LevelMap";
        public static readonly string StaticEnvironmentLayer = "StaticEnvironment";

        protected override void SingletonAwake()
        {
            if (LayerMask.NameToLayer(StaticEnvironmentLayer) == -1)
            {
                throw new Exception($"\"{StaticEnvironmentLayer}\" layer is needed for this example to work. Please create this layer.");
            }
        }

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.G) && !isGenerating)
            {
                LoadNextLevel();
            }

            if (Input.GetKeyUp(KeyCode.U))
            {
                Canvas.SetActive(!Canvas.activeSelf);
            }
        }

        public override void LoadNextLevel()
        {
            isGenerating = true;

            // Show loading screen
            ShowLoadingScreen($"Dead Cells - {LevelType}", "loading..");

            // Find the generator runner
            var generator = GameObject.Find($"Platformer Generator").GetComponent<PlatformerGenerator>();

            // Start the generator coroutine
            StartCoroutine(GeneratorCoroutine(generator));
        }


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

            generatorElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            RefreshLevelInfo();
            HideLoadingScreen();
        }

        private void RefreshLevelInfo()
        {
            SetLevelInfo($"Generated in {generatorElapsedMilliseconds / 1000d:F}s\nLevel type: {LevelType}");
        }

        public bool LevelMapSupported()
        {
            var layer = LayerMask.NameToLayer(LevelMapLayer);

            if (layer == -1)
            {
                Debug.Log($"Level map is currently not supported. Please add a layer called \"{LevelMapLayer}\" to enable this feature and then restart the game.");
            }

            return layer != -1;
        }
    }
}