using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class verticesAndEdges : MonoBehaviour
{
  [SerializeField]
  private TextAsset m_txt_vert_pos;
  private int m_instantiate_ctr;
  GameObject[] m_array_go;
  GameObject m_vert_proto;

  void Start()
  {
    Debug.Log("PERK starting the EdgeVertices script");
    m_array_go = new GameObject[500];
    m_instantiate_ctr = 0;
    m_vert_proto = GameObject.Find("VertexProto");
    m_vert_proto.SetActive(false);

    // Assign a text file (MUST END IN ".txt") to m_txt_vert_pos in 
    // the inspector.  Can do this by drag/drop after you've added 
    // the .txt file as an asset to the project.  
    //
    // Below: get the assets "text" property and parse the text.
    // Here we assume first line is an integer (number of vertices)
    // and the subsequent lines contain x, y, z coordinates
    string wholeFile = m_txt_vert_pos.text;
    string[] lines = {"0 2 0","0 3 0","0 4 0","0 5 0","0 6 0","0 7 0","0 8 0","0 9 0","0 10 0","0 11 0","1 2 0","1 3 0","1 4 0","1 5 0","1 6 0","1 7 0","1 8 0","1 9 0","1 10 0","1 11 0"};
    int vert_cnt = 20;//int.Parse( lines[0] ); // first line is number of vertices
    Debug.Log("PERK num_vertices = " + vert_cnt);


// Loop to craete the left side vertices

for(int n = 0 ; n < 10 ; n++){

  double left_angle = ((60*(Math.PI))/180) * n;     // converting 60 degrees in radian
  float x = -(float)Math.Cos(left_angle)  ;
  float y =  (float)Math.Sin(left_angle) ; 
  float z = n ; 

  m_array_go[m_instantiate_ctr] = 
         Instantiate(m_vert_proto, new Vector3(x, y, z), 
                             Quaternion.Euler(0.0f, 0.0f, 0.0f));
      m_array_go[m_instantiate_ctr].SetActive(true);
      m_instantiate_ctr += 1;
}

// Lop to create right side vertices 
for(int n = 0 ; n < 10 ; n++){

  double left_angle = ((60*(Math.PI))/180) * n;                       // 60 degrees in radian
  double right_angle = left_angle + Math.PI ;           // 180 degrees in radian
  float x = -(float)Math.Cos(right_angle)  ;
  float y =  (float)Math.Sin(right_angle) ; 
  float z = n ; 

  m_array_go[m_instantiate_ctr] = 
         Instantiate(m_vert_proto, new Vector3(x, y, z), 
                             Quaternion.Euler(0.0f, 0.0f, 0.0f));
      m_array_go[m_instantiate_ctr].SetActive(true);
      m_instantiate_ctr += 1;
}

    // Step 2: Connecte the vertices with a line (this could have been
    // integrated with Step 1 above, but was separated for clarity)

    //Create LineRenderer for the left strand
    LineRenderer lr = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr.startWidth = 0.1f;
    lr.endWidth = 0.1f;
    lr.useWorldSpace = true;    
    lr.material.SetColor("_Color", Color.blue);
    lr.positionCount = 10;

    //For drawing line in the world space, provide the x,y,z values
    //of the points on the line
    for(int n = 0; n < 10; n++){
      
      // We have to take the negative of x since we want a RHS and
      // Unity is left-handed
       double left_angle = ((60*(Math.PI))/180) * n;                       // 60 degrees in radian
       double right_angle = left_angle + Math.PI ;           // 180 degrees in radian
       float x = -(float)Math.Cos(left_angle)  ;
        float y =  (float)Math.Sin(left_angle) ; 
        float z = n ; 
      lr.SetPosition(n, new Vector3(x, y, z) );
    }

    //Create LineRenderer for the left strand
    LineRenderer lra = new GameObject("TempLine").AddComponent<LineRenderer>();
    lra.startWidth = 0.1f;
    lra.endWidth = 0.1f;
    lra.useWorldSpace = true;    
    lra.material.SetColor("_Color", Color.blue);
    lra.positionCount = 10;

        for(int n = 0; n < 10; n++){
      
      // We have to take the negative of x since we want a RHS and
      // Unity is left-handed
       double left_angle = ((60*(Math.PI))/180) * n;                       // 60 degrees in radian
       double right_angle = left_angle + Math.PI ;           // 180 degrees in radian
       float x = -(float)Math.Cos(right_angle)  ;
        float y =  (float)Math.Sin(right_angle) ; 
        float z = n ; 
      lra.SetPosition(n, new Vector3(x, y, z) );
    }
    // each base pair line when n = 1
  LineRenderer lr1 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr1.startWidth = 0.1f;
    lr1.endWidth = 0.1f;
    lr1.useWorldSpace = true;    
    lr1.material.SetColor("_Color", Color.red);
    lr1.positionCount = 2;

    lr1.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 1) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 1) + Math.PI)), (1)) );
    lr1.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 1)), ( (float)Math.Sin(((60*(Math.PI))/180) * 1)), (1)) );

 // each 2 points
  LineRenderer lr2 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr2.startWidth = 0.1f;
    lr2.endWidth = 0.1f;
    lr2.useWorldSpace = true;    
    lr2.material.SetColor("_Color", Color.red);
    lr2.positionCount = 2;
    // each 2 points

      lr2.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 2) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 2) + Math.PI)), (2)) );
      lr2.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 2)), ( (float)Math.Sin(((60*(Math.PI))/180) * 2)), (2)) );

 // each 2 points
  LineRenderer lr3 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr3.startWidth = 0.1f;
    lr3.endWidth = 0.1f;
    lr3.useWorldSpace = true;    
    lr3.material.SetColor("_Color", Color.red);
    lr3.positionCount = 2;
    // each 2 points

      lr3.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 3) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 3) + Math.PI)), (3)) );
      lr3.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 3)), ( (float)Math.Sin(((60*(Math.PI))/180) * 3)), (3)) );

 // each 2 points
  LineRenderer lr4 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr4.startWidth = 0.1f;
    lr4.endWidth = 0.1f;
    lr4.useWorldSpace = true;    
    lr4.material.SetColor("_Color", Color.red);
    lr4.positionCount = 2;
    // each 2 points

      lr4.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 4) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 4) + Math.PI)), (4)) );
      lr4.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 4)), ( (float)Math.Sin(((60*(Math.PI))/180) * 4)), (4)) );

 // each 2 points
  LineRenderer lr5 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr5.startWidth = 0.1f;
    lr5.endWidth = 0.1f;
    lr5.useWorldSpace = true;    
    lr5.material.SetColor("_Color", Color.red);
    lr5.positionCount = 2;
    // each 2 points

      lr5.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 5) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 5) + Math.PI)), (5)) );
      lr5.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 5)), ( (float)Math.Sin(((60*(Math.PI))/180) * 5)), (5)) );

