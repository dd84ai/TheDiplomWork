//#include <Header.vert>
//#include <Cuter.vert>
//#include <Rotator.vert>
//#include <Sun.vert>

vec4 Processed(vec3 inp)
{
	 return Shifted_Position(inp);
}
void main(void) 
{
	PrepareRotator(vec3(sunMatrix[0]));
	PrepareSun();

	Cuter_PointOfView();
	Cuter_PlayerSidedAdvanced();
	
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * Processed(begin + vec3(1,0,0));
	vertex_y_out = Transform * Processed(begin + vec3(0,1,0));
	vertex_z_out = Transform * Processed(begin + vec3(0,0,1));

	gl_Position = Transform * Processed(begin);

	pass_Color = in_Color;
}
