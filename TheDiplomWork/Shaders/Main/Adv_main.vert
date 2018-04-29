//#include <Header.vert>
//#include <Cuter.vert>
//#include <Rotator.vert>
//#include <Sun.vert>

vec4 Processed(vec3 inp)
{
	if (sunMatrix[2].z < 0.5)
	{
		return Shifted_Position(Translate(Rotated_Around(Expander(inp),in_Center),vec3(playerMatrix[0].x,0,playerMatrix[0].z)));
	}
	else 
	{
		if (in_Center.y > 0)
		{
		float height = in_Center.y - in_Position.y;
		height = height - (9.8 / 2) * (TimeTotalSeconds - in_Center.x) * (TimeTotalSeconds - in_Center.x);
		if (height > 0)
		return Shifted_Position(Translate(inp,vec3(0,height,0)));
		else return Shifted_Position(inp);
		}
		else return Shifted_Position(inp);
	}
}
void main(void) 
{
	PrepareRotator(vec3(sunMatrix[0]));
	PrepareSun();

	Cuter_PointOfView();
	Cuter_PlayerSidedAdvanced();
	
	mat4 Transform = projectionMatrix *rotMatrix *  viewMatrix * modelMatrix;

	vertex_x_out = Transform * Processed(begin + vec3(1,0,0));
	vertex_y_out = Transform * Processed(begin + vec3(0,1,0));
	vertex_z_out = Transform * Processed(begin + vec3(0,0,1));

	gl_Position = Transform * Processed(begin);

	pass_Color = in_Color;
}
