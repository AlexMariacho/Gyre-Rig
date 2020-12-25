using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Games.Cubes.Mechanics
{
    public delegate void CubeHandler(Cube sender);
    
    public class Spawner
    {
        public event CubeHandler SpawnCube;
        
        private Transform _spawnPoint;
        private Transform _rootSpawn;
        private Cube _cubePrefab;

        public Spawner(Transform spawnPoint, Transform rootSpawn, Cube cubePrefab)
        {
            _spawnPoint = spawnPoint;
            _cubePrefab = cubePrefab;
            _rootSpawn = rootSpawn;
        }

        public void Spawn()
        {
            var cube = Object.Instantiate(_cubePrefab);
            cube.transform.position = _spawnPoint.position;
            cube.transform.SetParent(_rootSpawn);
            
            SpawnCube?.Invoke(cube);
        }
    }
}