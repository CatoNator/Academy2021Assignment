using UnityEngine;

public class ColourCircleGenerator : MonoBehaviour
{
    public GameObject ArcPiece;

    public float RotationSpeed = 1.0f;

    public bool Rectangle = false;

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
            NewArcPiece.transform.localRotation = Quaternion.Euler(0, 0, 90*i);

            //offsetting the side by 1 to make a rectangle, otherwise the side pieces will form a cross

            //this doesn't work... I give up
            if (Rectangle)
                NewArcPiece.transform.localPosition = new Vector3(1, 0, 0);

            //setting the colour of the arc piece
            ColourManagerScript CScript = NewArcPiece.GetComponent<ColourManagerScript>();
            if (CScript != null)
                CScript.CurrentColourIndex = i;
        }
    }
    
    void FixedUpdate()
    {
        //updating the circle rotation here
        transform.Rotate(new Vector3(0, 0, RotationSpeed));
    }
}
