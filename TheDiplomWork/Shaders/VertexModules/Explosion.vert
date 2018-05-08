﻿float TimePauseForExplosion = 0.6;
vec4 Explosion(vec3 inp)
{
	float TimeItTook = TimeTotalSeconds - in_Size.x;
	if (TimeItTook > TimePauseForExplosion) TimeItTook = TimeItTook - TimePauseForExplosion;
	else TimeItTook = 0;

	vec3 RelativeShift = vec3(
	in_Center.x * TimeItTook
	,in_Center.y * TimeItTook  - (9.8 / 2) * TimeItTook * TimeItTook
	,in_Center.z * TimeItTook
	);
	return Shifted_Position(Translate(inp,vec3(RelativeShift)));
}