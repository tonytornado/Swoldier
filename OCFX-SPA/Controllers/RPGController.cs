using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OCFX_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RPGController : ControllerBase
    {
        private readonly OCFXContext Context;

        public RPGController(OCFXContext context) => Context = context ?? throw new ArgumentNullException(nameof(context), "Where's the DB bro?");

        /// <summary>
        /// Gets a list of all the campaigns
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public List<Campaign> GetCampaigns() => Context.Campaigns.ToList();

        /// <summary>
        /// Gets all the quests
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public List<Quest> GetQuests() => Context.Quests.ToList();
    }
}