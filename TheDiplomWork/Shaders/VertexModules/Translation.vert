vec3 Translate(vec3 inp, vec3 shift)
{
	mat4 Translator = mat4(vec4(1,0,0,0),
	vec4(0,1,0,0),
	vec4(0,0,1,0),
	vec4(shift.x,shift.y,shift.z,1));

	vec4 result = (Translator * vec4(inp,1.0));
	return result.xyz;
}