using UnityEngine;

public class TestBase : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OUCH");

        EnemyLife life = other.GetComponent<EnemyLife>();
        if (life != null)
        {
            life.EnemyDied();
        }
    }
}
