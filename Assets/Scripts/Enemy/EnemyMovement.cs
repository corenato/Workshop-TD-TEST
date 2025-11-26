using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public WayPoints Path;
    public float Speed = 10f;
    private Transform Target;
    private int WayPointIndex = 0;

    private void Start()
    {
        Target = Path.Points[0];
    }

    private void Update()
    {
        Vector3 Dir = Target.position - transform.position;
        transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, Target.position) <= 0.3f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if(WayPointIndex >= Path.Points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        
        WayPointIndex++;
        Target = Path.Points[WayPointIndex];
    }
}
