using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rgb;
    const float magnitude = 10;
    const float deathTimer = 2f;
    Timer timer;

    private void Awake()
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
    public void ApplyForce(Vector2 direction)
    {
        rgb.AddForce(magnitude * direction, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (timer.Finished)
            Destroy(this.gameObject);
    }
}
