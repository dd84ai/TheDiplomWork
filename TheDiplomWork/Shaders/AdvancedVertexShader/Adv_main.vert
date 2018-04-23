//#include <Header.vert>
//#include <Header_plus.vert>
//#include <Cuter.vert>
//#include <Rotator.vert>

uniform mat3 sunMatrix;
void Cuter_PlayerSidedAdvanced()
{
	//PLAYER SIDED
	vec3 vector_side_x_out = Rotated_Around(vec3(1, 0, 0),in_Center);
	vec3 vector_side_y_out = Rotated_Around(vec3(0, 1, 0),in_Center);
	vec3 vector_side_z_out = Rotated_Around(vec3(0, 0, 1),in_Center);
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

	vertex_x_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(1,0,0),in_Center)));
	vertex_y_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(0,1,0),in_Center)));
	vertex_z_out = Transform * (Shifted_Position(Rotated_Around(begin + vec3(0,0,1),in_Center)));

	gl_Position = Transform * (Shifted_Position(Rotated_Around(begin,in_Center)));

	pass_Color = in_Color;
}
