using UnityEngine;

public class Bulletbehavior : MonoBehaviour
{

    private Transform Target;

    public GameObject ImpactEffect;

    public float BulletSpeed = 30f;
    public void Seek(Transform _Target)
    {
        Target = _Target;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 Dir = Target.position - transform.position;
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
}
