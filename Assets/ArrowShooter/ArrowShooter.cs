using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPosition;
    public int amountOfProjectiles;
    public float shootRate;
    public float spread;
    public float shotForce;
    public float projectileDamage;
    public float projectileLifeTime;

    public bool evenNumberOffset = true;

    private void Start()
    {
        StartCoroutine(ShotActivation());
    }

    IEnumerator ShotActivation()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootRate);
            Shoot();
        }
    }

    void Shoot()
    {
        float spreadModifier = amountOfProjectiles / 2;
        if (amountOfProjectiles % 2 == 0 && evenNumberOffset) spreadModifier -= 0.5f;

        Vector3 dir = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        for (int i = 0; i < amountOfProjectiles; i++)
        {
            Quaternion rotation = Quaternion.Euler(dir.x, dir.y - (spreadModifier * spread), dir.z);
            GameObject projectile = Instantiate(projectilePrefab, shootPosition.position, rotation);
            spreadModifier--;
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shotForce, ForceMode.Impulse);

            ArrowProjectile projectileScript = projectile.GetComponent<ArrowProjectile>();
            projectileScript.damage = projectileDamage;
            projectileScript.lifeTime = projectileLifeTime;
        }
    }
}
