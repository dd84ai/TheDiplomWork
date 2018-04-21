#version 150 core
layout(quads) in;
layout(quads, max_vertices=4) out;

void main()
{	
  for(int i=0; i<4; i++)
  {
    gl_Position = gl_in[i].gl_Position;
    EmitVertex();
  }
  EndPrimitive();
}  