//#include <Header.vert>
//#include <Header_plus.vert>
//#include <Cuter.vert>
//#include <Rotator.vert>

uniform mat3 sunMatrix;
void Cuter_PlayerSidedAdvanced()
{
	//PLAYER SIDED
	vec3 vector_side_x_out = Rotated_Position(vec3(1, 0, 0));
	vec3 vector_side_y_out = Rotated_Position(vec3(0, 1, 0));
	vec3 vector_side_z_out = Rotated_Position(vec3(0, 0, 1));
	vec3 VectorFromPlayerToCube = -VectoredToCube;
	scalar_sides.x = dot(vector_side_x_out, VectorFromPlayerToCube);
	scalar_sides.y = dot(vector_side_y_out, VectorFromPlayerToCube);
	scalar_sides.z = dot(vector_side_z_out, VectorFromPlayerToCube);
}
void main(void) 
{
	//PREPARE ROTATOR
	PrepareRotator(vec3(sunMatrix[0]));

	Cuter_PointOfView();
	Cuter_PlayerSidedAdvanced();
	
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(1,0,0),sunMatrix[1])));
	vertex_y_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(0,1,0),sunMatrix[1])));
	vertex_z_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(0,0,1),sunMatrix[1])));

	gl_Position = Transform * (Shifted_Position(Rotated_Around(begin,sunMatrix[1])));

	pass_Color = in_Color;
}
