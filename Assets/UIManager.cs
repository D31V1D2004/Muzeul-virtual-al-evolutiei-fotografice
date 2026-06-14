using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject panouInfo;
    public TextMeshProUGUI textNume;
    public TextMeshProUGUI textCreator; 
    public TextMeshProUGUI textDescriere;

    private void Awake()
    {
        Instance = this;
        if (panouInfo != null) panouInfo.SetActive(false);
    }

    // Funcția primește acum și numele creatorului
    public void ArataPanou(string nume, string creator, string descriere)
    {
        panouInfo.SetActive(true);
        textNume.text = nume;
        textCreator.text = "Creator: " + creator; 
        textDescriere.text = descriere;
    }

    public void AscundePanou()
    {
        panouInfo.SetActive(false);
    }
}