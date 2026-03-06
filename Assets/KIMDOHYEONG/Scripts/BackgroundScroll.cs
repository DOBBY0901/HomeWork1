using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed = 1f;
    public float height = 10f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y <= -height)
        {
            transform.position += new Vector3(0, height * 2f, 0);
        }
    }
}