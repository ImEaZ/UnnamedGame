using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
    public Material RippleMaterial;
    public Material NullMaterial;
    public float MaxAmount = 50f;
    private Material mainMat;
    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;

    public bool played = false, allowToRip = false;

    void Start()
    {
        mainMat = NullMaterial;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && allowToRip)
        {
            played = true;
            this.Amount = this.MaxAmount;
            Vector3 pos = Input.mousePosition;
            this.RippleMaterial.SetFloat("_CenterX", pos.x);
            this.RippleMaterial.SetFloat("_CenterY", pos.y);
        }

        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.mainMat);
    }

    public void PlayLevelUpRipple(Vector3 pos)
    {
        this.mainMat = this.RippleMaterial;
        this.Amount = this.MaxAmount;
        this.mainMat.SetFloat("_CenterX", pos.x);
        this.mainMat.SetFloat("_CenterY", pos.y);
        StartCoroutine(WaitAnim(pos));
    }
    IEnumerator WaitAnim(Vector3 pos)
    {
        //To wait, type this:
        //Stuff before waiting
            yield return new WaitForSeconds(0.3f);
        //Stuff after waiting.
        this.mainMat = NullMaterial;
    }
}
