#version 150 core

in vec3 in_Position;
in vec3 in_Color;  
in vec3 in_Center;
in vec3 in_Angles;  
out vec3 pass_Color;
out vec4 vertex_x_out;
out vec4 vertex_y_out;
out vec4 vertex_z_out;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform mat4 rotMatrix;
uniform mat4 PlayerAndLocaledRangeAndSun;
mat4 Rotator;
vec4 Center;
vec4 DeCenter(in vec4 input)
{
	return input - vec4(in_Center,1.0);
}
vec4 ReCenter(in vec4 input)
{
	return input + vec4(in_Center,1.0);
}
void PrepareRotator()
{
	mat4 RotateX = mat4(vec4(1,0,0,0),
	vec4(0,cos(in_Angles.x),-sin(in_Angles.x),0),
	vec4(0,sin(in_Angles.x),cos(in_Angles.x),0),
	vec4(0,0,0,1));

	mat4 RotateY = mat4(vec4(cos(in_Angles.y),0,sin(in_Angles.y),0),
	vec4(0,1,0,0),
	vec4(-sin(in_Angles.y),0,cos(in_Angles.y),0),
	vec4(0,0,0,1));

	mat4 RotateZ = mat4(vec4(cos(in_Angles.z),-sin(in_Angles.z),0,0),
	vec4(sin(in_Angles.z),cos(in_Angles.z),0,0),
	vec4(0,0,1,0),
	vec4(0,0,0,1));
	Rotator = RotateX * RotateY * RotateZ;
}
vec4 Rotated_Position(vec4 input_vec)
{
	
	return ReCenter(DeCenter(input_vec) * Rotator);
}
void main(void) 
{
	Center = vec4(in_Position,1.0) + vec4(0.5,0.5,0.5,0.0);
	vec4 begin = vec4(in_Position,1.0);
	PrepareRotator();
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * Rotated_Position(begin + vec4(1,0,0,0));
	vertex_y_out = Transform * Rotated_Position(begin + vec4(0,1,0,0));
	vertex_z_out = Transform * Rotated_Position(begin + vec4(0,0,1,0));

	gl_Position = Transform * Rotated_Position(begin);

	pass_Color = in_Color;
}
