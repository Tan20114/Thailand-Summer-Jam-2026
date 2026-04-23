using UnityEngine;
using UnityEngine.UI;

public class MainMenuVisual : MonoBehaviour
{
    [SerializeField] Image cd;
    [SerializeField] float rotationSpeed = 30;

    // Update is called once per frame
    void Update()
    {
        cd.rectTransform.rotation = Quaternion.Euler(0, 0, cd.rectTransform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime);
    }
}
