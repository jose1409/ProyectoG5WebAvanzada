﻿namespace API.Models
{
    public class ApiResponse
    {
        public int Codigo { get; set; }
        public string? Mensaje { get; set; }
        public object? Contenido { get; set; }
    }
}
