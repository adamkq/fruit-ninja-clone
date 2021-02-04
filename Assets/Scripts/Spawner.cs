using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject objToSpawn = null;

    [Header("GOs and Transforms")]
    public GameObject projectileContainer;
    public GameObject bomb;
    public GameObject[] fruitsToSpawn;
    public Transform[] spawnLocations;

    [Header("Timing")]
    [SerializeField]
    private float minTimeBetweenSpawn = 0.3f;

    [SerializeField]
    private float maxTimeBetweenSpawn = 1f;

    [Header("Other")]
    [SerializeField]
    private float minLaunchForce = 12f;

    [SerializeField]
    private float maxLaunchForce = 20f;

    [SerializeField]
    [Tooltip("Force applied at +/- 90 deg to the launch force.")]
    private float maxTransverseForce = 1f;

    [SerializeField]
    [Tooltip("Out of 100.")]
    private float bombChance = 5;

    void Start()
    {
        UpdateObjToSpawn();
        StartCoroutine(SpawnFruit());
    }

    private void Update()
    {
        UpdateObjToSpawn();
    }

    private void UpdateObjToSpawn()
    {
        if (fruitsToSpawn.Length == 0 && bomb == null) return;

        if (fruitsToSpawn.Length == 0)
        {
            objToSpawn = bomb;
            return;
        }

        float prob = Random.Range(0f, 1f);

        if (prob < bombChance/100f){
            objToSpawn = bomb;
        }else{
            objToSpawn = fruitsToSpawn[Random.Range(0, fruitsToSpawn.Length)];
        }
    }

    private IEnumerator SpawnFruit()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn));

            // Preparation
            Transform tf = spawnLocations[Random.Range(0, spawnLocations.Length)];

            Vector2 launchForce = Random.Range(minLaunchForce, maxLaunchForce) * tf.transform.up + Random.Range(-maxTransverseForce, maxTransverseForce) * tf.transform.right;

            // Instantiation
            GameObject _obj = Instantiate(objToSpawn, tf);

            _obj.transform.SetParent(projectileContainer.transform);

            Rigidbody2D _rb2D = _obj.GetComponent<Rigidbody2D>();
            _rb2D.AddForce(launchForce, ForceMode2D.Impulse);
            _rb2D.AddTorque(Random.Range(-5, 5), ForceMode2D.Impulse);
        }
    }
}
