#version 150 core
layout(lines) in;
layout(triangle_strip, max_vertices=3) out;

void main()
{	
  
    gl_Position = gl_in[0].gl_Position;
	EmitVertex();

	gl_Position = gl_in[1].gl_Position;
    EmitVertex();

	gl_Position = gl_in[1].gl_Position - vec4(gl_in[1].gl_Position.x - gl_in[0].gl_Position.x, 0.0, 0.0, 0.0);
    EmitVertex();


  EndPrimitive();
}  