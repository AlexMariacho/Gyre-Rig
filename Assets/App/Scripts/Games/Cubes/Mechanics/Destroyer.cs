using System.Threading.Tasks;
using App.Scripts.Interfaces;
using UnityEngine;

namespace App.Scripts.Games.Cubes.Mechanics
{
    public class Destroyer : IController
    {
        public event CubeHandler NeedDestroy;
        private Distancer _distancer;

        public Destroyer(Distancer distancer)
        {
            _distancer = distancer;
        }

        public void Destroy(Cube cube)
        {
            if (cube != null)
                Object.Destroy(cube.gameObject);
        }
        
        public void Activate()
        {
            _distancer.CubeFinish += OnNeedDestroy;
        }
        
        public void Deactivate()
        {
            _distancer.CubeFinish -= OnNeedDestroy;
        }
        
        private void OnNeedDestroy(Cube sender)
        {
            NeedDestroy?.Invoke(sender);
        }
    }
}