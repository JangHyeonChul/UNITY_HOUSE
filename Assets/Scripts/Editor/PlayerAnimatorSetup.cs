using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public static class PlayerAnimatorSetup
{
    const string ControllerPath = "Assets/Scripts/PlayerAnimator.controller";

    [MenuItem("Tools/PlayerAnimator - Make Transitions (Idle ↔ Move)")]
    public static void MakeTransitions()
    {
        var controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(ControllerPath);
        if (controller == null)
        {
            Debug.LogError($"Controller not found: {ControllerPath}");
            return;
        }

        // Ensure MoveY float parameter exists
        bool hasMoveY = false;
        foreach (var p in controller.parameters)
        {
            if (p.name == "MoveY" && p.type == AnimatorControllerParameterType.Float)
            {
                hasMoveY = true;
                break;
            }
        }
        if (!hasMoveY)
        {
            controller.AddParameter("MoveY", AnimatorControllerParameterType.Float);
        }

        var layer = controller.layers[0];
        var sm = layer.stateMachine;
        UnityEditor.Animations.AnimatorState stateIdle = null;
        UnityEditor.Animations.AnimatorState stateMove = null;

        for (int i = 0; i < sm.states.Length; i++)
        {
            var s = sm.states[i].state;
            if (s.name == "player_front_idle") stateIdle = s;
            if (s.name == "player_front_move") stateMove = s;
        }

        if (stateIdle == null || stateMove == null)
        {
            Debug.LogError("PlayerAnimator: player_front_idle or player_front_move state not found.");
            return;
        }

        bool hasIdleToMove = HasTransitionTo(stateIdle, stateMove);
        bool hasMoveToIdle = HasTransitionTo(stateMove, stateIdle);

        if (!hasIdleToMove)
        {
            var idleToMove = stateIdle.AddTransition(stateMove);
            idleToMove.hasExitTime = false;
            idleToMove.duration = 0.25f;
            idleToMove.AddCondition(AnimatorConditionMode.Greater, 0.1f, "MoveY");
        }

        if (!hasMoveToIdle)
        {
            var moveToIdle = stateMove.AddTransition(stateIdle);
            moveToIdle.hasExitTime = false;
            moveToIdle.duration = 0.25f;
            moveToIdle.AddCondition(AnimatorConditionMode.Less, 0.1f, "MoveY");
        }

        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
        Debug.Log("PlayerAnimator: Make Transitions (Idle <-> Move) done.");
    }

    static bool HasTransitionTo(UnityEditor.Animations.AnimatorState from, UnityEditor.Animations.AnimatorState to)
    {
        foreach (var t in from.transitions)
        {
            if (t.destinationState == to) return true;
        }
        return false;
    }
}
