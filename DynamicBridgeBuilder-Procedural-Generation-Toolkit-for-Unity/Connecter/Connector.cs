using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public List<GameObject> Point_List_1 = new List<GameObject>();
    public List<GameObject> Point_List_2 = new List<GameObject>();

    public GameObject Point_Prefab;


    public int Subdivision_1 = 5;
    public int Subdivision_2 = 7;

    public float Height = 5.5f;
    public float Lenght = 5.5f;


    public float Gizmos_Radius = 1f;
    public float Gizmos_Life_Time = 1f;


    public float Point_Offset = 0;

    public bool _Create;
    public bool _Update;
    public bool _Clear;
    public bool Connect;
    public bool Start_From_The_Top;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        if (_Create)
        {
            if (Subdivision_1 != 0 && Subdivision_2 != 0)
            {
                Clear_Point();
                _Clear = false;
                Creat_Point();
            }
            _Create = false;
        }
        else if (_Update)
        {
            if (Point_List_1.Count != 0 && Point_List_2.Count != 0)
            {
                Update_Point();
                if (Connect)
                {
                    Connect_All();
                }
            }

            _Update = false;
        }
        else if (_Clear)
        {
            Clear_Point();
            _Clear = false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, Gizmos_Radius);
        Gizmos.DrawSphere(transform.position + transform.forward * Lenght, Gizmos_Radius);

        Gizmos.DrawSphere(transform.position + transform.up * Height, Gizmos_Radius);
        Gizmos.DrawSphere(transform.position + transform.forward * Lenght + transform.up * Height, Gizmos_Radius);

    }

    void Creat_Point()
    {
        float Point_Distance_1 = Lenght / Subdivision_1;
        float Point_Distance_2 = Lenght / Subdivision_2;

        float Temp_z = 0;
        for (int i = 0; i < Subdivision_1; i++)
        {
            GameObject Temp_Point = Instantiate(Point_Prefab, Vector3.zero, this.transform.rotation, this.transform);
            Temp_Point.transform.localPosition = new Vector3(0, 0, Temp_z + Point_Offset);

            Temp_z += Point_Distance_1;
            Point_List_1.Add(Temp_Point);
        }

        Temp_z = 0;
        for (int i = 0; i < Subdivision_2; i++)
        {
            GameObject Temp_Point = Instantiate(Point_Prefab, Vector3.zero, this.transform.rotation, this.transform);
            Temp_Point.transform.localPosition = new Vector3(0, Height, Temp_z + Point_Offset);

            Temp_z += Point_Distance_2;
            Point_List_2.Add(Temp_Point);

        }

    }

    void Update_Point()
    {


        if (Subdivision_1 > Point_List_1.Count)
        {
            for (int i = Point_List_1.Count; i < Subdivision_1; i++)
            {
                GameObject Temp_Point = Instantiate(Point_Prefab, Vector3.zero, this.transform.rotation, this.transform);

                Point_List_1.Add(Temp_Point);
            }

        }
        else if (Subdivision_1 < Point_List_1.Count)
        {
            for (int i = 0; i < Point_List_1.Count - Subdivision_1; i++)
            {

                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(Point_List_1[i]);

                };

                Point_List_1.RemoveAt(i);

            }
        }

        float Temp_z = 0;
        float Point_Distance_1 = Lenght / Subdivision_1;
        foreach (var Temp_Points in Point_List_1)
        {
            Temp_Points.transform.localPosition = new Vector3(0, 0, Temp_z + Point_Offset);
            Temp_z += Point_Distance_1;
        }


        if (Subdivision_2 > Point_List_2.Count)
        {
            for (int i = Point_List_2.Count; i < Subdivision_2; i++)
            {
                GameObject Temp_Point = Instantiate(Point_Prefab, Vector3.zero, this.transform.rotation, this.transform);

                Point_List_2.Add(Temp_Point);
            }

        }
        else if (Subdivision_2 < Point_List_2.Count)
        {
            for (int i = 0; i < Point_List_2.Count - Subdivision_2; i++)
            {


                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(Point_List_2[i]);

                };
                Point_List_2.RemoveAt(i);
            }
        }

        float Point_Distance_2 = Lenght / Subdivision_2;
        Temp_z = 0;
        foreach (var Temp_Points in Point_List_2)
        {
            Temp_Points.transform.localPosition = new Vector3(0, Height, Temp_z + Point_Offset);
            Temp_z += Point_Distance_2;

        }



    }

    void Clear_Point()
    {
        Transform[] All_Childrens = this.GetComponentsInChildren<Transform>();


#if UNITY_EDITOR

        foreach (Transform child in this.transform)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (child.GetComponent<Rope_Bridge>() == null && child != null)
                {
                    DestroyImmediate(child.gameObject);

                }
            };
        }

