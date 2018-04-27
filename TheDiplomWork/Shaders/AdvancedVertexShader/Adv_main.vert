//#include <Header.vert>
//#include <Cuter.vert>
//#include <Rotator.vert>
//#include <Sun.vert>

void main(void) 
{
	//PREPARE ROTATOR
	PrepareRotator(vec3(sunMatrix[0]));
	PrepareSun();

	Cuter_PointOfView();
	Cuter_PlayerSidedAdvanced();
	
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(1,0,0),in_Center)));
	vertex_y_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(0,1,0),in_Center)));
	vertex_z_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(0,0,1),in_Center)));

	gl_Position = Transform * (Shifted_Position(Rotated_Around(begin,in_Center)));

	pass_Color = in_Color;
}
