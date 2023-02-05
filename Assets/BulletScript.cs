using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    public float Life;
    public float StartingVelocity;

    private void Start()
    {
        var a = transform.rotation.eulerAngles.z -90f;
        var r = a * Mathf.Deg2Rad;
        var vec2 = new Vector2(Mathf.Cos(r), Mathf.Sin(r));
        GetComponent<Rigidbody2D>().velocity = vec2.normalized * StartingVelocity;
    }

    private void Update()
    {
        Life -= Time.deltaTime;
        if (Life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
