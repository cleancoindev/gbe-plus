// GB Enhanced+ Copyright Daniel Baxter 2016
// Licensed under the GPLv2
// See LICENSE.txt for full license text

// File : bad_bloom.fs
// Date : August 15, 2016
// Description : GBE+ Fragment Shader - "Bad Bloom"
//
// Low-quality bloom filter

#version 330 core
in vec2 texture_coordinates;

out vec4 color;

uniform sampler2D screen_texture;
uniform int screen_x_size;
uniform int screen_y_size;

float luma_threshold = 0.60;
float dark_threshold = 0.9;
float light_threshold = 1.4;

//Get perceived brightness, aka luma
void get_brightness(in vec4 input_color, out float luma)
{
	luma = (input_color.r * 0.2126) + (input_color.g * 0.7152) + (input_color.b * 0.0722);
}

//Blend two colors
void rgb_blend(in vec4 color_1, in vec4 color_2, out vec4 final_color)
{
	final_color.r = (color_1.r + color_2.r) / 2.0;
	final_color.g = (color_1.g + color_2.g) / 2.0;
	final_color.b = (color_1.b + color_2.b) / 2.0;
}


void main()
{
	float tex_x = 1.0 / screen_x_size;
	float tex_y = 1.0 / screen_y_size;

	vec2 current_pos = texture_coordinates;
	vec4 current_color = texture(screen_texture, texture_coordinates);

	float current_luma = 0.0;
	float input_luma = 0.0;
	float luma_distance = 0.0;

	vec2 temp_pos;
	vec4 temp_color;

	//Raise brightness of current texel
	current_color.r *= light_threshold;
	current_color.g *= light_threshold;
	current_color.b *= light_threshold;

	//Blend far texels, top, bottom, left, right
	if((current_pos.x + tex_x) <= 1.0)
	{
		temp_pos.x = current_pos.x + tex_x;
		temp_pos.y = current_pos.y;
		temp_color = texture(screen_texture, temp_pos);

		get_brightness(temp_color, input_luma);
		get_brightness(current_color, current_luma);
		luma_distance = abs(current_luma - input_luma);
		
		if(luma_distance >= 0.15)
		{
			if(input_luma >= luma_threshold)
			{
				rgb_blend(current_color, temp_color, current_color);
			}
		}
	}

	if((current_pos.x - tex_x) >= 0.0)
	{
		temp_pos.x = current_pos.x - tex_x;
		temp_pos.y = current_pos.y;
		temp_color = texture(screen_texture, temp_pos);

		get_brightness(temp_color, input_luma);
		get_brightness(current_color, current_luma);
		luma_distance = abs(current_luma - input_luma);
		
		if(luma_distance >= 0.15)
		{
			if(input_luma >= luma_threshold)
			{
				rgb_blend(current_color, temp_color, current_color);
			}
		}
	}

	if((current_pos.y + tex_y) <= 1.0)
	{
		temp_pos.x = current_pos.x;
		temp_pos.y = current_pos.y + tex_y;
		temp_color = texture(screen_texture, temp_pos);

		get_brightness(temp_color, input_luma);
		get_brightness(current_color, current_luma);
		luma_distance = abs(current_luma - input_luma);
		
		if(luma_distance >= 0.15)
		{
			if(input_luma >= luma_threshold)
			{
				rgb_blend(current_color, temp_color, current_color);
			}
		}
	}

	if((current_pos.y - tex_y) >= 0.0)
	{
		temp_pos.x = current_pos.x;
		temp_pos.y = current_pos.y - tex_y;
		temp_color = texture(screen_texture, temp_pos);

		get_brightness(temp_color, input_luma);
		get_brightness(current_color, current_luma);
		luma_distance = abs(current_luma - input_luma);
		
		if(luma_distance >= 0.15)
		{
			if(input_luma >= luma_threshold)
			{
				rgb_blend(current_color, temp_color, current_color);
			}
		}
	}


	if((current_pos.x + tex_x + tex_x) <= 1.0)
	{
		temp_pos.x = current_pos.x + tex_x + tex_x;
		temp_pos.y = current_pos.y;
		temp_color = texture(screen_texture, temp_pos);

		get_brightness(temp_color, input_luma);
		get_brightness(current_color, current_luma);
		luma_distance = abs(current_luma - input_luma);
		
		if(luma_distance >= 0.15)
		{
			if(input_luma >= luma_threshold)
			{
				rgb_blend(current_color, temp_color, current_color);
			}
		}
	}

	if((current_pos.x - tex_x - tex_x) >= 0.0)
	{
		temp_pos.x = current_pos.x - tex_x - tex_x;
		temp_pos.y = current_pos.y;
		temp_color = texture(screen_texture, temp_pos);

		get_brightness(temp_color, input_luma);
		get_brightness(current_color, current_luma);
		luma_distance = abs(current_luma - input_luma);
		
		if(luma_distance >= 0.15)
		{
			if(input_luma >= luma_threshold)
			{
				rgb_blend(current_color, temp_color, current_color);
			}
		}
	}

	if((current_pos.y + tex_y + tex_y) <= 1.0)
	{
		temp_pos.x = current_pos.x;
		temp_pos.y = current_pos.y + tex_y + tex_y;
		temp_color = texture(screen_texture, temp_pos);

		get_brightness(temp_color, input_luma);
		get_brightness(current_color, current_luma);
		luma_distance = abs(current_luma - input_luma);
		
		if(luma_distance >= 0.15)
		{
			if(input_luma >= luma_threshold)
			{
				rgb_blend(current_color, temp_color, current_color);
			}
		}
	}

	if((current_pos.y - tex_y - tex_y) >= 0.0)
	{
		temp_pos.x = current_pos.x;
		temp_pos.y = current_pos.y - tex_y - tex_y;
		temp_color = texture(screen_texture, temp_pos);

		get_brightness(temp_color, input_luma);
		get_brightness(current_color, current_luma);
		luma_distance = abs(current_luma - input_luma);
		
		if(luma_distance >= 0.15)
		{
			if(input_luma >= luma_threshold)
			{
				rgb_blend(current_color, temp_color, current_color);
			}
		}
	}



	get_brightness(texture(screen_texture, texture_coordinates), current_luma);

	if(current_luma >= luma_threshold)
	{
		current_color.r *= light_threshold;
		current_color.g *= light_threshold;
		current_color.b *= light_threshold;
	}

	else
	{
		current_color.r *= dark_threshold;
		current_color.g *= dark_threshold;
		current_color.b *= dark_threshold;
	}

	color = current_color;
}


