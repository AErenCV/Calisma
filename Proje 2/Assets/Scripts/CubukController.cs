using System.Collections.Generic;
using UnityEngine;

public class CubukController : MonoBehaviour
{

    private List<GameObject> takiliSimitler = new List<GameObject>();

    public GameObject[] simitCubuklar;

    public bool setTamam = false;

    // On start of the game we find all the cubuk game objects and place them an array for later use
    void Start()
    {
        simitCubuklar = GameObject.FindGameObjectsWithTag("Cubuk");
    }

    /* When trigger enters a cubuk first we control is the triggered game object a simit
     * then control the simit number on the cubuk. If there are 3 simit on cubuk, we send the triggered simit 
     * to its old location.
     * Later with the last simit if number equals to 3 we control simits if all simits same color.
     * if All the simits are same color we control is there another cubuk with same colored simit of 3
     * if there is than game ends
     * */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KirmiziSimit") || other.CompareTag("MaviSimit"))
        {
            takiliSimitler.Add(other.gameObject);
        }

        if (takiliSimitler.Count > 3)
        {
            other.GetComponent<SimitController>().GeriGonder();
        }

        if (takiliSimitler.Count == 3)
        {
            if (SetControl()) {
                BitisControl();
            }
        }
    }

    //when trigger exits simit leaves the cubuk so we remove that simit from list
    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("KirmiziSimit") || other.CompareTag("MaviSimit")))
        {
            takiliSimitler.RemoveAt(takiliSimitler.Count - 1);
        }
    }
    //When mouse gown event happens on a cubuk collider we activate the last placed simit for movement
    private void OnMouseDown()
    {
        if(takiliSimitler.Count > 0)
        {
            takiliSimitler[takiliSimitler.Count - 1].GetComponent<SimitController>().aktifSimit = true;
            takiliSimitler[takiliSimitler.Count - 1].GetComponent<SimitController>().simitIlkPos = gameObject.transform.position.z;
        }
    }

    //With this function we control the color of all the simit on cubuk
    private bool SetControl()
    {
        if(takiliSimitler[0].tag == takiliSimitler[1].tag && takiliSimitler[1].tag == takiliSimitler[2].tag)
        {
            setTamam = true;
        }

        return setTamam;
    }

    //With this function we control the is there 2 cubuk with setTamam is true
    private void BitisControl()
    {
        if(
            (simitCubuklar[0].GetComponent<CubukController>().setTamam && simitCubuklar[1].GetComponent<CubukController>().setTamam) ||
            (simitCubuklar[1].GetComponent<CubukController>().setTamam && simitCubuklar[2].GetComponent<CubukController>().setTamam) ||
            (simitCubuklar[0].GetComponent<CubukController>().setTamam && simitCubuklar[2].GetComponent<CubukController>().setTamam) 
          )
        {
            ResetCubuklar();
            GameObject.Find("GameManager").GetComponent<GameManager>().EndGame();
        }
    }

    // When game over we reset the saved gameobjects and setTamam variable for restarting the game
    public void ResetCubuklar()
    {
        foreach(GameObject cubuk in simitCubuklar)
        {
            cubuk.GetComponent<CubukController>().takiliSimitler.Clear();
            cubuk.GetComponent<CubukController>().setTamam = false;
        }
    }
}
