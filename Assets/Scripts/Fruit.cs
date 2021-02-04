using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField]
    private int score = 1;

    public GameObject slicedFruit;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void TurnIntoSlicedFruit()
    {
        GameObject _slicedFruit = Instantiate(slicedFruit, transform.position, transform.rotation);

        Rigidbody[] _slicedFruit_rbs = _slicedFruit.transform.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in _slicedFruit_rbs)
        {
            rb.AddExplosionForce(Random.Range(750, 1000), transform.position, 1f);
            rb.angularVelocity = 5f * Random.insideUnitSphere; // arbitrary scaling
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5f);
        }

        // move to container
        _slicedFruit.transform.SetParent(GameObject.Find("ProjectileContainer").transform);

        FindObjectOfType<FNGameManager>().IncreaseScore(score);

        Destroy(_slicedFruit, 5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Blade blade = collision.GetComponent<Blade>();

        if (!blade) return;

        TurnIntoSlicedFruit();
    }
}
