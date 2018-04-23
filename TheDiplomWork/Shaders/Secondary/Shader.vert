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

uniform mat3 sunMatrix;
out float pointofview;

mat3 Rotator;
vec3 Angles;

void PrepareRotator()
{
	mat3 RotateX = mat3(vec3(1,0,0),
	vec3(0,cos(Angles.x),-sin(Angles.x)),
	vec3(0,sin(Angles.x),cos(Angles.x)));

	mat3 RotateY = mat3(vec3(cos(Angles.y),0,sin(Angles.y)),
	vec3(0,1,0),
	vec3(-sin(Angles.y),0,cos(Angles.y)));

	mat3 RotateZ = mat3(vec3(cos(Angles.z),-sin(Angles.z),0),
	vec3(sin(Angles.z),cos(Angles.z),0),
	vec3(0,0,1));
	Rotator = RotateX * RotateY * RotateZ;
}
vec4 Rotated_Position(vec3 input_vec)
{
	
	return vec4((input_vec * Rotator + in_Position + vec3(0.5,0.5,0.5)),1.0);
}
void main(void) 
{
	float range;
	vec3 playerpos = vec3(sunMatrix[0].xyz);
	vec3 playerpos_look = sunMatrix[1].xyz;//vec3(sunMatrix[0].y,sunMatrix[1].y,sunMatrix[2].y);
	vec3 VectoredLook = normalize(playerpos_look - playerpos);
	vec3 VectoredToCube = normalize(playerpos_look - in_Position);
	pointofview = VectoredLook.x * VectoredToCube.x + VectoredLook.y * VectoredToCube.y + VectoredLook.z * VectoredToCube.z;

	vec3 begin = vec3(-0.5,-0.5,-0.5);
	Angles = vec3(0,0,0);
	PrepareRotator();
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * (Rotated_Position(begin + vec3(1,0,0)));
	vertex_y_out = Transform * (Rotated_Position(begin + vec3(0,1,0)));
	vertex_z_out = Transform * (Rotated_Position(begin + vec3(0,0,1)));

	gl_Position = Transform * (Rotated_Position(begin));

	pass_Color = in_Color;
}
