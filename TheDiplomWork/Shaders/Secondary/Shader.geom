#version 150 core
layout(points) in;
layout(triangle_strip, max_vertices=14) out;

in vec4 vertex_x_out[];
in vec4 vertex_y_out[];
in vec4 vertex_z_out[];
in vec3 pass_Color[];
out vec3 f_Color;

void main()
{	
	vec4 vectorx = vertex_x_out[0] - gl_in[0].gl_Position;
	vec4 vectory = vertex_y_out[0] - gl_in[0].gl_Position;
	vec4 vectorz = vertex_z_out[0] - gl_in[0].gl_Position;

	vec4 point1 = gl_in[0].gl_Position + vectorx;
	vec4 point2 = gl_in[0].gl_Position + vectory;
	vec4 point3 = gl_in[0].gl_Position + vectorz;
	vec4 point4 = gl_in[0].gl_Position + vectorx + vectory;
	vec4 point5 = gl_in[0].gl_Position + vectorx + vectorz;
	vec4 point6 = gl_in[0].gl_Position + vectorz + vectory;
	vec4 point7 = gl_in[0].gl_Position + vectorx + vectory + vectorz;

	vec3 color0 = (pass_Color[0] * 7 + vec3(0,0,0)) / 8;
	vec3 color1 = (pass_Color[0] * 7 + vec3(1,1,1)) / 8;
	vec3 color2 = (pass_Color[0] * 7 + vec3(1,1,1)) / 8;
	vec3 color3 = (pass_Color[0] * 7 + vec3(1,1,1)) / 8;
	vec3 color4 = (pass_Color[0] * 7 + vec3(0,0,0)) / 8;
	vec3 color5 = (pass_Color[0] * 7 + vec3(0,0,0)) / 8;
	vec3 color6 = (pass_Color[0] * 7 + vec3(0,0,0)) / 8;
	vec3 color7 = (pass_Color[0] * 7 + vec3(1,1,1)) / 8;

	f_Color = color0;  // 1
    gl_Position = gl_in[0].gl_Position;
	EmitVertex();
	f_Color = color2;  // 2
	gl_Position = point2;
    EmitVertex();
	f_Color = color1;  // 3
	gl_Position = point1;
    EmitVertex();
	f_Color = color4;  // 4
	gl_Position = point4;
	EmitVertex();
	f_Color = color7;  // 5
	gl_Position = point7;
	EmitVertex();
	f_Color = color2;  // 6
	gl_Position = point2;
    EmitVertex();
	f_Color = color6;  // 7
	gl_Position = point6;
	EmitVertex(); 
	f_Color = color3;  // 8
	gl_Position = point3;
	EmitVertex();
	f_Color = color7;  // 9
	gl_Position = point7;
	EmitVertex();
	f_Color = color5;  // 10
	gl_Position = point5;
	EmitVertex();
	f_Color = color1;  // 11
	gl_Position = point1;
    EmitVertex();
	f_Color = color3;  // 12
	gl_Position = point3;
	EmitVertex();
	f_Color = color0;  // 13
	gl_Position = gl_in[0].gl_Position;
    EmitVertex();
	f_Color = color2;  // 14
	gl_Position = point2;
    EmitVertex();

  EndPrimitive();
}  