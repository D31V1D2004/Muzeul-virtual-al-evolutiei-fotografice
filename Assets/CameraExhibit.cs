using UnityEngine;

public class CameraExhibit : MonoBehaviour
{
    [Header("Informații Exponat")]
    public string cameraName = "Nume Cameră";
    public string creatorName = "Nume Creator / Autor";

    [TextArea(5, 12)]
    public string technicalDescription = "Descriere...";

    private bool isPlayerNearby = false;
    private bool isPanelOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            isPanelOpen = false;
            UIManager.Instance.AscundePanou();
        }
    }

    private void OnMouseDown()
    {
        if (isPlayerNearby)
        {
            if (!isPanelOpen)
            {
                // Aici sunt toți cei 3 parametri ceruți de UIManager: nume, creator, descriere
                UIManager.Instance.ArataPanou(cameraName, creatorName, technicalDescription);
                isPanelOpen = true;
            }
            else
            {
                UIManager.Instance.AscundePanou();
                isPanelOpen = false;
            }
        }
    }
}