
void main(void) 
{
	//POINT OF VIEW
	vec3 VectoredLook = normalize(playerMatrix[1] - playerMatrix[0]);
	vec3 VectoredToCube = normalize(playerMatrix[1] - in_Position);
	pointofview = dot(VectoredLook, VectoredToCube);

	//PREPARE ROTATOR
	vec3 begin = vec3(-0.5,-0.5,-0.5);
	Angles = vec3(0,0,0);
	PrepareRotator();

	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	//PLAYER SIDED
	vec3 vector_side_x_out = Rotated_Position(vec3(1,0,0));
	vec3 vector_side_y_out = Rotated_Position(vec3(0,1,0));
	vec3 vector_side_z_out = Rotated_Position(vec3(0,0,1));
	vec3 VectorFromPlayerToCube = - VectoredToCube;
	scalar_sides.x = vector_side_x_out.x * VectorFromPlayerToCube.x + vector_side_x_out.y * VectorFromPlayerToCube.y + vector_side_x_out.z * VectorFromPlayerToCube.z;
	scalar_sides.y = vector_side_y_out.x * VectorFromPlayerToCube.x + vector_side_y_out.y * VectorFromPlayerToCube.y + vector_side_y_out.z * VectorFromPlayerToCube.z;
	scalar_sides.z = vector_side_z_out.x * VectorFromPlayerToCube.x + vector_side_z_out.y * VectorFromPlayerToCube.y + vector_side_z_out.z * VectorFromPlayerToCube.z;

	vertex_x_out = Transform * (Shifted_Position(Rotated_Position(begin + vec3(1,0,0))));
	vertex_y_out = Transform * (Shifted_Position(Rotated_Position(begin + vec3(0,1,0))));
	vertex_z_out = Transform * (Shifted_Position(Rotated_Position(begin + vec3(0,0,1))));

	gl_Position = Transform * (Shifted_Position(Rotated_Position(begin)));

	pass_Color = in_Color;
}
