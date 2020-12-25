namespace App.Scripts.Games.Cubes.Mechanics
{
    public class Distancer
    {
        public event CubeHandler CubeFinish;
        
        private float _destroyDistance;

        public Distancer(float destroyDistance)
        {
            _destroyDistance = destroyDistance;
        }

        public void CheckDistance(Cube cube)
        {
            if (cube.WalkedDistance >= _destroyDistance)
            {
                CubeFinish?.Invoke(cube);
            }
        }
    }
}