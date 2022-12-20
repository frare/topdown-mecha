using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBehaviour : MonoBehaviour
{
    [SerializeField] private Material material;
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private Material[] materialVector;




    private void Awake()
    {
        materialVector = new Material[] { material };

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>(true))
            originalMaterials.Add(renderer, renderer.materials);
        foreach (SkinnedMeshRenderer renderer in GetComponentsInChildren<SkinnedMeshRenderer>(true))
            originalMaterials.Add(renderer, renderer.materials);
    }

    public IEnumerator Flash(float duration, float speed)
    {
        for (float time = 0; time < duration; time += speed * 2)
        {
            foreach (Renderer renderer in originalMaterials.Keys) renderer.materials = materialVector;
            yield return new WaitForSeconds(speed);

            foreach (Renderer renderer in originalMaterials.Keys) renderer.materials = originalMaterials[renderer];
            yield return new WaitForSeconds(speed);
        }
    }

    public IEnumerator FlashOnce(float speed)
    {
        foreach (Renderer renderer in originalMaterials.Keys) renderer.materials = materialVector;

        yield return new WaitForSeconds(speed);

        foreach (Renderer renderer in originalMaterials.Keys) renderer.materials = originalMaterials[renderer];
    }
}