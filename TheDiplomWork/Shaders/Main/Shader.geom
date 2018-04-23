#version 150 core
layout(points) in;
layout(triangle_strip, max_vertices=24) out;

in vec4 vertex_x_out[];
in vec4 vertex_y_out[];
in vec4 vertex_z_out[];
in vec3 pass_Color[];
in float pointofview[];

in vec3 scalar_sides[];

out vec3 f_Color;

vec3 color[8];
vec4 point[8];

void Front()
{
	f_Color = color[0];  // 1
    gl_Position = gl_in[0].gl_Position;
	EmitVertex();
	f_Color = color[2];  // 2
	gl_Position = point[2];
    EmitVertex();
	f_Color = color[1];  // 3
	gl_Position = point[1];
    EmitVertex();
	f_Color = color[4];  // 4
	gl_Position = point[4];
	EmitVertex();
	EndPrimitive();
}
void Back()
{
	f_Color = color[3];  // 1
    gl_Position = point[3];
	EmitVertex();
	f_Color = color[5];  // 2
	gl_Position = point[5];
    EmitVertex();
	f_Color = color[6];  // 3
	gl_Position = point[6];
    EmitVertex();
	f_Color = color[7];  // 4
	gl_Position = point[7];
	EmitVertex();
	EndPrimitive();
}
void Left()
{
	f_Color = color[0];  // 1
    gl_Position = gl_in[0].gl_Position;
	EmitVertex();
	f_Color = color[2];  // 2
	gl_Position = point[2];
    EmitVertex();
	f_Color = color[3];  // 3
	gl_Position = point[3];
    EmitVertex();
	f_Color = color[6];  // 4
	gl_Position = point[6];
	EmitVertex();
	EndPrimitive();
}
void Right()
{
	f_Color = color[1];  // 1
    gl_Position = point[1];
	EmitVertex();
	f_Color = color[4];  // 2
	gl_Position = point[4];
    EmitVertex();
	f_Color = color[5];  // 3
	gl_Position = point[5];
    EmitVertex();
	f_Color = color[7];  // 4
	gl_Position = point[7];
	EmitVertex();
	EndPrimitive();
}
void Top()
{
	f_Color = color[2];  // 1
    gl_Position = point[2];
	EmitVertex();
	f_Color = color[4];  // 2
	gl_Position = point[4];
    EmitVertex();
	f_Color = color[6];  // 3
	gl_Position = point[6];
    EmitVertex();
	f_Color = color[7];  // 4
	gl_Position = point[7];
	EmitVertex();
	EndPrimitive();
}
void Bottom()
{
	f_Color = color[0];  // 1
    gl_Position = gl_in[0].gl_Position;
	EmitVertex();
	f_Color = color[1];  // 2
	gl_Position = point[1];
    EmitVertex();
	f_Color = color[3];  // 3
	gl_Position = point[3];
    EmitVertex();
	f_Color = color[5];  // 4
	gl_Position = point[5];
	EmitVertex();
	EndPrimitive();
}
float SunSidedCoef = 0;
void main()
{	
	vec4 vectorx = vertex_x_out[0] - gl_in[0].gl_Position;
	vec4 vectory = vertex_y_out[0] - gl_in[0].gl_Position;
	vec4 vectorz = vertex_z_out[0] - gl_in[0].gl_Position;

	point[1] = gl_in[0].gl_Position + vectorx;
	point[2] = gl_in[0].gl_Position + vectory;
	point[3] = gl_in[0].gl_Position + vectorz;
	point[4] = point[1] + vectory;
	point[5] = point[1] + vectorz;
	point[6] = point[3] + vectory;
	point[7] = point[4] + vectorz;

	color[0] = (pass_Color[0] * 7 + vec3(0,0,0)) / 8;
	color[1] = (pass_Color[0] * 7 + vec3(1,1,1)) / 8;
	color[2] = (pass_Color[0] * 7 + vec3(1,1,1)) / 8;
	color[3] = (pass_Color[0] * 7 + vec3(1,1,1)) / 8;
	color[4] = (pass_Color[0] * 7 + vec3(0,0,0)) / 8;
	color[5] = (pass_Color[0] * 7 + vec3(0,0,0)) / 8;
	color[6] = (pass_Color[0] * 7 + vec3(0,0,0)) / 8;
	color[7] = (pass_Color[0] * 7 + vec3(1,1,1)) / 8;

	if (pointofview[0] > 0.4)
	{
	if (scalar_sides[0].z > SunSidedCoef) Front();
	else Back();
	if (scalar_sides[0].x > SunSidedCoef) Left();
	else Right();
	if (scalar_sides[0].y > SunSidedCoef) Bottom();
	else Top();
	//Front();
	//Back();
	//Left();
	//Right();
	//Bottom();
	//Top();
	}
}  