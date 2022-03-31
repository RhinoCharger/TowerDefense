using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    float speed = 5;

    public float minX = -20;
    public float maxX = 20;

    public float minY = 5;
    public float maxY = 20;
    
    public float minZ = -20;
    public float maxZ = 20;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
        ConstrainCamera();
    }

    void Controls()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"), Space.World);

        transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"), Space.World);

        transform.Translate(Vector3.up * speed * -30 * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel"), Space.World);
    }

    void ConstrainCamera()
    {
        float xPos = transform.position.x;
        xPos = Mathf.Clamp(xPos, minX, maxX);
        
        float yPos = transform.position.y;
        yPos = Mathf.Clamp(yPos, minY, maxY);
        
        float zPos = transform.position.z;
        zPos = Mathf.Clamp(zPos, minZ, maxZ);

        transform.position = new Vector3(xPos, yPos, zPos);

    }


}
