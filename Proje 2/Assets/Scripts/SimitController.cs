using UnityEngine;

public class SimitController : MonoBehaviour
{
    private Vector3 mouseP;
    public float simitIlkPos;
    private float yukseklik = 4f;
    private float[] cubukCenterPoints = new float[] { -4f, 0f, 4f };
    public bool aktifSimit {get; set;} = false;

    /*
     * On Update function we are tracing aktif simit and when mouse up happens we place simit to cubuk and 
     * reverse the aktif variable to false
     * */
    void Update()
    {
        if (aktifSimit)
        {
            ConvertMousePosition();

            if (Input.GetMouseButtonUp(0))
            {
                aktifSimit = false;
                CubugaTak();
            }
        }
    }
   // This Function trace mouse position for aktif simit elements
    private void ConvertMousePosition()
    {
        Vector3 newMousePos;

        mouseP = Input.mousePosition;
        mouseP.z = Camera.main.nearClipPlane + 6;
        newMousePos = Camera.main.ScreenToWorldPoint(mouseP);

        
        gameObject.transform.position = new Vector3(0f, yukseklik, newMousePos.z);
    }
    //When mouse up event happens this function move simit right above the cubuk
    private void CubugaTak()
    {
        gameObject.transform.position = new Vector3(0f, yukseklik, YakinCubuk());
    }
    
    /* 
     * This Function calculates nearest cubuk. Ýf player leaves simit mittle ground or not above a cubuk
     * with this function simit moves the nearest cubuk.
     * */
    private float YakinCubuk()
    {
        float simitKonumuZ = gameObject.transform.position.z;
        float uzaklikCubuk1 = Mathf.Abs(cubukCenterPoints[0] - simitKonumuZ);
        float uzaklikCubuk2 = Mathf.Abs(cubukCenterPoints[1] - simitKonumuZ);
        float uzaklikCubuk3 = Mathf.Abs(cubukCenterPoints[2] - simitKonumuZ);
        

        if (uzaklikCubuk1 < uzaklikCubuk2 && uzaklikCubuk1 < uzaklikCubuk3)
        {
            return cubukCenterPoints[0];
        }
        else if (uzaklikCubuk2 < uzaklikCubuk1 && uzaklikCubuk2 < uzaklikCubuk3)
        {
            return cubukCenterPoints[1];
        }
        else if (uzaklikCubuk3 < uzaklikCubuk1 && uzaklikCubuk3 < uzaklikCubuk2)
        {
            return cubukCenterPoints[2];
        }

        return 0;
    }

    //If cubuk full with this function simit moved the old cubuk position
    public void GeriGonder()
    {
        gameObject.transform.position = new Vector3(0f, yukseklik, simitIlkPos);
    }
}
