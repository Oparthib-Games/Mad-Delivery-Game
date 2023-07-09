using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class HumanFakeRig : MonoBehaviour
{

    [System.Serializable]
    public class HumanFakeRigNames
    {
        public string ROOT = "----- ROOT -----";
        public string HIP = "----- HIP -----";
        public string SPINE = "----- SPINE -----";
        public string CHEST = "----- CHEST -----";
        public string NECK = "----- NECK -----";
        public string HEAD = "----- HEAD -----";

        public string L_THIGH = "----- L THIGH -----";
        public string R_THIGH = "----- R THIGH -----";
        public string L_LEG = "----- L LEG -----";
        public string R_LEG = "----- R LEG -----";
        public string L_FEET = "----- L FEET -----";
        public string R_FEET = "----- R FEET -----";
        public string L_TOE = "----- L TOE -----";
        public string R_TOE = "----- R TOE -----";

        public string L_SHOULDER = "----- L SHOULDER -----";
        public string R_SHOULDER = "----- R SHOULDER -----";
        public string L_ARM = "----- L ARM -----";
        public string R_ARM = "----- R ARM -----";
        public string L_FOREARM = "----- L FOREARM -----";
        public string R_FOREARM = "----- R FOREARM -----";
        public string L_HAND = "----- L HAND -----";
        public string R_HAND = "----- R HAND -----";

        public string IK_L_FEET = "IK L FEET";
        public string IK_R_FEET = "IK R FEET";
    }
    [Header("NAMES")]
    [SerializeField] public HumanFakeRigNames humanFakeRigNames;

    [System.Serializable]
    public class L_FingerCtrl
    {
        //[Range(15, -90)] public float LV_1_Finger_Rotation = 0;
        public Transform[] LV1_L_Fingers = new Transform[5];
        public Transform[] LV2_L_Fingers = new Transform[5];
        public Transform[] LV3_L_Fingers = new Transform[5];
    }
    [Header("Finger Ctrl")]
    [SerializeField] public L_FingerCtrl l_fingerCtrl;
    
    [System.Serializable]
    public class R_FingerCtrl
    {
        //[Range(90, -15)] public float LV_1_Finger_Rotation = 0;
        public Transform[] LV1_R_Fingers = new Transform[5];
        public Transform[] LV2_R_Fingers = new Transform[5];
        public Transform[] LV3_R_Fingers = new Transform[5];
    }
    [SerializeField] public R_FingerCtrl r_fingerCtrl;

    
    public enum RigStatusOptions
    {
        NOT_INITIALIZED,
        HUMANOID_RIG,
        HAND_RIG,
    }
    [HideInInspector]
    public RigStatusOptions rigStatus = RigStatusOptions.NOT_INITIALIZED;

    public void Init()
    {
        for (int i = 0; i < 2; i++)
        {
            char LorR = i == 0 ? 'L' : 'R'; // Left or Right
            for (int i1 = 0; i1 < 5; i1++)
            {
                for (int k = 0; k < 3; k++)
                {
                    string name = $"----- {LorR} FINGER {i1 + 1}.{k + 1} -----";
                    GameObject foundGO = Helper.FindInChildren(this.gameObject, name);
                    if (foundGO == null)
                        Debug.LogError($"{name} NOT FOUND!!");
                    else
                    {
                        if (LorR == 'L')
                        {
                            switch (k)
                            {
                                case 0:
                                    l_fingerCtrl.LV1_L_Fingers[i1] = foundGO.transform;
                                    break;
                                case 1:
                                    l_fingerCtrl.LV2_L_Fingers[i1] = foundGO.transform;
                                    break;
                                case 2:
                                    l_fingerCtrl.LV3_L_Fingers[i1] = foundGO.transform;
                                    break;
                                default:
                                    Debug.LogWarning("Why Default???");
                                    break;
                            }
                        }
                        else
                        {
                            switch (k)
                            {
                                case 0:
                                    r_fingerCtrl.LV1_R_Fingers[i1] = foundGO.transform;
                                    break;
                                case 1:
                                    r_fingerCtrl.LV2_R_Fingers[i1] = foundGO.transform;
                                    break;
                                case 2:
                                    r_fingerCtrl.LV3_R_Fingers[i1] = foundGO.transform;
                                    break;
                                default:
                                    Debug.LogWarning("Why Default???");
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void Update()
    {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying) return;
#endif
        if (rigStatus == RigStatusOptions.NOT_INITIALIZED) return;

        //updateFingers(l_fingerCtrl.LV1_L_Fingers, l_fingerCtrl.LV_1_Finger_Rotation);
        //updateFingers(l_fingerCtrl.LV2_L_Fingers, l_fingerCtrl.LV_1_Finger_Rotation);
        //updateFingers(l_fingerCtrl.LV3_L_Fingers, l_fingerCtrl.LV_1_Finger_Rotation);

        //updateFingers(r_fingerCtrl.LV1_R_Fingers, r_fingerCtrl.LV_1_Finger_Rotation);
    }

    void updateFingers(Transform[] fingers, float value)
    {
        foreach (var finger in fingers)
        {
            //finger.rotation = Quaternion.Euler(finger.rotation.x, finger.rotation.y, value);
            Vector3 currentEulerAngles = new Vector3(finger.rotation.x, finger.rotation.y, value);
            finger.eulerAngles = currentEulerAngles;
        }
    }

    public void Select(string name)
    {
        Debug.Log(name);
        GameObject foundGO = Helper.FindInChildren(this.gameObject, name);

        if (foundGO == null)
            Debug.LogError($"{name} NOT FOUND!!");
        else
        {
#if UNITY_EDITOR
            Selection.activeGameObject = foundGO;
#endif
        }
    }

    [ContextMenu("RESET A POSE")]
    public void ResetAPose()
    {
        ResetRigRotation(humanFakeRigNames.ROOT);
        ResetRigRotation(humanFakeRigNames.HIP);
        ResetRigRotation(humanFakeRigNames.SPINE);
        ResetRigRotation(humanFakeRigNames.CHEST);
        ResetRigRotation(humanFakeRigNames.NECK);
        ResetRigRotation(humanFakeRigNames.HEAD);
        ResetRigRotation(humanFakeRigNames.L_THIGH);
        ResetRigRotation(humanFakeRigNames.R_THIGH);
        ResetRigRotation(humanFakeRigNames.L_LEG);
        ResetRigRotation(humanFakeRigNames.R_LEG);
        ResetRigRotation(humanFakeRigNames.L_FEET);
        ResetRigRotation(humanFakeRigNames.R_FEET);
        ResetRigRotation(humanFakeRigNames.L_TOE);
        ResetRigRotation(humanFakeRigNames.R_TOE);
        ResetRigRotation(humanFakeRigNames.L_SHOULDER);
        ResetRigRotation(humanFakeRigNames.R_SHOULDER);
        ResetRigRotation(humanFakeRigNames.L_ARM);
        ResetRigRotation(humanFakeRigNames.R_ARM);
        ResetRigRotation(humanFakeRigNames.L_FOREARM);
        ResetRigRotation(humanFakeRigNames.R_FOREARM);
        ResetRigRotation(humanFakeRigNames.L_HAND);
        ResetRigRotation(humanFakeRigNames.R_HAND);
        ResetRigRotation(humanFakeRigNames.IK_L_FEET);
        ResetRigRotation(humanFakeRigNames.IK_R_FEET);
    }

    public void ResetRigRotation(string name)
    {
        Debug.Log(name);
        GameObject foundGO = Helper.FindInChildren(this.gameObject, name);

        if (foundGO == null)
        {
            Debug.LogError($"{name} NOT FOUND!!");
            return;
        }

        foundGO.transform.eulerAngles = Vector3.zero;
    }
}
