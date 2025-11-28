using UnityEngine;

public class TurretBehaviorGround : MonoBehaviour
{
   
    [SerializeField] private Transform Target;
    public float Range = 2f;

    public string EnemyTag = "GroundEnemy";
    

    public Transform PartToRotate;

    [SerializeField] private float TurnSpeed = 10f;

    public float Firerate = 1f;
    private float FireCoutDown = 0f;

    public GameObject BulletPrefab;
    public Transform FirePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }

        Vector3 Dir = Target.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(Dir);
        Vector3 Rotation = Quaternion.Lerp(PartToRotate.rotation, LookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(Rotation.x, Rotation.y, 0f);

        if (FireCoutDown <= 0f)
        {
            Shoot();
            FireCoutDown = 1 / Firerate;
        }

        FireCoutDown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject BulletGO = (GameObject)Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bulletbehavior Bullet = BulletGO.GetComponent<Bulletbehavior>();

        if (Bullet != null)
        {
            Bullet.Seek(Target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] groundEnemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        float ShortestDistance = Mathf.Infinity;
        GameObject NearestEnemy = null;

        foreach (GameObject enemy in groundEnemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (DistanceToEnemy < ShortestDistance)
            {
                ShortestDistance = DistanceToEnemy;
                NearestEnemy = enemy;
            }

        }


        if (NearestEnemy != null && ShortestDistance <= Range)
        {
            Target = NearestEnemy.transform;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
