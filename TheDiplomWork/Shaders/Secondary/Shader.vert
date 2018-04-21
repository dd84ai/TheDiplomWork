#version 150 core

in vec3 in_Position;
in vec3 in_Color;  
in vec3 in_Center;
in vec3 in_Angles;  
out vec3 pass_Color;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform mat4 rotMatrix;
vec3 DeCenter()
{
	return in_Position - in_Center;
}
void main(void) 
{
	vec3 Changed_Position = DeCenter();
	vec4 Rotated_Position = vec4(Changed_Position, 1.0);
	Rotated_Position = Rotated_Position;
	
	gl_Position = (projectionMatrix *rotMatrix *  viewMatrix * modelMatrix) * Rotated_Position;

	pass_Color = in_Color;
}
