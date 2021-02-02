using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using StackExchange.Redis;
using System;
using Masivian_Roulette.Models;

namespace Masivian_Roulette.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IDatabase _database;

        public RouletteController(IDatabase database)
        {
            _database = database;

        }
        [HttpGet]
        public string Get([FromQuery] string key)
        {
            return _database.StringGet(key);

        }
        /*
        [HttpPost]
        public void Post([FromBody] KeyValuePair<string, string>keyValue)
        {
            //_database.StringSet(keyValue.Key, keyValue.Value);

        }*/
        [HttpPost]
        [Route("post")]
        public string createRoulette([FromBody] Roulette rouleteMod)
        {
            int lengthStr = 10;
            string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            int numChars = 36;
            Random rand = new Random();
            string idRoullete = "";
            for(int i = 0; i < lengthStr; i++)
            {
                int randomNumber = rand.Next(1,numChars);
                string oneChar = chars.Substring(randomNumber-1,1);
                idRoullete += oneChar;
            }
            _database.StringSet(idRoullete, rouleteMod.Name);
            return idRoullete;
        }

    }
}
