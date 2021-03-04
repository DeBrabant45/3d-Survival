using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHelper 
{
    public void SwapToSelectionMaterial(GameObject objectToModify, List<Material[]> currentColliderMaterailsList, Material selectionMaterial)
    {
        currentColliderMaterailsList.Clear();

        if (objectToModify.transform.childCount > 0)
        {
            foreach (Transform child in objectToModify.transform)
            {
                PrepareRendererToSwapMaterials(child.gameObject, currentColliderMaterailsList, selectionMaterial);
            }
        }
        else
        {
            PrepareRendererToSwapMaterials(objectToModify, currentColliderMaterailsList, selectionMaterial);
        }
    }

    public void PrepareRendererToSwapMaterials(GameObject objectToModify, List<Material[]> currentColliderMaterailsList, Material selectionMaterial)
    {
        var renderer = objectToModify.GetComponent<Renderer>();
        currentColliderMaterailsList.Add(renderer.sharedMaterials);
        SwapMaterials(renderer, selectionMaterial);
    }

    public void SwapMaterials(Renderer renderer, Material selectionMaterial)
    {
        Material[] matArray = new Material[renderer.materials.Length];
        for (int i = 0; i < matArray.Length; i++)
        {
            matArray[i] = selectionMaterial;
        }
        renderer.materials = matArray;
    }

    public void SwapToOriginalMaterial(GameObject objectToModify, List<Material[]> currentColliderMaterailsList)
    {
        if (currentColliderMaterailsList.Count > 1)
        {
            for (int i = 0; i < currentColliderMaterailsList.Count; i++)
            {
                var childRenderer = objectToModify.transform.GetChild(i).GetComponent<Renderer>();
                if(childRenderer != null)
                {
                    childRenderer.materials = currentColliderMaterailsList[i];
                }
            }
        }
        else
        {
            var renderer = objectToModify.GetComponent<Renderer>();
            renderer.materials = currentColliderMaterailsList[0];
        }
    }
}
