using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Antialiasing : PostEffectsBase {
    public bool dlaaSharp = false;
    public Shader dlaaShader;
    private Material dlaa;

    public override bool CheckResources() {
        CheckSupport(false);
        dlaa = CreateMaterial(dlaaShader, dlaa);
        return isSupported;
    }

    public void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (CheckResources() == false) {
            Graphics.Blit(source, destination);
            return;
        }

        if (dlaa != null) {
            source.anisoLevel = 0;
            RenderTexture interim = RenderTexture.GetTemporary(source.width, source.height);
            Graphics.Blit(source, interim, dlaa, 0);
            Graphics.Blit(interim, destination, dlaa, dlaaSharp ? 2 : 1);
            RenderTexture.ReleaseTemporary(interim);
        }
    }
}