#version 150 core

in vec3 in_Position;
in vec3 in_Color;  
in vec3 in_Center;
in vec3 in_Angles;  
out vec3 pass_Color;
out vec4 vertex_x_out;
out vec4 vertex_y_out;
out vec4 vertex_z_out;

out vec3 scalar_sides;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform mat4 rotMatrix;

uniform mat3 playerMatrix;
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
vec4 Shifted_Position(vec3 input_vec)
{
	return vec4((input_vec + in_Position + vec3(0.5,0.5,0.5)),1.0);
}
vec3 Rotated_Position(vec3 input_vec)
{
	return (input_vec * Rotator);
}

void main(void) 
{
	//POINT OF VIEW
	vec3 VectoredLook = normalize(playerMatrix[1] - playerMatrix[0]);
	vec3 VectoredToCube = normalize(playerMatrix[1] - in_Position);
	pointofview = dot(VectoredLook, VectoredToCube);

	//PREPARE ROTATOR
	vec3 begin = vec3(-0.5,-0.5,-0.5);
	Angles = vec3(0,0,0);
	PrepareRotator();

	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	//PLAYER SIDED
	vec3 vector_side_x_out = Rotated_Position(vec3(1,0,0));
	vec3 vector_side_y_out = Rotated_Position(vec3(0,1,0));
	vec3 vector_side_z_out = Rotated_Position(vec3(0,0,1));
	vec3 VectorFromPlayerToCube = - VectoredToCube;
	scalar_sides.x = vector_side_x_out.x * VectorFromPlayerToCube.x + vector_side_x_out.y * VectorFromPlayerToCube.y + vector_side_x_out.z * VectorFromPlayerToCube.z;
	scalar_sides.y = vector_side_y_out.x * VectorFromPlayerToCube.x + vector_side_y_out.y * VectorFromPlayerToCube.y + vector_side_y_out.z * VectorFromPlayerToCube.z;
	scalar_sides.z = vector_side_z_out.x * VectorFromPlayerToCube.x + vector_side_z_out.y * VectorFromPlayerToCube.y + vector_side_z_out.z * VectorFromPlayerToCube.z;

	vertex_x_out = Transform * (Shifted_Position(Rotated_Position(begin + vec3(1,0,0))));
	vertex_y_out = Transform * (Shifted_Position(Rotated_Position(begin + vec3(0,1,0))));
	vertex_z_out = Transform * (Shifted_Position(Rotated_Position(begin + vec3(0,0,1))));

	gl_Position = Transform * (Shifted_Position(Rotated_Position(begin)));

	pass_Color = in_Color;
}
