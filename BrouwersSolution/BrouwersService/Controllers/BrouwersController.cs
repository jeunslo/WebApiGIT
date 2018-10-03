﻿using BrouwersService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrouwersService.Controllers
{
    [RoutePrefix("brewers")]
    public class BrouwersController : ApiController
    {
        /// <summary>
        /// Alle brouwers lezen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route]
        public IHttpActionResult Allen()
        {
            //******3e code**************
            var brouwers = new Brouwers();
            var detail = this.Request.RequestUri.AbsoluteUri + "/";
            brouwers.AddRange(from brouwer in InMemoryDataBase.Brouwers.Values
                              orderby brouwer.Naam
                              select new BrouwerBeknopt
                              {
                                  ID = brouwer.ID,
                                  Naam = brouwer.Naam,
                                  Detail = detail + brouwer.ID
                              });
            return this.Ok(brouwers);

            //******2e code**************
            //var brouwers = new Brouwers();
            //brouwers.AddRange(InMemoryDataBase.Brouwers.Values);
            //return this.Ok(brouwers);

            //******1st code**************
            //var brouwers = new Brouwers();
            //return this.Ok(InMemoryDataBase.Brouwers.Values.ToList());
        }

        /// <summary>
        /// Een brouwer lezen
        /// </summary>
        /// <param name="id">De id van de te lezen brouwer</param>
        /// <returns>De brouwer</returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Een(int id)
        {
            if(InMemoryDataBase.Brouwers.ContainsKey(id))
            {
                return this.Ok(InMemoryDataBase.Brouwers[id]);
            }
            return this.NotFound();
        }

        [HttpGet]
        [Route]
        public IHttpActionResult ByBeginNaam(string beginNaam)
        {
            //******1st code**************
            //beginNaam = beginNaam.ToLower();
            //return Ok((from brouwer in InMemoryDataBase.Brouwers.Values
            //           where brouwer.Naam.ToLower().StartsWith(beginNaam)
            //           orderby brouwer.Naam
            //           select brouwer).ToList());

            //******2e code**************
            //beginNaam = beginNaam.ToLower();
            //var brouwers = new Brouwers();
            //brouwers.AddRange(from brouwer in InMemoryDataBase.Brouwers.Values
            //                  where brouwer.Naam.ToLower().StartsWith(beginNaam)
            //                  orderby brouwer.Naam
            //                  select brouwer);
            //return Ok(brouwers);

            //******3e code**************
            beginNaam = beginNaam.ToLower();
            var brouwers = new Brouwers();
            var detail = this.Request.RequestUri.AbsoluteUri;
            detail = detail.Substring(0, detail.IndexOf("?"));
            detail += "/";
            brouwers.AddRange(from brouwer in InMemoryDataBase.Brouwers.Values
                              where brouwer.Naam.ToLower().StartsWith(beginNaam)
                              orderby brouwer.Naam
                              select new BrouwerBeknopt
                              {
                                  ID = brouwer.ID,
                                  Naam = brouwer.Naam,
                                  Detail = detail + brouwer.ID
                              });
            return Ok(brouwers);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Verwijder(int id)
        {
            if(InMemoryDataBase.Brouwers.ContainsKey(id))
            {
                InMemoryDataBase.Brouwers.Remove(id);
                return this.Ok();
            }
            return this.NotFound();
        }

        [HttpPost]
        [Route]
        public IHttpActionResult VoegToe(Brouwer brouwer)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);
            var id = InMemoryDataBase.Brouwers.Keys.Max() + 1;
            brouwer.ID = id;
            InMemoryDataBase.Brouwers[id] = brouwer;
            return this.Created(this.Request.RequestUri.AbsoluteUri + "/" + id, brouwer);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Wijzig(int id, Brouwer brouwer)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);
            if (InMemoryDataBase.Brouwers.ContainsKey(id))
            {
                InMemoryDataBase.Brouwers[id] = brouwer;
                return this.Ok();
            }
            return this.NotFound();
        }
    }
}