#endif
        if (Application.isPlaying)
        {
            foreach (Transform child in this.transform)
            {

                if (child.GetComponent<Rope_Bridge>() == null && child != null)
                {
                    DestroyImmediate(child.gameObject);

                }

            }
        }







        _Create = false;
        _Clear = false;

        Point_List_1.Clear();
        Point_List_2.Clear();



    }

    void Connect_All_1()
    {

        List<List<GameObject>> _2D_List = new List<List<GameObject>>();

        int Switching = 0;
        int temp_value = -1;

        if (Start_From_The_Top)
        {
            _2D_List.Add(Point_List_1);
            _2D_List.Add(Point_List_2);
        }
        else
        {
            _2D_List.Add(Point_List_2);
            _2D_List.Add(Point_List_1);


            Switching = 1;
            temp_value = 1;
        }


        for (int i = 0; i < _2D_List[0].Count; i++)
        {
            Switching += 1 * -temp_value;

            temp_value *= -1;

            //Debug_Draw_Line(_2D_List[Previous][i].transform.position, _2D_List[Switching][i].transform.position, false, 10);
            //  Debug_Draw_Line(_2D_List[Previous][i].transform.position, _2D_List[Switching][i + 1].transform.position, false, 10);

            if (_2D_List[Switching][i].transform.position.z >= _2D_List[Switching * -1][i].transform.position.z)
            {
                Debug_Draw_Line(_2D_List[Switching * -1][i].transform.position, _2D_List[Switching][i + 1].transform.position, false, 10);

            }
            else
            {
                Debug_Draw_Line(_2D_List[Switching * -1][i].transform.position, _2D_List[Switching][i + 1].transform.position, false, 10);

            }


            Debug.Log(Switching);
        }

    }
    void Connect_All()
    {



        if (Start_From_The_Top)
        {

            for (int i = 0; i < Point_List_1.Count; i++)
            {
                if (i == 0)
                {
                    Debug.DrawLine(Point_List_1[0].transform.position, Point_List_2[0].transform.position, Color.red, Gizmos_Life_Time);
                    Debug.DrawLine(Point_List_2[0].transform.position, Point_List_1[1].transform.position, Color.red, Gizmos_Life_Time);

                }
                else if (i == Point_List_1.Count - 1)
                {
                    Debug.DrawLine(Point_List_2[Point_List_2.Count - 1].transform.position, Point_List_1[Point_List_1.Count - 1].transform.position, Color.red, Gizmos_Life_Time);

                    for (int y = 0; y < Point_List_1.Count; y++)
                    {


                        if (Point_List_1[y].transform.position.z >= Point_List_2[Point_List_2.Count - 1].transform.position.z)
                        {
                            Debug.DrawLine(Point_List_2[Point_List_2.Count - 1].transform.position, Point_List_1[y].transform.position, Color.red, Gizmos_Life_Time);

                        }
                    }



                }
                else
                {

                    for (int z = 0; z < Point_List_2.Count; z++)
                    {
                        if (Point_List_1[i].transform.position.z < Point_List_2[z].transform.position.z)
                        {
                            Debug.DrawLine(Point_List_1[i].transform.position, Point_List_2[z].transform.position, Color.red, Gizmos_Life_Time);
                            if (i + 1 < Point_List_1.Count - 1)
                            {
                                Debug.DrawLine(Point_List_2[z].transform.position, Point_List_1[i + 1].transform.position, Color.red, Gizmos_Life_Time);

                            }

                            break;
                        }
                        else
                        {
                            if (Point_List_2[z].transform.position.z > Point_List_1[i - 1].transform.position.z)
                            {
                                Debug.DrawLine(Point_List_1[i].transform.position, Point_List_2[z].transform.position, Color.red, Gizmos_Life_Time);

                            }
                        }
                    }

                }


            }
        }
        else
        {
            for (int i = 0; i < Point_List_2.Count; i++)
            {
                if (i == 0)
                {
                    Debug.DrawLine(Point_List_2[0].transform.position, Point_List_1[0].transform.position, Color.red, Gizmos_Life_Time);
                    Debug.DrawLine(Point_List_1[0].transform.position, Point_List_2[1].transform.position, Color.red, Gizmos_Life_Time);

                }
                else if (i == Point_List_2.Count - 1)
                {
                    Debug.DrawLine(Point_List_1[Point_List_1.Count - 1].transform.position, Point_List_2[Point_List_2.Count - 1].transform.position, Color.red, Gizmos_Life_Time);


                    for (int y = 0; y < Point_List_2.Count; y++)
                    {


                        if (Point_List_2[y].transform.position.z >= Point_List_1[Point_List_1.Count - 1].transform.position.z)
                        {
                            Debug.DrawLine(Point_List_1[Point_List_1.Count - 1].transform.position, Point_List_2[y].transform.position, Color.red, Gizmos_Life_Time);

                        }
                    }

                }
                else
                {

                    for (int z = 0; z < Point_List_1.Count; z++)
                    {
                        if (Point_List_2[i].transform.position.z < Point_List_1[z].transform.position.z)
                        {
                            Debug.DrawLine(Point_List_2[i].transform.position, Point_List_1[z].transform.position, Color.red, Gizmos_Life_Time);
                            if (i + 1 < Point_List_2.Count - 1)
                            {
                                Debug.DrawLine(Point_List_1[z].transform.position, Point_List_2[i + 1].transform.position, Color.red, Gizmos_Life_Time);

                            }

                            break;
                        }
                        else
                        {
                            if (Point_List_1[z].transform.position.z > Point_List_2[i - 1].transform.position.z)
                            {
                                Debug.DrawLine(Point_List_2[i].transform.position, Point_List_1[z].transform.position, Color.red, Gizmos_Life_Time);

                            }
                        }
                    }

                }


            }
        }



    }

    void Debug_Draw_Line(Vector3 Pos1, Vector3 Pos2, bool Ran_Color, float Duration)
    {
        Color new_Color;
        if (Ran_Color)
        {

            float r = Random.Range(0, 255);
            float g = Random.Range(0, 255);
            float b = Random.Range(0, 255);



            new_Color = new Color(0.2F, 0.3F, 0.4F, 255f);
        }
        else
        {
            new_Color = Color.red;
        }

        Debug.DrawLine(Pos1, Pos2, new_Color, Duration);
    }

}
