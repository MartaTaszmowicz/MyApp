﻿namespace MyApp.Model;

public class RoomType
{
    public string? code { get; set; }
    public string? description { get; set; }
    public List<string>? amenities { get; set; }
    public List<string>? features { get; set; }
}