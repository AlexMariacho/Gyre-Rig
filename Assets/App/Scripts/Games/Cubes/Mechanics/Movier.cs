using App.Scripts.Interfaces;
using UnityEngine;

namespace App.Scripts.Games.Cubes.Mechanics
{
    public class Movier
    {
        private float _speed;

        public Movier(float speed)
        {
            _speed = speed;
        }

        public void Move(Cube moveCube, Vector3 direction, float correctTime)
        {
            moveCube.transform.Translate(direction * _speed * correctTime);
            moveCube.AddDistance(_speed * correctTime);
        }

    }
}