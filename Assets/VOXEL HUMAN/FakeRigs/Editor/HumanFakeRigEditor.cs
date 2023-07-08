using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HumanFakeRig))]
public class HumanFakeRigEditor : Editor
{
    HumanFakeRig rig;

    public override void OnInspectorGUI()
    {

        rig = target as HumanFakeRig;

        // Rig Status Control
        GUILayout.BeginHorizontal();
            if(rig.rigStatus == HumanFakeRig.RigStatusOptions.NOT_INITIALIZED)
            {
                GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Initialized", GUILayout.Height(30), GUILayout.Width(150)))
                    {
                        rig.rigStatus = HumanFakeRig.RigStatusOptions.HUMANOID_RIG; // Show button for humanoid rig
                        rig.Init(); // Init Button
                    }
                GUILayout.FlexibleSpace();
            }
            else if (rig.rigStatus != HumanFakeRig.RigStatusOptions.NOT_INITIALIZED && rig.rigStatus != HumanFakeRig.RigStatusOptions.HUMANOID_RIG)
            {
                GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Show Humanoid Rig", GUILayout.Height(30), GUILayout.Width(150)))
                        rig.rigStatus = HumanFakeRig.RigStatusOptions.HUMANOID_RIG; // Show button for humanoid rig
                GUILayout.FlexibleSpace();
            }
            else if (rig.rigStatus != HumanFakeRig.RigStatusOptions.NOT_INITIALIZED && rig.rigStatus != HumanFakeRig.RigStatusOptions.HAND_RIG)
            {
                GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Show Hand Rig", GUILayout.Height(30), GUILayout.Width(150)))
                        rig.rigStatus = HumanFakeRig.RigStatusOptions.HAND_RIG; // Show button for thumb rig
                GUILayout.FlexibleSpace();
            }
        GUILayout.EndHorizontal();


        if(rig.rigStatus == HumanFakeRig.RigStatusOptions.HUMANOID_RIG)
        {
            renderHumanoidRig();
        }
        else if (rig.rigStatus == HumanFakeRig.RigStatusOptions.HAND_RIG)
        {
            renderHandRig();
        }

        base.OnInspectorGUI();
    }

    void renderHumanoidRig()
    {
        // Humanoid Rig
        GUILayout.BeginVertical("Box");
            //renderTitle();

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();

                renderLHand();

                renderBody();

                renderRHand();

                GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();

            renderFeetToe();
            renderFeetIKCheck();

            GUILayout.Space(20);

        GUILayout.EndVertical();
    }

    void renderHandRig()
    {
        GUILayout.BeginVertical(GUILayout.Height(600));
            GUILayout.BeginVertical(GUILayout.Height(600));
                GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                        renderLeftHand();
                        renderRightHand();
                    GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();


        GUILayout.EndVertical();
    }

