#version 150 core

in vec3 in_Position;
in vec3 in_Color;
in vec3 in_Center;
in vec3 in_Size;
out vec3 pass_Color;
out vec4 vertex_x_out;
out vec4 vertex_y_out;
out vec4 vertex_z_out;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform mat4 rotMatrix;
uniform mat3 sunMatrix;
uniform float settingsTransparency;
uniform float TimeTotalSeconds;
uniform float settingsTHIS_IS_EXPLOSION;
uniform mat3 projectileMatrix;

vec4 Shifted_Position(vec3 input_vec)
{
	return vec4((input_vec + in_Position + vec3(0.5, 0.5, 0.5)), 1.0);
}
vec3 Shifted(vec3 input_vec)
{
	return vec3(input_vec + (in_Position + vec3(0.5, 0.5, 0.5)));
}
vec3 Deshifted(vec3 input_vec)
{
	return vec3(input_vec - (in_Position + vec3(0.5, 0.5, 0.5)));
}


//Угол ноль.
vec3 begin = vec3(-0.5, -0.5, -0.5);



