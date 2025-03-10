﻿using Application.Abstractions;
using System.Text.Json.Serialization;

namespace Application.Commands.Donuts
{
    /// <summary>
    /// Comando para actualizar dona
    /// </summary>
    public class UpdateDonutCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
