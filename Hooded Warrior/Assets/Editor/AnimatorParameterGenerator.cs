using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;


// Select the gameobject that has an animator component
// Then go to path shown in MenuItem
// The file should be generated in "/Scripts/Animator Parameters/"
public class AnimatorParameterGenerator : Editor
{
    [MenuItem("EditorHelper/Update Animator Parameters")]
    public static void UpdateAnimatorParameters()
    {
        var animatorController = GetAnimatorController();

        if (null != animatorController)
        {
            UpdateParameters(animatorController);
            Debug.Log("Animator Parameters Updated");
        }
        else
            Debug.LogWarning("No Animator Controller found");
    }

    private static AnimatorController GetAnimatorController()
    {
        // Get the currently selected GameObject
        GameObject selectedObject = Selection.activeGameObject;

        // Check if the GameObject has an Animator component
        if (null != selectedObject)
        {
            // Get the Animator Controller from the Animator component
            if (selectedObject.TryGetComponent<Animator>(out var animator))
                return animator.runtimeAnimatorController as AnimatorController;
        }

        return null;
    }

    private static void UpdateParameters(AnimatorController animatorController)
    {
        // Generate the new content for AnimatorParameters class
        string newClassName = $"{animatorController.name}Parameters";
        string newContent   = "public static class " + newClassName + "\n{\n";

        foreach (var parameter in animatorController.parameters)
            newContent += $"    public static readonly string {parameter.name} = \"{parameter.name}\";\n";

        newContent += "}\n";

        Helpers.GenerateFile(newClassName, newContent);
    }
}