using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float force = 100f;
    public float fireRate = 10f;

    public ParticleSystem shootingFlash;
    public GameObject hitImpact;
    public Camera camPoint;

    public float nextTimeFire = 0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeFire)
        {
            nextTimeFire = Time.time + 1f / fireRate;
            Shooting();
        }
    }

    void Shooting()
    {
        shootingFlash.Play();

        RaycastHit hitInfo;

        if(Physics.Raycast(camPoint.transform.position, camPoint.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);

            Hittarget hitTarget = hitInfo.transform.GetComponent<Hittarget>();
            if(hitTarget != null)
            {
                hitTarget.HitDamage(damage);
            }

            if(hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * force);
            }

            GameObject impactPrefab  = Instantiate(hitImpact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impactPrefab, 2f);
        }
    }
}
