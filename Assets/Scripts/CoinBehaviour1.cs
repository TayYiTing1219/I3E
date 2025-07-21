using UnityEngine;


public class CoinBehaviour1 : MonoBehaviour
{
    MeshRenderer myMeshRenderer;

    [SerializeField]
    Material highlightMat; 
    Material originalMat;
    public int coinValue = 10;

    void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        originalMat = myMeshRenderer.material; // Store the original material
    }

    public void Highlight()
    {
        myMeshRenderer.material = highlightMat; // Change to highlight material
    }

    public void Unhighlight()
    {
        myMeshRenderer.material = originalMat; // Reset to original material
    }

    public void Collect(PlayerBehaviour1 player)
    {
        GameManager1.instance.ModifyScore(coinValue); 
        Destroy(gameObject);
    }
}