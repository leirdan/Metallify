using Metallify.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metallify.Tests.Fixtures;

internal static class AlbumsFixture
{
    public static List<Album> GetAlbumsForTests() => new()
    {
        new() { Id = 1, Title = "Refugium", Genre = new(){ Name = "Atmospheric Black Metal", Description = "Best BM's genre." } },
        new() { Id = 2, Title = "MAY LONELINESS CONSUME YOU", Genre = new(){ Name = "Black Noise", Description = "Experimental but always good." } },
        new() { Id = 3, Title = "Pale Swordsman", Genre = new(){ Name = "Romantic Black Metal", Description = "A new creative genre." } },
    };
}
