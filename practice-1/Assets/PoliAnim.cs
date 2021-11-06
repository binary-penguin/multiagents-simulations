using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliAnim : MonoBehaviour
{   
    Vector3[] points; // GEOMETRY
    int[] tris;       // TOPOLOGY
    float angle = 0;
    float t = 0.0f;
    int currentEquation = 0; // Control variable for equations
    float deltaX = 0.0f, deltaY = 0.0f, deltaZ = 0.0f;
    bool visited1 = false, visited2 = false, visited3 = false, visited4 = true;

    void TransformTriangle() 
    {

        Vector4 v0 = points[0];
        Vector4 v1 = points[1];
        Vector4 v2 = points[2];
        Vector4 v3 = points[3];
        Vector4 v4 = points[4];
        Vector4 v5 = points[5];
        Vector4 v6 = points[6];
        Vector4 v7 = points[7];


        
       // El ultimo componente de la transformacion lleva 1's
        v0.w = 1.0f;
        v1.w = 1.0f;
        v2.w = 1.0f;
        v3.w = 1.0f;
        v4.w = 1.0f;
        v5.w = 1.0f;
        v6.w = 1.0f;
        v7.w = 1.0f;


        // Creamos la matriz basado en el no. de ecuacion que nos encontremos
        Matrix4x4 A = this.matrizMaker();

        // NO HAGAS transform * transform2 * vi
        v0 = A * v0;
        v1 = A * v1;
        v2 = A * v2;
        v3 = A * v3;
        v4 = A * v4;
        v5 = A * v5;
        v6 = A * v6;
        v7 = A * v7;

        // Regresamos a 3D
        Vector3[] points2 = {v0, v1, v2, v3, v4, v5, v6, v7};


        // Revisamos si es necesario cambiar de ecuacion
        if ((int) points2[0][1] == 45.0 && (int)points2[0][0] == 0.0 && !visited1) {
            currentEquation = 1;
            visited1 = true;
            t = 0;
            Debug.Log("Reincia1: " + t);
        }

        else if ((int) points2[0][1] == 45.0 && (int)points2[0][0] == 45.0 && visited1) {
            currentEquation = 2;
            visited2 = true;
            t = 0;
            Debug.Log("Reincia2: " + t);
        }

        else if ((int) points2[0][1] == 0.0 && (int)points2[0][0] == 45.0 && visited2) {
            currentEquation = 3;
            visited3 = true;
            t = 0;
            Debug.Log("Reincia3: " + t);
        }
        else if ((int) points2[0][1] == 0.0 && (int)points2[0][0] == 0.0 && visited3) {
            currentEquation = 0;
            visited1 = false;
            visited2 = false;
            visited3 = false;
            t = 0;
            Debug.Log("Reincia4: " + t);
        }
        
        //Debug.Log("x: " + (int) points2[0][0]);
        //Debug.Log("y: " + (int) points2[0][1]);
        //Debug.Log("z: " + (int) points2[0][2]);
        
        
        // Cambiamos la geometria del triangulo
        GetComponent<MeshFilter>().mesh.vertices = points2;


    }

   

    Matrix4x4 matrizMaker() {

        if (currentEquation == 0) {
            deltaX =  (float) 0.0f * t;
            deltaY =  (float) 45.0f * t;
            deltaZ =  (float) 0.0f;
        }
        else if (currentEquation == 1) {
            deltaX =  (float) 45.0f * t;
            deltaY =  (float) 45.0f;
            deltaZ =  (float) 0.0f;
        }

        else if (currentEquation == 2) {
            deltaX =  (float) 45.0f;
            deltaY =  (float) 45.0f - (45.0f * t);
            deltaZ =  (float) 0.0f;
        }

        else if (currentEquation == 3) {
            deltaX =  (float) 45.0f - (45.0f * t);
            deltaY =  (float) 0.0f;
            deltaZ =  (float) 0.0f;
        }

        Matrix4x4 transform = Transformations.TranslateM(deltaX, deltaY, deltaZ);
        return transform;

    }

    // Start is called before the first frame update
    void Start()
    {
        // 1. Todo en OpenGL es un conjunto de mallas
        Mesh mesh = new Mesh();
        // 2. Vincular mesh interno de OpenGl con el instanciado
        GetComponent<MeshFilter>().mesh = mesh;


        // 3. Hacemos la geometria (definir puntos)
        points = new Vector3[8];
        //points[0] = new Vector3(0, 0, 0);
        points[0] = new Vector3(0, 0, 0);
        points[1] = new Vector3(25, 0, 0);
        points[2] = new Vector3(25, 25, 0);
        points[3] = new Vector3(0, 25, 0);
        points[4] = new Vector3(0, 25, 25);
        points[5] = new Vector3(25, 25, 25);
        points[6] = new Vector3(25, 0, 25);
        points[7] = new Vector3(0, 0, 25);

        // 4. Hacemos la topologia (hacer las lineas)

        // Vamos a poner en cada tris[n] a n
        int[] triangles = {
			0, 2, 1, //face front
			0, 3, 2,
			2, 3, 4, //face top
			2, 4, 5,
			1, 2, 5, //face right
			1, 5, 6,
			0, 7, 4, //face left
			0, 4, 3,
			5, 4, 7, //face back
			5, 7, 6,
			0, 6, 7, //face bottom
			0, 1, 6
		};
        

        // Le decimos al mesh todo el relajo
        // Oye mesh estos son los puntos
        mesh.vertices = points;

        // Oye mesh estas son las lineas del triangulo
        mesh.triangles = triangles;

        // Oye mesh echate el triangulito
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(200, 0, 0), Color.red);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 200, 0), Color.green);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 0, 200), Color.blue);

        // Cada frame aumentamos en 1grado el angulo
        angle += 1.0f;
        t += 0.001f;

        // Para evitar un overflow limita los angulos (cosas raras)
        if (angle > 360) 
            angle = 0.0f;
        

        //Debug.Log("t: " + t);
        
        
        TransformTriangle();
    }
}
