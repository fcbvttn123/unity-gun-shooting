using UnityEngine;
using TMPro;

public class BulletScript : MonoBehaviour
{

    [Tooltip("Furthest distance bullet will look for target")]
    public float maxDistance = 1000000;
    RaycastHit hit;
    [Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
    public GameObject decalHitWall;
    [Tooltip("Decal will need to be slightly in front of the wall so it doesn't cause rendering problems, for best feel put between 0.01-0.1.")]
    public float floatInfrontOfWall;
    [Tooltip("Blood prefab particle this bullet will create upon hitting enemy")]
    public GameObject bloodEffect;
    [Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
    public LayerMask ignoreLayer;
    [Tooltip("Maximum distance the Dummie will respawn around its original position.")]
    public float respawnRange = 5.0f;

    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, ~ignoreLayer))
        {
            if (decalHitWall)
            {
                if (hit.transform.tag == "LevelPart")
                {
                    Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
                }
                if (hit.transform.tag == "Dummie")
                {
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    UpdateScoreText();
                    RespawnDummie(hit.transform);
                }
            }
            Destroy(gameObject);
        }
        Destroy(gameObject, 0.1f);
    }

    void RespawnDummie(Transform dummieTransform)
    {
        Vector3 originalPosition = dummieTransform.position;
        Vector3 randomOffset = new Vector3(
            Random.Range(-respawnRange, respawnRange),
            0,  
            Random.Range(-respawnRange, respawnRange)
        );
        dummieTransform.position = originalPosition + randomOffset;
    }

    void UpdateScoreText()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+1);
    }
}
