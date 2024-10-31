using System.Collections.Generic;
using UnityEngine;
public class Rope_Bridge : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject Plank_Prefabe;
    public GameObject Joint_Prefabe;
    public GameObject Anchor_Prefabe;
    public GameObject Rope_Joint_Prefabe;

    //Private Empty Folders (For Organisation)
    private GameObject Plank_Empty;
    private GameObject Bridge_Joint_Empty;
    private GameObject Bridge_Joint_Left_Empty;
    private GameObject Bridge_Joint_Right_Empty;
    private GameObject Rope_Empty;
    private GameObject Rope_Right_Empty;
    private GameObject Rope_Left_Empty;
    private GameObject Rope_Joint_Empty;
    private GameObject Rope_Joint_Right_Empty;
    private GameObject Rope_Joint_Left_Empty;
    private GameObject Anchor_Empty;

    [Header("Size")]

    public Vector3 PLank_Size = new Vector3(3, .5f, 1);
    public Vector2 Joint_Size = new Vector2(1, 1);
    public Vector2 Anchor_Size = new Vector2(1, 1);
    public Vector2 Vertical_Joint_Size = new Vector2(.1f, .1f);
    [Header("List")]

    public List<GameObject> PLank_List = new List<GameObject>();
    public List<GameObject> Bridge_Joint_Left_List = new List<GameObject>();
    public List<GameObject> Bridge_Joint_Right_List = new List<GameObject>();
    public List<GameObject> Rope_Left_List = new List<GameObject>();
    public List<GameObject> Rope_Right_List = new List<GameObject>();
    public List<GameObject> Rope_Joint_Left_List = new List<GameObject>();
    public List<GameObject> Rope_Joint_Right_List = new List<GameObject>();
    public List<GameObject> Anchor_Right = new List<GameObject>();
    public List<GameObject> Anchor_Left = new List<GameObject>();
    public List<GameObject> Rope_Vertical_Joint_Left = new List<GameObject>();
    public List<GameObject> Rope_Vertical_Joint_Right = new List<GameObject>();
    [Header("Plank")]
    public float Plank_Offset = 0;
    public float Plank_Distance = 1.5f;

    private int[] Sides = { 1, -1 };

    public int Bridge_Lenght = 6;
    [Header("Anchor / Rope")]

    public float Anchor_Height = 3;
    public int Rope_Segments = 6;

    public float Gizmos_Life_Time = 1f;
    [Header("Option")]

    public bool Use_Vertical_Rope;
    public bool Use_Rope;

    [Header("Actions")]

    public bool _Update;
    public bool _Destroy;
    public bool _Is_Kinematic;
    public bool _Clear;
    public bool _Connect;
    public bool _Start_From_The_Top;
    [Header("Joint Infor")]

    public float Limite_Min = 0;
    public float Limite_Max = 5;
    public float bounciness = 0;
    public float bounceMinVelocity = 0.2f;
    public float breakForce = 2000;
    public float breakTorque = 2000;
    public float Joint_Mass = 1;

    private void OnValidate()
    {

        Handle_Input();

    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    void Handle_Input()
    {
        if (_Update)
        {
            if (PLank_List.Count == 0) // Creat New Script
            {
                if (Bridge_Lenght > 3)
                {
                    Creat_Plank();

                }
                else
                {
                    Debug.LogError("Bridge_Lenght Must Be > To 3 ");
                }
            }
            else //Update existing Bridge
            {
                Update_Plank();

            }


            if (Bridge_Joint_Left_List.Count == 0) // Creat New Script
            {
                if (Bridge_Lenght > 3)
                {
                    Creat_Joint();
                }
                else
                {
                    Debug.LogError("Bridge_Lenght Must Be > To 3 ");
                }
            }
            else //Update existing Bridge
            {
                Update_Joint();
            }

            Handle_Bridge_Joint();


            if (Use_Rope)
            {
                Creat_Rope();
                Handle_Rope_Joint();
                if (Use_Vertical_Rope)
                {
                    Connect_Point();

                }
            }



            _Update = false;
            _Destroy = false;
            _Clear = false;

        }
        else if (_Destroy)
        {
            Destroy_Plank();
            Destroy_Joint();
            Destroy_Rope();
            Destroy_Vertical_Rope();
            Destroy_Dependencies();

        }
        else if (_Clear)
        {
            Clear_All();

        }

    }
    void Creat_Plank()
    {
        //creat Plank
        Plank_Empty = new GameObject("Bridge_Planks");
        Plank_Empty.transform.parent = this.transform;
        Plank_Empty.transform.localPosition = Vector3.zero;

        float z = 0;
        for (int i = 0; i < Bridge_Lenght; i++)
        {

            GameObject Temp_Plank = Instantiate(Plank_Prefabe, Plank_Empty.transform);
            Temp_Plank.name = (Plank_Prefabe.name + " : " + i).ToString();
            Temp_Plank.transform.localPosition = new Vector3(0, 0, z + Plank_Offset);
            Temp_Plank.transform.localScale = PLank_Size;

            PLank_List.Add(Temp_Plank);
            z += Plank_Distance;
        }
    }
    void Creat_Joint()
    {
        Bridge_Joint_Empty = new GameObject("Bridge_Joint");

        Bridge_Joint_Left_Empty = new GameObject("Bridge_Joint_Left");
        Bridge_Joint_Right_Empty = new GameObject("Bridge_Joint_Right");

        Bridge_Joint_Empty.transform.parent = this.transform;
        Bridge_Joint_Left_Empty.transform.parent = Bridge_Joint_Empty.transform;
        Bridge_Joint_Right_Empty.transform.parent = Bridge_Joint_Empty.transform;

        Bridge_Joint_Empty.transform.localPosition = Vector3.zero;
        Bridge_Joint_Left_Empty.transform.localPosition = Vector3.zero;
        Bridge_Joint_Right_Empty.transform.localPosition = Vector3.zero;

        foreach (var item in Sides)
        {
            //creat Joint
            float z = Plank_Distance / 2;
            for (int i = 0; i < Bridge_Lenght - 1; i++)
            {

                GameObject Temp_Joint = Instantiate(Joint_Prefabe, this.transform);

                Temp_Joint.transform.localPosition = new Vector3(
                   item * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2,
                    0,
                    z + Plank_Offset);


                Temp_Joint.transform.localScale = new Vector3(
                  Joint_Size.x,
                  (Plank_Distance - PLank_Size.z) / 2,
                  Joint_Size.y);

                if (item == 1)
                {
                    Bridge_Joint_Left_List.Add(Temp_Joint);
                    Temp_Joint.name = (Joint_Prefabe.name + " Left " + i).ToString();

                    Temp_Joint.transform.parent = Bridge_Joint_Left_Empty.transform;

                }
                else if (item == -1)
                {
                    Bridge_Joint_Right_List.Add(Temp_Joint);
                    Temp_Joint.name = (Joint_Prefabe.name + " Right  " + i).ToString();

                    Temp_Joint.transform.parent = Bridge_Joint_Right_Empty.transform;


                }

                z += Plank_Distance;
            }
        }


    }
    void Creat_Rope()
    {

        //Rope Anchors
        Anchor_Empty = new GameObject("Anchor");
        Anchor_Empty.transform.parent = this.transform;
        Anchor_Empty.transform.localPosition = Vector3.zero;

        foreach (int Current_Side in Sides)
        {
            // Anchors Creation
            GameObject Start_Anchor = Instantiate(Anchor_Prefabe,
                PLank_List[0].transform.position + new Vector3(Current_Side * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2, (Anchor_Height / 4) + (PLank_List[0].transform.localScale.y), 0),
                PLank_List[0].transform.rotation,
                Anchor_Empty.transform);

            Start_Anchor.transform.localScale = new Vector3(Anchor_Size.x, PLank_List[0].transform.localScale.y + (Anchor_Height / 2), Anchor_Size.y);

            GameObject End_Anchor = Instantiate(Anchor_Prefabe,
                PLank_List[PLank_List.Count - 1].transform.position + new Vector3(Current_Side * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2, Anchor_Height / 4 + PLank_List[PLank_List.Count - 1].transform.localScale.y, 0),
                PLank_List[PLank_List.Count - 1].transform.rotation,
                 Anchor_Empty.transform);


            End_Anchor.transform.localScale = new Vector3(Anchor_Size.x, PLank_List[PLank_List.Count - 1].transform.localScale.y + Anchor_Height / 2, Anchor_Size.y);

            if (Current_Side == 1)
            {
                Anchor_Left.Add(Start_Anchor);
                Anchor_Left.Add(End_Anchor);
                Start_Anchor.name = ("Anchor " + Anchor_Prefabe.name + " Left ").ToString();
                End_Anchor.name = ("Anchor " + Anchor_Prefabe.name + " Left ").ToString();


            }
            else if (Current_Side == -1)
            {
                Anchor_Right.Add(Start_Anchor);
                Anchor_Right.Add(End_Anchor);
                Start_Anchor.name = ("Anchor " + Anchor_Prefabe.name + " Right ").ToString();
                End_Anchor.name = ("Anchor " + Anchor_Prefabe.name + " Right ").ToString();


            }

        }

        //Rope Joint

        Rope_Empty = new GameObject("Rope");

        Rope_Left_Empty = new GameObject("Rope_Left");
        Rope_Right_Empty = new GameObject("Rope_Right");

        Rope_Empty.transform.parent = this.transform;
        Rope_Left_Empty.transform.parent = Rope_Empty.transform;
        Rope_Right_Empty.transform.parent = Rope_Empty.transform;

        Rope_Empty.transform.localPosition = Vector3.zero;
        Rope_Left_Empty.transform.localPosition = Vector3.zero;
        Rope_Right_Empty.transform.localPosition = Vector3.zero;



        Rope_Joint_Empty = new GameObject("Rope_Joint");

        Rope_Joint_Left_Empty = new GameObject("Rope_Joint_Left");
        Rope_Joint_Right_Empty = new GameObject("Rope_Joint_Right");

        Rope_Joint_Empty.transform.parent = this.transform;
        Rope_Joint_Left_Empty.transform.parent = Rope_Joint_Empty.transform;
        Rope_Joint_Right_Empty.transform.parent = Rope_Joint_Empty.transform;

        Rope_Joint_Empty.transform.localPosition = Vector3.zero;
        Rope_Joint_Left_Empty.transform.localPosition = Vector3.zero;
        Rope_Joint_Right_Empty.transform.localPosition = Vector3.zero;


        foreach (var Current_Side in Sides)
        {
            /*
            float Rope_Segments_Scale = (Plank_Distance * Bridge_Lenght) / Rope_Segments;
            Debug.Log("Rope_Segments_Scale " + Rope_Segments_Scale);
            float Actual_Size = Rope_Segments_Scale - (Rope_Joint_Prefabe.transform.localScale.z / 1.4f * (Bridge_Lenght - 1));
            Debug.Log("Actual size " + Actual_Size);
            */
            float Lenght = 0;

            if (Current_Side == 1)
            {
                Lenght = Vector3.Distance(Anchor_Left[0].transform.position, Anchor_Left[1].transform.position);

            }
            else if (Current_Side == -1)
            {
                Lenght = Vector3.Distance(Anchor_Right[0].transform.position, Anchor_Right[1].transform.position);

            }
            float Not_Size = Lenght / Rope_Segments;
            float Temp = Lenght - Rope_Joint_Prefabe.transform.localScale.z * (Rope_Segments - 1);
            float Rope_Segments_Scale = Temp / Rope_Segments;
            // Debug.Log("Rope_Segments_Scale " + Rope_Segments_Scale);
            float Actual_Size = Rope_Segments_Scale - (Rope_Joint_Prefabe.transform.localScale.z / 1.4f * (Bridge_Lenght - 1));
            // Debug.Log("Actual size " + Actual_Size);


            //Creat Rope 

            float z = Not_Size / 2;
            for (int i = 0; i < Rope_Segments; i++)
            {


                GameObject Temp_Rope = Instantiate(Joint_Prefabe, this.transform);

                Temp_Rope.transform.localPosition = new Vector3(
                   Current_Side * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2,
                    Anchor_Height / 2,
                    z + Plank_Offset);


                Temp_Rope.transform.localScale = new Vector3(
                  Joint_Size.x,
                 (Not_Size / 2) - Joint_Prefabe.transform.localScale.z / 4,
                  Joint_Size.y);

                z += Not_Size;




                if (Current_Side == 1)
                {
                    Rope_Left_List.Add(Temp_Rope);
                    Temp_Rope.name = ("Rope Left : " + i).ToString();
                    Temp_Rope.transform.parent = Rope_Left_Empty.transform;

                }
                else if (Current_Side == -1)
                {

                    Rope_Right_List.Add(Temp_Rope);
                    Temp_Rope.name = ("Rope Right : " + i).ToString();
                    Temp_Rope.transform.parent = Rope_Right_Empty.transform;

                }

            }


            //Creat Rope_Joint_Prefabe
            z = Not_Size;
            for (int i = 0; i < Rope_Segments; i++)
            {
                if (i == Rope_Segments - 1)
                {

                }
                else
                {
                    GameObject Temp_Rope_Joint = Instantiate(Rope_Joint_Prefabe, this.transform);

                    Temp_Rope_Joint.transform.localPosition = new Vector3(
                       Current_Side * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2,
                        Anchor_Height / 2,
                        z + Plank_Offset);



                    z += Not_Size;




                    if (Current_Side == 1)
                    {
                        Rope_Joint_Left_List.Add(Temp_Rope_Joint);
                        Temp_Rope_Joint.name = ("Rope Joint Left : " + i).ToString();
                        Temp_Rope_Joint.transform.parent = Rope_Joint_Left_Empty.transform;

                    }
                    else if (Current_Side == -1)
                    {

                        Rope_Joint_Right_List.Add(Temp_Rope_Joint);
                        Temp_Rope_Joint.name = ("Rope Joint Right : " + i).ToString();
                        Temp_Rope_Joint.transform.parent = Rope_Joint_Right_Empty.transform;

                    }
                }



            }




        }
    }
    void Update_Plank()
    {


        //Update PLank
        if (PLank_List.Count < Bridge_Lenght)
        {
            float z = 0;

            foreach (var item in PLank_List)
            {

                z += Plank_Distance;

            }

            for (int i = PLank_List.Count; i < Bridge_Lenght; i++)
            {
                GameObject Temp_Plank = Instantiate(Plank_Prefabe, this.transform);
                Temp_Plank.transform.localPosition = new Vector3(0, 0, z + Plank_Offset);
                Temp_Plank.transform.localScale = PLank_Size;

                PLank_List.Add(Temp_Plank);
                z += Plank_Distance;
            }

        }
        else if (PLank_List.Count == Bridge_Lenght)
        {

            float z = 0;
            foreach (var Current_Plank in PLank_List)
            {
                if (Current_Plank != null)
                {
                    Current_Plank.transform.localPosition = new Vector3(0, 0, z + Plank_Offset);
                    Current_Plank.transform.localScale = new Vector3(PLank_Size.x, 1, 1);

                }
                z += Plank_Distance;
            }

        }


    }
    void Update_Joint()
    {
        foreach (var Curr in Sides)
        {
            if (Curr == 1)
            {
                //Update Joint
                if (Bridge_Joint_Left_List.Count < Bridge_Lenght - 1)
                {
                    float z = Plank_Distance / 2;

                    foreach (var item in Bridge_Joint_Left_List)
                    {

                        z += Plank_Distance;

                    }

                    for (int i = Bridge_Joint_Left_List.Count; i < Bridge_Lenght; i++)
                    {
                        GameObject Temp_Joint = Instantiate(Joint_Prefabe, this.transform);
                        Temp_Joint.transform.localPosition = new Vector3(
                                        Curr * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2,
                                         0,
                                         z + Plank_Offset); Bridge_Joint_Left_List.Add(Temp_Joint);
                        z += Plank_Distance;
                    }

                }
                else if (Bridge_Joint_Left_List.Count == Bridge_Lenght - 1)
                {

                    float z = Plank_Distance / 2;
                    foreach (var Current_Joint in Bridge_Joint_Left_List)
                    {
                        if (Current_Joint != null)
                        {
                            Current_Joint.transform.localPosition = new Vector3(
                                            Curr * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2,
                                             0,
                                             z + Plank_Offset);
                        }
                        z += Plank_Distance;
                    }

                }



            }
            else if (Curr == -1)
            {
                //Update Joint
                if (Bridge_Joint_Right_List.Count < Bridge_Lenght - 1)
                {
                    float z = Plank_Distance / 2;

                    foreach (var item in Bridge_Joint_Right_List)
                    {

                        z += Plank_Distance;

                    }

                    for (int i = Bridge_Joint_Right_List.Count; i < Bridge_Lenght; i++)
                    {
                        GameObject Temp_Joint = Instantiate(Joint_Prefabe, this.transform);
                        Temp_Joint.transform.localPosition = new Vector3(
                                                                Curr * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2,
                                                                 0,
                                                                 z + Plank_Offset); Bridge_Joint_Left_List.Add(Temp_Joint); Bridge_Joint_Right_List.Add(Temp_Joint);
                        z += Plank_Distance;
                    }

                }
                else if (Bridge_Joint_Right_List.Count == Bridge_Lenght - 1)
                {

                    float z = Plank_Distance / 2;
                    foreach (var Current_Joint in Bridge_Joint_Right_List)
                    {
                        if (Current_Joint != null)
                        {
                            Current_Joint.transform.localPosition = new Vector3(
                                                                        Curr * (PLank_Size.x - Joint_Prefabe.transform.localScale.x) / 2,
                                                                         0,
                                                                         z + Plank_Offset);
                        }
                        z += Plank_Distance;
                    }

                }



            }



        }



    }
    void Destroy_Plank()
    {
        //Destroy Plank

        Use_Delete(PLank_List);

        PLank_List.Clear();

    }
    void Destroy_Joint()
    {

        Bridge_Joint_Left_List.Clear();
        Use_Delete(Bridge_Joint_Right_List);
        Use_Delete(Bridge_Joint_Left_List);
        Bridge_Joint_Right_List.Clear();
    }
    void Destroy_Rope()
    {
        //Destroy Rope Anchors Anchor_Right Anchor_Left

        // destroy Anchor
        //Destroy Plank
        Use_Delete(Anchor_Right);

        Anchor_Right.Clear();


        Use_Delete(Anchor_Left);

        Anchor_Left.Clear();


        // destroy Rope Joints
        Use_Delete(Rope_Joint_Left_List);

        Rope_Joint_Left_List.Clear();

        // destroy Rope Joints
        Use_Delete(Rope_Joint_Right_List);

        Rope_Joint_Right_List.Clear();


        // destroy Rope Right
        Use_Delete(Rope_Right_List);

        Rope_Right_List.Clear();

        // destroy Rope Left
        Use_Delete(Rope_Left_List);
        Rope_Left_List.Clear();

    }
    void Destroy_Vertical_Rope()
    {

        Use_Delete(Rope_Vertical_Joint_Left);
        Use_Delete(Rope_Vertical_Joint_Right);

        Rope_Vertical_Joint_Right.Clear();
        Rope_Vertical_Joint_Left.Clear();


    }
    void Destroy_Dependencies()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {

            DestroyImmediate(Plank_Empty);
            DestroyImmediate(Bridge_Joint_Empty);
            DestroyImmediate(Bridge_Joint_Left_Empty);
            DestroyImmediate(Bridge_Joint_Right_Empty);
            DestroyImmediate(Rope_Empty);
            DestroyImmediate(Rope_Right_Empty);
            DestroyImmediate(Rope_Left_Empty);
            DestroyImmediate(Rope_Joint_Empty);
            DestroyImmediate(Rope_Joint_Right_Empty);
            DestroyImmediate(Rope_Joint_Left_Empty);
            DestroyImmediate(Anchor_Empty);

            _Update = false;
            _Destroy = false;
            _Clear = false;

            PLank_List.Clear();
            Bridge_Joint_Left_List.Clear();
            Bridge_Joint_Right_List.Clear();
            Rope_Joint_Left_List.Clear();
            Rope_Joint_Right_List.Clear();
            Rope_Left_List.Clear();
            Rope_Right_List.Clear();
            Anchor_Right.Clear();
            Anchor_Left.Clear();
            Rope_Vertical_Joint_Right.Clear();
            Rope_Vertical_Joint_Left.Clear();

        };

#endif



    }
    void Clear_All()
    {
        Transform[] All_Childrens = this.GetComponentsInChildren<Transform>();

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

        _Update = false;
        _Destroy = false;
        _Clear = false;

        PLank_List.Clear();
        Bridge_Joint_Left_List.Clear();
        Bridge_Joint_Right_List.Clear();
        Rope_Joint_Left_List.Clear();
        Rope_Joint_Right_List.Clear();
        Rope_Left_List.Clear();
        Rope_Right_List.Clear();
        Anchor_Right.Clear();
        Anchor_Left.Clear();
        Rope_Vertical_Joint_Right.Clear();
        Rope_Vertical_Joint_Left.Clear();

    }
    void Handle_Bridge_Joint()
    {
        if (_Is_Kinematic)
        {

            PLank_List[0].GetComponent<Rigidbody>().isKinematic = true;
            PLank_List[Bridge_Lenght - 1].GetComponent<Rigidbody>().isKinematic = true; ;

        }


        int i = 0;
        foreach (var Current_PLank in PLank_List)
        {
            if (i == PLank_List.Count - 1)
            {

            }
            else
            {
                List<HingeJoint> A_Hinge_Joint_List = new List<HingeJoint>();



                HingeJoint Left_HingeJoint1 = Bridge_Joint_Left_List[i].AddComponent<HingeJoint>();
                HingeJoint Left_HingeJoint2 = Bridge_Joint_Left_List[i].AddComponent<HingeJoint>();

                Left_HingeJoint1.connectedBody = Current_PLank.GetComponent<Rigidbody>();

                Left_HingeJoint2.connectedBody = PLank_List[i + 1].GetComponent<Rigidbody>();

                HingeJoint Right_HingeJoint1 = Bridge_Joint_Right_List[i].AddComponent<HingeJoint>();
                HingeJoint Right_HingeJoint2 = Bridge_Joint_Right_List[i].AddComponent<HingeJoint>();

                Right_HingeJoint1.connectedBody = Current_PLank.GetComponent<Rigidbody>();

                Right_HingeJoint2.connectedBody = PLank_List[i + 1].GetComponent<Rigidbody>();


                A_Hinge_Joint_List.Add(Right_HingeJoint1);
                A_Hinge_Joint_List.Add(Right_HingeJoint2);
                A_Hinge_Joint_List.Add(Left_HingeJoint1);
                A_Hinge_Joint_List.Add(Left_HingeJoint2);

                Apply_Joint_Data(A_Hinge_Joint_List);
                A_Hinge_Joint_List.Clear();
            }

            i++;
        }

    }
    void Handle_Rope_Joint()
    {
        if (_Is_Kinematic)
        {
            Rope_Left_List[0].GetComponent<Rigidbody>().isKinematic = true;
            Rope_Left_List[Rope_Segments - 1].GetComponent<Rigidbody>().isKinematic = true;
            Rope_Right_List[0].GetComponent<Rigidbody>().isKinematic = true;
            Rope_Right_List[Rope_Segments - 1].GetComponent<Rigidbody>().isKinematic = true;
        }


        int i = 0;

        foreach (var Current_Side in Sides)
        {



            foreach (var Current_PLank in Rope_Left_List)
            {
                if (i == Rope_Left_List.Count - 1)
                {

                }
                else if (i < Rope_Left_List.Count)
                {
                    List<HingeJoint> A_Hinge_Joint_List = new List<HingeJoint>();



                    HingeJoint Left_HingeJoint1 = Rope_Joint_Left_List[i].AddComponent<HingeJoint>();
                    HingeJoint Left_HingeJoint2 = Rope_Joint_Left_List[i].AddComponent<HingeJoint>();

                    Left_HingeJoint1.connectedBody = Rope_Left_List[i].GetComponent<Rigidbody>();
                    Left_HingeJoint2.connectedBody = Rope_Left_List[i + 1].GetComponent<Rigidbody>();

                    HingeJoint Right_HingeJoint1 = Rope_Joint_Right_List[i].AddComponent<HingeJoint>();
                    HingeJoint Right_HingeJoint2 = Rope_Joint_Right_List[i].AddComponent<HingeJoint>();

                    Right_HingeJoint1.connectedBody = Rope_Right_List[i].GetComponent<Rigidbody>();
                    Right_HingeJoint2.connectedBody = Rope_Right_List[i + 1].GetComponent<Rigidbody>();


                    A_Hinge_Joint_List.Add(Right_HingeJoint1);
                    A_Hinge_Joint_List.Add(Right_HingeJoint2);
                    A_Hinge_Joint_List.Add(Left_HingeJoint1);
                    A_Hinge_Joint_List.Add(Left_HingeJoint2);

                    Apply_Joint_Data(A_Hinge_Joint_List);
                    A_Hinge_Joint_List.Clear();
                }

                i++;
            }




        }
    }
    void Apply_Joint_Data(List<HingeJoint> List_Hinge_Joint)
    {
        foreach (var Temp_Hinge_Joint in List_Hinge_Joint)
        {


            Temp_Hinge_Joint.autoConfigureConnectedAnchor = true;
            JointLimits limits = Temp_Hinge_Joint.limits;
            limits.min = Limite_Min;
            limits.max = Limite_Max;
            limits.bounciness = bounciness;
            limits.bounceMinVelocity = bounceMinVelocity;
            Temp_Hinge_Joint.limits = limits;
            Temp_Hinge_Joint.useLimits = true;
            Temp_Hinge_Joint.breakForce = breakForce;
            Temp_Hinge_Joint.breakTorque = breakTorque;
            Temp_Hinge_Joint.massScale = Joint_Mass;


        }
    }
    void Connect_Point()
    {


        if (_Start_From_The_Top)
        {

            for (int i = 0; i < Bridge_Joint_Left_List.Count; i++)
            {
                if (i == 0)
                {
                    Debug.DrawLine(Bridge_Joint_Left_List[0].transform.position, Rope_Joint_Left_List[0].transform.position, Color.red, Gizmos_Life_Time);
                    Handle_Vertical_Rope(Bridge_Joint_Left_List[0].transform, Rope_Joint_Left_List[0].transform, false);


                    Debug.DrawLine(Rope_Joint_Left_List[0].transform.position, Bridge_Joint_Left_List[1].transform.position, Color.red, Gizmos_Life_Time);
                    Handle_Vertical_Rope(Rope_Joint_Left_List[0].transform, Bridge_Joint_Left_List[1].transform, false);

                }
                else if (i == Bridge_Joint_Left_List.Count - 1)
                {
                    Debug.DrawLine(Rope_Joint_Left_List[Rope_Joint_Left_List.Count - 1].transform.position, Bridge_Joint_Left_List[Bridge_Joint_Left_List.Count - 1].transform.position, Color.red, Gizmos_Life_Time);
                    Handle_Vertical_Rope(Rope_Joint_Left_List[Rope_Joint_Left_List.Count - 1].transform, Bridge_Joint_Left_List[Bridge_Joint_Left_List.Count - 1].transform, false);

                    for (int y = 0; y < Bridge_Joint_Left_List.Count; y++)
                    {


                        if (Bridge_Joint_Left_List[y].transform.position.z >= Rope_Joint_Left_List[Rope_Joint_Left_List.Count - 1].transform.position.z)
                        {
                            Debug.DrawLine(Rope_Joint_Left_List[Rope_Joint_Left_List.Count - 1].transform.position, Bridge_Joint_Left_List[y].transform.position, Color.red, Gizmos_Life_Time);
                            Handle_Vertical_Rope(Rope_Joint_Left_List[Rope_Joint_Left_List.Count - 1].transform, Bridge_Joint_Left_List[y].transform, false);

                        }
                    }



                }
                else
                {

                    for (int z = 0; z < Rope_Joint_Left_List.Count; z++)
                    {
                        if (Bridge_Joint_Left_List[i].transform.position.z < Rope_Joint_Left_List[z].transform.position.z)
                        {
                            Debug.DrawLine(Bridge_Joint_Left_List[i].transform.position, Rope_Joint_Left_List[z].transform.position, Color.red, Gizmos_Life_Time);
                            Handle_Vertical_Rope(Bridge_Joint_Left_List[i].transform, Rope_Joint_Left_List[z].transform, false);

                            if (i + 1 < Bridge_Joint_Left_List.Count - 1)
                            {
                                Debug.DrawLine(Rope_Joint_Left_List[z].transform.position, Bridge_Joint_Left_List[i + 1].transform.position, Color.red, Gizmos_Life_Time);
                                Handle_Vertical_Rope(Rope_Joint_Left_List[z].transform, Bridge_Joint_Left_List[i + 1].transform, false);

                            }

                            break;
                        }
                        else
                        {
                            if (Rope_Joint_Left_List[z].transform.position.z > Bridge_Joint_Left_List[i - 1].transform.position.z)
                            {
                                Debug.DrawLine(Bridge_Joint_Left_List[i].transform.position, Rope_Joint_Left_List[z].transform.position, Color.red, Gizmos_Life_Time);
                                Handle_Vertical_Rope(Bridge_Joint_Left_List[i].transform, Rope_Joint_Left_List[z].transform, false);

                            }
                        }
                    }

                }


            }
        }
        else
        {
            for (int i = 0; i < Rope_Joint_Left_List.Count; i++)
            {
                if (i == 0)
                {
                    Debug.DrawLine(Rope_Joint_Left_List[0].transform.position, Bridge_Joint_Left_List[0].transform.position, Color.red, Gizmos_Life_Time);
                    Debug.DrawLine(Bridge_Joint_Left_List[0].transform.position, Rope_Joint_Left_List[1].transform.position, Color.red, Gizmos_Life_Time);

                }
                else if (i == Rope_Joint_Left_List.Count - 1)
                {
                    Debug.DrawLine(Bridge_Joint_Left_List[Bridge_Joint_Left_List.Count - 1].transform.position, Rope_Joint_Left_List[Rope_Joint_Left_List.Count - 1].transform.position, Color.red, Gizmos_Life_Time);


                    for (int y = 0; y < Rope_Joint_Left_List.Count; y++)
                    {


                        if (Rope_Joint_Left_List[y].transform.position.z >= Bridge_Joint_Left_List[Bridge_Joint_Left_List.Count - 1].transform.position.z)
                        {
                            Debug.DrawLine(Bridge_Joint_Left_List[Bridge_Joint_Left_List.Count - 1].transform.position, Rope_Joint_Left_List[y].transform.position, Color.red, Gizmos_Life_Time);

                        }
                    }

                }
                else
                {

                    for (int z = 0; z < Bridge_Joint_Left_List.Count; z++)
                    {
                        if (Rope_Joint_Left_List[i].transform.position.z < Bridge_Joint_Left_List[z].transform.position.z)
                        {
                            Debug.DrawLine(Rope_Joint_Left_List[i].transform.position, Bridge_Joint_Left_List[z].transform.position, Color.red, Gizmos_Life_Time);
                            if (i + 1 < Rope_Joint_Left_List.Count - 1)
                            {
                                Debug.DrawLine(Bridge_Joint_Left_List[z].transform.position, Rope_Joint_Left_List[i + 1].transform.position, Color.red, Gizmos_Life_Time);

                            }

                            break;
                        }
                        else
                        {
                            if (Bridge_Joint_Left_List[z].transform.position.z > Rope_Joint_Left_List[i - 1].transform.position.z)
                            {
                                Debug.DrawLine(Rope_Joint_Left_List[i].transform.position, Bridge_Joint_Left_List[z].transform.position, Color.red, Gizmos_Life_Time);

                            }
                        }
                    }

                }


            }
        }



        if (_Start_From_The_Top)
        {

            for (int i = 0; i < Bridge_Joint_Right_List.Count; i++)
            {
                if (i == 0)
                {
                    Debug.DrawLine(Bridge_Joint_Right_List[0].transform.position, Rope_Joint_Right_List[0].transform.position, Color.red, Gizmos_Life_Time);
                    Handle_Vertical_Rope(Bridge_Joint_Right_List[0].transform, Rope_Joint_Right_List[0].transform, true);


                    Debug.DrawLine(Rope_Joint_Right_List[0].transform.position, Bridge_Joint_Right_List[1].transform.position, Color.red, Gizmos_Life_Time);
                    Handle_Vertical_Rope(Rope_Joint_Right_List[0].transform, Bridge_Joint_Right_List[1].transform, true);

                }
                else if (i == Bridge_Joint_Right_List.Count - 1)
                {
                    Debug.DrawLine(Rope_Joint_Right_List[Rope_Joint_Right_List.Count - 1].transform.position, Bridge_Joint_Right_List[Bridge_Joint_Right_List.Count - 1].transform.position, Color.red, Gizmos_Life_Time);
                    Handle_Vertical_Rope(Rope_Joint_Right_List[Rope_Joint_Right_List.Count - 1].transform, Bridge_Joint_Right_List[Bridge_Joint_Right_List.Count - 1].transform, true);

                    for (int y = 0; y < Bridge_Joint_Right_List.Count; y++)
                    {


                        if (Bridge_Joint_Right_List[y].transform.position.z >= Rope_Joint_Right_List[Rope_Joint_Right_List.Count - 1].transform.position.z)
                        {
                            Debug.DrawLine(Rope_Joint_Right_List[Rope_Joint_Right_List.Count - 1].transform.position, Bridge_Joint_Right_List[y].transform.position, Color.red, Gizmos_Life_Time);
                            Handle_Vertical_Rope(Rope_Joint_Right_List[Rope_Joint_Right_List.Count - 1].transform, Bridge_Joint_Right_List[y].transform, true);

                        }
                    }



                }
                else
                {

                    for (int z = 0; z < Rope_Joint_Right_List.Count; z++)
                    {
                        if (Bridge_Joint_Right_List[i].transform.position.z < Rope_Joint_Right_List[z].transform.position.z)
                        {
                            Debug.DrawLine(Bridge_Joint_Right_List[i].transform.position, Rope_Joint_Right_List[z].transform.position, Color.red, Gizmos_Life_Time);
                            Handle_Vertical_Rope(Bridge_Joint_Right_List[i].transform, Rope_Joint_Right_List[z].transform, true);

                            if (i + 1 < Bridge_Joint_Right_List.Count - 1)
                            {
                                Debug.DrawLine(Rope_Joint_Right_List[z].transform.position, Bridge_Joint_Right_List[i + 1].transform.position, Color.red, Gizmos_Life_Time);
                                Handle_Vertical_Rope(Rope_Joint_Right_List[z].transform, Bridge_Joint_Right_List[i + 1].transform, true);

                            }

                            break;
                        }
                        else
                        {
                            if (Rope_Joint_Right_List[z].transform.position.z > Bridge_Joint_Right_List[i - 1].transform.position.z)
                            {
                                Debug.DrawLine(Bridge_Joint_Right_List[i].transform.position, Rope_Joint_Right_List[z].transform.position, Color.red, Gizmos_Life_Time);
                                Handle_Vertical_Rope(Bridge_Joint_Right_List[i].transform, Rope_Joint_Right_List[z].transform, true);

                            }
                        }
                    }

                }


            }
        }
        else
        {
            for (int i = 0; i < Rope_Joint_Right_List.Count; i++)
            {
                if (i == 0)
                {
                    Debug.DrawLine(Rope_Joint_Right_List[0].transform.position, Bridge_Joint_Right_List[0].transform.position, Color.red, Gizmos_Life_Time);
                    Debug.DrawLine(Bridge_Joint_Right_List[0].transform.position, Rope_Joint_Right_List[1].transform.position, Color.red, Gizmos_Life_Time);

                }
                else if (i == Rope_Joint_Right_List.Count - 1)
                {
                    Debug.DrawLine(Bridge_Joint_Right_List[Bridge_Joint_Right_List.Count - 1].transform.position, Rope_Joint_Right_List[Rope_Joint_Right_List.Count - 1].transform.position, Color.red, Gizmos_Life_Time);


                    for (int y = 0; y < Rope_Joint_Right_List.Count; y++)
                    {


                        if (Rope_Joint_Right_List[y].transform.position.z >= Bridge_Joint_Right_List[Bridge_Joint_Right_List.Count - 1].transform.position.z)
                        {
                            Debug.DrawLine(Bridge_Joint_Right_List[Bridge_Joint_Right_List.Count - 1].transform.position, Rope_Joint_Right_List[y].transform.position, Color.red, Gizmos_Life_Time);

                        }
                    }

                }
                else
                {

                    for (int z = 0; z < Bridge_Joint_Right_List.Count; z++)
                    {
                        if (Rope_Joint_Right_List[i].transform.position.z < Bridge_Joint_Right_List[z].transform.position.z)
                        {
                            Debug.DrawLine(Rope_Joint_Right_List[i].transform.position, Bridge_Joint_Right_List[z].transform.position, Color.red, Gizmos_Life_Time);
                            if (i + 1 < Rope_Joint_Right_List.Count - 1)
                            {
                                Debug.DrawLine(Bridge_Joint_Right_List[z].transform.position, Rope_Joint_Right_List[i + 1].transform.position, Color.red, Gizmos_Life_Time);

                            }

                            break;
                        }
                        else
                        {
                            if (Bridge_Joint_Right_List[z].transform.position.z > Rope_Joint_Right_List[i - 1].transform.position.z)
                            {
                                Debug.DrawLine(Rope_Joint_Right_List[i].transform.position, Bridge_Joint_Right_List[z].transform.position, Color.red, Gizmos_Life_Time);

                            }
                        }
                    }

                }


            }
        }



    }
    void Handle_Vertical_Rope(Transform Point_1, Transform Point_2, bool Is_Right)
    {

        GameObject Current_Joint = Creat_Vertical_Rope_Joint(Point_1, Point_2, Is_Right);


        List<HingeJoint> A_Hinge_Joint_List = new List<HingeJoint>();


        HingeJoint Left_HingeJoint1 = Current_Joint.AddComponent<HingeJoint>();
        HingeJoint Left_HingeJoint2 = Current_Joint.AddComponent<HingeJoint>();

        Left_HingeJoint1.connectedBody = Point_1.GetComponent<Rigidbody>();
        Left_HingeJoint2.connectedBody = Point_2.GetComponent<Rigidbody>();

        A_Hinge_Joint_List.Add(Left_HingeJoint1);
        A_Hinge_Joint_List.Add(Left_HingeJoint2);

        /*
        HingeJoint Right_HingeJoint1 = Rope_Joint_Right_List[i].AddComponent<HingeJoint>();
        HingeJoint Right_HingeJoint2 = Rope_Joint_Right_List[i].AddComponent<HingeJoint>();

        Right_HingeJoint1.connectedBody = Rope_Right_List[i].GetComponent<Rigidbody>();
        Right_HingeJoint2.connectedBody = Rope_Right_List[i + 1].GetComponent<Rigidbody>();
        */


        Apply_Joint_Data(A_Hinge_Joint_List);
        A_Hinge_Joint_List.Clear();

    }
    GameObject Creat_Vertical_Rope_Joint(Transform Point_1, Transform Point_2, bool Is_Right)
    {
        GameObject Temp_Vertical_Rope_Joint = Instantiate(Joint_Prefabe, this.transform);

        Temp_Vertical_Rope_Joint.transform.localRotation = Quaternion.LookRotation(Point_2.localPosition - Point_1.localPosition);

        float Temp_Scale = Vector3.Distance(Point_1.transform.position, Point_2.transform.position) / 2;

        Temp_Vertical_Rope_Joint.transform.Rotate(90, 0, 0);

        Temp_Vertical_Rope_Joint.transform.localPosition = (Point_1.localPosition + Point_2.localPosition) / 2f;

        Temp_Vertical_Rope_Joint.transform.localScale = new Vector3(Vertical_Joint_Size.x, Temp_Scale, Vertical_Joint_Size.y);

        if (Is_Right)
        {
            Rope_Vertical_Joint_Right.Add(Temp_Vertical_Rope_Joint);

        }
        else
        {
            Rope_Vertical_Joint_Left.Add(Temp_Vertical_Rope_Joint);

        }

        return Temp_Vertical_Rope_Joint;
    }



    void Use_Delete(List<GameObject> A_List)
    {
#if UNITY_EDITOR
        foreach (var Object in A_List)
        {


            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (Object != null)
                {
                    DestroyImmediate(Object.gameObject);

                }
            };


        }
#endif

        if (Application.isPlaying)
        {
            foreach (var Object in A_List)
            {


                if (Object != null)
                {
                    DestroyImmediate(Object.gameObject);

                }



            }
        }
    }
}


