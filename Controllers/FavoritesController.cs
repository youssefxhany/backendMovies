using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserLogin.Data;
using UserLogin.Models;

namespace UserLogin.Controllers
{
    [Route("MoviesMoviesApi/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public FavoritesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites()
        {
            return await _context.favorites.ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetUserFavorites(int id)
        {
            user my_user = await _context.users.FindAsync(id);
            if (my_user == null)
            {
                return NotFound("no such user in system");
            }
            var my_favorites = await _context.favorites.Where(c => c.user_id == my_user).ToListAsync();

            return my_favorites;
        }


        [HttpPost("{id}")]
        public async Task<ActionResult<Favorite>> addFavorite(int id,Favorite my_new_favorite)
        {
            var my_user = await _context.users.FindAsync(id);
            if(my_user == null)
            {
                return NotFound("no such user in system");
            }
            my_new_favorite.user_id = await _context.users.FindAsync(id);
            var my_favorites = await _context.favorites.Where(c => (c.imdb_identification == my_new_favorite.imdb_identification && c.user_id == my_new_favorite.user_id) ).ToListAsync();
            if (IsEmpty(my_favorites)) {
                _context.favorites.Add(my_new_favorite);
                await _context.SaveChangesAsync();
                return Ok(_context.favorites);
            }
            return NotFound("item already in favorites");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> removeFavorite(int id, Favorite remove_favorite)
        {
            var my_user = await _context.users.FindAsync(id);
            if(my_user == null)
            {
                return NotFound("no such user in system");
            }
            remove_favorite.user_id = my_user;
            var my_favorite = await _context.favorites.Where(c => (c.imdb_identification == remove_favorite.imdb_identification && c.user_id == remove_favorite.user_id)).ToListAsync();
            if (IsEmpty(my_favorite))
            {
                return NotFound("book not available in favorites");
            }
            //return my_favorite;
            var favorite_to_remove = await _context.favorites.FindAsync(my_favorite[0].favoriteId);
            _context.favorites.Remove(favorite_to_remove);
            await _context.SaveChangesAsync();
            return Ok(_context.favorites);
        }

        public static bool IsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }

    }
}