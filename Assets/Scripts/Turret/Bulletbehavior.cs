using UnityEngine;
using UnityEngine.Rendering;

public class Bulletbehavior : MonoBehaviour
{

    private Transform target;

    public GameObject ImpactEffect;
    public float BulletSpeed = 30f;

    [SerializeField] private string groundEnemyTag = "GroundEnemy";
    [SerializeField] private int bulletDamage;

    public void Seek(Transform _Target)
    {
        target = _Target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 Dir = target.position - transform.position;
        float DistancethisFrame = BulletSpeed * Time.deltaTime;

        if (Dir.magnitude <= DistancethisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(Dir.normalized * DistancethisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject EffectINS = (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(EffectINS, 2f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(groundEnemyTag))
        {
            collision.gameObject.GetComponent<EnemyManager>().TakeDamage(bulletDamage);
        }
    }
}
