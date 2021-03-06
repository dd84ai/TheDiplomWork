#version 150 core
layout(points) in;
layout(triangle_strip, max_vertices=24) out;

in vec4 vertex_x_out[];
in vec4 vertex_y_out[];
in vec4 vertex_z_out[];
in vec3 pass_Color[];
in float pointofview[];

in vec3 sun_position[];
out vec3 sun_position_to_frag;

in float sun_distance_to_cube_to_geom[];
out float sun_distance_to_cube;

in vec3 sun_vector[];
in float SunSide[];

in vec3 scalar_sides[];

in float range[];

out vec3 f_Color;

vec3 color[8];
vec4 point[8];

uniform vec3 SunPosition;

uniform mat3 sunMatrix;
uniform vec3 viewparameters;
float min_light = 0.1;
float Fixer(float a, float b)
{
float c = a;
if (b > a) return b;
return c;
}
vec3 MainLight(float vector)
{
	//if (sun_vector[0].y > 0) return min(sun_vector[0].y,vector) * vec3(0.6,0.6,0.6) + vec3(0.4,0.4,0.4);
	//else return min(-sun_vector[0].y,-vector) * vec3(0.2,0.5,0.6) + vec3(0.4,0.4,0.4);

	vec3 sun = max(SunSide[0] * vector * vec3(0.6,0.6,0.6),vec3(0,0,0));
	vec3 moon = max((1 - SunSide[0]) * (-vector) * vec3(0.2,0.5,0.6),vec3(0,0,0));
	vec3 amb = vec3(0.4,0.4,0.4);

	return sun + moon + amb;

	//if (sun_vector[0].y > 0) return sun + amb;
	//else return moon + amb;

	 //res.x = min(res.x,1.0);
	 //res.y = min(res.y,1.0);
	 //res.z = min(res.z,1.0);

	//return sun;
}
vec3 TheLight(float vector)
{
	return max(MainLight(vector),vec3(sunMatrix[2].x));
}

