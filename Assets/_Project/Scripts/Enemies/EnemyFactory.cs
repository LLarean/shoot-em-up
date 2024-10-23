namespace Shmup.Enemies
{
    public class EnemyFactory
    {
        public Enemy CreateEnemy(Enemy enemy)
        {
            EnemyBuilder enemyBuilder = new EnemyBuilder()
                .SerBasePrefab(enemy);

            return enemyBuilder.Build();
        }
    }
}