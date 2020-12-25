using UnityEngine;

namespace App.Scripts.Games.Cubes
{
    public class Cube : MonoBehaviour
    {
        public float WalkedDistance => _walkedDistance;
        
        private float _walkedDistance;

        public void AddDistance(float walked)
        {
            _walkedDistance += walked;
        }
    }
}