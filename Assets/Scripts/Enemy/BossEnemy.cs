using UnityEngine;

public class BossEnemy : Enemy
{
    
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedNorBullet = 20f;
    [SerializeField] private float speedRoundBullet = 8f;
    [SerializeField] private float hpValue = 100f;
    [SerializeField] private GameObject miniEnemy;
    [SerializeField] private float skillCooldown = 2f;
    private float nextSkillTime = 0f;
    [SerializeField] private GameObject usbPrefabs;
    protected override void Update()
    {
        base.Update();
        if(Time.time >= nextSkillTime)
        {
            SudungSkill();
        }
    }
    protected override void Die()
    {
        
            Instantiate(usbPrefabs, transform.position, Quaternion.identity);
       
        base.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

            player.TakeDamage(enterDamage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

            player.TakeDamage(stayDamage);
        }
    }
    private void NormalBullets()
    {
        if (player != null)
        { 
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedNorBullet);
        }
    }
    private void RoundBullets()
    {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(bulletDirection * speedRoundBullet);

        }
    }
    private void BossHeal(float hpAmount)
    {
        currentHp=Mathf.Min(currentHp + hpAmount, maxHp);
        UpdateHpBar();
    }
    private void MiniEnemy()
    {
        Instantiate(miniEnemy, transform.position, Quaternion.identity);
    }
    private void DichChuyen()
    {
        if(player !=null)
        {
            transform.position = player.transform.position;
        }
    }
    private void RandomSkill()
    {
        int randomSkill = Random.Range(0, 5);
        switch(randomSkill)
        {
            case 0:
                NormalBullets();
                break;
            case 1:
                RoundBullets();
                break;
            case 2:
                BossHeal(hpValue);
                break;
            case 3:
                MiniEnemy();
                break;
            case 4:
                DichChuyen();
                break;
        }
    }
    private void  SudungSkill()
    {
        nextSkillTime = Time.time + skillCooldown;
        RandomSkill();
    }

}
