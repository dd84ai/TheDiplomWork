﻿//#include <Header.vert>
//#include <Cuter.vert>

void main(void) 
{
	Cuter_without_angles();

	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * (Shifted_Position((begin + vec3(1,0,0))));
	vertex_y_out = Transform * (Shifted_Position((begin + vec3(0,1,0))));
	vertex_z_out = Transform * (Shifted_Position((begin + vec3(0,0,1))));

	gl_Position = Transform * (Shifted_Position((begin)));

	pass_Color = in_Color;
}