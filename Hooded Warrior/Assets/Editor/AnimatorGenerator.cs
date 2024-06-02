using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;


public class AnimatorGenerator : Editor
{
    [MenuItem("EditorHelper/Create Animator Controller")]
    public static void CreateAnimatorController()
    {
        // Get the selected GameObject
        GameObject selectedObject = Selection.activeGameObject;
        if (selectedObject == null)
        {
            Debug.LogWarning("No GameObject selected.");
            return;
        }

        // Create a new AnimatorController
        // TODO: change path
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath("Assets/Animations/Generated/" + "AnimatorControllerX.controller");

        // Get the AnimatorStateMachine
        AnimatorStateMachine rootStateMachine = animatorController.layers[0].stateMachine;

        // Find all nested GameObjects
        Transform[] allTransforms = selectedObject.GetComponentsInChildren<Transform>();

        // Add parameters for each nested GameObject
        foreach (Transform t in allTransforms)
            if (t != selectedObject) // Exclude the root GameObject
            {
                switch (t.name[^2..])    // Get the last two characters of the name
                {
                    case "_b":
                        animatorController.AddParameter(t.name, AnimatorControllerParameterType.Bool);
                        break;
                    case "_f":
                        animatorController.AddParameter(t.name, AnimatorControllerParameterType.Float);
                        break;
                    default:
                        // Handle other cases if needed
                        animatorController.AddParameter(t.name, AnimatorControllerParameterType.Bool);

                        break;
                }

                AnimatorState state = rootStateMachine.AddState(t.name);
                rootStateMachine.AddEntryTransition(state).AddCondition(AnimatorConditionMode.If, 0, t.name);
                state.AddExitTransition().AddCondition(AnimatorConditionMode.IfNot, 0, t.name);
            }

        // Attach the Animator to the selected GameObject if it doesn't already have one
        Animator animator = selectedObject.GetComponent<Animator>();

        if (animator == null)
        {
            animator = selectedObject.AddComponent<Animator>();

            // Assign the created AnimatorController to the Animator
            animator.runtimeAnimatorController = animatorController;

            Debug.Log("Animator Controller created and assigned to " + selectedObject.name);
        }
        else
            Debug.Log("Animator Controller created for " + selectedObject.name);
    }

    private static void CreateBlendTree(AnimatorStateMachine rootStateMachine)
    {
        BlendTree blendTree = null;
        AnimatorState blendTreeState = rootStateMachine.AddState("Blend Tree");
        blendTreeState.motion = blendTree = new BlendTree();
        blendTree.name = "Blend Tree";
        blendTree.blendType = BlendTreeType.FreeformDirectional2D;

        // Set the blend parameter (ensure this parameter exists in the AnimatorController)
        //blendTree.blendParameter = "Blend";

        // Add motions to the blend tree
        //blendTree.AddChild(animationClip1);
        //blendTree.AddChild(animationClip2);

        // Optionally, set thresholds for each motion
        //blendTree.children[0].threshold = 0.0f;
        //blendTree.children[1].threshold = 1.0f;

        // Add the blend tree to the state machine
        //blendTreeState.motion = blendTree;
    }
}
