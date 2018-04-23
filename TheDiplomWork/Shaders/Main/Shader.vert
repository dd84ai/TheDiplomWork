#version 150 core

in vec3 in_Position;
in vec3 in_Color;   
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

vec4 Shifted_Position(vec3 input_vec)
{
	return vec4((input_vec + in_Position + vec3(0.5,0.5,0.5)),1.0);
}

void main(void) 
{
	//POINT OF VIEW
	vec3 VectoredLook = normalize(playerMatrix[1] - playerMatrix[0]);
	vec3 VectoredToCube = normalize(playerMatrix[1] - in_Position);
	pointofview = dot(VectoredLook, VectoredToCube);

	//PREPARE ROTATOR
	vec3 begin = vec3(-0.5,-0.5,-0.5);

	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	//PLAYER SIDED
	scalar_sides.x = -VectoredToCube.x;
	scalar_sides.y = -VectoredToCube.y;
	scalar_sides.z = -VectoredToCube.z;

	vertex_x_out = Transform * (Shifted_Position((begin + vec3(1,0,0))));
	vertex_y_out = Transform * (Shifted_Position((begin + vec3(0,1,0))));
	vertex_z_out = Transform * (Shifted_Position((begin + vec3(0,0,1))));

	gl_Position = Transform * (Shifted_Position((begin)));

	pass_Color = in_Color;
}
