using System.Collections.Generic;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullets> pooledBulletsList;

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject) 
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;
            this.pooledBulletsList = new List<PooledBullets>();
        }
        public BulletController GetBullet()
        {
            if (pooledBulletsList.Count > 0) 
            {
                PooledBullets pooledBullets = pooledBulletsList.Find(item => !item.isUsed);
                if (pooledBullets != null) 
                { 
                    pooledBullets.isUsed = true;
                    return pooledBullets.Bullet;
                }
            }
            return CreateNewPooledBullet();
        }
        public void ReturnBullet(BulletController returnedBullet)
        {
            PooledBullets pooledBullets = pooledBulletsList.Find(item => item.Bullet.Equals(returnedBullet));
            pooledBullets.isUsed = false;
        }
        private BulletController CreateNewPooledBullet()
        {
            PooledBullets pooledBullets = new PooledBullets();
            pooledBullets.Bullet = new BulletController(bulletView, bulletScriptableObject);
            pooledBullets.isUsed = true;
            pooledBulletsList.Add(pooledBullets);

            return pooledBullets.Bullet;
        }
        public class PooledBullets
        {
            public BulletController Bullet;
            public bool isUsed;
        }
    }
}