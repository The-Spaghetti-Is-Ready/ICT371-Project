# Low Poly Nature Pack 🌿 Documentation

Welcome to the Low Poly Nature Pack! 

## 🎉 Thank You for Your Purchase!

We truly appreciate your support. Every purchase fuels our passion to create even more awesome content for the Unity community.

## 🚀 Installation Instructions:

1. Download the pack.
2. Import directly into Unity. 

For palette customization, refer to our annotated guide: `Assets\VertexRageStudio\LowPolyNature\Textures\Palette-Annotated.png`. Note that not all sections of this palette are utilized in this pack, as it's a shared resource across several of our projects.

## 🛠 Rendering Support:

Seamlessly integrate with Unity's rendering systems. This pack supports:
- BuiltIn Pipeline
- Universal Render Pipeline (URP)
- High Definition Render Pipeline (HDRP)

## 🎨 SRP Compatibility:

The Low Poly Nature Pack comes prepared for both URP and HDRP. To ensure compatibility with your preferred Scriptable Render Pipeline, follow these steps:

### For URP (Universal Render Pipeline):

1. Navigate to `VertexRageStudio/LowPolyNature/SRP/`.
2. Double-click the `URP.unitypackage`.
3. Allow Unity to extract and overwrite the existing files. This will apply all URP-related changes.

### For HDRP (High Definition Render Pipeline):

1. Navigate to `VertexRageStudio/LowPolyNature/SRP/`.
2. Double-click the `HDRP.unitypackage`.
3. Allow Unity to extract and overwrite the existing files. This will implement the necessary HDRP adjustments.

## 🌾 Maximizing Grass Density in the 'Forest Path' Scene:

Please note that by default, the `Forest Path` scene is quite CPU demending due to large number of objects placed for demonstration purposes. It's tailored with techniques like occlusion culling and GPU instancing in mind to optimize performance.

Additionally there's an option to achieve especially dense grass, but it requires a bit of setup and GPU instancing solution. 

If you're using a GPU instancing solution (like GPU Instancer):

1. Find the disabled game object: `Vegetation/Grass/Dense Grass - use only with GPU instancing!`.
2. Enable this game object.
3. Ensure instancing is enabled for the following grass assets:

   - GrassLumpA
   - GrassLumpB
   - GrassLumpC
   - GrassLumpD
   - GrassLumpE
   - GrassLumpF
   - GrassLumpLargeA
   - GrassLumpLargeC
   - GrassLumpLargeFTall
   - GrassLumpLargeHTall
   - MixedGrassLumpLargetFTall
   - MixedGrassLumpLargetHTall


## 💌 Need Help or Have Suggestions?

We're here to assist and value your feedback. Don't hesitate to reach out!

- **Discord:** [Join our community](https://discord.gg/hJUbu9vHFg)
- **E-mail:** support@vertexrage.com

## 🌟 Leave a Review!

Your feedback makes a big difference! If you're enjoying the Low Poly Nature Pack, please consider leaving a review on the Asset Store. It helps us immensely and lets others know about your positive experience.


---

Proudly crafted with ❤️ for the Unity community!

---

## ⚠️ Note: 

This pack includes 3D models, prefabs, scenes, materials, and textures. Effects, water shaders, and volumetrics from videos and screenshots are excluded. Most notably see Volumetric Fog and Mist 2 by Kronnect (https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/volumetric-fog-mist-2-162694) and URP Stylized Water Shader - Proto Series by Bitgem (https://assetstore.unity.com/packages/vfx/shaders/urp-stylized-water-shader-proto-series-187485) and skyboxes from 'AllSky' by rpgwhitelock (https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-220-sky-skybox-set-10109). In one of the demo scenes, dense grass is rendered using the GPU Instancer by GurBu Technologies (https://assetstore.unity.com/packages/tools/utilities/gpu-instancer-117566). Please note, GPU Instancer is NOT included in the asset pack, but the grass is available (disabled) for those with a similar tool. All screenshots are from URP. Overview images remain unaltered without post-processing or special effects.