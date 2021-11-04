using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliAnim : MonoBehaviour
{   
    Vector3[] points; // GEOMETRY
    int[] tris;       // TOPOLOGY
    float angle = 0, deltaZ = 0, deltaY = 0, deltaX = 0;
    float time_stepX = 0f, time_stepY = 0f;
    int t=1;
    

    void TransformTriangle() 
    {
        Vector4 v0 = points[0];
        Vector4 v1 = points[1];
        Vector4 v2 = points[2];

        // El ultimo componente de la transformacion lleva 1's
        v0.w = 1.0f;
        v1.w = 1.0f;
        v2.w = 1.0f;

        // Traslado 3.2 a la derecha y 2.4 hacia abajo
        Matrix4x4 transform1 = Transformations.TranslateM(deltaX, deltaY, deltaZ);

        // Usando DP, solo calculamos una vez A

        Matrix4x4 A = transform1;

        // NO HAGAS transform1 * transform2 * vi
        v0 = A * v0;
        v1 = A * v1;
        v2 = A * v2;

        // Regresamos a 3D

        Vector3[] points2 = {v0, v1, v2};
        // Cambiamos la geometria del triangulo
        GetComponent<MeshFilter>().mesh.vertices = points2;


    }

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
        points[2] = new Vector3(0, 5, 0);

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

        // Cada frame aumentamos en 1grado el angulo
        angle += 1.0f;
        

       
        

        if (deltaX <= 0 && deltaY <= 0) {
            time_stepX=0.2f;
            time_stepY=0f;
            t=1;
          
            
        }
            

        if (deltaX >= 5 && deltaY <= 0) {
            time_stepX=0f;
            time_stepY=0.2f;
            t=1;
  
            
        }
           
        if (deltaX >= 5 && deltaY >= 5) {
            time_stepX=-0.2f;
            time_stepY=0f;
            t=1;

        }
        
        if (deltaX <= 0 && deltaY >= 5) {
            time_stepX=0f;
            time_stepY=-0.2f;
            t=1;
        }

        if (deltaX>0 && deltaY>0){
            deltaX=t*time_stepX;
            deltaY=t*time_stepY;
        }else{
            deltaX=30+t*time_stepX;
            deltaY=30+t*time_stepY;
        }
        
        t+=1;


        // Para evitar un overflow limita los angulos (cosas raras)
        if (angle > 360) 
            angle = 0.0f;
        
        Debug.Log("Dx: " + deltaX);
        Debug.Log("Dy: " + deltaY);
        Debug.Log("Dz: " + deltaZ);
        
        TransformTriangle();
    }
}
