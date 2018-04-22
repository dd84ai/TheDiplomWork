#version 150 core

in vec3 in_Position;
in vec3 in_Color;  
out vec3 pass_Color;
out vec4 vertex_x_out;
out vec4 vertex_y_out;
out vec4 vertex_z_out;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform mat4 rotMatrix;
uniform mat4 PlayerAndLocaledRangeAndSun;

void main(void) 
{
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * (vec4(in_Position, 0.0) + vec4(1,0,0,1));
	vertex_y_out = Transform * (vec4(in_Position, 0.0) + vec4(0,1,0,1));
	vertex_z_out = Transform * (vec4(in_Position, 0.0) + vec4(0,0,1,1));

	gl_Position = Transform * vec4(in_Position, 1.0);

	pass_Color = in_Color;
}