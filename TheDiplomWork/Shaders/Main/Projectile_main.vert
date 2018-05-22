//#include <Header.vert>
//#include <Cuter.vert>
//#include <Rotator.vert>
//#include <Sun.vert>

vec3 ProcessingProjectile(vec3 inp)
{
	//Rotate
	vec3 ShiftToRotate = Shifted(inp) - projectileMatrix[2];
	ShiftToRotate = Rotate(projectileMatrix[1], ShiftToRotate);
	ShiftToRotate = Deshifted(ShiftToRotate) + projectileMatrix[2];

	vec3 ShiftedToRelativePosition = projectileMatrix[0] + ShiftToRotate;

	return ShiftedToRelativePosition;
}
vec4 Processed(vec3 inp)
{
	if (settingsTHIS_IS_EXPLOSION > 0.5)
	{
		return Shifted_Position(RotatedExplosion(Rotate(projectileMatrix[1], in_Center)) + ProcessingProjectile(AngularRotating(Rotate(projectileMatrix[1], in_Center),inp)));
	}
	else
	{
		return Shifted_Position(ProcessingProjectile(inp));
	}
	 
}
void main(void) 
{
	PrepareRotator(vec3(sunMatrix[0]));
	PrepareSun();

	
	sun_vector.x = dot(sun_position,Rotate(projectileMatrix[1],vec3(1,0,0)));
	sun_vector.y = dot(sun_position,Rotate(projectileMatrix[1],vec3(0,1,0)));
	sun_vector.z = dot(sun_position,Rotate(projectileMatrix[1],vec3(0,0,1)));

	Cuter_PointOfView();
	Cuter_PlayerSidedAdvanced();
	
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * Processed(begin + vec3(1,0,0));
	vertex_y_out = Transform * Processed(begin + vec3(0,1,0));
	vertex_z_out = Transform * Processed(begin + vec3(0,0,1));

	gl_Position = Transform * Processed(begin);

	pass_Color = in_Color;
}
