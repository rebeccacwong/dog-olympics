void ToonShading_float(in float3 Normal, in float3 ClipSpacePos, in float3 WorldPos, in float4 ToonRampTinting,
in float ToonRampOffset, in float ToonRampSteps, out float3 ToonRampOutput, out float3 Direction)
{

	// set the shader graph node previews
	#ifdef SHADERGRAPH_PREVIEW
		ToonRampOutput = float3(0.5,0.5,0);
		Direction = float3(0.5,0.5,0);
	#else

		// grab the shadow coordinates
		#if SHADOWS_SCREEN
			half4 shadowCoord = ComputeScreenPos(ClipSpacePos);
		#else
			half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
		#endif 

		// grab the main light
		#if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
			Light light = GetMainLight(shadowCoord);
		#else
			Light light = GetMainLight();
		#endif

		// Dot product for toonramp diffuse texture.
		half d = dot(Normal, light.direction) * 0.5 + 0.5;
		d = exp(-pow(2 * (1 - d), 1.3));
		
		// multiply with shadows;
		float toonRamp = d * light.shadowAttenuation;
		// add in lights and extra tinting
		if (d > ToonRampOffset)
		{
			ToonRampOutput = light.color * (toonRamp + ToonRampTinting) ;
		}
		else
		{
			ToonRampOutput = light.color * toonRamp;
		}
		// output direction for rimlight
		Direction = light.direction;
	#endif

}