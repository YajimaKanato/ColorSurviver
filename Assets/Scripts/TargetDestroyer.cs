using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TargetDestroyer : MonoBehaviour
{
    BoxCollider2D _bc2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.tag != "Destroy")
        {
            gameObject.tag = "Destroy";
        }
        _bc2d = GetComponent<BoxCollider2D>();
        _bc2d.isTrigger = true;
    }
}
