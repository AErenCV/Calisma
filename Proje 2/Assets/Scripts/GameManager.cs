using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float[] positions = { -4, 0, 4 };
    private string[] simitTags = {"KirmiziSimit", "MaviSimit"};
    private int simitSayisi = 6;
    private GameObject startMenu;

    public GameObject simitPrefab;
    public Material[] simitMaterials;

    public bool gameOver = true;

    // Start of the game we find the starting menu
    void Start()
    {
        startMenu = GameObject.Find("Start Tema");
    }

    //With this function we deactivate start menu and add the simit game objects
    public void StartGame()
    {
        startMenu.SetActive(false);
        gameOver = false;
        StartGameInitialize();
    }

    //This function adds simits to game
    private void StartGameInitialize()
    {
        GameObject simitClone;

        for (int i = 0; i < simitSayisi; i++)
        {
            simitClone = Instantiate(simitPrefab, new Vector3(0, 4, positions[i % 3]), simitPrefab.transform.rotation);
            simitClone.tag = simitTags[i % 2];
            simitClone.GetComponent<Renderer>().material = simitMaterials[i % 2];
        }
    }

    //When game ends this function removes the simits from the game and opens the starting menu
    public void EndGame()
    {
        GameObject[] maviSimitler, kirmiziSimitler;

        maviSimitler = GameObject.FindGameObjectsWithTag("MaviSimit");
        kirmiziSimitler = GameObject.FindGameObjectsWithTag("KirmiziSimit");

        foreach (GameObject simit in maviSimitler)
        {
            Destroy(simit);
        }

        foreach (GameObject simit in kirmiziSimitler)
        {
            Destroy(simit);
        }

        gameOver = true;
        startMenu.SetActive(true);
    }
}
