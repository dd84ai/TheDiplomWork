
uniform mat3 playerMatrix;
out float pointofview;
out vec3 scalar_sides;
out float range;
vec3 VectoredToCube;
void Cuter_Ranger()
{
	range = length(playerMatrix[0] - in_Position);
}
void Cuter_PointOfView()
{
	//POINT OF VIEW
	vec3 VectoredLook = normalize(playerMatrix[1] - playerMatrix[0]);
	VectoredToCube = normalize(playerMatrix[1] - in_Position);
	pointofview = dot(VectoredLook, VectoredToCube);

	Cuter_Ranger();
}
void Cuter_PlayerSidedRegular()
{
	//PLAYER SIDED
	scalar_sides.x = -VectoredToCube.x;
	scalar_sides.y = -VectoredToCube.y;
	scalar_sides.z = -VectoredToCube.z;
}
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
void Cuter_without_angles()
{
	Cuter_PointOfView();
	Cuter_PlayerSidedRegular();
}