// each 2 points
  LineRenderer lr6 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr6.startWidth = 0.1f;
    lr6.endWidth = 0.1f;
    lr6.useWorldSpace = true;    
    lr6.material.SetColor("_Color", Color.red);
    lr6.positionCount = 2;
    // each 2 points

      lr6.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 6) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 6) + Math.PI)), (6)) );
      lr6.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 6)), ( (float)Math.Sin(((60*(Math.PI))/180) * 6)), (6)) );
// each 2 points
  LineRenderer lr7 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr7.startWidth = 0.1f;
    lr7.endWidth = 0.1f;
    lr7.useWorldSpace = true;    
    lr7.material.SetColor("_Color", Color.red);
    lr7.positionCount = 2;
    // each 2 points

      lr7.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 7) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 7) + Math.PI)), (7)) );
      lr7.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 7)), ( (float)Math.Sin(((60*(Math.PI))/180) * 7)), (7)) );

// each 2 points
  LineRenderer lr8 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr8.startWidth = 0.1f;
    lr8.endWidth = 0.1f;
    lr8.useWorldSpace = true;    
    lr8.material.SetColor("_Color", Color.red);
    lr8.positionCount = 2;
    // each 2 points

      lr8.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 8) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 8) + Math.PI)), (8)) );
      lr8.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 8)), ( (float)Math.Sin(((60*(Math.PI))/180) * 8)), (8)) );



// each 2 points
  LineRenderer lr9 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr9.startWidth = 0.1f;
    lr9.endWidth = 0.1f;
    lr9.useWorldSpace = true;    
    lr9.material.SetColor("_Color", Color.red);
    lr9.positionCount = 2;
    // each 2 points

      lr9.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 9) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 9) + Math.PI)), (9)) );
      lr9.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 9)), ( (float)Math.Sin(((60*(Math.PI))/180) * 9)), (9)) );


// each 2 points
  LineRenderer lr0 = new GameObject("TempLine").AddComponent<LineRenderer>();
    lr0.startWidth = 0.1f;
    lr0.endWidth = 0.1f;
    lr0.useWorldSpace = true;    
    lr0.material.SetColor("_Color", Color.red);
    lr0.positionCount = 2;
    // each 2 points

      lr0.SetPosition(0, new Vector3( (-(float)Math.Cos((((60*(Math.PI))/180) * 0) + Math.PI)) , ((float)Math.Sin((((60*(Math.PI))/180) * 0) + Math.PI)), (0)) );
      lr0.SetPosition(1, new Vector3((-(float)Math.Cos(((60*(Math.PI))/180) * 0)), ( (float)Math.Sin(((60*(Math.PI))/180) * 0)), (0)) );


	}
}