void Front()
{
	f_Color = color[0] * TheLight(-sun_vector[0].z);  // 1
    gl_Position = gl_in[0].gl_Position;
	EmitVertex();
	f_Color = color[2] * TheLight(-sun_vector[0].z);  // 2
	gl_Position = point[2];
    EmitVertex();
	f_Color = color[1] * TheLight(-sun_vector[0].z);  // 3
	gl_Position = point[1];
    EmitVertex();
	f_Color = color[4] * TheLight(-sun_vector[0].z);  // 4
	gl_Position = point[4];
	EmitVertex();
	EndPrimitive();
}
void Back()
{
	f_Color = color[3] * TheLight(sun_vector[0].z);  // 1
    gl_Position = point[3];
	EmitVertex();
	f_Color = color[5] * TheLight(sun_vector[0].z);  // 2
	gl_Position = point[5];
    EmitVertex();
	f_Color = color[6] * TheLight(sun_vector[0].z);  // 3
	gl_Position = point[6];
    EmitVertex();
	f_Color = color[7] * TheLight(sun_vector[0].z);  // 4
	gl_Position = point[7];
	EmitVertex();
	EndPrimitive();
}
void Left()
{
	f_Color = color[0] * TheLight(-sun_vector[0].x);  // 1
    gl_Position = gl_in[0].gl_Position;
	EmitVertex();
	f_Color = color[2] * TheLight(-sun_vector[0].x);  // 2
	gl_Position = point[2];
    EmitVertex();
	f_Color = color[3] * TheLight(-sun_vector[0].x);  // 3
	gl_Position = point[3];
    EmitVertex();
	f_Color = color[6] * TheLight(-sun_vector[0].x);  // 4
	gl_Position = point[6];
	EmitVertex();
	EndPrimitive();
}
void Right()
{
	f_Color = color[1] * TheLight(sun_vector[0].x);  // 1
    gl_Position = point[1];
	EmitVertex();
	f_Color = color[4] * TheLight(sun_vector[0].x);  // 2
	gl_Position = point[4];
    EmitVertex();
	f_Color = color[5] * TheLight(sun_vector[0].x);  // 3
	gl_Position = point[5];
    EmitVertex();
	f_Color = color[7] * TheLight(sun_vector[0].x);  // 4
	gl_Position = point[7];
	EmitVertex();
	EndPrimitive();
}
void Top()
{
	f_Color = color[2] * TheLight(sun_vector[0].y);  // 1
    gl_Position = point[2];
	EmitVertex();
	f_Color = color[4] * TheLight(sun_vector[0].y);  // 2
	gl_Position = point[4];
    EmitVertex();
	f_Color = color[6] * TheLight(sun_vector[0].y);  // 3
	gl_Position = point[6];
    EmitVertex();
	f_Color = color[7] * TheLight(sun_vector[0].y);  // 4
	gl_Position = point[7];
	EmitVertex();
	EndPrimitive();
}
void Bottom()
{
	f_Color = color[0] * TheLight(-sun_vector[0].y);  // 1
    gl_Position = gl_in[0].gl_Position;
	EmitVertex();
	f_Color = color[1] * TheLight(-sun_vector[0].y);  // 2
	gl_Position = point[1];
    EmitVertex();
	f_Color = color[3] * TheLight(-sun_vector[0].y);  // 3
	gl_Position = point[3];
    EmitVertex();
	f_Color = color[5] * TheLight(-sun_vector[0].y);  // 4
	gl_Position = point[5];
	EmitVertex();
	EndPrimitive();
}
float SunSidedCoef = 0;
void Compressed(const vec4 Position, vec3 colour)
{
	vec4 vectorx = vertex_x_out[0] - Position;
	vec4 vectory = vertex_y_out[0] - Position;
	vec4 vectorz = vertex_z_out[0] - Position;

	point[1] = Position + vectorx;
	point[2] = Position + vectory;
	point[3] = Position + vectorz;
	point[4] = point[1] + vectory;
	point[5] = point[1] + vectorz;
	point[6] = point[3] + vectory;
	point[7] = point[4] + vectorz;

	color[0] = (colour * 7 + vec3(0,0,0)) / 8;
	color[1] = (colour * 7 + vec3(1,1,1)) / 8;
	color[2] = (colour * 7 + vec3(1,1,1)) / 8;
	color[3] = (colour * 7 + vec3(1,1,1)) / 8;
	color[4] = (colour * 7 + vec3(0,0,0)) / 8;
	color[5] = (colour * 7 + vec3(0,0,0)) / 8;
	color[6] = (colour * 7 + vec3(0,0,0)) / 8;
	color[7] = (colour * 7 + vec3(1,1,1)) / 8;
}
void Wrapper(const vec4 Position, vec3 colour)
{
if (!(sunMatrix[2].z < 0.5))
	{
		if ((range[0] < viewparameters.x && pointofview[0] > viewparameters.y))
		{
		Compressed(Position,colour);
		if (scalar_sides[0].z > SunSidedCoef) Front();
		else Back();
		if (scalar_sides[0].x > SunSidedCoef) Left();
		else Right();
		if (scalar_sides[0].y > SunSidedCoef) Bottom();
		else Top();
		}
	}
	else
	{
	Compressed(Position,colour);
	Front();
	Back();
	Left();
	Right();
	Bottom();
	Top();
	}
}
void main()
{	
	sun_position_to_frag = sun_position[0];
	sun_distance_to_cube = sun_distance_to_cube_to_geom[0];

	Wrapper(gl_in[0].gl_Position,pass_Color[0]);

	//vec4 vector = normalize(gl_in[0].gl_Position - vec4(sun_position_to_frag,1.0));
	//Wrapper(gl_in[0].gl_Position - vector*4,pass_Color[0]);
}  
