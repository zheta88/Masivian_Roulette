using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using StackExchange.Redis;
using System;
using Masivian_Roulette.Models;
using Newtonsoft.Json;
using System.Linq;

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
            rouleteMod.Status = false;
            _database.StringSet(idRoullete, JsonConvert.SerializeObject(rouleteMod));
            return idRoullete;
        }

        [HttpGet]
        [Route("get")]
        public string ListRouletteGet()
        {
            var result = new List<KeyValuePair<string, object>>();
            var endpoints = _database.Multiplexer.GetEndPoints();
            var server = _database.Multiplexer.GetServer(endpoints.First());

            string response = "";
            var keys = server.Keys(_database.Database);
            foreach (string key in keys)
            {
                Console.WriteLine(key);
                response += _database.StringGet(key) + ",";
                
            }
            response = response.Substring(0, response.Length - 2);
            return "[" + response + "]";
        }
       
    }
}
