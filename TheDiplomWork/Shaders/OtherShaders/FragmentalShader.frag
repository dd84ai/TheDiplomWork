#version 150 core
in vec3 f_Color;
out vec4 out_Color;
uniform float settingsTransparency;

uniform sampler2D ShadowMap;
in vec3 sun_position_to_frag;
in float sun_distance_to_cube;

void main(void) 
{
	vec4 ShadowCoordinate = vec4(sun_position_to_frag,1.0);
	float DistanceFromLight = texture2D( ShadowMap, ShadowCoordinate.st ).z;

	out_Color = vec4(f_Color, settingsTransparency);
	//vec3 color = vec3(sun_distance_to_cube/200,sun_distance_to_cube/200,sun_distance_to_cube/200);
	//out_Color = vec4(color,1.0);
}