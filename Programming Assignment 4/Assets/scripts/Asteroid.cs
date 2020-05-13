using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{

    [SerializeField]
    Sprite[] asteroids = new Sprite[3];
    Vector3 position;
    const float MinImpulseForce = 1f;
    const float MaxImpulseForce = 2f;
    float angle = 0;
    GameObject miniAsteroid;
    Vector3 miniAsteroidSize;
    float miniAsteroidRadius;
    float radius;

    public void Initialize(Direction direction, Vector3 position)
    {
        GetComponent<SpriteRenderer>().sprite = asteroids[Random.Range(0, asteroids.Length)];
        position = new Vector3(position.x, position.y, 0f);
        transform.position = position;
        switch (direction)
        {
            case Direction.Up:
                angle = Random.Range(75 * Mathf.Deg2Rad, 105 * Mathf.Deg2Rad);
                break;
            case Direction.Down:
                angle = Random.Range(255 * Mathf.Deg2Rad, 285 * Mathf.Deg2Rad);
                break;
            case Direction.Left:
                angle = Random.Range(165 * Mathf.Deg2Rad, 195 * Mathf.Deg2Rad);
                break;
            case Direction.Right:
                angle = Random.Range(-15 * Mathf.Deg2Rad, 15 * Mathf.Deg2Rad);
                break;
        }
        miniAsteroidSize = gameObject.transform.localScale;
        miniAsteroidRadius = GetComponent<CircleCollider2D>().radius;
        StartMoving(angle);
    }

    void StartMoving(float tempAngle)
    {
        float MinImpulseForce = 1f;
        float MaxImpulseForce = 2f;

        if (miniAsteroidSize.x >= 0.6)
        {
            MinImpulseForce = 1.5f;
            MaxImpulseForce = 2.5f;
        }
        else if (miniAsteroidSize.x < 0.6 && miniAsteroidSize.x >= 0.3f)
        {
            MinImpulseForce = 1f;
            MaxImpulseForce = 2.5f;
        }
        else if (miniAsteroidSize.x < 0.15f && miniAsteroidSize.x >= 0.15f)
        {
            MinImpulseForce = 1.5f;
            MaxImpulseForce = 3f;
        }
        Vector2 moveDirection = new Vector2(Mathf.Cos(tempAngle), Mathf.Sin(tempAngle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float angle = Random.Range(0f, 2f) * Mathf.PI;
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(collision.gameObject);
                }
        if (miniAsteroidSize.x >= 0.15f)
        {
            miniAsteroidSize = new Vector3(gameObject.transform.localScale.x * 0.5f, gameObject.transform.localScale.y * 0.5f, gameObject.transform.localScale.z);
            miniAsteroidRadius = GetComponent<CircleCollider2D>().radius * 0.5f;
            for (int i = 0; i < 2; i++)
            {
                miniAsteroid = Instantiate(gameObject, transform.position, transform.rotation);
                miniAsteroid.tag = "Asteroid";
                miniAsteroid.transform.localScale = miniAsteroidSize;
                miniAsteroid.GetComponent<CircleCollider2D>().radius = miniAsteroidRadius;
                miniAsteroid.GetComponent<Asteroid>().StartMoving(angle);
                angle = Random.Range(0f, 2f) * Mathf.PI;
            }
        }
        Destroy(gameObject);
        AudioManager.Play(AudioClipName.AsteroidHit);
    }
}
