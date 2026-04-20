using UnityEngine;

public class DhadZone : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 45f;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
