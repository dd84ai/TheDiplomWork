#version 150 core
in vec3 f_Color;
out vec4 out_Color;

void main(void) 
{
	out_Color = vec4(f_Color, 1.0);
}