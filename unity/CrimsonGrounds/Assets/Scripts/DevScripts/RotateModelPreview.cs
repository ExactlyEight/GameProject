using UnityEngine;

public class RotateModelPreview : MonoBehaviour
{
    public bool rotate = true;
    public float speed = 1f;

    void Update()
    {
        if(rotate)
            transform.Rotate(Vector3.up * (speed * Time.deltaTime));
    }
}
