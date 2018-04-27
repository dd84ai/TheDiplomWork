vec3 Size(vec3 inp, vec3 _Size)
{
	mat3 SizeMatrix = mat3(vec3(_Size.x,0,0),
	vec3(0,_Size.y,0),
	vec3(0,0,_Size.z));

	return inp * SizeMatrix;
}
vec3 Expander(vec3 inp)
{
	mat3 SizeMatrix = mat3(vec3(4,0,0),
	vec3(0,4,0),
	vec3(0,0,4));

	return inp * SizeMatrix;
}