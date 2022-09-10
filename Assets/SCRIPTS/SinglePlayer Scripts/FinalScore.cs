using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField] SinglePlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "$" + playerData.finalScore.ToString();
    }
}
