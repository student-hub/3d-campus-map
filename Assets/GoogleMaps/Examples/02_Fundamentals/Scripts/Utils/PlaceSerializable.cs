using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AddressComponent
{
  public string long_name;
  public string short_name;
  public List<string> types;
}

[Serializable]
public class Location
{
  public double lat;
  public double lng;
}

[Serializable]
public class Northeast
{
  public double lat;
  public double lng;
}

[Serializable]
public class Southwest
{
  public double lat;
  public double lng;
}

[Serializable]
public class Viewport
{
  public Northeast northeast;
  public Southwest southwest;
}

[Serializable]
public class Geometry
{
  public Location location;
  public Viewport viewport;
}

[Serializable]
public class Close
{
  public int day;
  public string time;
}

[Serializable]
public class Open
{
  public int day;
  public string time;
}

[Serializable]
public class Period
{
  public Close close;
  public Open open;
}

[Serializable]
public class OpeningHours
{
  public bool open_now;
  public List<Period> periods;
  public List<string> weekday_text;
}

[Serializable]
public class Photo
{
  public int height;
  public List<string> html_attributions;
  public string photo_reference;
  public int width;
}

[Serializable]
public class PlusCode
{
  public string compound_code;
  public string global_code;
}

[Serializable]
public class Review
{
  public string author_name;
  public string author_url;
  public string language;
  public string profile_photo_url;
  public int rating;
  public string relative_time_description;
  public string text;
  public int time;
}

[Serializable]
public class Result
{
  public List<AddressComponent> address_components;
  public string adr_address;
  public string business_status;
  public string formatted_address;
  public string formatted_phone_number;
  public Geometry geometry;
  public string icon;
  public string international_phone_number;
  public string name;
  public OpeningHours opening_hours;
  public List<Photo> photos;
  public string place_id;
  public PlusCode plus_code;
  public double rating;
  public string reference;
  public List<Review> reviews;
  public List<string> types;
  public string url;
  public int user_ratings_total;
  public int utc_offset;
  public string vicinity;
  public string website;
}

[Serializable]
public class Place
{
  public List<object> html_attributions;
  public Result result;
  public string status;
}
