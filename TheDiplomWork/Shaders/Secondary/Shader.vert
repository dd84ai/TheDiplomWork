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
	return in_Position.xyz - in_Center.xyz;
}
vec3 ReCenter(in vec3 input)
{
	return input + in_Center.xyz;
}
vec3 Rotated_Position()
{
	mat3 RotateX = mat3(vec3(1,0,0),vec3(0,cos(in_Angles.x),-sin(in_Angles.x)),vec3(0,sin(in_Angles.x),cos(in_Angles.x)));
	mat3 RotateY = mat3(vec3(cos(in_Angles.y),0,sin(in_Angles.y)),vec3(0,1,0),vec3(-sin(in_Angles.y),0,cos(in_Angles.y)));
	mat3 RotateZ = mat3(vec3(cos(in_Angles.z),-sin(in_Angles.z),0),vec3(sin(in_Angles.x),cos(in_Angles.z),0),vec3(0,0,1));

	return ReCenter(DeCenter() * RotateX * RotateY * RotateZ);
}
void main(void) 
{	
	gl_Position = (projectionMatrix *rotMatrix *  viewMatrix * modelMatrix) * vec4(Rotated_Position(), 1.0);

	pass_Color = in_Color;
}
