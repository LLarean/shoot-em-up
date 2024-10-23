using UnityEngine;

namespace Shmup.Enemies
{
    public class EnemyBuilder
    {
        private Enemy _enemyPrefab;
        private float _speed;

        public EnemyBuilder SerBasePrefab(Enemy prefab)
        {
            _enemyPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        public EnemyBuilder SetWeapon()
        {
            return this;
        }

        public Enemy Build()
        {
            Enemy instance = Object.Instantiate(_enemyPrefab);
            // instance.transform.position = _splineContainer.EvaluatePosition(0f);
            //
            // SplineAnimate splineAnimate = instance.GetOrAddComponent<SplineAnimate>();
            // splineAnimate.Container = _splineContainer;
            // splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            // splineAnimate.ObjectUpAxis = SplineComponent.AlignAxis.ZAxis;
            // splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.YAxis;
            // splineAnimate.MaxSpeed = _speed;
            //
            // splineAnimate.Play();

            return instance;
        }

    }
}