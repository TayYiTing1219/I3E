using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class CoinBehaviour : MonoBehaviour
{
    MeshRenderer meshRenderer; // MeshRenderer to change the material of the coin
    [SerializeField]
    Material HighlightMaterial; // Material to highlight the coin
    Material OriginalMaterial; // Original material of the coin

    void Start()
    {
        // get the MeshRenderer component attached to this GameObject
        // store it in the meshRenderer variable
        meshRenderer = GetComponent<MeshRenderer>();

        // store the original color of the coin
        OriginalMaterial = meshRenderer.material;
    }
    public void Highlight()
    {
        meshRenderer.material = HighlightMaterial; // Change to highlight material
    }

    public void Unhighlight()
    {
        meshRenderer.material = OriginalMaterial; // Change back to the original material
    }

    public int coinValue = 100; // Value of the coin

    public void Collect(PlayerBehaviour player)
    {
        player.ModifyScore(coinValue); // Call the method to modify the score
        Destroy(gameObject); // Destroy the coin object
    }
}
