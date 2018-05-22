//SUN
out vec3 sun_vector;
vec3 sun_position;
out float SunSide;
void PrepareSun()
{
	sun_position = Rotated_Around(begin,sunMatrix[1]);
	sun_position = normalize(sun_position);

	sun_vector.x = dot(sun_position,vec3(1,0,0));
	sun_vector.y = dot(sun_position,vec3(0,1,0));
	sun_vector.z = dot(sun_position,vec3(0,0,1));

	SunSide = (sun_vector.y + 1)/2;
}