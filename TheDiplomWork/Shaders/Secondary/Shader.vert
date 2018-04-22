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
mat3 Rotator;
vec3 DeCenter(in vec3 input)
{
	return input - in_Center.xyz;
}
vec3 ReCenter(in vec3 input)
{
	return input + in_Center.xyz;
}
void PrepareRotator()
{
	mat3 RotateX = mat3(vec3(1,0,0),vec3(0,cos(in_Angles.x),-sin(in_Angles.x)),vec3(0,sin(in_Angles.x),cos(in_Angles.x)));
	mat3 RotateY = mat3(vec3(cos(in_Angles.y),0,sin(in_Angles.y)),vec3(0,1,0),vec3(-sin(in_Angles.y),0,cos(in_Angles.y)));
	mat3 RotateZ = mat3(vec3(cos(in_Angles.z),-sin(in_Angles.z),0),vec3(sin(in_Angles.z),cos(in_Angles.z),0),vec3(0,0,1));
	Rotator = RotateX * RotateY * RotateZ;
}
vec3 Rotated_Position(vec3 input_vec)
{
	
	return ReCenter(DeCenter(input_vec) * Rotator);
}
void main(void) 
{
	PrepareRotator();
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * (vec4(in_Position, 0.0) + vec4(1,0,0,1));
	vertex_y_out = Transform * (vec4(in_Position, 0.0) + vec4(0,1,0,1));
	vertex_z_out = Transform * (vec4(in_Position, 0.0) + vec4(0,0,1,1));

	gl_Position = Transform * vec4(Rotated_Position(in_Position), 1.0);

	pass_Color = in_Color;
}
