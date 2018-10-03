using KazenLibrary;
using KazenService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KazenService.Controllers
{
    public class KazenController : ApiController
    {
        /// <summary>
        /// Alle kazen opvragen
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAll()
        {
            Kazen kazen = new Kazen();
            string detail = this.Request.RequestUri.AbsoluteUri + "/";
            kazen.AddRange(from kaas in InMemoryDb.Kazen.Values
                           orderby kaas.Naam
                           select new KaasShort
                           {
                               Id = kaas.Id,
                               Naam = kaas.Naam,
                               Detail = detail + kaas.Id
                           });
            return this.Ok(kazen);
        }

        /// <summary>
        /// De kaas opvragen op basis van de id in een lijst van Kazen
        /// </summary>
        /// <param name="id">de id van een kaas</param>
        /// <returns>de kaas</returns>
        public IHttpActionResult Get(int id)
        {
           if(InMemoryDb.Kazen.ContainsKey(id))
            {
                return this.Ok(InMemoryDb.Kazen[id]);
            }

            return this.NotFound();
        }

        /// <summary>
        /// de kaas opvragen op basis van de smaak in een lijst van Kazen
        /// </summary>
        /// <param name="smaak">de smaak van een kaas</param>
        /// <returns>de kaas</returns>
        public IHttpActionResult GetBySmaak(string smaak)
        {
            Kazen kazen = new Kazen();
            string detail = this.Request.RequestUri.AbsoluteUri;
            detail = detail.Substring(0, detail.IndexOf("?"));
            detail += "/";
            kazen.AddRange(from kaas in InMemoryDb.Kazen.Values
                           where kaas.Smaak.ToLower() == smaak.ToLower()
                           orderby kaas.Naam
                           select new KaasShort
                           {
                               Id = kaas.Id,
                               Naam = kaas.Naam,
                               Detail = detail + kaas.Id
                           });
            return this.Ok(kazen);
        }

        public IHttpActionResult Delete(int id)
        {
            if (InMemoryDb.Kazen.ContainsKey(id))
            {
                InMemoryDb.Kazen.Remove(id);
                return this.Ok();
            }

            return this.NotFound();
        }

        public IHttpActionResult Post(Kaas kaas)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);
            int id = InMemoryDb.Kazen.Keys.Max() + 1;
            kaas.Id = id;
            InMemoryDb.Kazen[id] = kaas;
            return this.Created(this.Request.RequestUri.AbsoluteUri + "/" + id, kaas);
        }

        public IHttpActionResult Put(int id, Kaas kaas)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);
            if (InMemoryDb.Kazen.ContainsKey(id))
            {
                InMemoryDb.Kazen[id] = kaas;
                return this.Ok();
            }
            return this.NotFound();
        }
    }
}
