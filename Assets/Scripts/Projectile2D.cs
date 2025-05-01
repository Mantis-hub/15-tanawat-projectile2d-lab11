using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] private Transform ShootPoint;
    [SerializeField] GameObject Target;
    [SerializeField] private Rigidbody2D BulletPrefabs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);
            
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                Target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log("hit " + hit.collider.name);
                
                Vector2 projectileVelocity = CalculateProjectileVelocity(ShootPoint.position, hit.point, 1f);
                
                Rigidbody2D shootBullet = Instantiate(BulletPrefabs, ShootPoint.position, Quaternion.identity);

                shootBullet.linearVelocity = projectileVelocity;
            }
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityx = distance.x / time;
        float velocityy = distance.y / time +0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        
        Vector2 projectileVelocity = new Vector2(velocityx, velocityy);
        return projectileVelocity;
    }
}
