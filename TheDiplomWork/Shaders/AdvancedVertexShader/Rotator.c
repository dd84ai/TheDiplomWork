﻿
mat3 Rotator;
vec3 Angles;
void PrepareRotator()
{
	mat3 RotateX = mat3(vec3(1,0,0),
	vec3(0,cos(Angles.x),-sin(Angles.x)),
	vec3(0,sin(Angles.x),cos(Angles.x)));

	mat3 RotateY = mat3(vec3(cos(Angles.y),0,sin(Angles.y)),
	vec3(0,1,0),
	vec3(-sin(Angles.y),0,cos(Angles.y)));

	mat3 RotateZ = mat3(vec3(cos(Angles.z),-sin(Angles.z),0),
	vec3(sin(Angles.z),cos(Angles.z),0),
	vec3(0,0,1));
	Rotator = RotateX * RotateY * RotateZ;
}
vec3 Rotated_Position(vec3 input_vec)
{
	return (input_vec * Rotator);
}