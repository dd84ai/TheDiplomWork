
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
void Cuter_without_angles()
{
	Cuter_PointOfView();
	Cuter_PlayerSidedRegular();
}