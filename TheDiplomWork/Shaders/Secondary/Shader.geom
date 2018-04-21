#version 150 core
layout(lines_adjacency) in;
layout(triangle_strip, max_vertices=14) out;

void main()
{	
	vec4 vectorx = gl_in[1].gl_Position - gl_in[0].gl_Position;
	vec4 vectory = gl_in[2].gl_Position - gl_in[0].gl_Position;
	vec4 vectorz = gl_in[3].gl_Position - gl_in[0].gl_Position;

	vec4 point4 = gl_in[0].gl_Position + vectorx + vectory;
	vec4 point5 = gl_in[0].gl_Position + vectorx + vectorz;
	vec4 point6 = gl_in[0].gl_Position + vectorz + vectory;
	vec4 point7 = gl_in[0].gl_Position + vectorx + vectory + vectorz;

    gl_Position = gl_in[0].gl_Position;
	EmitVertex();
	gl_Position = gl_in[2].gl_Position;
    EmitVertex();
	gl_Position = gl_in[1].gl_Position;
    EmitVertex();
	gl_Position = point4;
	EmitVertex();
	gl_Position = point7;
	EmitVertex();
	gl_Position = gl_in[2].gl_Position;
    EmitVertex();
	gl_Position = point6;
	EmitVertex();
	gl_Position = gl_in[3].gl_Position;
	EmitVertex();
	gl_Position = point7;
	EmitVertex();
	gl_Position = point5;
	EmitVertex();
	gl_Position = gl_in[1].gl_Position;
    EmitVertex();
	gl_Position = gl_in[3].gl_Position;
	EmitVertex();
	gl_Position = gl_in[0].gl_Position;
    EmitVertex();
	gl_Position = gl_in[2].gl_Position;
    EmitVertex();

  EndPrimitive();
}  