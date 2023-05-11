using CityBikeAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace CityBikeAPI.Controllers
{
    public class InjectedController : ControllerBase
    {
        protected readonly CityBikeContext db;

        public InjectedController(CityBikeContext context)
        {
            db = context;
        }

    }
}
