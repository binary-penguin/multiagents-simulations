using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CREATE A TRIANGLE
public class Poli : MonoBehaviour
{

    Vector3[] points; // GEOMETRY
    int[] tris;       // TOPOLOGY
    float angle = 0;


    // Start is called before the first frame update
    void Start()
    {
        // 1. Todo en OpenGL es un conjunto de mallas
        Mesh mesh = new Mesh();
        // 2. Vincular mesh interno de OpenGl con el instanciado
        GetComponent<MeshFilter>().mesh = mesh;


        // 3. Hacemos la geometria (definir puntos)
        points = new Vector3[3];
        points[0] = new Vector3(0, 0, 0);
        points[1] = new Vector3(5, 0, 0);
        points[2] = new Vector3(2, 5, 0);

        // 4. Hacemos la topologia (hacer las lineas)

        // Vamos a poner en cada tris[n] a n
        tris = new int[3];
        tris[0] = 0;
        tris[1] = 1;
        tris[2] = 2;

        // Le decimos al mesh todo el relajo
        // Oye mesh estos son los puntos

        mesh.vertices = points;
        // Oye mesh estas son las lineas del triangulo
        mesh.triangles = tris;

        // Oye mesh echate el triangulito
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(50, 0, 0), Color.red);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 50, 0), Color.green);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 0, 50), Color.blue);
    }
}
