//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private GameObject blade;

    [Header("Explosion")]
    [SerializeField]
    private GameObject bombExplosion;

    [Header("Maneuvering")]
    [SerializeField]
    [Tooltip("How aggresively does the bomb follow the blade?")]
    private float homingCoefficient;

    [SerializeField]
    [Tooltip("How aggresively does the bomb randomly maneuver?")]
    private float randomForceCoefficient;


    private void Start()
    {
        Destroy(gameObject, 10f);
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        blade = GameObject.Find("Blade");
    }

    private void Update()
    {
        ExecuteManeuvers();
    }

    private void ExecuteManeuvers()
    {
        HomeInOnBlade();
        ApplyRandomForce(1f);
    }

    private void HomeInOnBlade()
    {
        if (!blade) return;

        Vector2 directionToBlade, homingForce;

        directionToBlade = blade.transform.position - transform.position;
        directionToBlade.Normalize();

        homingForce = directionToBlade * homingCoefficient;
        homingForce.y = Mathf.Min(0, homingForce.y, 0.5f * rb2D.mass * rb2D.gravityScale); // guarantees that the bomb will fall off the screen

        rb2D.AddForce(homingForce);
    }

    // Applied whenever the bomb moves below threshold
    private void ApplyRandomForce(float thresholdVelY = float.PositiveInfinity)
    {
        if (rb2D.velocity.y > thresholdVelY) return;

        Vector2 randomForce = randomForceCoefficient * Random.insideUnitCircle;

        rb2D.AddForce(randomForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!blade) return;

        FindObjectOfType<FNGameManager>().OnBombHit();

        GameObject _bombExplosion = Instantiate(bombExplosion, transform);

        ParticleSystem ps = _bombExplosion.GetComponent<ParticleSystem>();

        if (ps)
        {
            Debug.Log("Bomb Blast");
            ps.Play();
        }

        Destroy(_bombExplosion, 0.5f);
        Destroy(gameObject);
    }
}
