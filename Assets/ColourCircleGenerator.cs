using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourCircleGenerator : MonoBehaviour
{
    public GameObject ArcPiece;

    public float RotationSpeed = 1.0f;

    //just in case?
    const int ArcPieceCount = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ArcPieceCount; i++)
        {
            //generating arc piece
            GameObject NewArcPiece = (GameObject)Object.Instantiate(ArcPiece, transform);
            //setting the rotation of the new arc piece using Euler angles
            NewArcPiece.transform.rotation = Quaternion.Euler(0, 0, 90*i);

            //setting the colour of the arc piece
            ColourManagerScript CScript = NewArcPiece.GetComponent<ColourManagerScript>();
            if (CScript != null)
                CScript.CurrentColourIndex = i;
        }
    }
    
    void FixedUpdate()
    {
        //updating the circle rotation here
        //transform.rotation = Quaternion.Euler(0, 0, transform.rot);
    }
}
