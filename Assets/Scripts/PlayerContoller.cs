using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour
{
    public float reloadTime = 1;
    public Rigidbody projectile;
    private float reloadingTimeLeft = 0;
    public float projectileSpeed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed && reloadingTimeLeft <= 0)
        {
            Debug.Log("here?");
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation);

            // Give the cloned object an initial velocity along the current
            // object's Z axis
            clone.linearVelocity = transform.TransformDirection(Vector3.right * projectileSpeed);
            reloadingTimeLeft = reloadTime;
        }
        reloadingTimeLeft -= Time.deltaTime;
        if (reloadingTimeLeft < 0) reloadingTimeLeft = 0;
    }
}
