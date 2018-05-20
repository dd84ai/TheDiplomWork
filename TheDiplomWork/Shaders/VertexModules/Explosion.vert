float TimePauseForExplosion = 0.6;
vec3 Explosion()
{
	float TimeItTook = TimeTotalSeconds - in_Size.x;
	if (TimeItTook > TimePauseForExplosion) TimeItTook = TimeItTook - TimePauseForExplosion;
	else TimeItTook = 0;

	vec3 RelativeShift = vec3(
	in_Center.x * TimeItTook
	,in_Center.y * TimeItTook  - (9.8 / 2) * TimeItTook * TimeItTook
	,in_Center.z * TimeItTook
	);
	return (vec3(RelativeShift));
}
vec3 RotatedExplosion(vec3 inp)
{
	//InitialVelocity
	float TimeItTook = TimeTotalSeconds - in_Size.x;
	if (TimeItTook > TimePauseForExplosion) TimeItTook = TimeItTook - TimePauseForExplosion;
	else TimeItTook = 0;

	vec3 RelativeShift = vec3(
	inp.x * TimeItTook
	,inp.y * TimeItTook  - (9.8 / 2) * TimeItTook * TimeItTook
	,inp.z * TimeItTook
	);
	return (vec3(RelativeShift));
}
vec3 AngularRotating(vec3 angles, vec3 inp)
{
		float TimeItTook = TimeTotalSeconds - in_Size.x;
		if (TimeItTook > TimePauseForExplosion) TimeItTook = TimeItTook - TimePauseForExplosion;
		else TimeItTook = 0;

		return Rotate(angles * TimeItTook * 0.1,inp);
}