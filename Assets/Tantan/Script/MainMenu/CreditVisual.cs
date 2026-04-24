using UnityEngine;
using UnityEngine.UI;

public class CreditVisual : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] Image cd;
    [SerializeField] float rotationSpeed = 15;

    void Update()
    {
        cd.rectTransform.rotation = Quaternion.Euler(0, 0, cd.rectTransform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime);
    }
}
