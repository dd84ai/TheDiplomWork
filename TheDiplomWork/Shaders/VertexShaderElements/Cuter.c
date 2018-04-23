
uniform mat3 playerMatrix;
out float pointofview;
out vec3 scalar_sides;
void Cuter()
{
	//POINT OF VIEW
	vec3 VectoredLook = normalize(playerMatrix[1] - playerMatrix[0]);
	vec3 VectoredToCube = normalize(playerMatrix[1] - in_Position);
	pointofview = dot(VectoredLook, VectoredToCube);

	//PLAYER SIDED
	scalar_sides.x = -VectoredToCube.x;
	scalar_sides.y = -VectoredToCube.y;
	scalar_sides.z = -VectoredToCube.z;
}