    void renderLeftHand()
    {
        GUILayout.BeginVertical("Box", GUILayout.Height(200));
        renderTitle("Left Hand");

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                
                    // Left Pinky
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(20))) rig.Select(rig.l_fingerCtrl.LV3_L_Fingers[3].name);
                        if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(20))) rig.Select(rig.l_fingerCtrl.LV2_L_Fingers[3].name);
                        if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(20))) rig.Select(rig.l_fingerCtrl.LV1_L_Fingers[3].name);
                    GUILayout.EndVertical();
                    
                    // Left Ring
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.l_fingerCtrl.LV3_L_Fingers[2].name);
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.l_fingerCtrl.LV2_L_Fingers[2].name);
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.l_fingerCtrl.LV1_L_Fingers[2].name);
                    GUILayout.EndVertical();

                    // Left Middle
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(45), GUILayout.Width(25))) rig.Select(rig.l_fingerCtrl.LV3_L_Fingers[1].name);
                        if (GUILayout.Button("", GUILayout.Height(45), GUILayout.Width(25))) rig.Select(rig.l_fingerCtrl.LV2_L_Fingers[1].name);
                        if (GUILayout.Button("", GUILayout.Height(45), GUILayout.Width(25))) rig.Select(rig.l_fingerCtrl.LV1_L_Fingers[1].name);
                    GUILayout.EndVertical();

                    // Left Index
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.l_fingerCtrl.LV3_L_Fingers[0].name);
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.l_fingerCtrl.LV2_L_Fingers[0].name);
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.l_fingerCtrl.LV1_L_Fingers[0].name);
                    GUILayout.EndVertical();

                    // Left Thumb
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(26))) rig.Select(rig.l_fingerCtrl.LV3_L_Fingers[4].name);
                        if (GUILayout.Button("", GUILayout.Height(32), GUILayout.Width(26))) rig.Select(rig.l_fingerCtrl.LV2_L_Fingers[4].name);
                        if (GUILayout.Button("", GUILayout.Height(20), GUILayout.Width(26))) rig.Select(rig.l_fingerCtrl.LV1_L_Fingers[4].name);
                    GUILayout.EndVertical();
                

                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                    if (GUILayout.Button("", GUILayout.Height(50), GUILayout.Width(117))) rig.Select(rig.humanFakeRigNames.L_HAND);
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    void renderRightHand()
    {
        GUILayout.BeginVertical("Box", GUILayout.Height(200));
        renderTitle("Right Hand");

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                    // Left Thumb
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(26))) rig.Select(rig.r_fingerCtrl.LV3_R_Fingers[4].name);
                        if (GUILayout.Button("", GUILayout.Height(32), GUILayout.Width(26))) rig.Select(rig.r_fingerCtrl.LV2_R_Fingers[4].name);
                        if (GUILayout.Button("", GUILayout.Height(20), GUILayout.Width(26))) rig.Select(rig.r_fingerCtrl.LV1_R_Fingers[4].name);
                    GUILayout.EndVertical();

                    // Left Index
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.r_fingerCtrl.LV3_R_Fingers[0].name);
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.r_fingerCtrl.LV2_R_Fingers[0].name);
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.r_fingerCtrl.LV1_R_Fingers[0].name);
                    GUILayout.EndVertical();

                    // Left Middle
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(45), GUILayout.Width(25))) rig.Select(rig.r_fingerCtrl.LV3_R_Fingers[1].name);
                        if (GUILayout.Button("", GUILayout.Height(45), GUILayout.Width(25))) rig.Select(rig.r_fingerCtrl.LV2_R_Fingers[1].name);
                        if (GUILayout.Button("", GUILayout.Height(45), GUILayout.Width(25))) rig.Select(rig.r_fingerCtrl.LV1_R_Fingers[1].name);
                    GUILayout.EndVertical();

                    // Left Ring
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.r_fingerCtrl.LV3_R_Fingers[2].name);
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.r_fingerCtrl.LV2_R_Fingers[2].name);
                        if (GUILayout.Button("", GUILayout.Height(40), GUILayout.Width(22))) rig.Select(rig.r_fingerCtrl.LV1_R_Fingers[2].name);
                    GUILayout.EndVertical();
                
                    // Left Pinky
                    GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(20))) rig.Select(rig.r_fingerCtrl.LV3_R_Fingers[3].name);
                        if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(20))) rig.Select(rig.r_fingerCtrl.LV2_R_Fingers[3].name);
                        if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(20))) rig.Select(rig.r_fingerCtrl.LV1_R_Fingers[3].name);
                    GUILayout.EndVertical();

                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                    if (GUILayout.Button("", GUILayout.Height(50), GUILayout.Width(117))) rig.Select(rig.humanFakeRigNames.L_HAND);
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    void renderTitle(string text = "Fake Human Rig")
    {
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text);
            GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
    }

    void renderBody()
    {
        GUILayout.BeginVertical(GUILayout.Width(130));

            // HEAD
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("O O", GUILayout.Height(50), GUILayout.Width(40))) rig.Select(rig.humanFakeRigNames.HEAD); // HEAD
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // NECK
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", GUILayout.Height(20), GUILayout.Width(20))) rig.Select(rig.humanFakeRigNames.NECK); // NECK
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // CHEST
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", GUILayout.Height(60), GUILayout.Width(130))) rig.Select(rig.humanFakeRigNames.CHEST); // CHEST
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // SPINE
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", GUILayout.Height(70), GUILayout.Width(110))) rig.Select(rig.humanFakeRigNames.SPINE); // SPINE
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // HIP
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", GUILayout.Height(50), GUILayout.Width(100))) rig.Select(rig.humanFakeRigNames.HIP); // HIP
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // THIGH
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("L", GUILayout.Height(100), GUILayout.Width(40))) rig.Select(rig.humanFakeRigNames.L_THIGH); // L THIGH
                if (GUILayout.Button("", GUILayout.Height(30), GUILayout.Width(30))) rig.Select(rig.humanFakeRigNames.ROOT); // L THIGH
                if (GUILayout.Button("R", GUILayout.Height(100), GUILayout.Width(40))) rig.Select(rig.humanFakeRigNames.R_THIGH); // R THIGH
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // LEG
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("L", GUILayout.Height(120), GUILayout.Width(30))) rig.Select(rig.humanFakeRigNames.L_LEG); // L LEG
                GUILayout.Space(40);
                if (GUILayout.Button("R", GUILayout.Height(120), GUILayout.Width(30))) rig.Select(rig.humanFakeRigNames.R_LEG); // R LEG
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    void renderLHand()
    {
        GUILayout.BeginVertical(GUILayout.Width(40));
            GUILayout.Space(58);

            GUILayout.BeginHorizontal();
                if (GUILayout.Button("L", GUILayout.Height(30), GUILayout.Width(40))) rig.Select(rig.humanFakeRigNames.L_SHOULDER); // L SHOULDER
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("L", GUILayout.Height(80), GUILayout.Width(25))) rig.Select(rig.humanFakeRigNames.L_ARM); // L ARM
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("L", GUILayout.Height(90), GUILayout.Width(35))) rig.Select(rig.humanFakeRigNames.L_FOREARM); // L FOREARM
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                if (GUILayout.Button("L", GUILayout.Height(45), GUILayout.Width(20))) rig.Select(rig.humanFakeRigNames.L_HAND); // L HAND
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
    void renderRHand()
    {
        GUILayout.BeginVertical(GUILayout.Width(40));
            GUILayout.Space(58);

            GUILayout.BeginHorizontal();
                if (GUILayout.Button("R", GUILayout.Height(30), GUILayout.Width(40))) rig.Select(rig.humanFakeRigNames.R_SHOULDER); // R SHOULDER
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("R", GUILayout.Height(80), GUILayout.Width(25))) rig.Select(rig.humanFakeRigNames.R_ARM); // R ARM
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("R", GUILayout.Height(90), GUILayout.Width(35))) rig.Select(rig.humanFakeRigNames.R_FOREARM); // R FOREARM
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("R", GUILayout.Height(45), GUILayout.Width(20))) rig.Select(rig.humanFakeRigNames.R_HAND); // R HAND
            GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    void renderFeetToe()
    {
        // FEET, TOE
        GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("L", GUILayout.Height(20), GUILayout.Width(30))) rig.Select(rig.humanFakeRigNames.L_TOE); // L TOE
            GUILayout.EndVertical();

            if (GUILayout.Button("L", GUILayout.Height(30), GUILayout.Width(50))) rig.Select(rig.humanFakeRigNames.L_FEET); // L FEET
            GUILayout.Space(30);
            if (GUILayout.Button("R", GUILayout.Height(30), GUILayout.Width(50))) rig.Select(rig.humanFakeRigNames.R_FEET); // R FEET

            GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("R", GUILayout.Height(20), GUILayout.Width(30))) rig.Select(rig.humanFakeRigNames.R_TOE); // R TOE
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    void renderFeetIKCheck()
    {
        // FEET, TOE
        GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", GUILayout.Height(10), GUILayout.Width(90))) rig.Select(rig.humanFakeRigNames.IK_L_FEET); // L TOE
            GUILayout.EndVertical();

            GUILayout.Space(30);

            GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", GUILayout.Height(10), GUILayout.Width(90))) rig.Select(rig.humanFakeRigNames.IK_R_FEET); // R TOE
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
