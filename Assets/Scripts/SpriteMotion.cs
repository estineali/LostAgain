using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMotion : MonoBehaviour
{

    Rigidbody2D rb2d;
    Vector2 colliderSize; 
    Vector2 motionDirection;
    float motionForceMagnitude = 2f;
    const float rotateDegreesPerSecond = 15;
    float scaler;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scaler = transform.localScale.x;

        // initial motion Vector
        motionDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        colliderSize = GetComponent<BoxCollider2D>().size * scaler;

        Debug.Log(colliderSize);

        rb2d.AddForce(motionDirection * motionForceMagnitude);
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the ship about the Z axis

        float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;

        transform.Rotate(Vector3.forward, rotationAmount);

        //Update motion-direction vector
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;

        motionDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    /// <summary>
    /// This function wraps the spaceship 
    /// around the edges of the screen when it 
    /// goes out of the camera view
    /// </summary>
    ///

    private void FixedUpdate()
    {
        rb2d.AddForce(motionDirection * motionForceMagnitude);
        motionForceMagnitude = Random.Range(-2, 5);
    }

    private void OnBecameInvisible()
    {
        // Wrapping positions 
        #region
        Vector3 position = transform.position;

        // Wrapping in the horizontal axis
        if (transform.position.x - (colliderSize.x / 2)  >= ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenLeft + colliderSize.x / 2;
        }
        else if (transform.position.x + (colliderSize.x / 2) <= ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenRight - colliderSize.x / 2;
        }
        //Wrapping in the vertical axis
        if (transform.position.y - (colliderSize.y / 2) >= ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenBottom + colliderSize.y / 2 ;
        }
        else if (transform.position.y + (colliderSize.y / 2) <= ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenTop - colliderSize.y / 2;
        }

        transform.position = position;
        #endregion
    }
}
