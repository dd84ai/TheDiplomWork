#version 150 core
layout(lines_adjacency) in;
layout(triangle_strip, max_vertices=14) out;

in vec3 pass_Color[];
out vec3 f_Color;

void main()
{	
	vec4 vectorx = gl_in[1].gl_Position - gl_in[0].gl_Position;
	vec4 vectory = gl_in[2].gl_Position - gl_in[0].gl_Position;
	vec4 vectorz = gl_in[3].gl_Position - gl_in[0].gl_Position;

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
	gl_Position = gl_in[2].gl_Position;
    EmitVertex();
	f_Color = color1;  // 3
	gl_Position = gl_in[1].gl_Position;
    EmitVertex();
	f_Color = color4;  // 4
	gl_Position = point4;
	EmitVertex();
	f_Color = color7;  // 5
	gl_Position = point7;
	EmitVertex();
	f_Color = color2;  // 6
	gl_Position = gl_in[2].gl_Position;
    EmitVertex();
	f_Color = color6;  // 7
	gl_Position = point6;
	EmitVertex(); 
	f_Color = color3;  // 8
	gl_Position = gl_in[3].gl_Position;
	EmitVertex();
	f_Color = color7;  // 9
	gl_Position = point7;
	EmitVertex();
	f_Color = color5;  // 10
	gl_Position = point5;
	EmitVertex();
	f_Color = color1;  // 11
	gl_Position = gl_in[1].gl_Position;
    EmitVertex();
	f_Color = color3;  // 12
	gl_Position = gl_in[3].gl_Position;
	EmitVertex();
	f_Color = color0;  // 13
	gl_Position = gl_in[0].gl_Position;
    EmitVertex();
	f_Color = color2;  // 14
	gl_Position = gl_in[2].gl_Position;
    EmitVertex();

  EndPrimitive();
}  