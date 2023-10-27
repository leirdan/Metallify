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
        new() { Id = 1, Title = "TRAUMA", Genre = new(){ Name = "Post-hardcore", Description = "Awesome Genre!" } },
        new() { Id = 2, Title = "Ghost Like You", Genre = new(){ Name = "Post-hardcore", Description = "Awesome Genre!" } },
        new() { Id = 3, Title = "I Loved You At Your Darkest", Genre = new(){ Name = "Brutal blackened death metal", Description = "Best of both worlds." } },
    };
}
