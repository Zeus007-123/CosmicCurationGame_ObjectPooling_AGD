using System.Collections.Generic;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyPrefab;
        private EnemyData enemyData;
        private List<PooledEnemy> pooledEnemies;

        public EnemyPool(EnemyView enemyPrefab, EnemyData enemyData)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyData = enemyData;
            this.pooledEnemies = new List<PooledEnemy>();
        }
        public EnemyController GetEnemy()
        {
            if (pooledEnemies.Count > 0) 
            {
                PooledEnemy pooledEnemy = pooledEnemies.Find(item => !item.isUsed);
                if (pooledEnemy != null) 
                { 
                    pooledEnemy.isUsed = true;
                    return pooledEnemy.Enemy;
                }
            }
            return CreateNewPooledEnemy();
        }
        private EnemyController CreateNewPooledEnemy()
        {
            PooledEnemy pooledEnemy = new PooledEnemy();
            pooledEnemy.Enemy = new EnemyController(enemyPrefab, enemyData);
            pooledEnemy.isUsed = true;
            pooledEnemies.Add(pooledEnemy);

            return pooledEnemy.Enemy;
        }
        public void ReturnEnemy(EnemyController returnedEnemy)
        {
            PooledEnemy pooledEnemy = pooledEnemies.Find(item => item.Enemy.Equals(returnedEnemy));
            pooledEnemy.isUsed = false;
        }
        public class PooledEnemy
        {
            public EnemyController Enemy;
            public bool isUsed;

        }
    }
}