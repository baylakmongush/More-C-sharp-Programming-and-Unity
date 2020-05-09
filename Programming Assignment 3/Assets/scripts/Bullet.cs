using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rgb;
    const float magnitude = 20;
    const float deathTimer = 2f;
    Timer timer;

    float colliderRadius;
    void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = deathTimer;
        timer.Run();
    }

    /// <summary>
    /// Applies force to the bullet
    /// </summary>
    /// <param name="direction"></param>
    void ApplyForce(Vector2 direction)
    {
        rgb.AddForce(magnitude * direction, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (timer.Finished)
            Destroy(this.gameObject);
    }
}
