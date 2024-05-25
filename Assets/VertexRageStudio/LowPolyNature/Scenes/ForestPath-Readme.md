## 🌾 Maximizing Grass Density in the 'Forest Path' Demo Scene:

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