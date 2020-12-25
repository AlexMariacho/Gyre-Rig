using System;
using System.Collections.Generic;
using App.Scripts.Games.Cubes.Enums;
using App.Scripts.Games.Cubes.Mechanics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Games.Cubes
{
    public class CubeGame : MonoBehaviour
    {
        [Header("Ссылки")]
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private Transform _spawnPostion;
        [SerializeField] private Transform _rootSpawn;

        [Header("Игровые параметры")] 
        [SerializeField] private CubeMoveDirection _direction;
        [SerializeField] private float _speed;
        [SerializeField] private float _timeRespawn;
        [SerializeField] private float _distanceToKill;
        
        private Distancer _distancer;
        private Movier _movier;
        private Spawner _spawner;
        private Destroyer _destroyer;
        
        private List<Cube> _cubes = new List<Cube>();
        private List<Cube> _destroyCubes = new List<Cube>();
        private float _currentTime;
        private bool _isPlaying;

        private Dictionary<CubeMoveDirection, Vector3> CurrentDirection = new Dictionary<CubeMoveDirection, Vector3>()
        {
            {CubeMoveDirection.Up, Vector3.forward},
            {CubeMoveDirection.Down, Vector3.down},
            {CubeMoveDirection.Left, Vector3.left},
            {CubeMoveDirection.Right, Vector3.right}
        };

        public void StartGame()
        {
            _currentTime = 0;
            _isPlaying = true;
            _spawner.Spawn();
        }

        public void StopGame()
        {
            _isPlaying = false;
            DestroyAllCubes();
            _cubes.Clear();
            _destroyCubes.Clear();
        }

        private void Awake()
        {
            _distancer = new Distancer(_distanceToKill);
            _movier = new Movier(_speed);
            _spawner = new Spawner(_spawnPostion, _rootSpawn, _cubePrefab);
            _destroyer = new Destroyer(_distancer);
        }

        private void OnEnable()
        {
            _destroyer.Activate();
            _spawner.SpawnCube += OnSpawnedCube;
            _destroyer.NeedDestroy += OnNeedDestroy;
            StartGame();
        }

        private void OnDisable()
        {
            _destroyer.Deactivate();
            _spawner.SpawnCube -= OnSpawnedCube;
            _destroyer.NeedDestroy -= OnNeedDestroy;
            StopGame();
        }

        private void OnSpawnedCube(Cube sender)
        {
            _cubes.Add(sender);
        }
        
        private void OnNeedDestroy(Cube sender)
        {
            _destroyCubes.Add(sender);
        }
        
        private void FixedUpdate()
        {
            if (_isPlaying)
            {
                CheckSpawn();
                MoveCubes();
                CheckDistance();
                DestroyCubes();
            }
        }

        private void DestroyAllCubes()
        {
            foreach (var cube in _cubes)
            {
                _destroyer.Destroy(cube);
            }
        }

        private void CheckSpawn()
        {
            if (CheckTimeToSpawn())
            {
                _currentTime = 0;
                _spawner.Spawn();
            }
            else
            {
                _currentTime += Time.deltaTime;
            }
        }

        private bool CheckTimeToSpawn()
        {
            return _currentTime > _timeRespawn;
        }

        private void MoveCubes()
        {
            foreach (var cube in _cubes)
            {
                if (cube != null)
                    _movier.Move(cube, CurrentDirection[_direction], Time.deltaTime);
            }
        }

        private void CheckDistance()
        {
            foreach (var cube in _cubes)
            {
                if (cube != null)
                    _distancer.CheckDistance(cube);
            }
        }

        private void DestroyCubes()
        {
            if (_destroyCubes.Count > 0)
            {
                foreach (var cube in _destroyCubes)
                {
                    if (cube != null)
                    {
                        _destroyer.Destroy(cube);
                    }
                }
                _destroyCubes.Clear();
            }
        }
    }
}